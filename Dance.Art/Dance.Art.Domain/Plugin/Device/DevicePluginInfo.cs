using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 设备插件信息
    /// </summary>
    public class DevicePluginInfo : PluginInfoBase
    {
        /// <summary>
        /// 设备插件
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        /// <param name="category">类别</param>
        /// <param name="icon">图标</param>
        /// <param name="description">描述</param>
        /// <param name="documentType">文档类型</param>
        /// <param name="itemType">项类型</param>
        /// <param name="sourceType">源类型</param>
        public DevicePluginInfo(string id, string name, string category, string icon, string description, Type documentType, Type itemType, Type sourceType) : base(id, name)
        {
            this.Category = category;
            this.Icon = icon;
            this.Description = description;
            this.DocumentType = documentType;
            this.ItemType = itemType;
            this.SourceType = sourceType;
        }

        /// <summary>
        /// 类别
        /// </summary>
        public string Category { get; private set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; private set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 文档类型
        /// </summary>
        public Type DocumentType { get; private set; }

        /// <summary>
        /// 项类型
        /// </summary>
        public Type ItemType { get; private set; }

        /// <summary>
        /// 源类型
        /// </summary>
        public Type SourceType { get; private set; }
    }
}