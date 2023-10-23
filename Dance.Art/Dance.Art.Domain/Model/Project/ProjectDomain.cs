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


    }
}
