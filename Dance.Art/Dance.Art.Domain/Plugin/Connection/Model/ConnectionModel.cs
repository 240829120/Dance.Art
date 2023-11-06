﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 连接模型
    /// </summary>
    public class ConnectionModel : DanceModel
    {
        /// <summary>
        /// 连接模型
        /// </summary>
        /// <param name="pluginInfo">插件信息</param>
        /// <param name="group">所属分组</param>
        public ConnectionModel(ConnectionPluginInfoBase pluginInfo, ConnectionGroupModel group)
        {
            this.PluginInfo = pluginInfo;
            this.Group = group;
        }

        // ======================================================================================================
        // Property

        /// <summary>
        /// 插件信息
        /// </summary>
        public ConnectionPluginInfoBase PluginInfo { get; private set; }

        #region ID -- 编号

        private string? id;
        /// <summary>
        /// 编号
        /// </summary>
        public string? ID
        {
            get { return id; }
            set { id = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Name -- 名称

        private string? name;
        /// <summary>
        /// 名称
        /// </summary>
        public string? Name
        {
            get { return name; }
            set { name = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Description -- 描述

        private string? description;
        /// <summary>
        /// 描述
        /// </summary>
        public string? Description
        {
            get { return description; }
            set { description = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Group -- 分组

        private ConnectionGroupModel? group;
        /// <summary>
        /// 分组
        /// </summary>
        public ConnectionGroupModel? Group
        {
            get { return group; }
            set { group = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Status -- 状态

        private ConnectionStatus status;
        /// <summary>
        /// 状态
        /// </summary>
        public ConnectionStatus Status
        {
            get { return status; }
            set { status = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Source -- 源

        private object? source;
        /// <summary>
        /// 源
        /// </summary>
        public object? Source
        {
            get { return source; }
            set { source = value; this.OnPropertyChanged(); }
        }

        #endregion

        // ======================================================================================================
        // Override

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {

        }
    }
}