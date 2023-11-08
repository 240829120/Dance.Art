using Dance.Art.Domain;
using Dance.Wpf;
using Microsoft.ClearScript.JavaScript;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.Device
{
    /// <summary>
    /// UDP源模型
    /// </summary>
    public class UdpSourceModel : DanceWrapperModel, IDeviceSource
    {
        // =====================================================================================
        // Field

        /// <summary>
        /// Udp客户端
        /// </summary>
        private UdpClient? UdpClient;

        /// <summary>
        /// 接收数据线程
        /// </summary>
        private DanceThread? ReceiveThread;

        // =====================================================================================
        // Event

        /// <summary>
        /// 接收数据时触发
        /// </summary>
        public event EventHandler<DeviceReceiveBufferDataEventArgs>? ReceiveData;

        // =====================================================================================
        // Property

        /// <summary>
        /// 关联模型
        /// </summary>
        [NotNull]
        public DeviceModel? Model { get; set; }

        #region LocalHost -- 监听主机

        private string? localHost;
        /// <summary>
        /// 监听主机
        /// </summary>
        public string? LocalHost
        {
            get { return localHost; }
            set { localHost = value; this.OnWrapperPropertyChanged(); }
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
            set { localPort = value; this.OnWrapperPropertyChanged(); }
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
            set { remoteHost = value; this.OnWrapperPropertyChanged(); }
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
            set { remotePort = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        // =====================================================================================
        // Public Function

        /// <summary>
        /// 连接
        /// </summary>
        public void Connect()
        {
            if (string.IsNullOrWhiteSpace(this.LocalHost))
                throw new Exception("监听地址为空");

            if (this.LocalPort < 0)
                throw new Exception("监听端口不正确");

            if (string.IsNullOrWhiteSpace(this.RemoteHost))
                throw new Exception("远程主机为空");

            if (this.RemotePort < 0)
                throw new Exception("远程端口不正确");

            if (this.UdpClient != null || this.ReceiveThread != null)
                return;

            IPEndPoint localEndPoint = new(IPAddress.Parse(this.LocalHost), this.LocalPort);
            IPEndPoint remoteEndPoint = new(IPAddress.Parse(this.RemoteHost), this.RemotePort);

            this.UdpClient = new(localEndPoint);
            this.UdpClient.Connect(remoteEndPoint);

            this.ReceiveThread = new(this.ExecuteReceiveThread);
            this.ReceiveThread.Start();

            this.Model.Status = DeviceStatus.Connected;
        }

        /// <summary>
        /// 断开
        /// </summary>
        public void Disconnect()
        {
            this.ReceiveThread?.Stop();
            this.ReceiveThread = null;
            this.UdpClient?.Dispose();
            this.UdpClient = null;

            this.Model.Status = DeviceStatus.Disconnected;
        }

        /// <summary>
        /// 保存至仓储
        /// </summary>
        public void SaveToStorage()
        {
            if (ArtDomain.Current.ProjectDomain == null)
                return;

            var collection = ArtDomain.Current.ProjectDomain.CacheContext.Database.GetCollection<UdpSourceEntity>();
            UdpSourceEntity? entity = collection.FindById(this.Model.SourceID) ?? new();
            entity.LocalHost = this.LocalHost;
            entity.LocalPort = this.LocalPort;
            entity.RemoteHost = this.RemoteHost;
            entity.RemotePort = this.RemotePort;

            collection.Upsert(entity);
            this.Model.SourceID = entity.ID;
        }

        /// <summary>
        /// 从仓储中获取
        /// </summary>
        public void LoadFromStorage()
        {
            if (ArtDomain.Current.ProjectDomain == null)
                return;

            var collection = ArtDomain.Current.ProjectDomain.CacheContext.Database.GetCollection<UdpSourceEntity>();
            UdpSourceEntity? entity = collection.FindById(this.Model.SourceID);
            if (entity == null)
                return;

            this.LocalHost = entity.LocalHost;
            this.LocalPort = entity.LocalPort;
            this.RemoteHost = entity.RemoteHost;
            this.RemotePort = entity.RemotePort;
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void Delete()
        {
            if (ArtDomain.Current.ProjectDomain == null)
                return;

            var collection = ArtDomain.Current.ProjectDomain.CacheContext.Database.GetCollection<UdpSourceEntity>();
            collection.Delete(this.Model.SourceID);
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="buffer">数据</param>
        public void Send(IArrayBuffer buffer)
        {
            this.UdpClient?.Send(buffer.GetBytes());
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="buffer">数据</param>
        public void Send(byte[] buffer)
        {
            this.UdpClient?.Send(buffer);
        }

        // =====================================================================================
        // Private Function

        /// <summary>
        /// 执行数据接收线程
        /// </summary>
        /// <param name="context">上下文</param>
        private void ExecuteReceiveThread(DanceThreadContext context)
        {
            while (!context.IsCancel && this.UdpClient != null)
            {
                try
                {
                    IPEndPoint? endPoint = null;
                    byte[] data = this.UdpClient.Receive(ref endPoint);

                    this.ReceiveData?.Invoke(this, new DeviceReceiveBufferDataEventArgs(this, data));
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }
        }
    }
}