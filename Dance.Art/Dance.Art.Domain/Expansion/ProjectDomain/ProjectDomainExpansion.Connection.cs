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
    /// 项目领域扩展
    /// </summary>
    public static partial class ProjectDomainExpansion
    {
        /// <summary>
        /// 加载连接分组
        /// </summary>
        /// <param name="projectDomain">项目领域</param>
        public static void LoadConnectionGroups(this ProjectDomain projectDomain)
        {
            if (DanceDomain.Current is not ArtDomain artDomain)
                return;

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
                    ConnectionPluginInfo? pluginInfo = artDomain.ConnectionPlugins.FirstOrDefault(p => string.Equals(p.ID, connection.PluginID));
                    if (pluginInfo == null)
                    {
                        log.Info($"未找到连接插件: {connection.PluginID}");
                        continue;
                    }

                    ConnectionModel connectionModel = new(pluginInfo)
                    {
                        ID = connection.ID,
                        Name = connection.Name,
                        Description = connection.Description
                    };

                    groupModel.Connections.Add(connectionModel);

                    if (connection.Parameters == null || connection.Parameters.Count == 0)
                        continue;

                    foreach (var kv in connection.Parameters)
                    {
                        connectionModel.Parameters.Add(kv.Key, kv.Value);
                    }
                }
            }

            projectDomain.ConnectionGroups.ForEach(g => g.Connections.ForEach(i => i.Dispose()));
            projectDomain.ConnectionGroups.AddRange(groupModels);
        }

        /// <summary>
        /// 保存连接分组
        /// </summary>
        /// <param name="projectDomain">项目领域</param>
        public static void SaveConnectionGroups(this ProjectDomain projectDomain)
        {
            if (DanceDomain.Current is not ArtDomain artDomain)
                return;

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
                        Description = itemModel.Description,
                        Parameters = itemModel.Parameters.ToDictionary(p => p.Key, p => p.Value)
                    };

                    group.Connections.Add(item);
                }

                groups.Add(group);
            }

            projectDomain.CacheContext.ConnectionGroups.DeleteAll();
            projectDomain.CacheContext.ConnectionGroups.InsertBulk(groups);
        }

        /// <summary>
        /// 销毁连接分组
        /// </summary>
        /// <param name="projectDomain">项目领域</param>
        public static void DisposeConnectionGroups(this ProjectDomain projectDomain)
        {
            projectDomain.ConnectionGroups.ForEach(g => g.Connections.ForEach(i => i.Dispose()));
        }
    }
}
