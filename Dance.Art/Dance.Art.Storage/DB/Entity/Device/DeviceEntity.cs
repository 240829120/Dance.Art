using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Storage
{
    /// <summary>
    /// 设备实体
    /// </summary>
    public class DeviceEntity
    {
        /// <summary>
        /// 插件ID
        /// </summary>
        public string? PluginID { get; set; }

        /// <summary>
        /// 源ID
        /// </summary>
        public int SourceID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }
    }
}
