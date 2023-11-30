using CommunityToolkit.Mvvm.Input;
using Dance.Art.Domain;
using Dance.Art.Module;
using Dance.Wpf;
using Microsoft.ClearScript.JavaScript;
using Microsoft.ClearScript;
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
using System.Windows.Media.Media3D;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace Dance.Art.Scene
{
    /// <summary>
    /// 平行光
    /// </summary>
    [DisplayName("平行光")]
    public class DirectionalLightModel : SceneItemModelBase
    {
        /// <summary>
        /// 平行光
        /// </summary>
        public DirectionalLightModel() : base(SceneResourceDefines.DirectionalLight)
        {

        }

        // ================================================================================
        // Field

        // ================================================================================
        // Property

        #region Color -- 颜色

        private Color color;
        /// <summary>
        /// 颜色
        /// </summary>
        [Category(PropertyCategoryDefines.OTHER), PropertyOrder(0), Description("颜色"), DisplayName("颜色")]
        public Color Color
        {
            get { return color; }
            set { color = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        #region Direction -- 方向

        private Vector3D direction;
        /// <summary>
        /// 方向
        /// </summary>
        [Category(PropertyCategoryDefines.OTHER), PropertyOrder(0), Description("方向"), DisplayName("方向")]
        public Vector3D Direction
        {
            get { return direction; }
            set { direction = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion
    }
}
