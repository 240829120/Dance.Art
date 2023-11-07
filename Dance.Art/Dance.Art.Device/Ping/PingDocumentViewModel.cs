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
    /// Ping文档视图模型
    /// </summary>
    public class PingDocumentViewModel : DeviceDocumentViewModelBase, IDeviceDocumentViewModel
    {
        // ================================================================================================
        // Property

        #region Host -- 主机

        private string? host;
        /// <summary>
        /// 主机
        /// </summary>
        public string? Host
        {
            get { return host; }
            set { host = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Frequency -- 频率（单位：毫秒）

        private int frequency = 2000;
        /// <summary>
        /// 频率（单位：毫秒）
        /// </summary>
        public int Frequency
        {
            get { return frequency; }
            set { frequency = value; this.OnPropertyChanged(); }
        }

        #endregion

        // ================================================================================================
        // Override

        /// <summary>
        /// 重新加载
        /// </summary>
        protected override void Reload()
        {
            if (this.Model.Source is not PingSourceModel sourceModel)
                return;

            this.Host = sourceModel.Host;
            this.Frequency = sourceModel.Frequency;
        }

        /// <summary>
        /// 确定
        /// </summary>
        protected override void Enter()
        {
            if (this.Model.Source is not PingSourceModel sourceModel)
                return;

            if (string.IsNullOrWhiteSpace(this.Host))
            {
                DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "请输入地址", DanceMessageBoxAction.YES);
                return;
            }

            if (this.Frequency < 1000)
            {
                DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "频率最小1000毫秒", DanceMessageBoxAction.YES);
                return;
            }

            sourceModel.Host = this.Host;
            sourceModel.Frequency = this.Frequency;

            sourceModel.SaveToStorage();
            sourceModel.Disconnect();
            sourceModel.Connect();
        }
    }
}
