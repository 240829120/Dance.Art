using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Connection
{
    /// <summary>
    /// Ping编辑视图模型
    /// </summary>
    public class PingEditViewModel : DanceViewModel, IConnectionEditViewModel
    {
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

        /// <summary>
        /// 从模型中加载数据
        /// </summary>
        /// <param name="model">连接模型</param>
        public void LoadFromModel(ConnectionModel model)
        {
            if (model.Source is not PingSourceModel sourceModel)
                return;

            this.Host = sourceModel.Host;
            this.Frequency = sourceModel.Frequency;
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

            if (model.Source is not PingSourceModel sourceModel)
            {
                error = $"ConnectionSourceModel 类型不正确, 应该为: PingSourceModel, 实际为: {model.Source?.GetType().FullName}";
                return false;
            }

            if (string.IsNullOrWhiteSpace(this.Host))
            {
                error = "主机不能为空";
                return false;
            }

            if (this.Frequency < 1000)
            {
                error = "频率应该大于或等于1000";
                return false;
            }

            sourceModel.Host = this.Host;
            sourceModel.Frequency = this.Frequency;

            return true;
        }
    }
}
