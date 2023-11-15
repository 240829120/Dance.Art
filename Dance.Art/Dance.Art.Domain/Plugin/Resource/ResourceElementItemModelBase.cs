using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 资源元素项模型基类
    /// </summary>
    public abstract class ResourceElementItemModelBase : ResourceItemMoelBase
    {
        /// <summary>
        /// 资源元素项模型基类
        /// </summary>
        /// <param name="dataTemplate">模板</param>
        public ResourceElementItemModelBase(Type dataTemplate) : base(dataTemplate)
        {

        }

        #region BorderColor -- 边框颜色

        private Color borderColor;
        /// <summary>
        /// 边框颜色
        /// </summary>
        [Category(PropertyCategoryDefines.STYLE), Description("边框颜色"), DisplayName("边框颜色")]
        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        #region BorderThickness -- 边框

        private Thickness borderThickness;
        /// <summary>
        /// 边框
        /// </summary>
        [Category(PropertyCategoryDefines.STYLE), Description("边框厚度"), DisplayName("边框厚度")]
        public Thickness BorderThickness
        {
            get { return borderThickness; }
            set { borderThickness = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        #region BackgroundColor -- 背景颜色

        private Color backgroundColor;
        /// <summary>
        /// 背景颜色
        /// </summary>
        [Category(PropertyCategoryDefines.STYLE), Description("背景颜色"), DisplayName("背景颜色")]
        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion
    }
}
