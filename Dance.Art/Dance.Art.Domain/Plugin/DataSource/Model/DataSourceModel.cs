using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 数据源模型
    /// </summary>
    public class DataSourceModel : DanceModel
    {
        /// <summary>
        /// 数据源模型
        /// </summary>
        /// <param name="pluginInfo">插件信息</param>
        public DataSourceModel(DataSourcePluginInfo pluginInfo)
        {
            this.PluginInfo = pluginInfo;
            if (pluginInfo.SourceType.Assembly.CreateInstance(pluginInfo.SourceType.FullName ?? string.Empty) is not IDataSource source)
                throw new Exception($"构建Source失败, 类型: {pluginInfo.SourceType.FullName}");

            this.Source = source;
            this.Source.Model = this;
        }

        /// <summary>
        /// 插件信息
        /// </summary>
        public DataSourcePluginInfo PluginInfo { get; private set; }

        /// <summary>
        /// 源
        /// </summary>
        public IDataSource Source { get; private set; }

        #region SourceID -- 源ID

        private int sourceID;
        /// <summary>
        /// 源ID
        /// </summary>
        public int SourceID
        {
            get { return sourceID; }
            set { sourceID = value; this.OnPropertyChanged(); }
        }

        #endregion

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

        #region Group -- 所属分组

        private DataSourceGroupModel? group;
        /// <summary>
        /// 所属分组
        /// </summary>
        public DataSourceGroupModel? Group
        {
            get { return group; }
            set { group = value; this.OnPropertyChanged(); }
        }

        #endregion
    }
}
