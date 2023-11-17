using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.ControlGrid
{
    /// <summary>
    /// 控制面板元素
    /// </summary>
    public interface IControlGridItemModel : IResourceElementModel
    {
        /// <summary>
        /// 行
        /// </summary>
        int Row { get; set; }

        /// <summary>
        /// 列
        /// </summary>
        int Column { get; set; }

        /// <summary>
        /// 是否被选中
        /// </summary>
        bool IsSelected { get; set; }
    }
}
