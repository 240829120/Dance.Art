using Dance.Art.Domain;
using Dance.Wpf;
using Microsoft.VisualBasic.Logging;
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

namespace Dance.Art.WpfTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Closed += Window_Closed;
        }

        /// <summary>
        /// 窗口关闭
        /// </summary>
        private void Window_Closed(object? sender, EventArgs e)
        {
            DanceDomain.Current?.Dispose();
            Application.Current.Shutdown();
        }
    }
}
