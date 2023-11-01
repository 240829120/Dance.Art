using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 项目关闭消息
    /// </summary>
    public class ProjectClosedMessage
    {
        /// <summary>
        /// 项目关闭消息
        /// </summary>
        /// <param name="projectDomain">项目</param>
        public ProjectClosedMessage(ProjectDomain projectDomain)
        {
            this.ProjectDomain = projectDomain;
        }

        /// <summary>
        /// 项目
        /// </summary>
        public ProjectDomain ProjectDomain { get; private set; }

    }
}
