using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Panel
{
    /// <summary>
    /// 设备插件生命周期
    /// </summary>
    public class DevicePluginLifescope : DanceObject, IDancePluginLifescope
    {
        /// <summary>
        /// 编号
        /// </summary>
        public const string ID = "[Dance.Art.Panel]:Device";

        /// <summary>
        /// 名称
        /// </summary>
        public const string NAME = "设备";

        /// <summary>
        /// 注册插件
        /// </summary>
        /// <returns>插件信息</returns>
        public IDancePluginInfo Register()
        {
            return new PanelPluginInfo(ID, NAME, typeof(DeviceView));
        }

        /// <summary>
        /// 初始化插件
        /// </summary>
        public void Initialize()
        {

        }
    }
}
