using Dance.Art.Domain;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Panel
{
    /// <summary>
    /// 设备项目领域构建器
    /// </summary>
    public class DeviceProjectDomainBuilder : DanceObject, IProjectDomainBuilder
    {
        /// <summary>
        /// 设备仓储
        /// </summary>
        private readonly IDeviceStorage DeviceStorage = DanceDomain.Current.LifeScope.Resolve<IDeviceStorage>();

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; } = "设备";

        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="projectDomain">项目领域</param>
        public void Build(ProjectDomain projectDomain)
        {
            projectDomain.DeviceGroups.AddRange(this.DeviceStorage.GetDeviceGroups(projectDomain));
            projectDomain.DeviceGroups.ForEach(g => g.Items.ForEach(i =>
            {
                try
                {
                    i.Source.Connect();
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }));
        }

        /// <summary>
        /// 销毁
        /// </summary>
        /// <param name="projectDomain">项目领域</param>
        public void Destroy(ProjectDomain projectDomain)
        {
            projectDomain.DeviceGroups.ForEach(g => g.Items.ForEach(i =>
            {
                try
                {
                    i.Source.Disconnect();
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }));
            projectDomain.DeviceGroups.Clear();
        }
    }
}
