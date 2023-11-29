using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Dance.Art.Document;
using Dance.Art.Document.Document;
using Dance.Art.Domain;
using Dance.Wpf;
using ICSharpCode.AvalonEdit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.ControlGrid
{
    /// <summary>
    /// 按钮组文档视图模型
    /// </summary>
    public class ControlGridDocumentViewModel : ControlDocumentViewModelBase
    {
        public ControlGridDocumentViewModel()
        {
            this.Items = new() { OwnerDocument = this };

            // 命令
            this.ResourceDropCommand = new(this.ResourceDrop);
            this.DeleteCommand = new(this.Delete, this.CanDelete);

            // 消息
            DanceDomain.Current.Messenger.Register<DockingDesignModeChangedMessage>(this, this.OnDockingDesignModeChanged);
        }

        // ====================================================================================
        // Field

        /// <summary>
        /// 文件管理器
        /// </summary>
        protected readonly IFileManager FileManager = DanceDomain.Current.LifeScope.Resolve<IFileManager>();

        // ====================================================================================
        // Property

        // -------------------------------------------------------------------------

        #region ControlGridModel -- 控制面板模型

        private ControlGridModel? controlGridModel;
        /// <summary>
        /// 控制面板模型
        /// </summary>
        public ControlGridModel? ControlGridModel
        {
            get { return controlGridModel; }
            set { controlGridModel = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Items -- 项集合

        /// <summary>
        /// 项集合
        /// </summary>
        public DocumentWrapperCollection<IControlGridItemModel> Items { get; }

        #endregion

        #region SelectedValue -- 当前选中项

        private IControlGridItemModel? selectedValue;
        /// <summary>
        /// 当前选中项
        /// </summary>
        public IControlGridItemModel? SelectedValue
        {
            get { return selectedValue; }
            set
            {
                selectedValue = value;
                this.OnPropertyChanged();

                if (!this.IsDesignMode)
                    return;

                DanceDomain.Current.Messenger.Send(new PropertySelectedChangedMessage(this, null, value));
            }
        }

        #endregion

        #region IsSelectedCanvas -- 是否选中画布

        private bool isSelectedCanvas;
        /// <summary>
        /// 是否选中画布
        /// </summary>
        public bool IsSelectedCanvas
        {
            get { return isSelectedCanvas; }
            set
            {
                isSelectedCanvas = value;
                this.OnPropertyChanged();

                DanceDomain.Current.Messenger.Send(new PropertySelectedChangedMessage(this, null, value ? this.ControlGridModel : null));
            }
        }

        #endregion

        #region IsDesignMode -- 是否是设计模式

        private bool isDesignMode = ArtDomain.Current.IsDesignMode;
        /// <summary>
        /// 是否是设计模式
        /// </summary>
        public bool IsDesignMode
        {
            get { return isDesignMode; }
            set { isDesignMode = value; this.OnPropertyChanged(); }
        }

        #endregion

        // ====================================================================================
        // Command

        #region ResourceDropCommand -- 资源拖拽结束命令

        /// <summary>
        /// 资源拖拽结束命令
        /// </summary>
        public RelayCommand<ControlGridDropEventArgs> ResourceDropCommand { get; private set; }

        /// <summary>
        /// 资源拖拽结束
        /// </summary>
        /// <param name="e">事件参数</param>
        private void ResourceDrop(ControlGridDropEventArgs? e)
        {
            if (e == null || ArtDomain.Current.ProjectDomain == null || e.Model == null)
                return;

            if (this.Items.Any(p => p.Column == e.Column && p.Row == e.Row))
                return;

            e.Model.Row = e.Row;
            e.Model.Column = e.Column;

            if (!this.Items.Contains(e.Model))
            {
                e.Model.OwnerDocument = this;
                this.Items.Add(e.Model);
            }

            this.IsModify = true;

            DanceDomain.Current.Messenger.Send(new PropertySelectedChangedMessage(this, null, e.Model));
        }

        #endregion

        #region DeleteCommand -- 删除命令

        /// <summary>
        /// 删除命令
        /// </summary>
        public RelayCommand DeleteCommand { get; private set; }

        /// <summary>
        /// 是否可以删除
        /// </summary>
        /// <returns></returns>
        private bool CanDelete()
        {
            return this.IsDesignMode && this.SelectedValue != null;
        }

        /// <summary>
        /// 删除
        /// </summary>
        private void Delete()
        {
            if (DanceXamlExpansion.IsInDesignMode)
                return;

            if (this.ViewPluginModel == null || !this.ViewPluginModel.IsActive)
                return;

            if (this.Items == null || this.Items.Count == 0 || this.SelectedValue == null)
                return;

            this.Items.Remove(this.SelectedValue);
            this.SelectedValue = null;
            this.IsModify = true;
        }

        #endregion

        #region CopyCommand -- 拷贝命令

        /// <summary>
        /// 是否可以拷贝
        /// </summary>
        /// <returns></returns>
        protected override bool CanCopy()
        {
            return this.IsDesignMode && this.SelectedValue != null;
        }

        /// <summary>
        /// 拷贝
        /// </summary>
        protected override void Copy()
        {
            if (DanceXamlExpansion.IsInDesignMode)
                return;

            if (this.ViewPluginModel == null || !this.ViewPluginModel.IsActive)
                return;

            if (!this.IsDesignMode || this.SelectedValue == null)
                return;

            Clipboard.SetData(typeof(IControlGridItemModel).FullName, JsonConvert.SerializeObject(this.SelectedValue));
        }

        #endregion

        #region PasteCommand -- 粘贴命令

        /// <summary>
        /// 粘贴
        /// </summary>
        protected override void Paste()
        {
            if (DanceXamlExpansion.IsInDesignMode)
                return;

            if (this.ViewPluginModel == null || !this.ViewPluginModel.IsActive)
                return;

            if (!this.IsDesignMode || this.ControlGridModel == null)
                return;

            string? json = Clipboard.GetData(typeof(IControlGridItemModel).FullName)?.ToString();
            if (string.IsNullOrWhiteSpace(json))
                return;

            IControlGridItemModel? model = JsonConvert.DeserializeObject<IControlGridItemModel>(json, new DanceJsonObjectConverter());
            if (model == null)
                return;

            model.ID = null;
            model.OwnerDocument = this;

            for (int r = 0; r < this.ControlGridModel.Rows; ++r)
            {
                for (int c = 0; c < this.ControlGridModel.Columns; ++c)
                {
                    if (!this.Items.Any(p => p.Column == c && p.Row == r))
                    {
                        model.Column = c;
                        model.Row = r;

                        this.Items.Add(model);

                        model.IsSelected = true;

                        return;
                    }
                }
            }
        }

        #endregion

        // ====================================================================================
        // Message

        #region DockingDesignModeChangedMessage -- 设计模式改变消息

        /// <summary>
        /// 设计模式改变消息
        /// </summary>
        private void OnDockingDesignModeChanged(object sender, DockingDesignModeChangedMessage msg)
        {
            this.IsDesignMode = msg.IsDesignMode;
            this.SelectedValue = null;
            this.IsSelectedCanvas = false;
        }

        #endregion

        // ====================================================================================
        // Override

        /// <summary>
        /// 加载
        /// </summary>
        public override void Load()
        {
            if (DanceXamlExpansion.IsInDesignMode)
                return;

            if (this.ViewPluginModel is not DocumentPluginModel document)
                return;

            ControlGridStorage? storage = DanceFileHelper.ReadJson<ControlGridStorage>(document.File, new DanceJsonObjectConverter());

            this.ControlGridModel = storage?.ControlGridModel ?? new();
            this.ControlGridModel.OwnerDocument = this;

            this.Items.Clear();
            this.Items.AddRange(storage?.Items);
            this.Items.ForEach(p => p.OwnerDocument = this);

            this.SelectedValue = null;

            this.IsModify = false;
            this.UdateDocumentStatus();
        }

        /// <summary>
        /// 保存
        /// </summary>
        public override void Save()
        {
            if (DanceXamlExpansion.IsInDesignMode)
                return;

            if (this.ViewPluginModel is not DocumentPluginModel document)
                return;

            ControlGridStorage storage = new()
            {
                ControlGridModel = this.ControlGridModel,
                Items = this.Items.ToList()
            };

            this.FileManager.SaveFile(document.File, () =>
            {
                DanceFileHelper.WriteJson(storage, document.File);
                this.IsModify = false;
                this.UdateDocumentStatus();
            });
        }

        // ====================================================================================
        // Public Function

        /// <summary>
        /// 根据ID获取项
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>项</returns>
        public IControlGridItemModel? GetItemByID(string id)
        {
            return this.Items?.FirstOrDefault(p => string.Equals(p.ID, id));
        }
    }
}
