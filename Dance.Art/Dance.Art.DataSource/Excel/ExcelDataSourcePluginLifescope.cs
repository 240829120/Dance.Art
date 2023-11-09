using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.DataSource
{
    /// <summary>
    /// Excel数据源插件生命周期
    /// </summary>
    public class ExcelDataSourcePluginLifescope : DanceObject, IDancePluginLifescope
    {
        /// <summary>
        /// 编号
        /// </summary>
        public const string ID = "[Dance.Art.DataSource]:Excel";

        /// <summary>
        /// 名称
        /// </summary>
        public const string NAME = "Excel";

        /// <summary>
        /// 注册插件
        /// </summary>
        /// <returns>插件信息</returns>
        public IDancePluginInfo Register()
        {
            return new DataSourcePluginInfo(ID, NAME, DataSourceGroupDefines.FILE, "pack://application:,,,/Dance.Art.DataSource;component/Themes/Resources/Icons/xls.svg",
                                            "Excel文件数据",
                                            typeof(ExcelDataSourceDocumentView), typeof(ExcelDataSourceModel));
        }

        /// <summary>
        /// 初始化插件
        /// </summary>
        public void Initialize()
        {

        }
    }
}
