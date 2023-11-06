using Dance.Art.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Connection
{
    /// <summary>
    /// Ping实体
    /// </summary>
    public class PingSourceEntity : EntityBase
    {
        /// <summary>
        /// 主机
        /// </summary>
        public string? Host { get; set; }

        /// <summary>
        /// 频率 （单位：毫秒）
        /// </summary>
        public int Frequency { get; set; }
    }
}