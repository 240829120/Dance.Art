using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Timeline
{
    /// <summary>
    /// 时间线拖拽信息
    /// </summary>
    public class TimelineDragInfo
    {
        /// <summary>
        /// 时间线拖拽信息
        /// </summary>
        /// <param name="model">时间线元素模型</param>
        public TimelineDragInfo(TimelineElementModelBase model)
        {
            this.Model = model;
        }

        /// <summary>
        /// 元素
        /// </summary>
        public TimelineElementModelBase Model { get; set; }
    }
}
