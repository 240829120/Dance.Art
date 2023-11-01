using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Plugin
{
    /// <summary>
    /// xml文件插件生命周期
    /// </summary>
    public class XmlPluginLifescope : DanceObject, IDancePluginLifescope
    {
        /// <summary>
        /// 编号
        /// </summary>
        public const string ID = "[Dance.Art.Plugin]:Xml";

        /// <summary>
        /// 名称
        /// </summary>
        public const string NAME = "Xml文件";

        /// <summary>
        /// 注册插件
        /// </summary>
        /// <returns>插件信息</returns>
        public IDancePluginInfo Register()
        {
            return new DocumentPluginInfo(ID, NAME, typeof(XmlView), new DocumentFileInfo(DocumentFileGroupInfoDefines.DATA_FILE, ".xml", "pack://application:,,,/Dance.Art.Plugin;component/Themes/Resources/Icons/xml.svg",
                                                                                          "可扩展标记语言"));
        }

        /// <summary>
        /// 初始化插件
        /// </summary>
        public void Initialize()
        {

        }
    }
}
