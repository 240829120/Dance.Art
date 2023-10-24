using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 项目打开消息
    /// </summary>
    public class ProjectOpenMessage
    {
        /// <summary>
        /// 老项目
        /// </summary>
        public ProjectDomain? OldProject { get; set; }

        /// <summary>
        /// 新项目
        /// </summary>
        public ProjectDomain? NewProject { get; set; }
    }
}
