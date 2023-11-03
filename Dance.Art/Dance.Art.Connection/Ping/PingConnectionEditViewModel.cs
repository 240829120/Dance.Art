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
    public class PingConnectionEditViewModel : DanceViewModel, IConnectionEditViewModel
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
            model.Parameters.TryGetValue("Host", out string? host);
            this.Host = host;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model">模型</param>
        /// <returns>是否保存成功</returns>
        public bool Save(ConnectionModel model)
        {
            model.Parameters["Host"] = this.Host ?? string.Empty;

            return true;
        }
    }
}
