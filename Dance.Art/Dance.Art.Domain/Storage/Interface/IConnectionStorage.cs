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
    public interface IConnectionStorage
    {
        /// <summary>
        /// 获取连接分组
        /// </summary>
        /// <param name="projectDomain">项目领域</param>
        List<ConnectionGroupModel>? GetConnectionGroups(ProjectDomain projectDomain);

        /// <summary>
        /// 保存连接分组
        /// </summary>
        /// <param name="projectDomain">项目领域</param>
        void SaveConnectionGroups(ProjectDomain projectDomain);
    }
}
