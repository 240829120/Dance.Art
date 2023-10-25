using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// Art领域
    /// </summary>
    public class ArtDomain : DanceDomain
    {
        // ----------------------------------------------------------------------------------------------------
        // Plugins

        /// <summary>
        /// 面板插件集合
        /// </summary>
        public ObservableCollection<PluginModelBase> PanelPlugins { get; } = new();

        /// <summary>
        /// 文档插件集合
        /// </summary>
        public ObservableCollection<PluginModelBase> DocumentPlugins { get; } = new();

        /// <summary>
        /// 设置插件集合
        /// </summary>
        public ObservableCollection<PluginModelBase> SettingPlugins { get; } = new();

        // ----------------------------------------------------------------------------------------------------
        // Views

        /// <summary>
        /// 面板集合
        /// </summary>
        public ObservableCollection<PanelViewModel> Panels { get; } = new();

        /// <summary>
        /// 文档集合
        /// </summary>
        public ObservableCollection<DocumentViewModel> Documents { get; } = new();

        // ----------------------------------------------------------------------------------------------------
        // Domain

        /// <summary>
        /// 项目领域
        /// </summary>
        public ProjectDomain? ProjectDomain { get; set; }
    }
}
