using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 服务管理器
    /// </summary>
    public interface IServerManager : IDisposable
    {
        /// <summary>
        /// 地址
        /// </summary>
        string URL { get; set; }

        /// <summary>
        /// 开启服务
        /// </summary>
        /// <param name="assemblyPrefixes">程序集前缀集合</param>
        void Start(List<string> assemblyPrefixes);

        /// <summary>
        /// 停止服务
        /// </summary>
        void Stop();
    }
}
