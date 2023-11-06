using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Script
{
    /// <summary>
    /// 连接脚本服务
    /// </summary>
    public class ConnectionScriptService : DanceObject
    {
        // ============================================================================================
        // Public Function

        /// <summary>
        /// 根据ID获取连接模型
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns>连接模型</returns>
        public ConnectionModel? GetConnectionByID(string id)
        {
            if (ArtDomain.Current.ProjectDomain == null)
                return null;

            foreach (ConnectionGroupModel groupModel in ArtDomain.Current.ProjectDomain.ConnectionGroups)
            {
                ConnectionModel? connectionModel = groupModel.Connections.FirstOrDefault(p => string.Equals(p.ID, id));
                if (connectionModel != null)
                    return connectionModel;
            }

            return null;
        }
    }
}
