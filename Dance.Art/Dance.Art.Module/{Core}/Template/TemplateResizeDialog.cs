using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dance.Art.Module
{
    /// <summary>
    /// 模板可改变大小弹出框
    /// </summary>
    public class TemplateResizeDialog : ContentControl
    {
        static TemplateResizeDialog()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TemplateResizeDialog), new FrameworkPropertyMetadata(typeof(TemplateResizeDialog)));
        }
    }
}
