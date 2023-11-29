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

namespace Dance.Art.Scene
{
    /// <summary>
    /// SceneDocumentView.xaml 的交互逻辑
    /// </summary>
    public partial class SceneDocumentView : UserControl
    {
        public SceneDocumentView()
        {
            InitializeComponent();

            this.DataContext = new SceneDocumentViewModel()
            {
                View = this
            };
        }
    }
}
