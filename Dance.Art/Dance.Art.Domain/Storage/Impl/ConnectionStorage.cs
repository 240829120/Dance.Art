using Dance.Art.Storage;
using Dance.Wpf;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 连接仓储
    /// </summary>
    [DanceSingleton(typeof(IConnectionStorage))]
    public class ConnectionStorage : DanceObject, IConnectionStorage
    {
        /// <summary>
        /// 获取连接分组
        /// </summary>
        /// <param name="projectDomain">项目领域</param>
        public List<ConnectionGroupModel>? GetConnectionGroups(ProjectDomain projectDomain)
        {
            List<ConnectionGroupModel> groupModels = new();

            var groups = projectDomain.CacheContext.ConnectionGroups.FindAll();
            foreach (var group in groups)
            {
                ConnectionGroupModel groupModel = new()
                {
                    Name = group.Name
                };

                groupModels.Add(groupModel);

                if (group.Connections == null || group.Connections.Count == 0)
                    continue;

                foreach (var connection in group.Connections)
                {
                    ConnectionPluginInfoBase? pluginInfo = ArtDomain.Current.GetPluginCollection<ConnectionPluginInfoBase>().FirstOrDefault(p => string.Equals(p.ID, connection.PluginID));
                    if (pluginInfo == null)
                    {
                        log.Info($"未找到连接插件: {connection.PluginID}");
                        continue;
                    }

                    ConnectionModel connectionModel = new(pluginInfo, groupModel)
                    {
                        ID = connection.ID,
                        Name = connection.Name,
                        Description = connection.Description
                    };

                    groupModel.Connections.Add(connectionModel);
                }
            }

            return groupModels;
        }

        /// <summary>
        /// 保存连接分组
        /// </summary>
        /// <param name="projectDomain">项目领域</param>
        public void SaveConnectionGroups(ProjectDomain projectDomain)
        {
            List<ConnectionGroupEntity> groups = new();
            foreach (ConnectionGroupModel groupModel in projectDomain.ConnectionGroups)
            {
                ConnectionGroupEntity group = new()
                {
                    Name = groupModel.Name,
                    Connections = new()
                };

                foreach (ConnectionModel itemModel in groupModel.Connections)
                {
                    ConnectionEntity item = new()
                    {
                        PluginID = itemModel.PluginInfo.ID,
                        ID = itemModel.ID,
                        Name = itemModel.Name,
                        Description = itemModel.Description
                    };

                    group.Connections.Add(item);
                }

                groups.Add(group);
            }

            projectDomain.CacheContext.ConnectionGroups.DeleteAll();
            projectDomain.CacheContext.ConnectionGroups.InsertBulk(groups);
        }
    }
}
