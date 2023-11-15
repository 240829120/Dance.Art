using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 设计模式改变消息
    /// </summary>
    public class DockingDesignModeChangedMessage
    {
        /// <summary>
        /// 设计模式改变消息
        /// </summary>
        /// <param name="isDesignMode">是否是设计模式</param>
        public DockingDesignModeChangedMessage(bool isDesignMode)
        {
            this.IsDesignMode = isDesignMode;
        }

        /// <summary>
        /// 是否是设计模式
        /// </summary>
        public bool IsDesignMode { get; private set; }
    }
}
