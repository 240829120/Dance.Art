using Dance.Art.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 设备仓储
    /// </summary>
    [DanceSingleton(typeof(IDataSourceStorage))]
    public class DataSourceStorage : IDataSourceStorage
    {
        /// <summary>
        /// 获取数据源分组
        /// </summary>
        /// <param name="projectDomain">项目领域</param>
        public List<DataSourceGroupModel>? GetDataSourceGroups(ProjectDomain projectDomain)
        {
            var plugins = ArtDomain.Current.GetPluginCollection<DataSourcePluginInfo>();
            var collection = projectDomain.CacheContext.Database.GetCollection<DataSourceGroupEntity>();

            List<DataSourceGroupModel> groupModels = new();
            foreach (var group in collection.FindAll())
            {
                DataSourceGroupModel groupModel = new()
                {
                    Name = group.Name,
                };

                groupModels.Add(groupModel);

                if (group.Items == null)
                    continue;

                foreach (var item in group.Items)
                {
                    var plugin = plugins.FirstOrDefault(p => p.ID == item.PluginID);
                    if (plugin == null)
                        continue;

                    DataSourceModel model = new(plugin)
                    {
                        SourceID = item.SourceID,
                        Name = item.Name,
                        Description = item.Description,
                        Group = groupModel
                    };
                    model.Source.LoadFromStorage();

                    groupModel.Items.Add(model);
                }
            }

            return groupModels;
        }

        /// <summary>
        /// 保存数据源分组
        /// </summary>
        /// <param name="projectDomain">项目领域</param>
        public void SaveDataSourceGroups(ProjectDomain projectDomain)
        {
            List<DataSourceGroupEntity> groups = new();

            foreach (var groupModel in projectDomain.DataSourceGroups)
            {
                DataSourceGroupEntity group = new()
                {
                    Name = groupModel.Name,
                    Items = groupModel.Items.Select(p => new DataSourceEntity()
                    {
                        PluginID = p.PluginInfo.ID,
                        Name = p.Name,
                        Description = p.Description,
                        SourceID = p.SourceID

                    }).ToList()
                };

                groups.Add(group);
            }

            var collection = projectDomain.CacheContext.Database.GetCollection<DataSourceGroupEntity>();
            collection.DeleteAll();
            collection.Insert(groups);
        }
    }
}
