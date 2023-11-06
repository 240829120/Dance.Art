using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Connection
{
    /// <summary>
    /// Ping连接编辑视图模型
    /// </summary>
    public class PingEditViewModel : DanceViewModel, IConnectionEditViewModel
    {
        // =============================================================================================
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

        #region Frequency -- 频率（单位：秒）

        private int frequency;
        /// <summary>
        /// 频率（单位：秒）
        /// </summary>
        public int Frequency
        {
            get { return frequency; }
            set { frequency = value; this.OnPropertyChanged(); }
        }

        #endregion

        // =============================================================================================
        // Command

        // =============================================================================================
        // Public Function

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="model">模型</param>
        public void Load(ConnectionModel model)
        {
            //model.Parameters.TryGetValue(PingConnectionParameters.Host, out string? host);
            //model.Parameters.TryGetValue(PingConnectionParameters.Frequency, out string? frequency);

            //this.Host = host;

            ////_ = int.TryParse(frequency, out int frequencyValue);
            //this.Frequency = Math.Max(2, frequencyValue);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model">模型</param>
        /// <returns>是否保存成功</returns>
        public bool Save(ConnectionModel model)
        {
            //model.Parameters[PingConnectionParameters.Host] = this.Host ?? string.Empty;
            //model.Parameters[PingConnectionParameters.Frequency] = this.Frequency.ToString();

            return true;
        }
    }
}
