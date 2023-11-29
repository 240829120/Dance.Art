using Dance.Art.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Dance.Art.Scene
{
    /// <summary>
    /// 变换分组
    /// </summary>
    public class SceneTransformGroup3D : SceneStructBase
    {
        public SceneTransformGroup3D()
        {
            this.TranslateTransform = new();
            this.ScaleTransform = new();

            this.TransformGroup = new();
            this.TransformGroup.Children.Add(this.TranslateTransform);
            this.TransformGroup.Children.Add(this.ScaleTransform);
        }

        // ===============================================================================================
        // Property

        // -----------------------------------------------------------------
        // 平移

        #region OffsetX -- X轴偏移量

        private double offsetX;
        /// <summary>
        /// X轴偏移量
        /// </summary>
        public double OffsetX
        {
            get { return offsetX; }
            set
            {
                offsetX = value;
                this.OnWrapperPropertyChanged();

                this.TranslateTransform.OffsetX = value;
            }
        }

        #endregion

        #region OffsetY -- Y轴偏移量

        private double offsetY;
        /// <summary>
        /// Y轴偏移量
        /// </summary>
        public double OffsetY
        {
            get { return offsetY; }
            set
            {
                offsetY = value;
                this.OnWrapperPropertyChanged();

                this.TranslateTransform.OffsetY = value;
            }
        }

        #endregion

        #region OffsetZ -- Z轴偏移量

        private double offsetZ;
        /// <summary>
        /// Z轴偏移量
        /// </summary>
        public double OffsetZ
        {
            get { return offsetZ; }
            set
            {
                offsetZ = value;
                this.OnWrapperPropertyChanged();

                this.TranslateTransform.OffsetZ = value;
            }
        }

        #endregion

        // -----------------------------------------------------------------
        // 缩放

        #region ScaleX -- X轴缩放

        private double scaleX;
        /// <summary>
        /// X轴缩放
        /// </summary>
        public double ScaleX
        {
            get { return scaleX; }
            set
            {
                scaleX = value;
                this.OnWrapperPropertyChanged();

                this.ScaleTransform.ScaleX = value;
            }
        }

        #endregion

        #region ScaleY -- Y轴缩放

        private double scaleY;
        /// <summary>
        /// Y轴缩放
        /// </summary>
        public double ScaleY
        {
            get { return scaleY; }
            set
            {
                scaleY = value;
                this.OnWrapperPropertyChanged();

                this.ScaleTransform.ScaleY = value;
            }
        }

        #endregion

        #region ScaleZ -- Z轴缩放

        private double scaleZ;
        /// <summary>
        /// Z轴缩放
        /// </summary>
        public double ScaleZ
        {
            get { return scaleZ; }
            set
            {
                scaleZ = value;
                this.OnWrapperPropertyChanged();

                this.ScaleTransform.ScaleZ = value;
            }
        }

        #endregion

        #region CenterX -- 中心X坐标

        private double centerX;
        /// <summary>
        /// 中心X坐标
        /// </summary>
        public double CenterX
        {
            get { return centerX; }
            set
            {
                centerX = value;
                this.OnWrapperPropertyChanged();

                this.ScaleTransform.CenterX = value;
            }
        }

        #endregion

        #region CenterY -- 中心Y坐标

        private double centerY;
        /// <summary>
        /// 中心Y坐标
        /// </summary>
        public double CenterY
        {
            get { return centerY; }
            set
            {
                centerY = value;
                this.OnWrapperPropertyChanged();

                this.ScaleTransform.CenterY = value;
            }
        }

        #endregion

        #region CenterZ -- 中心Z坐标

        private double centerZ;
        /// <summary>
        /// 中心Z坐标
        /// </summary>
        public double CenterZ
        {
            get { return centerZ; }
            set
            {
                centerZ = value;
                this.OnWrapperPropertyChanged();

                this.ScaleTransform.CenterZ = value;
            }
        }

        #endregion

        // -----------------------------------------------------------------
        // 旋转

        // ===============================================================================================
        // Control

        /// <summary>
        /// 变换分组
        /// </summary>
        [Browsable(false), JsonIgnore]
        public Transform3DGroup TransformGroup { get; }

        /// <summary>
        /// 平移变换
        /// </summary>
        [Browsable(false), JsonIgnore]
        public TranslateTransform3D TranslateTransform { get; }

        /// <summary>
        /// 缩放变换
        /// </summary>
        [Browsable(false), JsonIgnore]
        public ScaleTransform3D ScaleTransform { get; }
    }
}
