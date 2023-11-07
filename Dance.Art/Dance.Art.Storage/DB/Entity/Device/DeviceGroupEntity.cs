using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Storage
{
    /// <summary>
    /// 设备分组实体
    /// </summary>
    public class DeviceGroupEntity : EntityBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 设备集合
        /// </summary>
        public List<DeviceEntity>? Items { get; set; }
    }
}
