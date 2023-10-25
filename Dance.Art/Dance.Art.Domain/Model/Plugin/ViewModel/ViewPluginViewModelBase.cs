using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 视图插件视图模型基类
    /// </summary>
    public class ViewPluginViewModelBase : DanceViewModel
    {
        /// <summary>
        /// 视图插件视图模型基类
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        /// <param name="pluginModel">插件模型</param>
        public ViewPluginViewModelBase(string id, string name, PluginModelBase pluginModel)
        {
            this.id = id;
            this.name = name;
            this.pluginModel = pluginModel;
        }

        #region ID -- 编号

        private string id;
        /// <summary>
        /// 编号
        /// </summary>
        public string ID
        {
            get { return id; }
            set { id = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Name -- 名称

        private string name;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region PluginModel -- 插件

        private PluginModelBase pluginModel;
        /// <summary>
        /// 插件模型
        /// </summary>
        public PluginModelBase PluginModel
        {
            get { return pluginModel; }
            private set { pluginModel = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region IsVisible -- 是否可见

        private bool isVisible = true;
        /// <summary>
        /// 是否可见
        /// </summary>
        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region IsActive -- 是否激活

        private bool isActive;
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; this.OnPropertyChanged(); }
        }

        #endregion
    }
}
