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

namespace Dance.Art.Scene
{
    /// <summary>
    /// 长方体
    /// </summary>
    [DisplayName("长方体")]
    public class BoxModel : SceneItemModelBase
    {
        /// <summary>
        /// 长方体
        /// </summary>
        public BoxModel() : base(SceneResourceDefines.Box)
        {
            MeshBuilder builder = new();
            builder.AddBox(new Vector3(0, 0, 0), 1, 1, 1, BoxFaces.All);
            this.Geometry = builder.ToMeshGeometry3D();
        }

        // ====================================================================================================
        // Property

        /// <summary>
        /// 形状
        /// </summary>
        [Browsable(false), JsonIgnore]
        public MeshGeometry3D Geometry { get; }
    }
}
