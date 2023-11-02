﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 连接状态
    /// </summary>
    public enum ConnectionStatus
    {
        /// <summary>
        /// 断开
        /// </summary>
        Disconnected = 0,

        /// <summary>
        /// 正在断开
        /// </summary>
        Disconnecting = 1,

        /// <summary>
        /// 正在连接
        /// </summary>
        Connecting = 2,

        /// <summary>
        /// 已经连接
        /// </summary>
        Connected = 3
    }

}
