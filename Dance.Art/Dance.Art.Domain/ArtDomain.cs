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
        public ObservableCollection<PanelPluginInfo> PanelPlugins { get; } = new();

        /// <summary>
        /// 文档插件集合
        /// </summary>
        public ObservableCollection<DocumentPluginInfo> DocumentPlugins { get; } = new();

        /// <summary>
        /// 设置插件集合
        /// </summary>
        public ObservableCollection<SettingPluginInfo> SettingPlugins { get; } = new();

        /// <summary>
        /// 模板插件集合
        /// </summary>
        public ObservableCollection<TemplatePluginInfo> TemplatePlugins { get; } = new();

        /// <summary>
        /// 脚本插件集合
        /// </summary>
        public ObservableCollection<ScriptPluginInfo> ScriptPlugins { get; } = new();

        // ----------------------------------------------------------------------------------------------------
        // Views

        /// <summary>
        /// 面板集合
        /// </summary>
        public ObservableCollection<PanelPluginModel> Panels { get; } = new();

        /// <summary>
        /// 文档集合
        /// </summary>
        public ObservableCollection<DocumentPluginModel> Documents { get; } = new();

        // ----------------------------------------------------------------------------------------------------
        // Domain

        /// <summary>
        /// 项目领域
        /// </summary>
        public ProjectDomain? ProjectDomain { get; set; }
    }
}
