using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.ControlGrid
{
    /// <summary>
    /// 控件面板添加项请求
    /// </summary>
    public class ControlGridDeleteItemRequest
    {
        /// <summary>
        /// 文档相对路径
        /// </summary>
        public string? path { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string? id { get; set; }
    }
}
