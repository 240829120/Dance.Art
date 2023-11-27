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
    /// 触发器操作编辑
    /// </summary>
    public class TimelineTriggerOperateEditor : Control
    {
        static TimelineTriggerOperateEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TimelineTriggerOperateEditor), new FrameworkPropertyMetadata(typeof(TimelineTriggerOperateEditor)));
        }
    }
}
