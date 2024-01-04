using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 数据源插件信息
    /// </summary>
    /// <param name="id">编号</param>
    /// <param name="name">名称</param>
    /// <param name="category">类别</param>
    /// <param name="icon">图标</param>
    /// <param name="description">描述</param>
    /// <param name="viewType">文档类型</param>
    /// <param name="sourceType">源类型</param>
    public class DataSourcePluginInfo(string id, string name, string category, string icon, string description, Type viewType, Type sourceType) : DocumentPluginInfo(id, name, viewType)
    {
        /// <summary>
        /// 类别
        /// </summary>
        public string Category { get; private set; } = category;

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; private set; } = icon;

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; private set; } = description;

        /// <summary>
        /// 源类型
        /// </summary>
        public Type SourceType { get; private set; } = sourceType;
    }
}