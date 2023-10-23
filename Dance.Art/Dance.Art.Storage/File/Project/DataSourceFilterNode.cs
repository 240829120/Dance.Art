using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dance.Art.Storage
{
    /// <summary>
    /// 数据源过滤器
    /// </summary>
    public class DataSourceFilterNode
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public DataSourceFilterType Type { get; set; }

        /// <summary>
        /// 数据源名称
        /// </summary>
        public string? DataSourceName { get; set; }

        /// <summary>
        /// 参数集合
        /// </summary>
        public Dictionary<string, string>? Parameters { get; set; }
    }
}
