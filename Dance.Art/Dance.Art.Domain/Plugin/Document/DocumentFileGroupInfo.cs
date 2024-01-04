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
    /// <param name="name">名称</param>
    public class DocumentFileGroupInfo(string name)
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; } = name;

        /// <summary>
        /// 文件信息
        /// </summary>
        public List<DocumentFileInfo> FileInfos { get; } = [];
    }
}
