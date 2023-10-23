using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 视图插件模型
    /// </summary>
    public class PluginViewModel : PluginModelBase
    {
        /// <summary>
        /// 视图插件模型
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        /// <param name="viewType">视图类型</param>
        /// <param name="category">分类</param>
        public PluginViewModel(string id, string name, Type viewType, PluginViewCategory category) : base(id, name)
        {
            this.ViewType = viewType;
            this.Category = category;
        }

        /// <summary>
        /// 视图类型
        /// </summary>
        public Type ViewType { get; private set; }

        /// <summary>
        /// 分类
        /// </summary>
        public PluginViewCategory Category { get; private set; }
    }
}
