using Dance.Art.Document;
using Dance.Art.Domain;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using HelixToolkit.Wpf.SharpDX;
using HelixToolkit.SharpDX.Core.Model.Scene;
using System.Windows.Input;
using System.ComponentModel;

namespace Dance.Art.Scene
{
    /// <summary>
    /// 场景文档视图模型
    /// </summary>
    public class SceneDocumentViewModel : ControlDocumentViewModelBase
    {
        public SceneDocumentViewModel()
        {
            this.Items = new() { OwnerDocument = this };

            // 命令
            this.ResourceDropCommand = new(this.ResourceDrop);
            this.MouseDown3DCommand = new(this.MouseDown3D);
            this.DeleteCommand = new(this.Delete);

            // 消息
            DanceDomain.Current.Messenger.Register<DockingDesignModeChangedMessage>(this, this.OnDockingDesignModeChanged);
        }

        // ===============================================================================================
        // Field

        /// <summary>
        /// 文件管理器
        /// </summary>
        protected readonly IFileManager FileManager = DanceDomain.Current.LifeScope.Resolve<IFileManager>();

        // ===============================================================================================
        // Property

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

        #region SceneModel -- 场景模型

        private SceneModel? sceneModel;
        /// <summary>
        /// 场景模型
        /// </summary>
        public SceneModel? SceneModel
        {
            get { return sceneModel; }
            set { sceneModel = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Items -- 子项集合

        /// <summary>
        /// 子项集合
        /// </summary>
        public DocumentWrapperCollection<ISceneItemModel> Items { get; }

        #endregion

        #region SelectedItem -- 当前选中项

        private ISceneItemModel? selectedItem;
        /// <summary>
        /// 当前选中项
        /// </summary>
        public ISceneItemModel? SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                this.OnPropertyChanged();

                DanceDomain.Current.Messenger.Send(new PropertySelectedChangedMessage(this, null, value));
            }
        }

        #endregion

        // ===============================================================================================
        // Command 

        #region MouseDown3DCommand -- 3D鼠标点击命令

        /// <summary>
        /// 3D鼠标点击命令
        /// </summary>
        public RelayCommand<RoutedEventArgs> MouseDown3DCommand { get; private set; }

        /// <summary>
        /// 3D鼠标点击
        /// </summary>
        /// <param name="e">事件参数</param>
        private void MouseDown3D(RoutedEventArgs? e)
        {
            if (this.View is not SceneDocumentView view)
                return;

            if (e is not MouseDown3DEventArgs args || args.OriginalInputEventArgs is not MouseButtonEventArgs mouseEventArgs || mouseEventArgs.LeftButton != MouseButtonState.Pressed)
                return;

            if (args.HitTestResult == null)
            {
                this.SelectedItem = null;
                return;
            }

            if (args.HitTestResult.ModelHit is Element3D element)
            {
                if (view.manipulator.HitTest(element))
                    return;

                if (element.DataContext is not ISceneItemModel model)
                {
                    this.SelectedItem = null;
                    return;
                }

                this.SelectedItem = model;
                return;
            }
            else if (args.HitTestResult.ModelHit is MeshNode node && node.GetOnwer() is DanceGroupNode3D groupNode && groupNode.Element.DataContext is ISceneItemModel model)
            {
                this.SelectedItem = model;
                return;
            }

            this.SelectedItem = null;
        }

        private Element3D? TryFindTag(SceneNode node)
        {
            if (node.Tag is Element3D element)
                return element;

            return this.TryFindTag(node.Parent);
        }

        #endregion

        #region ResourceDropCommand -- 元素拖拽放置命令

        /// <summary>
        /// 资源拖拽结束命令
        /// </summary>
        public RelayCommand<DanceDragEventArgs> ResourceDropCommand { get; private set; }

        /// <summary>
        /// 资源拖拽结束
        /// </summary>
        /// <param name="e">事件参数</param>
        private void ResourceDrop(DanceDragEventArgs? e)
        {
            if (e == null || ArtDomain.Current.ProjectDomain == null)
                return;

            if (e.EventArgs.Data.GetData(typeof(ResourceInfoItemModel)) is not ResourceInfoItemModel resource || resource.Source == null)
                return;

            if (resource.Source.CreateInstance(ArtDomain.Current.ProjectDomain) is not ISceneItemModel model)
                return;

            model.OwnerDocument = this;
            model.Icon = resource.Icon;
            model.Name = resource.Name;
            model.Transform = new();

            this.Items.Add(model);

            DanceDomain.Current.Messenger.Send(new PropertySelectedChangedMessage(this, null, model));
        }

        #endregion

        #region DeleteCommand -- 删除命令

        /// <summary>
        /// 删除命令
        /// </summary>
        public RelayCommand DeleteCommand { get; private set; }

        /// <summary>
        /// 删除
        /// </summary>
        private void Delete()
        {
            if (this.SelectedItem == null)
                return;

            this.Items.Remove(this.SelectedItem);
            this.SelectedItem = null;
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

            SceneStorage? storage = DanceFileHelper.ReadJson<SceneStorage>(document.File, new DanceJsonObjectConverter());

            this.SceneModel = storage?.SceneModel ?? new();
            this.SceneModel.OwnerDocument = this;

            this.Items.ForEach(p => p.Dispose());
            this.Items.Clear();
            this.Items.AddRange(storage?.Items);
            this.Items.ForEach(p => p.OwnerDocument = this);

            this.IsModify = false;
            this.UdateDocumentStatus();
        }

        /// <summary>
        /// 保存
        /// </summary>
        public override void Save()
        {
            if (this.ViewPluginModel is not DocumentPluginModel document)
                return;

            SceneStorage storage = new()
            {
                SceneModel = this.SceneModel,
                Items = this.Items.ToList()
            };

            this.FileManager.SaveFile(document.File, () =>
            {
                DanceFileHelper.WriteJson(storage, document.File);
                this.IsModify = false;
                this.UdateDocumentStatus();
            });
        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            if (this.View is not SceneDocumentView view)
                return;

            view.viewport.Dispose();
            view.viewport.RenderHost?.Dispose();
        }
    }
}
