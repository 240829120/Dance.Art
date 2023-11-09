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

        /// <summary>
        /// 转化数据集
        /// </summary>
        /// <param name="entity">数据集实体</param>
        /// <returns>数据集模型</returns>
        public DataSetModel? ConvertDataSetEntity(DataSetEntity entity)
        {
            DataSetModel model = new()
            {
                Name = entity.Name,
            };

            if (entity.Cells != null && entity.Cells.Count > 0)
            {
                foreach (DataSetCellEntity cell in entity.Cells)
                {
                    model.Cells.Add(new DataSetCellModel()
                    {
                        Row = cell.Row,
                        Column = cell.Column,
                        Value = cell.Value
                    });
                }
            }

            return model;
        }

        /// <summary>
        /// 转化数据集
        /// </summary>
        /// <param name="model">数据集模型</param>
        /// <returns>数据集实体</returns>
        public DataSetEntity ConvertDataSetModel(DataSetModel model)
        {
            DataSetEntity entity = new()
            {
                Name = model.Name,
                Cells = new()
            };

            foreach (DataSetCellModel cellModel in model.Cells)
            {
                entity.Cells.Add(new DataSetCellEntity()
                {
                    Row = cellModel.Row,
                    Column = cellModel.Column,
                    Value = cellModel.Value
                });
            }

            return entity;
        }
    }
}
