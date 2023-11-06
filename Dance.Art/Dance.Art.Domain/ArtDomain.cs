using Dance.Wpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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
        /// <summary>
        /// 项目领域构建器
        /// </summary>
        public List<IProjectDomainBuilder> ProjectDomainBuilders { get; private set; } = new();

        /// <summary>
        /// 当前Art领域
        /// </summary>
        new public static ArtDomain Current { get { return (ArtDomain)DanceDomain.Current; } }

        /// <summary>
        /// 插件字典
        /// </summary>
        public Dictionary<Type, IList> PluginDic { get; private set; } = new();

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

        /// <summary>
        /// 脚本领域
        /// </summary>
        public ScriptDomain? ScriptDomain { get; set; }
    }
}
