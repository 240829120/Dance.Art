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
        /// <param name="extension">扩展名</param>
        /// <param name="icon">图标</param>
        public DocumentFileInfo(string extension, string icon)
        {
            this.Extension = extension;
            this.Icon = icon;
        }

        /// <summary>
        /// 扩展名
        /// </summary>
        public string Extension { get; private set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; private set; }
    }
}
