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
    /// 空项目插件生命周期
    /// </summary>
    public class EmptyPluginLifescope : DanceObject, IDancePluginLifescope
    {
        /// <summary>
        /// 编号
        /// </summary>
        public const string ID = "[Dance.Art.Template]:Empty";

        /// <summary>
        /// 名称
        /// </summary>
        public const string NAME = "空项目";

        /// <summary>
        /// 注册插件
        /// </summary>
        /// <returns>插件信息</returns>
        public IDancePluginInfo Register()
        {
            return new TemplatePluginInfo(ID, NAME, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProjectTemplate", "Empty")
                                                  , "pack://application:,,,/Dance.Art.Template;component/Themes/Resources/Icons/empty.svg");
        }

        /// <summary>
        /// 初始化插件
        /// </summary>
        public void Initialize()
        {

        }
    }
}
