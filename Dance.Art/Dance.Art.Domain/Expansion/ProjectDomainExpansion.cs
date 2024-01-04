using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 项目领域扩展
    /// </summary>
    public static partial class ProjectDomainExpansion
    {
        /// <summary>
        /// 日志
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(typeof(ProjectDomainExpansion));

        /// <summary>
        /// 获取设备模型
        /// </summary>
        /// <param name="projectDomain">项目领域</param>
        /// <param name="name">名称</param>
        /// <returns>设备模型</returns>
        public static DeviceModel? GetDeviceModel(this ProjectDomain? projectDomain, string name)
        {
            if (projectDomain == null || string.IsNullOrWhiteSpace(name))
                return null;

            try
            {
                foreach (DeviceGroupModel group in projectDomain.DeviceGroups)
                {
                    foreach (DeviceModel item in group.Items)
                    {
                        if (string.Equals(item.Name, name))
                            return item;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return null;
            }
        }

        /// <summary>
        /// 获取设备模型
        /// </summary>
        /// <param name="projectDomain">项目领域</param>
        /// <param name="names">名称集合</param>
        /// <returns>设备模型</returns>
        public static List<DeviceModel> GetDeviceModel(this ProjectDomain? projectDomain, List<string> names)
        {
            List<DeviceModel> list = [];

            if (projectDomain == null || names == null || names.Count == 0)
                return list;

            try
            {
                foreach (DeviceGroupModel group in projectDomain.DeviceGroups)
                {
                    foreach (DeviceModel item in group.Items)
                    {
                        if (string.IsNullOrWhiteSpace(item.Name))
                            continue;

                        if (names.Contains(item.Name))
                        {
                            list.Add(item);
                        }
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return [];
            }
        }

        /// <summary>
        /// 获取数据模型
        /// </summary>
        /// <param name="projectDomain">项目领域</param>
        /// <param name="name">名称</param>
        /// <returns>数据模型</returns>
        public static DataSourceModel? GetDataSourceModel(this ProjectDomain? projectDomain, string name)
        {
            if (projectDomain == null || string.IsNullOrWhiteSpace(name))
                return null;

            try
            {
                foreach (DataSourceGroupModel group in projectDomain.DataSourceGroups)
                {
                    foreach (DataSourceModel item in group.Items)
                    {
                        if (string.Equals(item.Name, name))
                            return item;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return null;
            }
        }
    }
}
