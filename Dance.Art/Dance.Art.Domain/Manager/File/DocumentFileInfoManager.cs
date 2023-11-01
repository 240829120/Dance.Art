using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 文档文件信息管理器
    /// </summary>
    [DanceSingleton(typeof(IDocumentFileInfoManager))]
    public class DocumentFileInfoManager : IDocumentFileInfoManager
    {
        /// <summary>
        /// 文档文件分组信息集合
        /// </summary>
        public List<DocumentFileGroupInfo> DocumentFileGroupInfos { get; private set; } = new();
    }
}
