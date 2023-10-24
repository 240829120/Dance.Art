using Dance.Art.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 项目领域
    /// </summary>
    public class ProjectDomain : DanceModel
    {
        /// <summary>
        /// 项目领域
        /// </summary>
        /// <param name="path">项目文件路径</param>
        public ProjectDomain(string path)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(path, nameof(path));

            this.projectFilePath = path;
            this.projectFolderPath = Path.GetDirectoryName(path);

            ArgumentNullException.ThrowIfNullOrEmpty(projectFolderPath, nameof(ProjectFolderPath));

            string cache = Path.Combine(this.projectFolderPath, $"{Path.GetFileNameWithoutExtension(path)}.{FileSuffixCategory.PROJECT_CACHE}");
            this.cacheContext = new ProjectCacheContext(cache);
        }

        // ===========================================================================
        // Project Property -- 项目属性

        #region Name -- 名称

        private string? name;
        /// <summary>
        /// 名称
        /// </summary>
        public string? Name
        {
            get { return name; }
            set { name = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Description -- 描述

        private string? description;
        /// <summary>
        /// 描述
        /// </summary>
        public string? Description
        {
            get { return description; }
            set { description = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Type -- 类型

        private ProjectType type;
        /// <summary>
        /// 类型
        /// </summary>
        public ProjectType Type
        {
            get { return type; }
            set { type = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region CollectionGroups -- 连接集合

        private ObservableCollection<CollectionGroup> collectionGroups = new();
        /// <summary>
        /// 连接分组
        /// </summary>
        public ObservableCollection<CollectionGroup> CollectionGroups
        {
            get { return collectionGroups; }
            private set { collectionGroups = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region DataSources -- 数据源

        private ObservableCollection<DataSource> dataSources = new();
        /// <summary>
        /// 数据源
        /// </summary>
        public ObservableCollection<DataSource> DataSources
        {
            get { return dataSources; }
            private set { dataSources = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region DataSourceFilters -- 数据源过滤器集合

        private ObservableCollection<DataSourceFilter> dataSourceFilters = new();
        /// <summary>
        /// 数据源过滤器集合
        /// </summary>
        public ObservableCollection<DataSourceFilter> DataSourceFilters
        {
            get { return dataSourceFilters; }
            private set { dataSourceFilters = value; this.OnPropertyChanged(); }
        }

        #endregion

        // ===========================================================================
        // Expand Property -- 扩展属性

        #region ProjectFilePath -- 项目文件路径

        private string? projectFilePath;
        /// <summary>
        /// 项目文件路径
        /// </summary>
        public string? ProjectFilePath
        {
            get { return projectFilePath; }
            set { projectFilePath = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region ProjectFolderPath -- 项目文件夹路径

        private string? projectFolderPath;
        /// <summary>
        /// 项目文件夹路径
        /// </summary>
        public string? ProjectFolderPath
        {
            get { return projectFolderPath; }
            set { projectFolderPath = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region CacheContext -- 缓存上下文

        private ProjectCacheContext cacheContext;
        /// <summary>
        /// 缓存上下文
        /// </summary>
        public ProjectCacheContext CacheContext
        {
            get { return cacheContext; }
            set { cacheContext = value; }
        }


        #endregion
    }
}
