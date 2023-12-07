using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.ControlGrid
{
    /// <summary>
    /// 控件面板移动项请求
    /// </summary>
    public class ControlGridMoveItemRequest
    {
        /// <summary>
        /// 文档相对路径
        /// </summary>
        public string? path { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string? id { get; set; }

        /// <summary>
        /// 行
        /// </summary>
        public int row { get; set; }

        /// <summary>
        /// 列
        /// </summary>
        public int column { get; set; }
    }
}
