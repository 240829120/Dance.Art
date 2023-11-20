using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.ControlGrid
{
    /// <summary>
    /// 控制面板仓储
    /// </summary>
    public class ControlGridStorage : DocumentItemModelBase
    {
        /// <summary>
        /// 控制面板
        /// </summary>
        public ControlGridModel? ControlGridModel { get; set; }

        /// <summary>
        /// 项对象
        /// </summary>
        public List<IControlGridItemModel>? Items { get; set; }
    }
}
