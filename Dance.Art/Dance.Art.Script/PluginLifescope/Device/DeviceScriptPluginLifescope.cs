﻿using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Script
{
    /// <summary>
    /// 设备脚本插件生命周期
    /// </summary>
    public class DeviceScriptPluginLifescope : DanceObject, IDancePluginLifescope
    {
        /// <summary>
        /// 编号
        /// </summary>
        public const string ID = "[Dance.Art.Script]:Device";

        /// <summary>
        /// 名称
        /// </summary>
        public const string NAME = "设备脚本";

        /// <summary>
        /// 命名空间
        /// </summary>
        public const string NAME_SPACE = ScriptNameSpace.DANCE_ART_SCRIPT;

        /// <summary>
        /// 注册插件
        /// </summary>
        /// <returns>插件信息</returns>
        public IDancePluginInfo Register()
        {
            return new ScriptPluginInfo(ID, NAME, new ScriptServiceInfo(NAME_SPACE, "DeviceScriptService", typeof(DeviceScriptService)));
        }

        /// <summary>
        /// 初始化插件
        /// </summary>
        public void Initialize()
        {

        }
    }
}
