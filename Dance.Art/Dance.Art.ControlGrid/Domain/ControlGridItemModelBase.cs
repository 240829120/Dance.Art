using Dance.Art.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.ControlGrid
{
    /// <summary>
    /// 控制面板项模型
    /// </summary>
    public abstract class ControlGridItemModelBase : ResourceElementModelBase, IControlGridItemModel
    {
        /// <summary>
        /// 控制面板项模型
        /// </summary>
        /// <param name="dataTemplate">模板</param>
        public ControlGridItemModelBase(Type dataTemplate) : base(dataTemplate)
        {

        }

        #region Row -- 行

        private int row;
        /// <summary>
        /// 行
        /// </summary>
        [Category(PropertyCategoryDefines.LAYOUT), Description("行"), DisplayName("行")]
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
        [Category(PropertyCategoryDefines.LAYOUT), Description("列"), DisplayName("列")]
        public int Column
        {
            get { return column; }
            set { column = value; this.OnWrapperPropertyChanged(); }
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
