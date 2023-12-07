using Dance.Art.Domain;
using Dance.Art.Scene;
using Dance.Wpf;
using HelixToolkit.SharpDX.Core.Assimp;
using HelixToolkit.SharpDX.Core;
using Microsoft.VisualBasic.Logging;
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
using System.Windows.Media.Media3D;
using HelixToolkit.SharpDX.Core.Model.Scene;
using HelixToolkit.Wpf.SharpDX;

namespace Dance.Art.WpfTest
{
    [DanceServiceRoute]
    public class TestService
    {
        [DanceServiceRoute]
        public int Add(int a, int b)
        {
            return a + b;
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly List<ISceneItemModel> Items = new();

        public MainWindow()
        {
            DanceServiceManager serviceManager = new();
            serviceManager.AddService(new TestService());
            var r = serviceManager.Invoke("Test/Add", new object[] { 1, 2 });



            InitializeComponent();

            this.viewport.ModelUpDirection = new System.Windows.Media.Media3D.Vector3D(0, 1, 0);
            this.viewport.Camera = new HelixToolkit.Wpf.SharpDX.PerspectiveCamera() { Position = new Point3D(0, 0, 200), LookDirection = new Vector3D(0, 0, -200), UpDirection = new Vector3D(0, 1, 0), FarPlaneDistance = 1000 };
            this.viewport.EffectsManager = new DefaultEffectsManager();

            string path = @"E:\学习\helix-toolkit-develop\helix-toolkit-develop\Models\FBX\obj_Neck_Mech_Walker_by_3DHaupt\Neck_Mech_Walker_by_3DHaupt-(Wavefront OBJ).fbx";
            Importer importer = new();

            using FileStream fs = new(path, FileMode.Open, FileAccess.Read);
            importer.Load(fs, path, ".fbx", out HelixToolkitScene scene, new DanceTexturePathResolver());


            this.Items.Add(new ModelModel { Root = scene.Root });
            this.Items.Add(new BoxModel { });

            this.itemsModel.ItemsSource = this.Items;


            this.Loaded += MainWindow_Loaded;
            this.Closed += Window_Closed;

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            //   this.manipulator.Target = this.Items[0];

        }

        private void Viewport_MouseDown3D(object sender, RoutedEventArgs e)
        {
            if (e is not MouseDown3DEventArgs args || args.OriginalInputEventArgs is not MouseButtonEventArgs mouseEventArgs || mouseEventArgs.LeftButton != MouseButtonState.Pressed)
                return;

            if (args.HitTestResult == null)
            {
                this.manipulator.Target = null;
                return;
            }

            if (args.HitTestResult.ModelHit is Element3D element)
            {
                if (this.manipulator.HitTest(element) || element.DataContext is not IDanceModel3D model)
                    return;

                this.manipulator.Target = model;
                return;
            }
            else if (args.HitTestResult.ModelHit is MeshNode node && node.GetOnwer() is DanceGroupNode3D groupNode && groupNode.Element.DataContext is IDanceModel3D model)
            {
                this.manipulator.Target = model;
                return;
            }

            this.manipulator.Target = null;
        }

        private Element3D? TryFindTag(SceneNode node)
        {
            if (node.Tag is Element3D element)
                return element;

            return this.TryFindTag(node.Parent);
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
