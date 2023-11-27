using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dance.Art.Timeline
{
    /// <summary>
    /// 结束时间编辑器
    /// </summary>
    public class TimelineEndTimeEditor : Control
    {
        static TimelineEndTimeEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TimelineEndTimeEditor), new FrameworkPropertyMetadata(typeof(TimelineEndTimeEditor)));
        }
    }
}
