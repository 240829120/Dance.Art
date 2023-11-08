using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Device
{
    /// <summary>
    /// UDP连接插件生命周期
    /// </summary>
    public class UdpPluginLifescope : DanceObject, IDancePluginLifescope
    {
        /// <summary>
        /// 编号
        /// </summary>
        public const string ID = "[Dance.Art.Device]:UDP";

        /// <summary>
        /// 名称
        /// </summary>
        public const string NAME = "UDP";

        /// <summary>
        /// 注册插件
        /// </summary>
        /// <returns>插件信息</returns>
        public IDancePluginInfo Register()
        {
            return new DevicePluginInfo(ID, NAME, DeviceGroupDefines.COMMON, "pack://application:,,,/Dance.Art.Device;component/Themes/Resources/Icons/udp.svg",
                                        "UDP设备",
                                        typeof(UdpDocumentView), typeof(UdpSourceModel));
        }

        /// <summary>
        /// 初始化插件
        /// </summary>
        public void Initialize()
        {

        }
    }
}
