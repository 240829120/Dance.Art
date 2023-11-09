using Dance.Art.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 数据源仓储
    /// </summary>
    public interface IDataSourceStorage
    {
        /// <summary>
        /// 获取数据源分组
        /// </summary>
        /// <param name="projectDomain">项目领域</param>
        List<DataSourceGroupModel>? GetDataSourceGroups(ProjectDomain projectDomain);

        /// <summary>
        /// 保存数据源分组
        /// </summary>
        /// <param name="projectDomain">项目领域</param>
        void SaveDataSourceGroups(ProjectDomain projectDomain);

        /// <summary>
        /// 转化数据集
        /// </summary>
        /// <param name="entity">数据集实体</param>
        /// <returns>数据集模型</returns>
        DataSetModel? ConvertDataSetEntity(DataSetEntity entity);

        /// <summary>
        /// 转化数据集
        /// </summary>
        /// <param name="model">数据集模型</param>
        /// <returns>数据集实体</returns>
        DataSetEntity ConvertDataSetModel(DataSetModel model);
    }
}
