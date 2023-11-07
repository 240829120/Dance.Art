using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Device
{
    /// <summary>
    /// Ping连接插件生命周期
    /// </summary>
    public class PingPluginLifescope : DanceObject, IDancePluginLifescope
    {
        /// <summary>
        /// 编号
        /// </summary>
        public const string ID = "[Dance.Art.Device]:Ping";

        /// <summary>
        /// 名称
        /// </summary>
        public const string NAME = "Ping";

        /// <summary>
        /// 注册插件
        /// </summary>
        /// <returns>插件信息</returns>
        public IDancePluginInfo Register()
        {
            return new DevicePluginInfo(ID, NAME, "测试", "pack://application:,,,/Dance.Art.Document;component/Themes/Resources/Icons/json.svg",
                                        "Ping设备, 该设备将持续Ping指定地址",
                                        typeof(PingDocumentView), typeof(PingItemView), typeof(PingSourceModel));
        }

        /// <summary>
        /// 初始化插件
        /// </summary>
        public void Initialize()
        {

        }
    }
}
