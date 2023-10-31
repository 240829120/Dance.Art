using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 视图插件模型基类
    /// </summary>
    public class ViewPluginModelBase : DanceViewModel
    {
        /// <summary>
        /// 视图插件模型基类
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        /// <param name="pluginInfo">插件信息</param>
        public ViewPluginModelBase(string id, string name, PluginInfoBase pluginInfo)
        {
            this.id = id;
            this.name = name;
            this.pluginInfo = pluginInfo;
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

        #region PluginInfo -- 插件信息

        private PluginInfoBase pluginInfo;
        /// <summary>
        /// 插件信息
        /// </summary>
        public PluginInfoBase PluginInfo
        {
            get { return pluginInfo; }
            private set { pluginInfo = value; this.OnPropertyChanged(); }
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

        #region PluginView -- 插件视图

        private FrameworkElement? pluginView;
        /// <summary>
        /// 插件视图
        /// </summary>
        public FrameworkElement? PluginView
        {
            get { return pluginView; }
            set { pluginView = value; this.OnPropertyChanged(); }
        }

        #endregion
    }
}
