using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Storage
{
    /// <summary>
    /// 连接实体
    /// </summary>
    public class ConnectionEntity
    {
        /// <summary>
        /// 插件ID
        /// </summary>
        public string? PluginID { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string? ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public Dictionary<string, string>? Parameters { get; set; }
    }
}
