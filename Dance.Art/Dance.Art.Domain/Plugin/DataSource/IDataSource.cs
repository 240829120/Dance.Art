using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 数据源
    /// </summary>
    public interface IDataSource : IDisposable
    {
        /// <summary>
        /// 关联模型
        /// </summary>
        DataSourceModel? Model { get; set; }

        /// <summary>
        /// 保存至仓储
        /// </summary>
        void SaveToStorage();

        /// <summary>
        /// 从仓储中获取
        /// </summary>
        void LoadFromStorage();

        /// <summary>
        /// 删除
        /// </summary>
        void Delete();
    }
}
