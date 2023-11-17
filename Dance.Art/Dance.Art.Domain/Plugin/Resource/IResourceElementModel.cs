using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 资源元素模型
    /// </summary>
    public interface IResourceElementModel : IResourceItemModel
    {
        /// <summary>
        /// 前景色
        /// </summary>
        Color ForegroundColor { get; set; }

        /// <summary>
        /// 边框颜色
        /// </summary>
        Color BorderColor { get; set; }

        /// <summary>
        /// 边框
        /// </summary>
        Thickness BorderThickness { get; set; }

        /// <summary>
        /// 背景颜色
        /// </summary>
        Color BackgroundColor { get; set; }
    }
}
