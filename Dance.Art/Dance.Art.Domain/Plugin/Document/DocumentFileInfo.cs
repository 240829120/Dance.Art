using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 文档文件信息
    /// </summary>
    /// <param name="group">分组</param>
    /// <param name="isPublic">是否公开</param>
    /// <param name="extension">扩展名</param>
    /// <param name="icon">图标</param>
    /// <param name="description">描述</param>
    public class DocumentFileInfo(string group, bool isPublic, string extension, string icon, string description)
    {
        /// <summary>
        /// 分组
        /// </summary>
        public string Group { get; private set; } = group;

        /// <summary>
        /// 是否是公开的
        /// </summary>
        public bool IsPublic { get; private set; } = isPublic;

        /// <summary>
        /// 扩展名
        /// </summary>
        public string Extension { get; private set; } = extension;

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; private set; } = icon;

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; private set; } = description;
    }
}
