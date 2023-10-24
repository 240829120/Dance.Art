using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 输出消息事件参数
    /// </summary>
    public class OutputEventArgs : EventArgs
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string? Message { get; set; }
    }
}
