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
    public class DocumentFileGroupInfo
    {
        /// <summary>
        /// 文档文件分组信息
        /// </summary>
        /// <param name="name">名称</param>
        public DocumentFileGroupInfo(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 文件信息
        /// </summary>
        public List<DocumentFileInfo> FileInfos { get; } = new();
    }
}
