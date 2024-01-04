using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 激活内容改变消息
    /// </summary>
    /// <param name="content">内容</param>
    public class DockingActiveContentChangedMessage(object? content)
    {
        /// <summary>
        /// 内容
        /// </summary>
        public object? Content { get; private set; } = content;
    }
}
