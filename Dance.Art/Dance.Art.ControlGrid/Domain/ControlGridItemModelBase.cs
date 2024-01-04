using Dance.Art.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace Dance.Art.ControlGrid
{
    /// <summary>
    /// 控制面板项模型
    /// </summary>
    /// <param name="dataTemplate">模板</param>
    public abstract class ControlGridItemModelBase(Type dataTemplate) : ResourceElementModelBase(dataTemplate), IControlGridItemModel
    {
        #region Row -- 行

        private int row;
        /// <summary>
        /// 行
        /// </summary>
        [Category(PropertyCategoryDefines.LAYOUT), PropertyOrder(0), Description("行"), DisplayName("行")]
        public int Row
        {
            get { return row; }
            set { row = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        #region Column -- 列

        private int column;
        /// <summary>
        /// 列
        /// </summary>
        [Category(PropertyCategoryDefines.LAYOUT), PropertyOrder(1), Description("列"), DisplayName("列")]
        public int Column
        {
            get { return column; }
            set { column = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        #region HorizontalAlignment -- 水平对齐

        private HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left;
        /// <summary>
        /// 水平对齐
        /// </summary>
        [Category(PropertyCategoryDefines.LAYOUT), PropertyOrder(2), Description("水平对齐"), DisplayName("水平对齐")]
        public HorizontalAlignment HorizontalAlignment
        {
            get { return horizontalAlignment; }
            set { horizontalAlignment = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        #region VerticalAlignment -- 垂直对齐

        private VerticalAlignment verticalAlignment = VerticalAlignment.Center;
        /// <summary>
        /// 垂直对齐
        /// </summary>
        [Category(PropertyCategoryDefines.LAYOUT), PropertyOrder(3), Description("垂直对齐"), DisplayName("垂直对齐")]
        public VerticalAlignment VerticalAlignment
        {
            get { return verticalAlignment; }
            set { verticalAlignment = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        #region IsSelected -- 是否选中

        private bool isSelected;
        /// <summary>
        /// 是否选中
        /// </summary>
        [Browsable(false), JsonIgnore]
        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; this.OnPropertyChanged(); }
        }

        #endregion
    }
}
