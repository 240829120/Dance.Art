using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 服务返回
    /// </summary>
    public class ServerResponse
    {
        /// <summary>
        /// 服务返回
        /// </summary>
        /// <param name="msg">消息</param>
        public ServerResponse(string? msg)
        {
            this.code = ServerResponseCode.SUCCESS;
            this.msg = msg;
        }

        /// <summary>
        /// 服务返回
        /// </summary>
        /// <param name="code">返回码<see cref="ServerResponseCode"/></param>
        /// <param name="msg">消息</param>
        public ServerResponse(string code, string? msg)
        {
            this.code = code;
            this.msg = msg;
        }

        /// <summary>
        /// 返回码
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string? msg { get; set; }
    }
}
