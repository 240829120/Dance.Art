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
    /// 连接项目构建器
    /// </summary>
    public class ConnectionProjectDomainBuilder : DanceModel, IProjectDomainBuilder
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
            List<ConnectionGroupModel>? groupModels = this.ConnectionStorage.GetConnectionGroups(projectDomain);
            if (groupModels == null)
                return;

            projectDomain.ConnectionGroups.AddRange(groupModels);

            foreach (ConnectionGroupModel groupModel in groupModels)
            {
                foreach (ConnectionModel model in groupModel.Connections)
                {
                    try
                    {
                        if (string.IsNullOrWhiteSpace(model.PluginInfo.SourceModelType.FullName))
                            continue;

                        model.Source = model.PluginInfo.SourceModelType.Assembly.CreateInstance(model.PluginInfo.SourceModelType.FullName);
                        model.PluginInfo.LoadFromStorage(model);
                        model.PluginInfo.Initialize(model);
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);
                    }
                }
            }
        }

        /// <summary>
        /// 销毁
        /// </summary>
        /// <param name="projectDomain">项目领域</param>
        public void Destroy(ProjectDomain projectDomain)
        {
            foreach (ConnectionGroupModel groupModel in projectDomain.ConnectionGroups)
            {
                foreach (ConnectionModel model in groupModel.Connections)
                {
                    try
                    {
                        model.PluginInfo.Destory(model);
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);
                    }
                }
            }

            projectDomain.ConnectionGroups.Clear();
        }
    }
}