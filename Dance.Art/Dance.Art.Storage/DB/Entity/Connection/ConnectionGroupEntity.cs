using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Storage
{
    /// <summary>
    /// 连接分组实体
    /// </summary>
    public class ConnectionGroupEntity : EntityBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 连接集合
        /// </summary>
        public List<ConnectionEntity>? Connections { get; set; }
    }
}
