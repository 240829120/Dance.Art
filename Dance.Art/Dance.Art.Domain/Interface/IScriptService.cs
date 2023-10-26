using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 脚本服务
    /// </summary>
    public interface IScriptService
    {
        /// <summary>
        /// 命名空间
        /// </summary>
        string ServiceNamespace { get; }

        /// <summary>
        /// 服务名
        /// </summary>
        string ServiceName { get; }
    }
}
