﻿using Dance.Art.Domain;
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
using System.Windows.Shapes;

namespace Dance.Art.Panel
{
    /// <summary>
    /// DeviceAddWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DeviceAddWindow : Window
    {
        public DeviceAddWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 添加连接窗口
        /// </summary>
        /// <param name="group">连接所属分组</param>
        public DeviceAddWindow(DeviceGroupModel group) : this()
        {
            DeviceAddWindowModel vm = new(group)
            {
                View = this
            };
            this.DataContext = vm;
        }
    }
}
