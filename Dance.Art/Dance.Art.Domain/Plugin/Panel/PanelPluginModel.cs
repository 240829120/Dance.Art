using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 面板插件模型
    /// </summary>
    public class PanelPluginModel : ViewPluginModelBase
    {
        /// <summary>
        /// 面板视图模型
        /// </summary>        
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        /// <param name="pluginInfo">插件信息</param>
        public PanelPluginModel(string id, string name, PanelPluginInfo pluginInfo) : base(id, name, pluginInfo)
        {

        }
    }
}
