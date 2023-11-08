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

namespace Dance.Art.DataSource
{
    /// <summary>
    /// TextDataSourceDocumentView.xaml 的交互逻辑
    /// </summary>
    public partial class TextDataSourceDocumentView : UserControl
    {
        public TextDataSourceDocumentView()
        {
            InitializeComponent();

            TextDataSourceDocumentViewModel vm = new()
            {
                View = this
            };
            this.DataContext = vm;
        }
    }
}
