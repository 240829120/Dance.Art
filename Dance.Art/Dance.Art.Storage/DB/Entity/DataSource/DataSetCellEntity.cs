using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Storage
{
    /// <summary>
    /// 数据集单元格
    /// </summary>
    public class DataSetCellEntity
    {
        /// <summary>
        /// 行
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// 列
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string? Value { get; set; }
    }
}
