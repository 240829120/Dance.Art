using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Template
{
    /// <summary>
    /// 设备控制模板插件生命周期
    /// </summary>
    public class DeviceControlTemplatePluginLifescope : DanceObject, IDancePluginLifescope
    {
        /// <summary>
        /// 编号
        /// </summary>
        public const string ID = "[Dance.Art.Template]:DeviceControl";

        /// <summary>
        /// 名称
        /// </summary>
        public const string NAME = "设备控制项目模板";

        /// <summary>
        /// 模板名称
        /// </summary>
        public const string TEMPLATE_NAME = "设备控制项目";

        /// <summary>
        /// 注册插件
        /// </summary>
        /// <returns>插件信息</returns>
        public IDancePluginInfo Register()
        {
            return new TemplatePluginInfo(ID, NAME, TEMPLATE_NAME,
                                          Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProjectTemplate", "DeviceControl"),
                                          "pack://application:,,,/Dance.Art.Template;component/Themes/Resources/Icons/device_control.svg");
        }

        /// <summary>
        /// 初始化插件
        /// </summary>
        public void Initialize()
        {

        }
    }
}
