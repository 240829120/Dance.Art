using Dance.Art.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.DataSource
{
    /// <summary>
    /// Excel数据源实体
    /// </summary>
    public class ExcelDataSourceEntity : EntityBase
    {
        /// <summary>
        /// 文件路径（相对路径 | 绝对路径）
        /// </summary>
        public string? Path { get; set; }
    }
}