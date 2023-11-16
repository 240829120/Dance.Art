using Dance.Art.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dance.Art.ButtonBox
{
    /// <summary>
    /// 脚本按钮
    /// </summary>
    public class ScriptButton : Button
    {
        static ScriptButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScriptButton), new FrameworkPropertyMetadata(typeof(ScriptButton)));
        }
    }
}
