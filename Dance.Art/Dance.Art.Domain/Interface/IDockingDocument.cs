using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// Docking文档
    /// </summary>
    public interface IDockingDocument : IDockingPanel
    {
        /// <summary>
        /// 文档模型
        /// </summary>
        DocumentPluginModel? DocumentModel { get; set; }
    }
}
