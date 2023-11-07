using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Document
{
    /// <summary>
    /// Excel文件插件生命周期
    /// </summary>
    public class ExcelPluginLifescope : DanceObject, IDancePluginLifescope
    {
        /// <summary>
        /// 编号
        /// </summary>
        public const string ID = "[Dance.Art.Document]:Excel";

        /// <summary>
        /// 名称
        /// </summary>
        public const string NAME = "Excel文件";

        /// <summary>
        /// 注册插件
        /// </summary>
        /// <returns>插件信息</returns>
        public IDancePluginInfo Register()
        {
            return new DocumentPluginInfo(ID, NAME, null, new DocumentFileInfo(DocumentFileGroupDefines.DATA, true, ".xls",
                                                                               "pack://application:,,,/Dance.Art.Document;component/Themes/Resources/Icons/xls.svg",
                                                                               "Microsoft Excel 工作表")
                                                        , new DocumentFileInfo(DocumentFileGroupDefines.DATA, true, ".xlsx",
                                                                               "pack://application:,,,/Dance.Art.Document;component/Themes/Resources/Icons/xlsx.svg",
                                                                               "Microsoft Excel 工作表")
                                                        , new DocumentFileInfo(DocumentFileGroupDefines.DATA, true, ".csv",
                                                                               "pack://application:,,,/Dance.Art.Document;component/Themes/Resources/Icons/csv.svg",
                                                                               "通用数据表格"));
        }

        /// <summary>
        /// 初始化插件
        /// </summary>
        public void Initialize()
        {

        }
    }
}
