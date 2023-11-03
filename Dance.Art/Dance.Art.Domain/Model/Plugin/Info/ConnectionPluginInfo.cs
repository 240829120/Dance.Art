using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 连接插件信息
    /// </summary>
    public class ConnectionPluginInfo : PluginInfoBase
    {
        /// <summary>
        /// 连接插件信息
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        /// <param name="factory">连接工厂</param>
        public ConnectionPluginInfo(string id, string name, IConnectionFactory factory) : base(id, name)
        {
            this.Factory = factory;
        }

        /// <summary>
        /// 连接工厂
        /// </summary>
        public IConnectionFactory Factory { get; private set; }
    }
}
