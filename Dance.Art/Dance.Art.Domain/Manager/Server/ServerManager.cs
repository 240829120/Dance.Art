using Dance.Common;
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
    [DanceSingleton(typeof(IServerManager))]
    public class ServerManager : DanceObject, IServerManager
    {
        /// <summary>
        /// WebAPI服务
        /// </summary>
        private DanceWebApiServer? Server;

        /// <summary>
        /// 地址
        /// </summary>
        public string URL { get; set; } = "http://127.0.0.1:8082";

        /// <summary>
        /// 开启服务
        /// </summary>
        /// <param name="assemblyPrefixes">程序集前缀集合</param>
        public void Start(List<string> assemblyPrefixes)
        {
            if (this.Server != null)
                return;

            this.Server = new();
            this.Server.Urls.Add(URL);

            List<string> files = new();
            List<Assembly> assemblies = new();
            foreach (string assemblyPrefix in assemblyPrefixes)
            {
                files.AddRange(Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll").Where(p => Path.GetFileName(p).StartsWith(assemblyPrefix)));
            }
            assemblies.AddRange(files.Select(p => Assembly.Load(AssemblyName.GetAssemblyName(p))));

            this.Server.Assemblies.AddRange(assemblies);

            this.Server.Start();
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        public void Stop()
        {
            this.Server?.Stop();
            this.Server = null;
        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            this.Server?.Stop();
            this.Server?.Dispose();
            this.Server = null;
        }
    }
}
