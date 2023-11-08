using Dance.Art.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.DataSource
{
    /// <summary>
    /// 文本数据源实体
    /// </summary>
    public class TextDataSourceEntity : EntityBase
    {
        /// <summary>
        /// 文本
        /// </summary>
        public string? Text { get; set; }
    }
}