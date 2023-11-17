using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 文档项模型
    /// </summary>
    public interface IDocumentItemModel : IDanceJsonObject
    {
        /// <summary>
        /// 所属文档
        /// </summary>
        IDocumentViewModel? OwnerDocument { get; set; }
    }
}
