using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 资源管理器
    /// </summary>
    [DanceSingleton(typeof(IResourceManager))]
    public class ResourceManager : IResourceManager
    {
        /// <summary>
        /// 缓存
        /// </summary>
        private readonly Dictionary<string, List<ResourceInfoGroupModel>> Cache = new();

        /// <summary>
        /// 根据文件路径获取资源集合
        /// </summary>
        /// <param name="extension">文件后缀</param>
        /// <returns>资源集合</returns>
        public List<ResourceInfoGroupModel>? GetOrCreateResources(string extension)
        {
            if (this.Cache.TryGetValue(extension, out List<ResourceInfoGroupModel>? groups))
                return groups;

            lock (this.Cache)
            {
                ResourceDocumentPluginInfo? pluginInfo = ArtDomain.Current.GetPluginCollection<ResourceDocumentPluginInfo>().FirstOrDefault(p => p.FileInfos.Any(p => string.Equals(p.Extension, extension, StringComparison.OrdinalIgnoreCase)));
                if (pluginInfo == null)
                {
                    groups = new();
                    this.Cache.Add(extension, groups);

                    return groups;
                }

                groups = new();

                var plugins = ArtDomain.Current.GetPluginCollection<ResourcePluginInfo>();
                foreach (var plugin in plugins)
                {
                    if (plugin.Resources == null || plugin.Resources.Count == 0)
                        continue;

                    foreach (var item in plugin.Resources)
                    {
                        if (item.ResourceType.IsAssignableTo(pluginInfo.ResourceType))
                        {
                            TryAddResource(groups, item);
                        }
                    }
                }

                this.Cache.Add(extension, groups);

                return groups;
            }
        }

        /// <summary>
        /// 尝试添加资源
        /// </summary>
        /// <param name="groups">分组</param>
        /// <param name="resouce">资源</param>
        private static void TryAddResource(List<ResourceInfoGroupModel> groups, IResourceSource resouce)
        {
            ResourceInfoGroupModel? group = groups.FirstOrDefault(p => p.Name == resouce.Group);
            if (group == null)
            {
                group = new()
                {
                    IsGroup = true,
                    Icon = "pack://application:,,,/Dance.Wpf;component/Themes/Resources/Icon/transparent.svg",
                    Name = resouce.Group
                };
                groups.Add(group);
            }

            ResourceInfoItemModel item = new()
            {
                Icon = resouce.Icon,
                Name = resouce.Name,
                Source = resouce
            };

            group.Items.Add(item);
        }
    }
}
