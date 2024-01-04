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
        public List<IProjectDomainBuilder> ProjectDomainBuilders { get; private set; } = [];

        /// <summary>
        /// 当前Art领域
        /// </summary>
        new public static ArtDomain Current { get { return (ArtDomain)DanceDomain.Current; } }

        /// <summary>
        /// 插件程序集前缀集合
        /// </summary>
        public List<string> PluginAssemblyPrefixes { get; } = [];

        /// <summary>
        /// 插件集合
        /// </summary>
        public List<IDancePluginInfo> Plugins { get; private set; } = [];

        // ----------------------------------------------------------------------------------------------------
        // Views

        /// <summary>
        /// 面板集合
        /// </summary>
        public ObservableCollection<PanelPluginModel> Panels { get; } = [];

        /// <summary>
        /// 文档集合
        /// </summary>
        public ObservableCollection<DocumentPluginModel> Documents { get; } = [];

        /// <summary>
        /// 当前激活的内容
        /// </summary>
        public object? CurrentActiveContent { get; set; }

        /// <summary>
        /// 当前选中对象
        /// </summary>
        public object? CurrentSelectedObject { get; set; }

        /// <summary>
        /// 是否是设计模式
        /// </summary>
        public bool IsDesignMode { get; set; }

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

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            base.Destroy();

            DanceDomain.Current.LifeScope.Resolve<IServerManager>()?.Dispose();
        }
    }
}
