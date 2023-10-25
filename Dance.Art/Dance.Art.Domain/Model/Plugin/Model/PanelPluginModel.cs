﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 面板插件模型
    /// </summary>
    public class PanelPluginModel : ViewPluginModelBase
    {
        /// <summary>
        /// 视图插件模型
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        /// <param name="viewType">视图类型</param>
        public PanelPluginModel(string id, string name, Type viewType) : base(id, name, viewType)
        {

        }
    }
}
