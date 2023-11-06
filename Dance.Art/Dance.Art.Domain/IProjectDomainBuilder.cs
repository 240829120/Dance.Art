using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 项目领域构建器
    /// </summary>
    public interface IProjectDomainBuilder
    {
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 构建
        /// </summary>
        void Build(ProjectDomain projectDomain);

        /// <summary>
        /// 销毁
        /// </summary>
        /// <param name="projectDomain">项目领域</param>
        void Destroy(ProjectDomain projectDomain);
    }
}
