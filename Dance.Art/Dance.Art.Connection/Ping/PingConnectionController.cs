using Dance.Art.Domain;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.Connection
{
    /// <summary>
    /// Ping连接控制器
    /// </summary>
    public class PingConnectionController : DanceObject, IConnectionController
    {
        public PingConnectionController()
        {
            this.LOOP_KEY = $"PingConnectionController__{this.GetHashCode()}";
            this.Ping = new();
        }

        // ======================================================================================================
        // Field

        /// <summary>
        /// 循环管理器
        /// </summary>
        private readonly IDanceLoopManager LoopManager = DanceDomain.Current.LifeScope.Resolve<IDanceLoopManager>();

        /// <summary>
        /// 循环键
        /// </summary>
        private readonly string LOOP_KEY;

        /// <summary>
        /// Ping
        /// </summary>
        private Ping? Ping;

        // ======================================================================================================
        // Property

        /// <summary>
        /// 连接模型
        /// </summary>
        public ConnectionModel? Model { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            if (this.Model == null)
                return;

            this.Model.Parameters.TryGetValue(PingConnectionParameters.Frequency, out string? frequency);
            _ = int.TryParse(frequency, out int frequencyValue);
            frequencyValue = Math.Max(frequencyValue, 1);

            this.LoopManager.Register(this.LOOP_KEY, frequencyValue, this.UpdateStatus);
        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            this.LoopManager.UnRegister(this.LOOP_KEY);
            this.Ping?.Dispose();
            this.Ping = null;
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        private void UpdateStatus()
        {
            if (this.Model == null || this.Ping == null)
                return;

            this.Model.Parameters.TryGetValue("Host", out string? host);
            if (string.IsNullOrWhiteSpace(host))
                return;

            this.Ping?.SendPingAsync(host).ContinueWith(r =>
            {
                if (this.Model == null)
                    return;

                Application.Current?.Dispatcher.BeginInvoke(() =>
                {
                    try
                    {
                        this.Model.Status = r.Result.Status == IPStatus.Success ? ConnectionStatus.Connected : ConnectionStatus.Disconnected;
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);
                    }
                });
            });
        }
    }
}
