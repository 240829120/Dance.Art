﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 连接控制器
    /// </summary>
    public interface IConnectionController : IDisposable
    {
        /// <summary>
        /// 连接模型
        /// </summary>
        ConnectionModel? Model { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        void Initialize();
    }
}
