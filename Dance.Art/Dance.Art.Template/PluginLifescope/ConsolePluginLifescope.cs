using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Template
{
    /// <summary>
    /// 控制台插件生命周期
    /// </summary>
    public class ConsolePluginLifescope : DanceObject, IDancePluginLifescope
    {
        /// <summary>
        /// 编号
        /// </summary>
        public const string ID = "[Dance.Art.Template]:Console";

        /// <summary>
        /// 名称
        /// </summary>
        public const string NAME = "控制台模板";

        /// <summary>
        /// 注册插件
        /// </summary>
        /// <returns>插件信息</returns>
        public IDancePluginInfo Register()
        {
            return new TemplatePluginInfo(ID, NAME, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProjectTemplate", "Console"));
        }

        /// <summary>
        /// 初始化插件
        /// </summary>
        public void Initialize()
        {

        }
    }
}
