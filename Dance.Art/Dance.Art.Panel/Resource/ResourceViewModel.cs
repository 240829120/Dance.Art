using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Dance.Art.Domain;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace Dance.Art.Panel
{
    /// <summary>
    /// 工具视图模型
    /// </summary>
    public class ResourceViewModel : PanelViewModelBase
    {
        /// <summary>
        /// 输出视图模型
        /// </summary>
        public ResourceViewModel()
        {
            // 命令
            this.LoadedCommand = new(this.Loaded);
            this.ResourceDragBeginCommand = new(this.ResourceDragBegin);

            // 消息
            DanceDomain.Current.Messenger.Register<DockingActiveContentChangedMessage>(this, this.OnDockingActiveContentChanged);
            DanceDomain.Current.Messenger.Register<ProjectClosedMessage>(this, this.OnProjectClosed);
        }

        // ============================================================================================
        // Field

        /// <summary>
        /// 资源管理器
        /// </summary>
        private readonly IResourceManager ResourceManager = DanceDomain.Current.LifeScope.Resolve<IResourceManager>();

        // ============================================================================================
        // Property

        #region Groups -- 分组集合

        private IReadOnlyList<ResourceInfoGroupModel>? groups;
        /// <summary>
        /// 分组集合
        /// </summary>
        public IReadOnlyList<ResourceInfoGroupModel>? Groups
        {
            get { return groups; }
            set { groups = value; this.OnPropertyChanged(); }
        }

        #endregion

        // ============================================================================================
        // Command

        #region LoadedCommand -- 加载命令

        /// <summary>
        /// 加载命令
        /// </summary>
        public RelayCommand LoadedCommand { get; private set; }

        /// <summary>
        /// 加载
        /// </summary>
        private void Loaded()
        {
            if (this.View is not ResourceView)
                return;

            if (ArtDomain.Current.CurrentActiveContent is not DocumentPluginModel vm)
                return;

            this.LoadResources(vm.File);
        }

        #endregion

        #region ResourceDragBeginCommand -- 资源拖拽开始命令

        /// <summary>
        /// 文件拖拽开始命令
        /// </summary>
        public RelayCommand<DanceDragBeginEventArgs> ResourceDragBeginCommand { get; private set; }

        /// <summary>
        /// 文件拖拽开始
        /// </summary>
        private void ResourceDragBegin(DanceDragBeginEventArgs? e)
        {
            if (e == null)
                return;

            if (e.Element.DataContext is ResourceInfoGroupModel || e.Element.DataContext is not ResourceInfoItemModel resource)
            {
                e.IsCancel = true;
                return;
            }

            e.Data = resource;
        }

        #endregion

        // ============================================================================================
        // Message

        #region DockingActiveContentChangedMessage -- 激活内容改变消息

        /// <summary>
        /// 激活内容改变消息
        /// </summary>
        private void OnDockingActiveContentChanged(object sender, DockingActiveContentChangedMessage msg)
        {
            if (msg.Content is not DocumentPluginModel vm)
                return;

            this.LoadResources(vm.File);
        }

        #endregion

        #region ProjectClosedMessage -- 项目关闭消息

        /// <summary>
        /// 项目关闭
        /// </summary>
        private void OnProjectClosed(object sender, ProjectClosedMessage msg)
        {
            this.Groups = null;
        }

        #endregion

        // ============================================================================================
        // Private Function

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <param name="path">资源</param>
        private void LoadResources(string path)
        {
            string extension = Path.GetExtension(path);
            if (string.IsNullOrWhiteSpace(extension))
                return;

            this.Groups = this.ResourceManager.GetOrCreateResources(extension);
        }
    }
}