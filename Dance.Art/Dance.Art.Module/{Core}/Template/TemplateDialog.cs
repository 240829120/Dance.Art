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
    /// 模板弹出框
    /// </summary>
    public class TemplateDialog : ContentControl
    {
        static TemplateDialog()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TemplateDialog), new FrameworkPropertyMetadata(typeof(TemplateDialog)));
        }
    }
}
