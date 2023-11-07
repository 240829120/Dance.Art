using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 设备源
    /// </summary>
    public interface IDeviceSource : IDisposable
    {
        /// <summary>
        /// 关联模型
        /// </summary>
        DeviceModel? Model { get; set; }

        /// <summary>
        /// 连接
        /// </summary>
        void Connect();

        /// <summary>
        /// 断开
        /// </summary>
        void Disconnect();

        /// <summary>
        /// 保存至仓储
        /// </summary>
        void SaveToStorage();

        /// <summary>
        /// 从仓储中获取
        /// </summary>
        void LoadFromStorage();

        /// <summary>
        /// 删除
        /// </summary>
        void Delete();
    }
}
