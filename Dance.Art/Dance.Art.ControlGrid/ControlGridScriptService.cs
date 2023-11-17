using Dance.Art.Domain;
using Dance.Art.Module;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.ControlGrid
{
    /// <summary>
    /// 控制面板脚本服务
    /// </summary>
    public class ControlGridScriptService : DanceWrapperModel
    {
        // ============================================================================================
        // Public Function

        /// <summary>
        /// 获取控制面板控件
        /// </summary>
        /// <param name="path">控制面板文件路径</param>
        /// <param name="id">编号</param>
        /// <returns>控制面板控件</returns>
        public object? GetControlGridItem(string path, string id)
        {
            object? result = null;

            Application.Current.Dispatcher.Invoke(() =>
            {
                if (ArtDomain.Current.ProjectDomain == null || string.IsNullOrWhiteSpace(ArtDomain.Current.ProjectDomain.ProjectFolderPath))
                    return;

                if (string.IsNullOrWhiteSpace(path))
                    return;

                if (ArtDomain.Current.ProjectDomain.GetDocumentViewModel(path) is not ControlGridDocumentViewModel vm)
                    return;

                result = vm.Items?.FirstOrDefault(p => string.Equals(p.ID, id));
            });

            return result;
        }
    }
}