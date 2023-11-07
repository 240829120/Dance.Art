using Microsoft.ClearScript.JavaScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 连接源接收数据事件参数
    /// </summary>
    public class ConnectionSourceReceiveDataEventArgs : EventArgs
    {
        /// <summary>
        /// 连接源接收数据事件参数
        /// </summary>
        /// <param name="data">数据</param>
        public ConnectionSourceReceiveDataEventArgs(byte[] data)
        {
            this.Data = data;
        }

        /// <summary>
        /// 数据
        /// </summary>
        public byte[] Data { get; private set; }
    }
}
