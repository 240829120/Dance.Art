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
    public interface IDocumentFileInfoManager
    {
        /// <summary>
        /// 文档文件分组信息集合
        /// </summary>
        List<DocumentFileGroupInfo> DocumentFileGroupInfos { get; }

        /// <summary>
        /// 构建
        /// </summary>
        void Build();
    }
}
