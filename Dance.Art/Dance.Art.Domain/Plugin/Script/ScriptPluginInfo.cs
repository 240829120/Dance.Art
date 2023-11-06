using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 脚本插件信息
    /// </summary>
    public class ScriptPluginInfo : PluginInfoBase
    {
        /// <summary>
        /// 脚本插件信息
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        /// <param name="services">脚本服务</param>
        public ScriptPluginInfo(string id, string name, params ScriptServiceInfo[] services) : base(id, name)
        {
            this.Services = services;
        }

        /// <summary>
        /// 脚本服务信息
        /// </summary>
        public ScriptServiceInfo[] Services { get; private set; }
    }
}
