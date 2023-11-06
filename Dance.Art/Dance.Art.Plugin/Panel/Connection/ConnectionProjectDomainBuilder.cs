using Dance.Art.Domain;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Plugin
{
    /// <summary>
    /// 连接项目构建器
    /// </summary>
    public class ConnectionProjectDomainBuilder : IProjectDomainBuilder
    {
        /// <summary>
        /// 连接仓储
        /// </summary>
        private readonly IConnectionStorage ConnectionStorage = DanceDomain.Current.LifeScope.Resolve<IConnectionStorage>();

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; } = "连接";

        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="projectDomain">项目领域</param>
        public void Build(ProjectDomain projectDomain)
        {
            projectDomain.ConnectionGroups.ForEach(g => g.Connections.ForEach(i => i.Dispose()));

            List<ConnectionGroupModel>? groupModels = this.ConnectionStorage.GetConnectionGroups(projectDomain);
            if (groupModels == null)
                return;

            projectDomain.ConnectionGroups.AddRange(groupModels);
        }

        /// <summary>
        /// 销毁
        /// </summary>
        /// <param name="projectDomain">项目领域</param>
        public void Destroy(ProjectDomain projectDomain)
        {
            projectDomain.ConnectionGroups.ForEach(g => g.Dispose());
        }
    }
}