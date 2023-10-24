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
        /// <summary>
        /// 面板集合
        /// </summary>
        public ObservableCollection<PluginViewModel> Panels { get; } = new();

        /// <summary>
        /// 文档集合
        /// </summary>
        public ObservableCollection<PluginViewModel> Documents { get; } = new();

        /// <summary>
        /// 设置集合
        /// </summary>
        public ObservableCollection<PluginViewModel> Settings { get; } = new();
    }
}
