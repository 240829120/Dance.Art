using Dance.Art.Domain;
using Dance.Wpf;
using HelixToolkit.SharpDX.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace Dance.Art.Scene
{
    /// <summary>
    /// 点光源模型
    /// </summary>
    public class PointLightModel : SceneItemModelBase
    {
        /// <summary>
        /// 平行光
        /// </summary>
        public PointLightModel() : base(SceneResourceDefines.PointLight)
        {
            var resource = Application.GetResourceStream(new Uri("pack://application:,,,/Dance.Art.Scene;component/Themes/Resources/Images/point_light.png", UriKind.RelativeOrAbsolute));
            this.billboardImage = new BillboardImage3D(TextureModel.Create(resource.Stream));
            this.billboardImage.ImageInfos.Add(new ImageInfo() { Width = 2, Height = 2, UV_TopLeft = new SharpDX.Vector2(0, 0), UV_BottomRight = new SharpDX.Vector2(1, 1) });
            this.innerBounds = new SharpDX.BoundingBox(new SharpDX.Vector3(-1, -1, -1), new SharpDX.Vector3(1, 1, 1));
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

        #region Attenuation -- 衰减

        private Vector3D attenuation = new(1.0f, 0.5f, 0.10f);
        /// <summary>
        /// 衰减
        /// </summary>
        [Category(PropertyCategoryDefines.OTHER), PropertyOrder(0), Description("衰减"), DisplayName("衰减")]
        public Vector3D Attenuation
        {
            get { return attenuation; }
            set { attenuation = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        // ================================================================================
        // Control

        #region InnerBounds -- 内置范围

        private SharpDX.BoundingBox? innerBounds;
        /// <summary>
        /// 内置范围
        /// </summary>
        [Browsable(false), JsonIgnore]
        public SharpDX.BoundingBox? InnerBounds
        {
            get { return innerBounds; }
        }


        #endregion

        #region BillboardImage -- 广告牌

        private BillboardImage3D billboardImage;

        /// <summary>
        /// 广告牌
        /// </summary>
        [Browsable(false), JsonIgnore]
        public BillboardImage3D BillboardImage
        {
            get { return billboardImage; }
        }

        #endregion
    }
}