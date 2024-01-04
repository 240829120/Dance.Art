using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 设备接收数据事件参数基类
    /// </summary>
    /// <param name="source">源</param>
    public abstract class DeviceReceiveDataEventArgsBase(IDeviceSource source) : EventArgs
    {
        /// <summary>
        /// 源
        /// </summary>
        public IDeviceSource Source { get; private set; } = source;
    }
}
