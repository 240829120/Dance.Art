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
    public abstract class PluginInfoBase : DanceModel, IDancePluginInfo
    {
        /// <summary>
        /// 插件模型信息基类
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        public PluginInfoBase(string id, string name)
        {
            this.ID = id;
            this.Name = name;
        }

        /// <summary>
        /// 编号
        /// </summary>
        public string ID { get; private set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }
    }
}
