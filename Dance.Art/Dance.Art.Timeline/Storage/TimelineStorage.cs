﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Timeline
{
    /// <summary>
    /// 时间线仓储
    /// </summary>
    public class TimelineStorage
    {
        /// <summary>
        /// 时间线模型
        /// </summary>
        public TimelineModel? TimelineModel { get; set; }

        /// <summary>
        /// 轨道
        /// </summary>
        public List<TimelineTrackModel>? Tracks { get; set; }
    }
}
