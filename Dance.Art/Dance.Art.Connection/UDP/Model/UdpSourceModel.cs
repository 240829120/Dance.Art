using Dance.Art.Domain;
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
    public class UdpSourceModel : DanceModel, IConnectionSourceModel
    {
        /// <summary>
        /// UDP客户端
        /// </summary>
        public UdpClient? Client { get; set; }

        /// <summary>
        /// 接收消息线程
        /// </summary>
        public DanceThread? ReceiveThread { get; set; }

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

        /// <summary>
        /// 接收数据时触发
        /// </summary>
        public event EventHandler<ConnectionSourceReceiveDataEventArgs>? ReceiveData;

        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="data">数据</param>
        public void Send(byte[] data)
        {
            if (this.Client == null)
                return;

            this.Client.Send(data);
        }

        /// <summary>
        /// 开始接收数据线程
        /// </summary>
        public void BeginReceiveThread()
        {
            if (this.ReceiveThread != null)
                return;

            this.ReceiveThread = new DanceThread(this.ExecuteReceive);
            this.ReceiveThread.Start();
        }

        /// <summary>
        /// 执行接收数据
        /// </summary>
        /// <param name="context">线程上下文</param>
        private void ExecuteReceive(DanceThreadContext context)
        {
            while (!context.IsCancel && this.Client != null)
            {
                System.Net.IPEndPoint? endPoint = null;
                byte[] data = this.Client.Receive(ref endPoint);

                try
                {
                    this.ReceiveData?.Invoke(this, new ConnectionSourceReceiveDataEventArgs(data));
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }
        }
    }
}
