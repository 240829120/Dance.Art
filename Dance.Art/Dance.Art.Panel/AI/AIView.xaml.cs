using Dance.Wpf;
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
    /// AIView.xaml 的交互逻辑
    /// </summary>
    public partial class AIView : UserControl
    {
        public AIView()
        {
            InitializeComponent();

            AIViewModel vm = DanceDomain.Current.LifeScope.Resolve<AIViewModel>();
            this.DataContext = vm;
            vm.View = this;
        }
    }
}
