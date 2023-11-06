using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 连接源模型
    /// </summary>
    public interface IConnectionSourceModel
    {
        /// <summary>
        /// 接收数据时触发
        /// </summary>
        event EventHandler<ConnectionSourceReceiveDataEventArgs>? ReceiveData;

        void Send(byte[] data);
    }
}
