using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 资源管理器
    /// </summary>
    public interface IResourceManager
    {
        /// <summary>
        /// 根据文件路径获取资源集合
        /// </summary>
        /// <param name="extension">文件后缀</param>
        /// <returns>资源集合</returns>
        List<ResourceInfoGroupModel>? GetOrCreateResources(string extension);
    }
}
