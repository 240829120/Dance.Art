using Dance.Art.Domain;
using Dance.Wpf;
using HelixToolkit.Wpf.SharpDX;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace Dance.Art.Scene
{
    /// <summary>
    /// 场景项模型基类
    /// </summary>
    public abstract class SceneItemModelBase : ResourceItemModelBase, ISceneItemModel
    {
        /// <summary>
        /// 场景项模型基类
        /// </summary>
        protected SceneItemModelBase() : base(null)
        {

        }

        // ===================================================================================================
        // Field

        /// <summary>
        /// 场景资源管理器
        /// </summary>
        protected readonly ISceneResourceManager SceneResourceManager = DanceDomain.Current.LifeScope.Resolve<ISceneResourceManager>();

        // ===================================================================================================
        // Property

        #region Icon -- 图标

        private string? icon;
        /// <summary>
        /// 图标
        /// </summary>
        [Browsable(false)]
        public string? Icon
        {
            get { return icon; }
            set { icon = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Name -- 名称

        private string? name;
        /// <summary>
        /// 名称
        /// </summary>
        [Category(PropertyCategoryDefines.BASE), PropertyOrder(1), Description("名称"), DisplayName("名称")]
        public string? Name
        {
            get { return name; }
            set { name = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        #region Transform -- 变换

        private SceneTransformGroup3D transform = new();
        /// <summary>
        /// 变换
        /// </summary>
        [Category(PropertyCategoryDefines.LAYOUT), PropertyOrder(0), Description("变换"), DisplayName("变换")]
        public SceneTransformGroup3D Transform
        {
            get { return transform; }
            set { transform = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        // ===================================================================================================
        // Controller

        #region Material -- 材质

        /// <summary>
        /// 材质
        /// </summary>
        [Browsable(false), JsonIgnore]
        public PhongMaterial Material { get; } = PhongMaterials.Red;

        #endregion
    }
}
