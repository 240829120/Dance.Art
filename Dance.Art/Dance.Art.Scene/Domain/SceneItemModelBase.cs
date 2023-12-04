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
        /// <param name="key">资源键<see cref="SceneResourceDefines"/></param>
        protected SceneItemModelBase(string key) : base(null)
        {
            this.DataTemplate = this.SceneResourceManager.Get(key);
            this.transform.PropertyChanged += Transform_PropertyChanged;
        }

        // ===================================================================================================
        // Field

        /// <summary>
        /// 场景资源管理器
        /// </summary>
        private readonly ISceneResourceManager SceneResourceManager = DanceDomain.Current.LifeScope.Resolve<ISceneResourceManager>();

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

        private DanceTransform3D transform = new();
        /// <summary>
        /// 变换信息
        /// </summary>
        [Category(PropertyCategoryDefines.TRANSFORM), PropertyOrder(0), Description("变换"), DisplayName("变换")]
        [Editor(typeof(TransformEditor), typeof(TransformEditor))]
        public DanceTransform3D Transform
        {
            get { return transform; }
            set
            {
                if (this.transform != null)
                {
                    this.transform.PropertyChanged -= Transform_PropertyChanged;
                }

                transform = value;
                this.OnWrapperPropertyChanged();

                if (this.transform != null)
                {
                    this.transform.PropertyChanged -= Transform_PropertyChanged;
                    this.transform.PropertyChanged += Transform_PropertyChanged;
                }
            }
        }

        /// <summary>
        /// 变换属性改变
        /// </summary>
        private void Transform_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            this.UpdateTransform();
            this.OnWrapperPropertyChanged(nameof(this.Transform));
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

        #region Element -- 所属元素

        private Element3D? element;
        /// <summary>
        /// 元素
        /// </summary>
        [Browsable(false), JsonIgnore]
        public Element3D? Element
        {
            get { return element; }
            set { element = value; this.OnPropertyChanged(); }
        }

        #endregion

        /// <summary>
        /// 更新变换
        /// </summary>
        public void UpdateTransform()
        {
            this.OnWrapperPropertyChanged(nameof(this.Transform));
        }
    }
}
