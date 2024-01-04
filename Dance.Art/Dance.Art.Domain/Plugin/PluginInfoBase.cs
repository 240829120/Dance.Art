using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 插件信息基类
    /// </summary>
    /// <param name="id">编号</param>
    /// <param name="name">名称</param>
    public abstract class PluginInfoBase(string id, string name) : DanceModel, IDancePluginInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string ID { get; private set; } = id;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; } = name;
    }
}
