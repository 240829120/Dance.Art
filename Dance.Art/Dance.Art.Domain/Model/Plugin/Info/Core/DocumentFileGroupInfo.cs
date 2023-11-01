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
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        public DocumentFileGroupInfo(string id, string name)
        {
            this.ID = id;
            this.Name = name;
        }

        /// <summary>
        /// 编号
        /// </summary>
        public string ID { get; private set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 文件信息
        /// </summary>
        public List<DocumentFileInfo> FileInfos { get; private set; } = new();
    }
}
