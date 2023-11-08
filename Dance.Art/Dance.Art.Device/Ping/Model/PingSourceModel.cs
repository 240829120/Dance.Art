using Dance.Art.Domain;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.Device
{
    /// <summary>
    /// Ping源模型
    /// </summary>
    public class PingSourceModel : DanceWrapperModel, IDeviceSource
    {
        // =====================================================================================
        // Field

        /// <summary>
        /// 最小频率
        /// </summary>
        private const int MIN_FREQUENCY = 1000;

        /// <summary>
        /// Ping线程
        /// </summary>
        private DanceThread? PingThread;

        // =====================================================================================
        // Property

        /// <summary>
        /// Ping
        /// </summary>
        public Ping? Ping { get; set; }

        /// <summary>
        /// 关联模型
        /// </summary>
        [NotNull]
        public DeviceModel? Model { get; set; }

        #region Host -- 主机

        private string? host;
        /// <summary>
        /// 主机
        /// </summary>
        public string? Host
        {
            get { return host; }
            set { host = value; this.OnWrapperPropertyChanged(); }
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
            set { frequency = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        // =====================================================================================
        // Public Function

        /// <summary>
        /// 连接
        /// </summary>
        public void Connect()
        {
            if (this.PingThread != null || this.Ping != null)
                return;

            this.Ping = new();
            this.PingThread = new(this.ExecutePingThread);
            this.PingThread.Start();
        }

        /// <summary>
        /// 断开
        /// </summary>
        public void Disconnect()
        {
            this.PingThread?.Stop();
            this.PingThread = null;
            this.Ping?.Dispose();
            this.Ping = null;

            this.Model.Status = DeviceStatus.Disconnected;
        }

        /// <summary>
        /// 保存至仓储
        /// </summary>
        public void SaveToStorage()
        {
            if (ArtDomain.Current.ProjectDomain == null)
                return;

            var collection = ArtDomain.Current.ProjectDomain.CacheContext.Database.GetCollection<PingSourceEntity>();
            PingSourceEntity? entity = collection.FindById(this.Model.SourceID) ?? new();
            entity.Host = this.Host;
            entity.Frequency = this.Frequency;

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

            var collection = ArtDomain.Current.ProjectDomain.CacheContext.Database.GetCollection<PingSourceEntity>();
            PingSourceEntity? entity = collection.FindById(this.Model.SourceID);
            if (entity == null)
                return;

            this.Host = entity.Host;
            this.Frequency = entity.Frequency;
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void Delete()
        {
            if (ArtDomain.Current.ProjectDomain == null)
                return;

            var collection = ArtDomain.Current.ProjectDomain.CacheContext.Database.GetCollection<PingSourceEntity>();
            collection.Delete(this.Model.SourceID);
        }

        // =====================================================================================
        // Private Function

        /// <summary>
        /// 执行Ping线程
        /// </summary>
        /// <param name="context">上下文</param>
        private void ExecutePingThread(DanceThreadContext context)
        {
            while (!context.IsCancel && this.Ping != null)
            {
                if (!string.IsNullOrWhiteSpace(this.Host))
                {
                    try
                    {
                        var result = this.Ping.Send(this.Host);

                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            this.Model.Status = result.Status == IPStatus.Success ? DeviceStatus.Connected : DeviceStatus.Disconnected;
                        });
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);
                    }
                }

                int max = Math.Max(MIN_FREQUENCY, this.Frequency);
                System.Threading.Thread.Sleep(max);
            }
        }
    }
}
