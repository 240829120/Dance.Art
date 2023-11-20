using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 资源
    /// </summary>
    public interface IResourceSource
    {
        /// <summary>
        /// 资源ID
        /// </summary>
        string ID { get; }

        /// <summary>
        /// 图标
        /// </summary>
        string Icon { get; }

        /// <summary>
        /// 分组
        /// </summary>
        string Group { get; }

        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 描述
        /// </summary>
        string Description { get; }

        /// <summary>
        /// 资源类型
        /// </summary>
        Type ResourceType { get; }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="projectDomain">项目领域</param>
        /// <returns>实例</returns>
        ResourceItemModelBase CreateInstance(ProjectDomain projectDomain);
    }
}
