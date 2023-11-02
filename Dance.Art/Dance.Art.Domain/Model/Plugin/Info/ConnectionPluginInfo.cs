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
        /// <param name="icon">团标</param>
        public ConnectionPluginInfo(string id, string name, IConnectionFactory factory, string icon) : base(id, name)
        {
            this.Factory = factory;
            this.Icon = icon;
        }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; private set; }

        /// <summary>
        /// 连接工厂
        /// </summary>
        public IConnectionFactory Factory { get; private set; }
    }
}
