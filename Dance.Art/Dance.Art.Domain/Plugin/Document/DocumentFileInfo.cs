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
    public class DocumentFileInfo
    {
        /// <summary>
        /// 文档文件信息
        /// </summary>
        /// <param name="group">分组</param>
        /// <param name="isPublic">是否公开</param>
        /// <param name="extension">扩展名</param>
        /// <param name="icon">图标</param>
        /// <param name="description">描述</param>
        public DocumentFileInfo(string group, bool isPublic, string extension, string icon, string description)
        {
            this.Group = group;
            this.IsPublic = isPublic;
            this.Extension = extension;
            this.Icon = icon;
            this.Description = description;
        }

        /// <summary>
        /// 分组
        /// </summary>
        public string Group { get; private set; }

        /// <summary>
        /// 是否是公开的
        /// </summary>
        public bool IsPublic { get; private set; }

        /// <summary>
        /// 扩展名
        /// </summary>
        public string Extension { get; private set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; private set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; private set; }
    }
}
