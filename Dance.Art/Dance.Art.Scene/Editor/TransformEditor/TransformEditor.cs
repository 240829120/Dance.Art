using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dance.Art.Scene
{
    /// <summary>
    /// 变换编辑器
    /// </summary>
    public class TransformEditor : Control
    {
        static TransformEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TransformEditor), new FrameworkPropertyMetadata(typeof(TransformEditor)));
        }
    }
}
