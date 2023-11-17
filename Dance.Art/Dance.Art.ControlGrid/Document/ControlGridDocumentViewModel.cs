﻿using CommunityToolkit.Mvvm.Input;
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

namespace Dance.Art.ControlGrid
{
    /// <summary>
    /// 按钮组文档视图模型
    /// </summary>
    public class ControlGridDocumentViewModel : ControlDocumentViewModelBase
    {
        public ControlGridDocumentViewModel()
        {
            // 命令
            this.ResourceDropCommand = new(this.ResourceDrop);
            this.DeleteCommand = new(this.Delete);

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
            set
            {
                controlGridModel = value;
                this.OnPropertyChanged();

                if (this.View is not ControlGridDocumentView view)
                    return;

                view.controlGrid.Refresh();
            }
        }

        #endregion

        #region Items -- 项集合

        /// <summary>
        /// 项集合
        /// </summary>
        public DanceWrapperCollection<ControlGridItemModelBase> Items { get; } = new();

        #endregion

        #region SelectedValue -- 当前选中项

        private ControlGridItemModelBase? selectedValue;
        /// <summary>
        /// 当前选中项
        /// </summary>
        public ControlGridItemModelBase? SelectedValue
        {
            get { return selectedValue; }
            set
            {
                selectedValue = value;
                this.OnPropertyChanged();

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
    }
}