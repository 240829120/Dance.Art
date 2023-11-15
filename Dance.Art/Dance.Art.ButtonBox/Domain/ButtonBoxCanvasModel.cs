using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace Dance.Art.ButtonBox
{
    /// <summary>
    /// 按钮组文档画布模型
    /// </summary>
    [DisplayName("按钮面板")]
    public class ButtonBoxCanvasModel : DocumentItemModelBase
    {
        #region Rows -- 行数

        private int rows = 10;
        /// <summary>
        /// 行数
        /// </summary>
        [Category(PropertyCategoryDefines.OTHER), PropertyOrder(0), Description("行数"), DisplayName("行数")]
        public int Rows
        {
            get { return rows; }
            set { rows = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        #region Columns -- 列数

        private int columns = 6;
        /// <summary>
        /// 列数
        /// </summary>
        [Category(PropertyCategoryDefines.OTHER), PropertyOrder(1), Description("列数"), DisplayName("列数")]
        public int Columns
        {
            get { return columns; }
            set { columns = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        #region UnitWidth -- 单位宽度

        private int unitWidth = 120;
        /// <summary>
        /// 单位宽度
        /// </summary>
        [Category(PropertyCategoryDefines.OTHER), PropertyOrder(2), Description("单元格宽度"), DisplayName("单元格宽度")]
        public int UnitWidth
        {
            get { return unitWidth; }
            set { unitWidth = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        #region UnitHeight -- 单位高度

        private int unitHeight = 40;
        /// <summary>
        /// 单位高度
        /// </summary>
        [Category(PropertyCategoryDefines.OTHER), PropertyOrder(3), Description("单元格高度"), DisplayName("单元格高度")]
        public int UnitHeight
        {
            get { return unitHeight; }
            set { unitHeight = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

    }
}
