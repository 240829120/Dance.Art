using Dance.Art.Domain;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Panel
{
    /// <summary>
    /// 数据项目领域构建器
    /// </summary>
    public class DataSourceProjectDomainBuilder : DanceObject, IProjectDomainBuilder
    {
        /// <summary>
        /// 数据仓储
        /// </summary>
        private readonly IDataSourceStorage DataSourceStorage = DanceDomain.Current.LifeScope.Resolve<IDataSourceStorage>();

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; } = "数据";

        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="projectDomain">项目领域</param>
        public void Build(ProjectDomain projectDomain)
        {
            projectDomain.DataSourceGroups.AddRange(this.DataSourceStorage.GetDataSourceGroups(projectDomain));
            projectDomain.DataSourceGroups.ForEach(g => g.Items.ForEach(i =>
            {
                try
                {
                    i.Source.Load();
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }));
        }

        /// <summary>
        /// 销毁
        /// </summary>
        /// <param name="projectDomain">项目领域</param>
        public void Destroy(ProjectDomain projectDomain)
        {
            projectDomain.DataSourceGroups.ForEach(g => g.Items.ForEach(i =>
            {
                try
                {
                    i.Source.Dispose();
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }));
            projectDomain.DataSourceGroups.Clear();
        }
    }
}
