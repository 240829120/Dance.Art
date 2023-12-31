﻿using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Connection
{
    /// <summary>
    /// UDP连接插件生命周期
    /// </summary>
    public class UdpPluginLifescope : DanceObject, IDancePluginLifescope
    {
        /// <summary>
        /// 编号
        /// </summary>
        public const string ID = "[Dance.Art.Connection]:UDP";

        /// <summary>
        /// 名称
        /// </summary>
        public const string NAME = "UDP";

        /// <summary>
        /// 注册插件
        /// </summary>
        /// <returns>插件信息</returns>
        public IDancePluginInfo Register()
        {
            return new UdpPluginInfo(ID, NAME, typeof(UdpEditView), typeof(UdpSourceModel));
        }

        /// <summary>
        /// 初始化插件
        /// </summary>
        public void Initialize()
        {

        }
    }
}
