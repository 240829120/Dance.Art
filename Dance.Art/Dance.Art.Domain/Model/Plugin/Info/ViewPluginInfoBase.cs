using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 视图插件信息基类
    /// </summary>
    public class ViewPluginInfoBase : PluginInfoBase
    {
        /// <summary>
        /// 视图插件信息基类
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        /// <param name="viewType">视图类型</param>
        public ViewPluginInfoBase(string id, string name, Type viewType) : base(id, name)
        {
            this.ViewType = viewType;
        }

        /// <summary>
        /// 视图类型
        /// </summary>
        public Type ViewType { get; private set; }
    }
}
