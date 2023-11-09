﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Storage
{
    /// <summary>
    /// 数据集合
    /// </summary>
    public class DataSetEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 单元格集合
        /// </summary>
        public List<DataSetCellEntity>? Cells { get; set; }
    }
}
