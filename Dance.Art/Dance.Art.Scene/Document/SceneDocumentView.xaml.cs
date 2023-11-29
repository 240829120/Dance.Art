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

            //this.Loaded += SceneDocumentView_Loaded;
        }

        //private void SceneDocumentView_Loaded(object sender, RoutedEventArgs e)
        //{
        //    string path = @"E:\学习\helix-toolkit-develop\helix-toolkit-develop\Models\FBX\obj_Neck_Mech_Walker_by_3DHaupt\Neck_Mech_Walker_by_3DHaupt-(Wavefront OBJ).fbx";

        //    Importer importer = new();

        //    using FileStream fs = new(path, FileMode.Open, FileAccess.Read);
        //    importer.Load(fs, path, ".fbx", out HelixToolkitScene scene, new DanceTexturePathResolver());

        //    SceneNodeGroupModel3D model = new();
        //    model.AddNode(scene.Root);

        //    this.presenter.Content = model;
        //}
    }
}
