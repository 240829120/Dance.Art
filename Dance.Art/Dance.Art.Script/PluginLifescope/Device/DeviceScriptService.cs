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
    /// 设备脚本服务
    /// </summary>
    public class DeviceScriptService : DanceWrapperModel
    {
        // ============================================================================================
        // Public Function

        /// <summary>
        /// 获取设备源
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>设备源</returns>
        public object? GetDeviceSource(string name)
        {
            if (ArtDomain.Current.ProjectDomain == null)
                return null;

            return ArtDomain.Current.ProjectDomain.GetDeviceModel(name)?.Source;
        }
    }
}
