using Dance.Art.Domain;
using Dance.Wpf;
using HelixToolkit.SharpDX.Core;
using HelixToolkit.Wpf.SharpDX;
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
    /// 场景模型
    /// </summary>
    [DisplayName("场景")]
    public class SceneModel : DocumentItemModelBase
    {
        public SceneModel()
        {
            this.camera = new HelixToolkit.Wpf.SharpDX.PerspectiveCamera
            {
                Position = new Point3D(3, 3, 5),
                LookDirection = new Vector3D(-3, -3, -5),
                UpDirection = new Vector3D(0, 1, 0),
                FarPlaneDistance = 50000
            };
        }

        // ===============================================================================================
        // Property

        #region AmbientLight -- 环境光

        private Color ambientLight = Colors.White;
        /// <summary>
        /// 环境光
        /// </summary>
        [Category(PropertyCategoryDefines.OTHER), PropertyOrder(0), Description("环境光"), DisplayName("环境光")]
        public Color AmbientLight
        {
            get { return ambientLight; }
            set { ambientLight = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        #region UpDirection -- 向上方向

        private Vector3D upDirection = new(0, 1, 0);
        /// <summary>
        /// 向上方向
        /// </summary>
        [Category(PropertyCategoryDefines.OTHER), PropertyOrder(1), Description("向上方向"), DisplayName("向上方向")]
        public Vector3D UpDirection
        {
            get { return upDirection; }
            set { upDirection = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        #region FXAALevel -- FXAA等级

        private FXAALevel _FXAALevel = FXAALevel.Low;
        /// <summary>
        /// FXAA等级
        /// </summary>
        [Category(PropertyCategoryDefines.OTHER), PropertyOrder(2), Description("FXAA等级"), DisplayName("FXAA等级")]
        public FXAALevel FXAALevel
        {
            get { return _FXAALevel; }
            set { _FXAALevel = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        // ===============================================================================================
        // Controller

        #region EffectsManager -- 效果管理器

        private IEffectsManager effectsManager = new DefaultEffectsManager();

        /// <summary>
        /// 效果管理器
        /// </summary>
        [Browsable(false), JsonIgnore]
        public IEffectsManager EffectsManager
        {
            get { return effectsManager; }
            set { effectsManager = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Camera -- 摄像机

        private HelixToolkit.Wpf.SharpDX.Camera camera;

        /// <summary>
        /// 摄像机
        /// </summary>
        [Browsable(false), JsonIgnore]
        public HelixToolkit.Wpf.SharpDX.Camera Camera
        {
            get { return camera; }
            set { camera = value; this.OnPropertyChanged(); }
        }

        #endregion
    }
}
