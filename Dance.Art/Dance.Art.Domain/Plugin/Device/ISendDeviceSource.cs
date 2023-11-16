using Microsoft.ClearScript.JavaScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 支持发送的设备源
    /// </summary>
    public interface ISendDeviceSource : IDeviceSource
    {
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="buffer">数据</param>
        /// <returns>发送数据长度</returns>
        int Send(IArrayBuffer buffer);

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="buffer">数据</param>
        /// <returns>发送数据长度</returns>
        int Send(byte[] buffer);
    }
}
