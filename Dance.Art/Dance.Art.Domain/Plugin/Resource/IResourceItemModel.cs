using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 资源项模型
    /// </summary>
    public interface IResourceItemModel : IDocumentItemModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        string? ID { get; set; }

        /// <summary>
        /// 模板
        /// </summary>
        DataTemplate DataTemplate { get; }
    }
}
