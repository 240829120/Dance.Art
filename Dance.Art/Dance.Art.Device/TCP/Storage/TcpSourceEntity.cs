using Dance.Art.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Device
{
    /// <summary>
    /// TCP实体
    /// </summary>
    public class TcpSourceEntity : EntityBase
    {
        /// <summary>
        /// 监听主机
        /// </summary>
        public string? LocalHost { get; set; }

        /// <summary>
        /// 远程主机
        /// </summary>
        public string? RemoteHost { get; set; }

        /// <summary>
        /// 远程端口
        /// </summary>
        public int RemotePort { get; set; }
    }
}