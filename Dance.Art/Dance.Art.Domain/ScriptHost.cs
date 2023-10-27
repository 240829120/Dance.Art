using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 脚本宿主
    /// </summary>
    public class ScriptHost : DanceObject
    {
        /// <summary>
        /// 脚本宿主
        /// </summary>
        public ScriptHost()
        {
            this.InitService();
        }

        /// <summary>
        /// 初始化服务
        /// </summary>
        private void InitService()
        {
            if (DanceDomain.Current is not ArtDomain domain)
                return;

            foreach (ScriptPluginInfo scriptPlugin in domain.ScriptPlugins)
            {
                if (scriptPlugin == null || scriptPlugin.Services == null || scriptPlugin.Services.Length == 0)
                    continue;

                foreach (ScriptServiceInfo serviceInfo in scriptPlugin.Services)
                {
                    if (serviceInfo == null || serviceInfo.Type == null)
                        continue;

                    this.ServiceCache.Add(serviceInfo, null);
                }
            }
        }

        // ===========================================================================================
        // Field

        /// <summary>
        /// 服务缓存
        /// </summary>
        private readonly Dictionary<ScriptServiceInfo, object?> ServiceCache = new();

        /// <summary>
        /// 是否已经销毁
        /// </summary>
        private bool IsDestroyed;

        // ===========================================================================================
        // Public Function

        /// <summary>
        /// 获取服务
        /// </summary>
        /// <param name="nameSpace">命名空间</param>
        /// <param name="name">名称</param>
        /// <returns>服务</returns>
        public object? GetService(string nameSpace, string name)
        {
            lock (this.ServiceCache)
            {
                ScriptServiceInfo? serviceInfo = this.ServiceCache.Keys.FirstOrDefault(p => string.Equals(p.NameSpace, nameSpace) && string.Equals(p.Name, name));
                if (serviceInfo == null)
                    return null;

                if (this.ServiceCache[serviceInfo] == null)
                {
                    if (string.IsNullOrWhiteSpace(serviceInfo.Type.FullName))
                        return null;

                    object? service = serviceInfo.Type.Assembly.CreateInstance(serviceInfo.Type.FullName);
                    if (service == null)
                        return null;

                    this.ServiceCache[serviceInfo] = service;

                    return service;
                }

                return this.ServiceCache[serviceInfo];
            }
        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            lock (this.ServiceCache)
            {

            }
        }
    }
}