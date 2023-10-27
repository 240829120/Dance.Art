using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 脚本状态
    /// </summary>
    public enum ScriptStatus
    {
        /// <summary>
        /// 空
        /// </summary>
        None,

        /// <summary>
        /// 正在运行
        /// </summary>
        Running,

        /// <summary>
        /// 调试
        /// </summary>
        Debugging
    }
}
