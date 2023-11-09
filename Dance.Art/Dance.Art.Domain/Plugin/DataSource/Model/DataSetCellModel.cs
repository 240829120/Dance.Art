using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 数据集单元格模型
    /// </summary>
    public class DataSetCellModel : DanceWrapperModel
    {
        #region Row -- 行

        private int row;
        /// <summary>
        /// 行
        /// </summary>
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
        public int Column
        {
            get { return column; }
            set { column = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        #region Value -- 值

        private string? _value;
        /// <summary>
        /// 值
        /// </summary>
        public string? Value
        {
            get { return _value; }
            set { _value = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion
    }
}
