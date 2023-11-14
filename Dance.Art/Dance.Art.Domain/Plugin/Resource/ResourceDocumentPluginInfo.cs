using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 资源文档插件信息
    /// </summary>
    public class ResourceDocumentPluginInfo : DocumentPluginInfo
    {
        /// <summary>
        /// 文档插件信息
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        /// <param name="viewType">视图类型</param>
        /// <param name="resourceIDs">资源ID集合</param>
        /// <param name="fileInfos">文件信息</param>
        public ResourceDocumentPluginInfo(string id, string name, Type? viewType, IEnumerable<string> resourceIDs, params DocumentFileInfo[] fileInfos) : base(id, name, viewType, fileInfos)
        {
            this.ResourceIDs.AddRange(resourceIDs);
        }

        /// <summary>
        /// 资源ID集合
        /// </summary>
        public List<string> ResourceIDs { get; } = new();
    }
}
