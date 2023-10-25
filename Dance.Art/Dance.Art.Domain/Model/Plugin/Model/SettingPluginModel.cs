using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 设置插件模型
    /// </summary>
    public class SettingPluginModel : ViewPluginModelBase
    {
        /// <summary>
        /// 设置插件模型
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        /// <param name="viewType">视图类型</param>
        public SettingPluginModel(string id, string name, Type viewType) : base(id, name, viewType)
        {
        }
    }
}