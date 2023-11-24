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
    /// 脚本元素
    /// </summary>
    public class ScriptElement : ContentControl
    {
        static ScriptElement()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScriptElement), new FrameworkPropertyMetadata(typeof(ScriptElement)));
        }
    }
}
