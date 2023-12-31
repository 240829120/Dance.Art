﻿using Dance.Wpf;
using System;
using System.Collections.Generic;
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

namespace Dance.Art.Panel
{
    /// <summary>
    /// CommandView.xaml 的交互逻辑
    /// </summary>
    public partial class CommandView : UserControl
    {
        public CommandView()
        {
            InitializeComponent();

            CommandViewModel vm = new()
            {
                View = this
            };
            this.DataContext = vm;
        }

        /// <summary>
        /// 编辑器鼠标按下
        /// </summary>
        private void edit_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.DataContext is not CommandViewModel vm || vm.ViewPluginModel == null)
                return;

            vm.ViewPluginModel.IsActive = true;
        }
    }
}
