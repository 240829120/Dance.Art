using CommunityToolkit.Mvvm.Input;
using Dance.Art.Document.Document;
using Dance.Art.Domain;
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
            this.ReloadCommand = new(this.Reload, this.CanReload);
        }

        /// <summary>
        /// 设备模型
        /// </summary>
        public DeviceModel? Model { get; private set; }

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
        protected abstract void Enter();

        #endregion

        #region ReloadCommand -- 重新加载命令

        /// <summary>
        /// 重新加载命令
        /// </summary>
        public RelayCommand ReloadCommand { get; private set; }

        /// <summary>
        /// 是否可以重新加载
        /// </summary>
        /// <returns>是否可以重新加载</returns>
        protected virtual bool CanReload()
        {
            return true;
        }

        /// <summary>
        /// 重新加载
        /// </summary>
        protected abstract void Reload();

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
    }
}
