using Dance.Art.Domain;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Connection
{
    /// <summary>
    /// Ping源模型
    /// </summary>
    public class PingSourceModel : DanceModel
    {
        /// <summary>
        /// Ping任务
        /// </summary>
        internal Task? PingTask;

        /// <summary>
        /// Ping
        /// </summary>
        public Ping? Ping { get; set; }

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

        private int frequency;
        /// <summary>
        /// 频率（单位：毫秒）
        /// </summary>
        public int Frequency
        {
            get { return frequency; }
            set { frequency = value; this.OnPropertyChanged(); }
        }

        #endregion
    }
}
