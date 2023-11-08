using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.DataSource
{
    /// <summary>
    /// 文本数据源插件生命周期
    /// </summary>
    public class TextDataSourcePluginLifescope : DanceObject, IDancePluginLifescope
    {
        /// <summary>
        /// 编号
        /// </summary>
        public const string ID = "[Dance.Art.DataSource]:Text";

        /// <summary>
        /// 名称
        /// </summary>
        public const string NAME = "Text";

        /// <summary>
        /// 注册插件
        /// </summary>
        /// <returns>插件信息</returns>
        public IDancePluginInfo Register()
        {
            return new DataSourcePluginInfo(ID, NAME, DataSourceGroupDefines.COMMON, "pack://application:,,,/Dance.Art.Device;component/Themes/Resources/Icons/ping.svg",
                                            "文本",
                                            typeof(TextDataSourceDocumentView), typeof(TextDataSourceModel));
        }

        /// <summary>
        /// 初始化插件
        /// </summary>
        public void Initialize()
        {

        }
    }
}
