using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 项目领域管理器
    /// </summary>
    public interface IProjectDomainManager
    {
        /// <summary>
        /// 加载插件
        /// </summary>
        /// <param name="assemblyPrefix">程序集前缀</param>
        void LoadPlugin(string assemblyPrefix);
    }
}
