using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 领域扩展
    /// </summary>
    public static class ArtDomainExpansion
    {
        /// <summary>
        /// 获取面板视图模型
        /// </summary>
        /// <typeparam name="T">视图模型基类</typeparam>
        /// <param name="domain">领域模型</param>
        /// <returns>面板视图模型</returns>
        public static T? GetPanelViewModel<T>(this ArtDomain domain) where T : DanceViewModel
        {
            if (domain == null || domain.Panels == null || domain.Panels.Count == 0)
                return default;

            foreach (PanelPluginModel pluginModel in domain.Panels)
            {
                if (pluginModel == null || pluginModel.View is not FrameworkElement view)
                    continue;

                if (view.DataContext is not T t)
                    continue;

                return t;
            }

            return null;
        }

        /// <summary>
        /// 获取文档视图模型
        /// </summary>
        /// <typeparam name="T">文档视图模型类</typeparam>
        /// <param name="domain">领域模型</param>
        /// <param name="file">文档路径</param>
        /// <returns>文档视图模型</returns>
        public static T? GetDocumentViewModel<T>(this ArtDomain domain, string file) where T : DanceViewModel
        {
            if (domain == null || domain.Documents == null || domain.Documents.Count == 0)
                return default;

            foreach (DocumentPluginModel documentModel in domain.Documents)
            {
                if (documentModel == null || !string.Equals(documentModel.File, file) || documentModel.View is not FrameworkElement view)
                    continue;

                if (view.DataContext is not T t)
                    continue;

                return t;
            }

            return null;
        }

        /// <summary>
        /// 获取插件列表
        /// </summary>
        /// <typeparam name="T">插件类型</typeparam>
        /// <param name="domain">领域模型</param>
        /// <returns>插件列表</returns>
        public static IList<T> GetPluginCollection<T>(this ArtDomain domain) where T : PluginInfoBase
        {
            lock (domain.PluginDic)
            {
                domain.PluginDic.TryGetValue(typeof(T), out IList? collection);
                if (collection == null)
                {
                    collection = new List<T>();
                    domain.PluginDic.Add(typeof(T), collection);
                }

                return (IList<T>)collection;
            }
        }
    }
}
