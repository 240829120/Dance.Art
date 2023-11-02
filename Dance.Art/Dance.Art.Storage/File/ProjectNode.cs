using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dance.Art.Storage
{
    /// <summary>
    /// 项目节点
    /// </summary>
    public class ProjectNode
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
        /// 项目模板
        /// </summary>
        public string? ProjectTemplate { get; set; }
    }
}
