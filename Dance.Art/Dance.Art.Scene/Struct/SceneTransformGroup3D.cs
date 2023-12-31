﻿using Dance.Art.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace Dance.Art.Scene
{
    /// <summary>
    /// 变换分组
    /// </summary>
    [ExpandableObject]
    public class SceneTransformGroup3D : SceneStructBase
    {
        /// <summary>
        /// 平移
        /// </summary>
        public const string TRANSLATE_TRANSFORM = "平移";

        /// <summary>
        /// 缩放
        /// </summary>
        public const string SCALE_TRANSFORM = "缩放";

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
        [Category(TRANSLATE_TRANSFORM), PropertyOrder(0), Description("X轴平移"), DisplayName("X轴平移")]
        [Editor(typeof(PropertyGridEditorDoubleUpDown), typeof(PropertyGridEditorDoubleUpDown))]
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
        [Category(TRANSLATE_TRANSFORM), PropertyOrder(1), Description("Y轴平移"), DisplayName("Y轴平移")]
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
        [Category(TRANSLATE_TRANSFORM), PropertyOrder(2), Description("Z轴平移"), DisplayName("Z轴平移")]
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

        private double scaleX = 1d;
        /// <summary>
        /// X轴缩放
        /// </summary>
        [Category(SCALE_TRANSFORM), PropertyOrder(0), Description("X轴缩放"), DisplayName("X轴缩放")]
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

        private double scaleY = 1d;
        /// <summary>
        /// Y轴缩放
        /// </summary>
        [Category(SCALE_TRANSFORM), PropertyOrder(1), Description("Y轴缩放"), DisplayName("Y轴缩放")]
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

        private double scaleZ = 1d;
        /// <summary>
        /// Z轴缩放
        /// </summary>
        [Category(SCALE_TRANSFORM), PropertyOrder(2), Description("Z轴缩放"), DisplayName("Z轴缩放")]
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
        [Category(SCALE_TRANSFORM), PropertyOrder(3), Description("中心X坐标"), DisplayName("中心X坐标")]
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
        [Category(SCALE_TRANSFORM), PropertyOrder(4), Description("中心Y坐标"), DisplayName("中心Y坐标")]
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
        [Category(SCALE_TRANSFORM), PropertyOrder(5), Description("中心Z坐标"), DisplayName("中心Z坐标")]
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
