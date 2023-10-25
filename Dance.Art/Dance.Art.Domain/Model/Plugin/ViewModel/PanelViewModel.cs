using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 面板视图模型
    /// </summary>
    public class PanelViewModel : ViewPluginViewModelBase
    {
        /// <summary>
        /// 面板视图模型
        /// </summary>        
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        /// <param name="pluginModel">插件模型</param>
        public PanelViewModel(string id, string name, PanelPluginModel pluginModel) : base(id, name, pluginModel)
        {

        }



    }
}
