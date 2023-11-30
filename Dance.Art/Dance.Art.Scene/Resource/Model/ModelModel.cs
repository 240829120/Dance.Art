using CommunityToolkit.Mvvm.Input;
using Dance.Art.Domain;
using Dance.Art.Module;
using Dance.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using HelixToolkit.Wpf.SharpDX;
using System.Windows.Media;
using HelixToolkit.SharpDX.Core;
using SharpDX;
using HelixToolkit.SharpDX.Core.Assimp;
using System.IO;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using HelixToolkit.SharpDX.Core.Model.Scene;

namespace Dance.Art.Scene
{
    /// <summary>
    /// 模型
    /// </summary>
    [DisplayName("模型")]
    public class ModelModel : SceneItemModelBase
    {
        /// <summary>
        /// 模型
        /// </summary>
        public ModelModel() : base(SceneResourceDefines.Model)
        {

        }

        // ====================================================================================================
        // Property

        #region FilePath -- 文件路径

        private string? filePath;
        /// <summary>
        /// 模型路径
        /// </summary>
        [Category(PropertyCategoryDefines.OTHER), PropertyOrder(0), Description("模型路径"), DisplayName("模型路径")]
        [Editor(typeof(OpenFileEditor), typeof(ModelOpenFileEditor))]
        public string? FilePath
        {
            get { return filePath; }
            set
            {
                filePath = value;
                this.OnWrapperPropertyChanged();

                this.Root = null;

                if (string.IsNullOrWhiteSpace(value) || !File.Exists(value))
                    return;

                Importer importer = new();

                using FileStream fs = new(value, FileMode.Open, FileAccess.Read);
                importer.Load(fs, value, Path.GetExtension(value), out HelixToolkitScene scene, new DanceTexturePathResolver());

                this.Root = scene.Root;
            }
        }

        #endregion

        // ====================================================================================================
        // Control

        #region Root -- 根

        private SceneNode? root;
        /// <summary>
        /// 根
        /// </summary>
        [Browsable(false), JsonIgnore]
        public SceneNode? Root
        {
            get { return root; }
            set { root = value; this.OnPropertyChanged(); }
        }

        #endregion

    }
}
