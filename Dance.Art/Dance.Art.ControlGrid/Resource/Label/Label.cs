using Dance.Art.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Dance.Art.ControlGrid
{
    /// <summary>
    /// 标签
    /// </summary>
    public class Label : ContentControl
    {
        static Label()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Label), new FrameworkPropertyMetadata(typeof(Label)));
        }
    }
}
