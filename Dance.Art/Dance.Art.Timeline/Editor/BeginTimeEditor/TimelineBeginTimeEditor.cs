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
    /// 开始时间编辑器
    /// </summary>
    public class TimelineBeginTimeEditor : Control
    {
        static TimelineBeginTimeEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TimelineBeginTimeEditor), new FrameworkPropertyMetadata(typeof(TimelineBeginTimeEditor)));
        }
    }
}
