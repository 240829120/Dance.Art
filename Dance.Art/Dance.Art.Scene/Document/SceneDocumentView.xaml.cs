using HelixToolkit.SharpDX.Core.Assimp;
using HelixToolkit.SharpDX.Core;
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
using Dance.Wpf;
using HelixToolkit.Wpf.SharpDX;
using SharpDX;

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

            var grid = new LineBuilder();
            for (var i = -1000; i <= 1000; i += 30)
            {
                grid.AddLine(new Vector3(i, 0, -1000), new Vector3(i, 0, 1000));
                grid.AddLine(new Vector3(-1000, 0, i), new Vector3(1000, 0, i));
            }

            this.grid.Geometry = grid.ToLineGeometry3D();
            this.grid.Color = (System.Windows.Media.Color)ColorConverter.ConvertFromString("#FF444444");

            this.DataContext = new SceneDocumentViewModel()
            {
                View = this
            };
        }
    }
}
