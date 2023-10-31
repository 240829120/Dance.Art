﻿using Dance.Wpf;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace Dance.Art.Plugin
{
    /// <summary>
    /// OutputView.xaml 的交互逻辑
    /// </summary>
    public partial class OutputView : UserControl
    {
        public OutputView()
        {
            InitializeComponent();

            using Stream? stream = Application.GetResourceStream(new Uri("pack://application:,,,/Dance.Art.Plugin;component/Panel/Output/OutputHighlighting.xml", UriKind.RelativeOrAbsolute))?.Stream;
            if (stream != null)
            {
                using XmlReader reader = new XmlTextReader(stream);
                this.edit.SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
            }

            OutputViewModel vm = new()
            {
                View = this
            };
            this.DataContext = vm;
        }
    }
}
