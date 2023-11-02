using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Storage
{
    /// <summary>
    /// 命令缓存实体
    /// </summary>
    public class CommandCacheEntity : EntityBase
    {
        /// <summary>
        /// 命令
        /// </summary>
        public string? Command { get; set; }
    }
}
