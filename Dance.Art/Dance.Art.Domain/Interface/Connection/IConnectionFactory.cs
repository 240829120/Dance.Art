﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 连接工厂
    /// </summary>
    public interface IConnectionFactory
    {
        /// <summary>
        /// 编辑视图类型
        /// </summary>
        Type EdtiViewType { get; }

        /// <summary>
        /// 创建连接控制器
        /// </summary>
        /// <returns>连接控制器</returns>
        IConnectionController CreateController();
    }
}
