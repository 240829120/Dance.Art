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
    public class ProjectCloseMessage
    {
        /// <summary>
        /// 项目关闭消息
        /// </summary>
        /// <param name="projectDomain">项目领域</param>
        public ProjectCloseMessage(ProjectDomain projectDomain)
        {
            this.ProjectDomain = projectDomain;
        }

        /// <summary>
        /// 项目领域
        /// </summary>
        public ProjectDomain ProjectDomain { get; private set; }
    }
}
