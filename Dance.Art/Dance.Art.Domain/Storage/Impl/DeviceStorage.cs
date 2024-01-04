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
    [DanceSingleton(typeof(IDeviceStorage))]
    public class DeviceStorage : IDeviceStorage
    {
        /// <summary>
        /// 获取设备分组
        /// </summary>
        /// <param name="projectDomain">项目领域</param>
        public List<DeviceGroupModel>? GetDeviceGroups(ProjectDomain projectDomain)
        {
            var plugins = ArtDomain.Current.GetPluginCollection<DevicePluginInfo>();
            var collection = projectDomain.CacheContext.Database.GetCollection<DeviceGroupEntity>();

            List<DeviceGroupModel> groupModels = [];
            foreach (var group in collection.FindAll())
            {
                DeviceGroupModel groupModel = new()
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

                    DeviceModel model = new(plugin)
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
        /// 保存设备分组
        /// </summary>
        /// <param name="projectDomain">项目领域</param>
        public void SaveDeviceGroups(ProjectDomain projectDomain)
        {
            List<DeviceGroupEntity> groups = [];

            foreach (var groupModel in projectDomain.DeviceGroups)
            {
                DeviceGroupEntity group = new()
                {
                    Name = groupModel.Name,
                    Items = groupModel.Items.Select(p => new DeviceEntity()
                    {
                        PluginID = p.PluginInfo.ID,
                        Name = p.Name,
                        Description = p.Description,
                        SourceID = p.SourceID

                    }).ToList()
                };

                groups.Add(group);
            }

            var collection = projectDomain.CacheContext.Database.GetCollection<DeviceGroupEntity>();
            collection.DeleteAll();
            collection.Insert(groups);
        }
    }
}
