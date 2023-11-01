using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain.Message
{
    /// <summary>
    /// 脚本停止消息
    /// </summary>
    public class ScriptStopMessage
    {
        /// <summary>
        /// 脚本运行信息
        /// </summary>
        /// <param name="projectDomain">项目上下文</param>
        public ScriptStopMessage(ProjectDomain projectDomain)
        {
            this.ProjectDomain = projectDomain;
        }

        /// <summary>
        /// 项目上下文
        /// </summary>
        public ProjectDomain ProjectDomain { get; private set; }
    }
}
