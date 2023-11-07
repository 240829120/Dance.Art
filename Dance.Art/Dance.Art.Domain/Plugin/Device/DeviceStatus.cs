using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 设备状态
    /// </summary>
    public enum DeviceStatus
    {
        /// <summary>
        /// 断开
        /// </summary>
        Disconnected = 0,

        /// <summary>
        /// 等待
        /// </summary>
        Waiting = 1,

        /// <summary>
        /// 已经连接
        /// </summary>
        Connected = 2
    }
}
