using Dance.Art.Domain;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Dance.Art.Script
{
    /// <summary>
    /// 数据脚本服务
    /// </summary>
    public class DataSourceScriptService : DanceWrapperModel
    {
        // ============================================================================================
        // Public Function

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>设备源</returns>
        public object? GetDataSource(string name)
        {
            if (ArtDomain.Current.ProjectDomain == null)
                return null;

            return ArtDomain.Current.ProjectDomain.GetDataSourceModel(name)?.Source;
        }
    }
}
