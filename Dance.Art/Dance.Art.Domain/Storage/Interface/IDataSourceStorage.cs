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
    }
}
