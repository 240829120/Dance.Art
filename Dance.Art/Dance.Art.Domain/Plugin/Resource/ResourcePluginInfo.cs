using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 资源插件信息
    /// </summary>
    public class ResourcePluginInfo : PluginInfoBase
    {
        /// <summary>
        /// 插件模型信息基类
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        /// <param name="resources">提供的资源集合</param>
        public ResourcePluginInfo(string id, string name, params IResourceSource[] resources) : base(id, name)
        {
            this.Resources.AddRange(resources);
        }

        /// <summary>
        /// 资源
        /// </summary>
        public List<IResourceSource> Resources { get; } = new();
    }
}
