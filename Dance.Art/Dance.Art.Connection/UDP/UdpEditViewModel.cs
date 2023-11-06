using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Connection
{
    /// <summary>
    /// UDP编辑视图模型
    /// </summary>
    public class UdpEditViewModel : DanceViewModel, IConnectionEditViewModel
    {
        #region LocalHost -- 本机主机

        private string? localHost = "127.0.0.1";
        /// <summary>
        /// 本机主机
        /// </summary>
        public string? LocalHost
        {
            get { return localHost; }
            set { localHost = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region LocalPort -- 本机端口

        private int localPort = 7777;
        /// <summary>
        /// 本机端口
        /// </summary>
        public int LocalPort
        {
            get { return localPort; }
            set { localPort = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region RemoteHost -- 远端主机

        private string? remoteHost = "127.0.0.1";
        /// <summary>
        /// 远端主机
        /// </summary>
        public string? RemoteHost
        {
            get { return remoteHost; }
            set { remoteHost = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region RemotePort -- 远端端口

        private int remotePort = 7778;
        /// <summary>
        /// 远端端口
        /// </summary>
        public int RemotePort
        {
            get { return remotePort; }
            set { remotePort = value; this.OnPropertyChanged(); }
        }

        #endregion

        /// <summary>
        /// 从模型中加载数据
        /// </summary>
        /// <param name="model">连接模型</param>
        public void LoadFromModel(ConnectionModel model)
        {
            if (model.Source is not UdpSourceModel sourceModel)
                return;

            this.LocalHost = sourceModel.LocalHost;
            this.LocalPort = sourceModel.LocalPort;
            this.RemoteHost = sourceModel.RemoteHost;
            this.RemotePort = sourceModel.RemotePort;
        }

        /// <summary>
        /// 保存至模型
        /// </summary>
        /// <param name="model">连接模型</param>
        /// <param name="error">错误信息</param>
        /// <returns>是否成功保存</returns>
        public bool SaveToModel(ConnectionModel model, out string error)
        {
            error = string.Empty;

            if (model.Source is not UdpSourceModel sourceModel)
            {
                error = $"ConnectionSourceModel 类型不正确, 应该为: UdpSourceModel, 实际为: {model.Source?.GetType().FullName}";
                return false;
            }

            if (string.IsNullOrWhiteSpace(this.LocalHost))
            {
                error = "本机主机不能为空";
                return false;
            }

            if (this.LocalPort <= 0)
            {
                error = "请输入监听端口";
                return false;
            }

            if (string.IsNullOrWhiteSpace(this.RemoteHost))
            {
                error = "远端主机不能为空";
                return false;
            }

            if (this.RemotePort <= 0)
            {
                error = "远端端口不正确";
                return false;
            }

            sourceModel.LocalHost = this.LocalHost;
            sourceModel.LocalPort = this.LocalPort;
            sourceModel.RemoteHost = this.RemoteHost;
            sourceModel.RemotePort = this.RemotePort;

            return true;
        }
    }
}
