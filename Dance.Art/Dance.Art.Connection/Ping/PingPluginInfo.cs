using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Connection
{
    /// <summary>
    /// Ping插件信息
    /// </summary>
    public class PingPluginInfo : ConnectionPluginInfoBase
    {
        /// <summary>
        /// 连接插件信息基类
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        /// <param name="editViewType">编辑器视图类型</param>
        public PingPluginInfo(string id, string name, Type editViewType) : base(id, name, editViewType)
        {

        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="model">连接模型</param>
        public override void Initialize(ConnectionModel model)
        {

        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model">连接模型</param>
        public override void Save(ConnectionModel model)
        {

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model">连接模型</param>
        public override void Delete(ConnectionModel model)
        {

        }
    }
}
