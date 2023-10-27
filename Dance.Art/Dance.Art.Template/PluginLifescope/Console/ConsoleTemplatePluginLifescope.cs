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
    /// 控制台模板插件生命周期
    /// </summary>
    public class ConsoleTemplatePluginLifescope : DanceObject, IDancePluginLifescope
    {
        /// <summary>
        /// 编号
        /// </summary>
        public const string ID = "[Dance.Art.Template]:Console";

        /// <summary>
        /// 名称
        /// </summary>
        public const string NAME = "控制台项目模板";

        /// <summary>
        /// 模板名称
        /// </summary>
        public const string TEMPLATE_NAME = "控制台项目";

        /// <summary>
        /// 注册插件
        /// </summary>
        /// <returns>插件信息</returns>
        public IDancePluginInfo Register()
        {
            return new TemplatePluginInfo(ID, NAME, TEMPLATE_NAME,
                                          Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProjectTemplate", "Console"),
                                          "pack://application:,,,/Dance.Art.Template;component/Themes/Resources/Icons/console.svg");
        }

        /// <summary>
        /// 初始化插件
        /// </summary>
        public void Initialize()
        {

        }
    }
}
