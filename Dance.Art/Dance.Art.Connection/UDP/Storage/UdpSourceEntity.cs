using Dance.Art.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Connection
{
    /// <summary>
    /// UDP连接
    /// </summary>
    public class UdpSourceEntity : EntityBase
    {
        /// <summary>
        /// 本机主机
        /// </summary>
        public string? LocalHost { get; set; }

        /// <summary>
        /// 本机端口
        /// </summary>
        public int LocalPort { get; set; }

        /// <summary>
        /// 远端主机
        /// </summary>
        public string? RemoteHost { get; set; }

        /// <summary>
        /// 远端端口
        /// </summary>
        public int RemotePort { get; set; }
    }
}
