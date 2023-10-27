using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 输出管理器
    /// </summary>
    public interface IOutputManager
    {
        /// <summary>
        /// 输出消息时触发
        /// </summary>
        event EventHandler<OutputEventArgs>? OnOutput;

        /// <summary>
        /// 清理消息时触发
        /// </summary>
        event EventHandler<EventArgs>? OnClear;

        /// <summary>
        /// 写入行
        /// </summary>
        /// <param name="msg">消息</param>
        void WriteLine(string msg);

        /// <summary>
        /// 清理
        /// </summary>
        void Clear();
    }
}
