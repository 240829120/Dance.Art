﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 连接插件信息基类
    /// </summary>
    public abstract class ConnectionPluginInfoBase : PluginInfoBase
    {
        /// <summary>
        /// 连接插件信息基类
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        /// <param name="editViewType">编辑器视图类型</param>
        public ConnectionPluginInfoBase(string id, string name, Type editViewType) : base(id, name)
        {
            this.EditViewType = editViewType;
        }

        /// <summary>
        /// 编辑视图类型
        /// </summary>
        public Type EditViewType { get; private set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="model">连接模型</param>
        public abstract void Initialize(ConnectionModel model);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model">连接模型</param>
        public abstract void Save(ConnectionModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model">连接模型</param>
        public abstract void Delete(ConnectionModel model);
    }
}