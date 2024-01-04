using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 文档插件信息
    /// </summary>
    /// <param name="id">编号</param>
    /// <param name="name">名称</param>
    /// <param name="viewType">视图类型</param>
    /// <param name="fileInfos">文件信息</param>
    public class DocumentPluginInfo(string id, string name, Type? viewType, params DocumentFileInfo[] fileInfos) : ViewPluginInfoBase(id, name, viewType)
    {
        /// <summary>
        /// 能处理文件的扩展名
        /// </summary>
        public DocumentFileInfo[] FileInfos { get; private set; } = fileInfos ?? [];
    }
}
