using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 输出管理器
    /// </summary>
    [DanceSingleton(typeof(IOutputManager))]
    public class OutputManager : IOutputManager
    {
        /// <summary>
        /// 当输出消息时触发
        /// </summary>
        public event EventHandler<OutputEventArgs>? OnOutput;

        /// <summary>
        /// 清理消息时触发
        /// </summary>
        public event EventHandler<EventArgs>? OnClear;

        /// <summary>
        /// 写入行
        /// </summary>
        /// <param name="msg">消息</param>
        public void WriteLine(string msg)
        {
            this.OnOutput?.Invoke(this, new OutputEventArgs() { Message = msg });
        }

        /// <summary>
        /// 清理
        /// </summary>
        public void Clear()
        {
            this.OnClear?.Invoke(this, new EventArgs());
        }
    }
}
