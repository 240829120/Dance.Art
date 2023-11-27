using Dance.Art.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Dance.Art.Timeline
{
    /// <summary>
    /// 命令元素
    /// </summary>
    public class CommandElement : ContentControl
    {
        static CommandElement()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CommandElement), new FrameworkPropertyMetadata(typeof(CommandElement)));
        }
    }
}
