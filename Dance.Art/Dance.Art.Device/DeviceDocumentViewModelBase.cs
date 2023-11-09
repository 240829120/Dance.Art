using CommunityToolkit.Mvvm.Input;
using Dance.Art.Document.Document;
using Dance.Art.Domain;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Device
{
    /// <summary>
    /// 设备文档视图模型基类
    /// </summary>
    public abstract class DeviceDocumentViewModelBase : DocumentViewModelBase, IDeviceDocumentViewModel
    {
        /// <summary>
        /// 设备文档视图模型基类
        /// </summary>
        public DeviceDocumentViewModelBase()
        {
            this.EnterCommand = new(this.Enter, this.CanEnter);
        }

        /// <summary>
        /// 设备仓储
        /// </summary>
        private readonly IDeviceStorage DeviceStorage = DanceDomain.Current.LifeScope.Resolve<IDeviceStorage>();

        /// <summary>
        /// 设备模型
        /// </summary>
        public DeviceModel? Model { get; private set; }

        #region Name -- 名称

        private string? name;
        /// <summary>
        /// 名称
        /// </summary>
        public string? Name
        {
            get { return name; }
            set { name = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Description -- 描述

        private string? description;
        /// <summary>
        /// 描述
        /// </summary>
        public string? Description
        {
            get { return description; }
            set { description = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region EnterComamnd -- 确定命令

        /// <summary>
        /// 确定命令
        /// </summary>
        public RelayCommand EnterCommand { get; private set; }

        /// <summary>
        /// 是否可以确定
        /// </summary>
        /// <returns>是否可以确定</returns>
        protected virtual bool CanEnter()
        {
            return true;
        }

        /// <summary>
        /// 确定
        /// </summary>
        protected virtual void Enter()
        {
            if (this.Model == null)
                return;

            this.Model.Name = this.Name;
            this.Model.Description = this.Description;
        }

        #endregion

        /// <summary>
        /// 加载
        /// </summary>
        public override void Load()
        {
            this.Model = this.ViewPluginModel?.Data as DeviceModel;
        }

        /// <summary>
        /// 是否可以拷贝
        /// </summary>
        protected override bool CanCopy()
        {
            return false;
        }

        /// <summary>
        /// 是否可以剪切
        /// </summary>
        /// <returns></returns>
        protected override bool CanCut()
        {
            return false;
        }

        /// <summary>
        /// 复制
        /// </summary>
        protected override void Copy()
        {
            // nothing todo.
        }

        /// <summary>
        /// 粘贴
        /// </summary>
        protected override void Cut()
        {
            // nothing todo.
        }

        /// <summary>
        /// 粘贴
        /// </summary>
        protected override void Paste()
        {
            // nothing todo.
        }

        /// <summary>
        /// 校验设备名称
        /// </summary>
        /// <returns>是否通过校验</returns>
        protected bool CheckName()
        {
            if (ArtDomain.Current.ProjectDomain == null)
                return false;

            if (string.IsNullOrWhiteSpace(this.Name))
            {
                DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "名称不能为空", DanceMessageBoxAction.YES);
                return false;
            }

            if (ArtDomain.Current.ProjectDomain.DeviceGroups.Any(g => g.Items.Any(i => i != this.Model && string.Equals(i.Name, this.Name))))
            {
                DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "名称重复", DanceMessageBoxAction.YES);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 修改文档标题
        /// </summary>
        protected void ChangeDocumentTitle()
        {
            if (string.IsNullOrWhiteSpace(this.Name) || this.ViewPluginModel is not DocumentPluginModel documentPluginModel)
                return;

            documentPluginModel.File = $"[设备]{this.Name}";
            documentPluginModel.Name = $"[设备]{this.Name}";
        }

        /// <summary>
        /// 保存设备分组
        /// </summary>
        protected void SaveDeviceGroups()
        {
            if (ArtDomain.Current.ProjectDomain == null)
                return;

            this.DeviceStorage.SaveDeviceGroups(ArtDomain.Current.ProjectDomain);
        }
    }
}
