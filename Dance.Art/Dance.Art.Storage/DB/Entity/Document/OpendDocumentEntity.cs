using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Storage
{
    /// <summary>
    /// 打开的文档实体
    /// </summary>
    public class OpendDocumentEntity : EntityBase
    {
        /// <summary>
        /// 文档路径
        /// </summary>
        public string? Path { get; set; }
    }
}
