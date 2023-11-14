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
    /// 按钮组文档项
    /// </summary>
    public class ButtonBoxItem : ListBoxItem
    {
        static ButtonBoxItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ButtonBoxItem), new FrameworkPropertyMetadata(typeof(ButtonBoxItem)));
        }
    }
}
