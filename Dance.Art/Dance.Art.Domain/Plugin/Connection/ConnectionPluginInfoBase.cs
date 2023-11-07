using System;
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
        /// <param name="sourceModelType">源模型类型</param>
        public ConnectionPluginInfoBase(string id, string name, Type editViewType, Type sourceModelType) : base(id, name)
        {
            this.EditViewType = editViewType;
            this.SourceModelType = sourceModelType;
        }

        /// <summary>
        /// 编辑视图类型
        /// </summary>
        public Type EditViewType { get; private set; }

        /// <summary>
        /// 源模型类型
        /// </summary>
        public Type SourceModelType { get; private set; }

        /// <summary>
        /// 从仓储加载数据
        /// </summary>
        /// <param name="model">连接模型</param>
        public abstract void LoadFromStorage(ConnectionModel model);

        /// <summary>
        /// 保存至仓储
        /// </summary>
        /// <param name="model">连接模型</param>
        /// <returns>仓储ID</returns>
        public abstract int SaveToStorage(ConnectionModel model);

        /// <summary>
        /// 删除连接模型
        /// </summary>
        /// <param name="model">连接模型</param>
        public abstract void Delete(ConnectionModel model);

        /// <summary>
        /// 初始化连接模型
        /// </summary>
        /// <param name="model">连接模型</param>
        public abstract void Initialize(ConnectionModel model);

        /// <summary>
        /// 销毁连接模型
        /// </summary>
        /// <param name="model">连接模型</param>
        public abstract void Destory(ConnectionModel model);
    }
}
