﻿using Dance.Art.Domain;
using Dance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Scene
{
    /// <summary>
    /// 场景资源插件生命周期
    /// </summary>
    public class SceneResourcePluginLifescope : DanceObject, IDancePluginLifescope
    {
        /// <summary>
        /// 编号
        /// </summary>
        public const string ID = "[Dance.Art.Scene]:Resource";

        /// <summary>
        /// 名称
        /// </summary>
        public const string NAME = "场景资源";

        /// <summary>
        /// 注册插件
        /// </summary>
        /// <returns>插件信息</returns>
        public IDancePluginInfo Register()
        {
            List<IResourceSource> resources =
            [
                new DirectionalLightSource(),
                new PointLightSource(),
                new BoxSource(),
                new ModelSource(),
            ];

            return new ResourcePluginInfo(ID, NAME, [.. resources]);
        }

        /// <summary>
        /// 初始化插件
        /// </summary>
        public void Initialize()
        {

        }
    }
}

