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
    public interface IDeviceStorage
    {
        /// <summary>
        /// 获取设备分组
        /// </summary>
        /// <param name="projectDomain">项目领域</param>
        List<DeviceGroupModel>? GetDeviceGroups(ProjectDomain projectDomain);

        /// <summary>
        /// 保存设备分组
        /// </summary>
        /// <param name="projectDomain">项目领域</param>
        void SaveDeviceGroups(ProjectDomain projectDomain);
    }
}
