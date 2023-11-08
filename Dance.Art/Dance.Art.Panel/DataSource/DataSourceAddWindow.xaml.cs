using Dance.Art.Domain;
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
    /// DataSourceAddWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DataSourceAddWindow : Window
    {
        public DataSourceAddWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 添加数据源窗口
        /// </summary>
        /// <param name="group">数据源所属分组</param>
        public DataSourceAddWindow(DataSourceGroupModel group) : this()
        {
            DataSourceAddWindowModel vm = new(group)
            {
                View = this
            };
            this.DataContext = vm;
        }
    }
}
