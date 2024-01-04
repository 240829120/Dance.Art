using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 脚本运行消息
    /// </summary>
    /// <param name="projectDomain">项目上下文</param>
    /// <param name="scriptDomain">脚本上下文</param>
    /// <param name="isDebug">是否是调试模式</param>
    public class ScriptRunningMessage(ProjectDomain projectDomain, ScriptDomain scriptDomain, bool isDebug)
    {
        /// <summary>
        /// 是为否调试模式
        /// </summary>
        public bool IsDebug { get; private set; } = isDebug;

        /// <summary>
        /// 项目上下文
        /// </summary>
        public ProjectDomain ProjectDomain { get; private set; } = projectDomain;

        /// <summary>
        /// 脚本上下文
        /// </summary>
        public ScriptDomain ScriptDomain { get; private set; } = scriptDomain;
    }
}
