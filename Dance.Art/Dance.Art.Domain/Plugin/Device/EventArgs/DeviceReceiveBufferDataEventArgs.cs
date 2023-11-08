using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 设备接收二进制数据事件参数
    /// </summary>
    public class DeviceReceiveBufferDataEventArgs : DeviceReceiveDataEventArgsBase
    {
        /// <summary>
        /// 设备接收数据事件参数
        /// </summary>
        /// <param name="source">源</param>
        /// <param name="buffer">二进制数据</param>
        public DeviceReceiveBufferDataEventArgs(IDeviceSource source, byte[] buffer) : base(source)
        {
            this.Buffer = buffer;
        }

        /// <summary>
        /// 二进制数据
        /// </summary>
        public byte[] Buffer { get; private set; }

        /// <summary>
        /// 获取字符数据
        /// </summary>
        /// <returns></returns>
        public string GetBufferString()
        {
            return Encoding.UTF8.GetString(this.Buffer);
        }
    }
}
