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
        /// 项目打开消息
        /// </summary>
        /// <param name="oldProject">老项目</param>
        /// <param name="newProject">新项目</param>
        public ProjectOpenMessage(ProjectDomain? oldProject, ProjectDomain newProject)
        {
            this.OldProject = oldProject;
            this.NewProject = newProject;
        }

        /// <summary>
        /// 老项目
        /// </summary>
        public ProjectDomain? OldProject { get; private set; }

        /// <summary>
        /// 新项目
        /// </summary>
        public ProjectDomain NewProject { get; private set; }
    }
}
