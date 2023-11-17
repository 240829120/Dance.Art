using Dance.Art.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dance.Art.ControlGrid
{
    /// <summary>
    /// 命令按钮
    /// </summary>
    public class CommandButton : Button
    {
        static CommandButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CommandButton), new FrameworkPropertyMetadata(typeof(CommandButton)));
        }
    }
}
