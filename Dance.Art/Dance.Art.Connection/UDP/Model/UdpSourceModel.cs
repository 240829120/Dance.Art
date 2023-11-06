using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Connection
{
    /// <summary>
    /// Udp源模型
    /// </summary>
    public class UdpSourceModel : DanceModel
    {
        /// <summary>
        /// UDP客户端
        /// </summary>
        public UdpClient? Client { get; set; }

        #region LocalHost -- 本机主机

        private string? localHost;
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

        private int localPort;
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

        private string? remoteHost;
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

        private int remotePort;
        /// <summary>
        /// 远端端口
        /// </summary>
        public int RemotePort
        {
            get { return remotePort; }
            set { remotePort = value; this.OnPropertyChanged(); }
        }

        #endregion
    }
}
