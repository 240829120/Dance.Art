using Dance.Art.Domain;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace Dance.Art.Module
{
    /// <summary>
    /// 项目领域扩展
    /// </summary>
    public static class ProjectDomainExpansion
    {
        /// <summary>
        /// 根据路径获取文档视图模型
        /// </summary>
        /// <param name="projectDomain">项目领域</param>
        /// <param name="path">文档路径（相对路径 | 绝对路径）</param>
        /// <returns>文档视图模型</returns>
        public static IDocumentViewModel? GetDocumentViewModel(this ProjectDomain? projectDomain, string path)
        {
            if (projectDomain == null || string.IsNullOrWhiteSpace(projectDomain.ProjectFolderPath))
                return null;

            string fullPath = Path.IsPathRooted(path) ? path : Path.Combine(projectDomain.ProjectFolderPath, path);

            MainViewModel vm = DanceDomain.Current.LifeScope.Resolve<MainViewModel>();
            DocumentPluginModel? pluginModel = vm?.Documents?.FirstOrDefault(p => string.Equals(p.File, fullPath));
            if (pluginModel == null)
                return null;

            if (pluginModel.View is not FrameworkElement view || view.DataContext is not IDocumentViewModel document)
                return null;

            return document;
        }
    }
}
