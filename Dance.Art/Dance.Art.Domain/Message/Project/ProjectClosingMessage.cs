using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 项目关闭前消息
    /// </summary>
    /// <param name="projectDomain">项目</param>
    public class ProjectClosingMessage(ProjectDomain projectDomain)
    {
        /// <summary>
        /// 项目
        /// </summary>
        public ProjectDomain ProjectDomain { get; private set; } = projectDomain;

        /// <summary>
        /// 是否取消
        /// </summary>
        public bool IsCancel { get; set; }
    }
}
