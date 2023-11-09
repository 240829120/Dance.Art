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
    /// UDP文档视图模型
    /// </summary>
    public class UdpDocumentViewModel : DeviceDocumentViewModelBase, IDeviceDocumentViewModel
    {
        // ================================================================================================
        // Property

        #region LocalHost -- 监听主机

        private string? localHost;
        /// <summary>
        /// 监听主机
        /// </summary>
        public string? LocalHost
        {
            get { return localHost; }
            set { localHost = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region LocalPort -- 监听端口

        private int localPort;
        /// <summary>
        /// 监听端口
        /// </summary>
        public int LocalPort
        {
            get { return localPort; }
            set { localPort = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region RemoteHost -- 远程主机

        private string? remoteHost;
        /// <summary>
        /// 远程主机
        /// </summary>
        public string? RemoteHost
        {
            get { return remoteHost; }
            set { remoteHost = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region RemotePort -- 远程端口

        private int remotePort;
        /// <summary>
        /// 远程端口
        /// </summary>
        public int RemotePort
        {
            get { return remotePort; }
            set { remotePort = value; this.OnPropertyChanged(); }
        }

        #endregion

        // ================================================================================================
        // Override

        /// <summary>
        /// 加载
        /// </summary>
        public override void Load()
        {
            base.Load();

            if (this.Model == null || this.Model.Source is not UdpSourceModel sourceModel)
                return;

            this.Name = this.Model?.Name;
            this.Description = this.Model?.Description;
            this.LocalHost = sourceModel.LocalHost;
            this.LocalPort = sourceModel.LocalPort;
            this.RemoteHost = sourceModel.RemoteHost;
            this.RemotePort = sourceModel.RemotePort;
        }

        /// <summary>
        /// 确定
        /// </summary>
        protected override void Enter()
        {
            try
            {
                if (this.Model == null || this.Model.Source is not UdpSourceModel sourceModel)
                    return;

                if (!this.CheckName())
                    return;

                if (string.IsNullOrWhiteSpace(this.LocalHost))
                {
                    DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "监听地址为空", DanceMessageBoxAction.YES);
                    return;
                }

                if (this.LocalPort < 0)
                {
                    DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "监听端口不正确", DanceMessageBoxAction.YES);
                    return;
                }

                if (string.IsNullOrWhiteSpace(this.RemoteHost))
                {
                    DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "远程主机为空", DanceMessageBoxAction.YES);
                    return;
                }

                if (this.RemotePort < 0)
                {
                    DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "远程端口不正确", DanceMessageBoxAction.YES);
                    return;
                }

                this.ChangeDocumentTitle();
                this.Model.Name = this.Name;
                this.Model.Description = this.Description;
                sourceModel.LocalHost = this.LocalHost;
                sourceModel.LocalPort = this.LocalPort;
                sourceModel.RemoteHost = this.RemoteHost;
                sourceModel.RemotePort = this.RemotePort;

                this.SaveDeviceGroups();
                sourceModel.SaveToStorage();

                sourceModel.Disconnect();
                sourceModel.Connect();

                DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "应用成功", DanceMessageBoxAction.YES);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                DanceMessageExpansion.ShowMessageBox("错误", DanceMessageBoxIcon.Failure, ex.Message, DanceMessageBoxAction.YES);
            }
        }
    }
}
