using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class Student
    {
        [Category("基础"), Description("描述描述123")]
        public string? Property_1_1 { get; set; }


        [Category("基础")]
        public int Property_1_2 { get; set; }


        [Category("基础")]
        public string? Property_1_3 { get; set; }

        [Category("基础")]
        public string? Property_1_4 { get; set; }


        [Category("基础2")]
        public string? Property_2_1 { get; set; }

        [Category("基础2")]
        public string? Property_2_2 { get; set; }

        [Category("基础2")]
        public string? Property_2_3 { get; set; }

        [Category("基础2")]
        public string? Property_2_4 { get; set; }

        [Category("基础2")]
        public string? Property_2_5 { get; set; }
    }

    /// <summary>
    /// PropertyView.xaml 的交互逻辑
    /// </summary>
    public partial class PropertyView : UserControl
    {
        public PropertyView()
        {
            InitializeComponent();

            PropertyViewModel vm = new()
            {
                View = this
            };
            this.DataContext = vm;

            this.propertyGrid.SelectedObject = new Student();
        }
    }
}
