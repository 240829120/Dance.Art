using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.ControlGrid
{
    /// <summary>
    /// 文本框
    /// </summary>
    public class TextBox : System.Windows.Controls.TextBox
    {
        static TextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBox), new FrameworkPropertyMetadata(typeof(TextBox)));
        }
    }
}
