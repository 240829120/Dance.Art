using AvalonDock.Layout.Serialization;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Dance.Art.Domain;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.Module
{
    /// <summary>
    /// 主视图模型
    /// </summary>
    [DanceSingleton]
    public class MainViewModel : DanceViewModel
    {
        public MainViewModel()
        {
            this.LoadedCommand = new(this.Loaded);
            this.SaveLayoutCommand = new(this.SaveLayout);
            this.LoadLayoutCommand = new(this.LoadLayout);

            this.OpenProjectCommand = new(this.OpenProject);
            this.SaveCommand = new(this.Save);
            this.SaveAllCommand = new(this.SaveAll);
            this.RedoCommand = new(this.Redo);
            this.UndoCommand = new(this.Undo);
            this.ClosingCommand = new(this.Closing);
            this.ClosedCommand = new(this.Closed);

            DanceDomain.Current.Messenger.Register<FileOpenMessage>(this, this.OnFileOpen);
        }

        // ========================================================================================
        // Field

        /// <summary>
        /// 文件管理器
        /// </summary>
        private readonly IFileManager FileManager = DanceDomain.Current.LifeScope.Resolve<IFileManager>();

        // ========================================================================================
        // Property

        #region Panels -- 面板集合

        private ObservableCollection<PanelPluginModel>? panels;

        /// <summary>
        /// 面板集合
        /// </summary>
        public ObservableCollection<PanelPluginModel>? Panels
        {
            get { return panels; }
            private set { panels = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Documents -- 文档集合

        private ObservableCollection<DocumentPluginModel>? documents;

        /// <summary>
        /// 面板集合
        /// </summary>
        public ObservableCollection<DocumentPluginModel>? Documents
        {
            get { return documents; }
            private set { documents = value; this.OnPropertyChanged(); }
        }

        #endregion

        // ========================================================================================
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
            if (DanceDomain.Current is not ArtDomain domain)
                return;

            foreach (PanelPluginInfo plugin in domain.PanelPlugins)
            {
                PanelPluginModel vm = new(plugin.ID, plugin.Name, plugin);

                domain.Panels.Add(vm);
            }

            this.Panels = domain.Panels;
            this.Documents = domain.Documents;

            this.LoadLayout();
        }

        #endregion

        #region SaveLayoutCommand -- 保存布局命令

        /// <summary>
        /// 保存布局命令
        /// </summary>
        public RelayCommand SaveLayoutCommand { get; private set; }

        /// <summary>
        /// 保存布局
        /// </summary>
        private void SaveLayout()
        {
            if (this.View is not MainView view)
                return;

            string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "layout");
            string path = Path.Combine(dir, "default.xml");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var layoutSerializer = new XmlLayoutSerializer(view.docking);
            layoutSerializer.Serialize(path);

            DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Success, "布局保存成功", DanceMessageBoxAction.YES);
        }

        #endregion

        #region LoadLayoutCommand -- 加载布局命令

        /// <summary>
        /// 加载布局命令
        /// </summary>
        public RelayCommand LoadLayoutCommand { get; private set; }

        /// <summary>
        /// 加载布局
        /// </summary>
        private void LoadLayout()
        {
            if (this.View is not MainView view)
                return;

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "layout", "default.xml");
            if (!File.Exists(path))
                return;

            var layoutSerializer = new XmlLayoutSerializer(view.docking);
            layoutSerializer.Deserialize(path);
        }

        #endregion

        #region OpenProjectCommand -- 打开项目命令

        /// <summary>
        /// 打开项目命令
        /// </summary>
        public RelayCommand OpenProjectCommand { get; private set; }

        /// <summary>
        /// 打开项目
        /// </summary>
        private void OpenProject()
        {
            if (DanceDomain.Current is not ArtDomain artDomain)
                return;

            ProjectDomain domain = new(@"E:\test_project\test.art");

            this.FileManager.Initialize(domain);

            ProjectOpenMessage msg = new()
            {
                OldProject = artDomain.ProjectDomain,
                NewProject = domain,
            };

            artDomain.ProjectDomain = domain;

            artDomain.Messenger.Send(msg);
        }

        #endregion

        #region SaveCommand -- 保存当前激活的文档

        /// <summary>
        /// 保存当前激活的文档
        /// </summary>
        public RelayCommand SaveCommand { get; private set; }

        /// <summary>
        /// 保存当前激活的文档命令
        /// </summary>
        private void Save()
        {
            if (this.View is not MainView view || view.docking.ActiveContent is not DocumentPluginModel document)
                return;

            if (document.View is not FrameworkElement documentView || documentView.DataContext is not IDockingDocument dockingDocument)
                return;

            dockingDocument.Save();
        }

        #endregion

        #region SaveAllCommand -- 保存全部文档命令

        /// <summary>
        /// 保存全部文档命令
        /// </summary>
        public RelayCommand SaveAllCommand { get; private set; }

        /// <summary>
        /// 保存全部文档
        /// </summary>
        private void SaveAll()
        {
            if (DanceDomain.Current is not ArtDomain domain)
                return;

            foreach (DocumentPluginModel document in domain.Documents)
            {
                if (document.View is not FrameworkElement documentView || documentView.DataContext is not IDockingDocument dockingDocument)
                    continue;

                dockingDocument.Save();
            }
        }

        #endregion

        #region RedoCommand -- 重做命令

        /// <summary>
        /// 重做命令
        /// </summary>
        public RelayCommand RedoCommand { get; private set; }

        /// <summary>
        /// 重做
        /// </summary>
        private void Redo()
        {
            if (this.View is not MainView view || view.docking.ActiveContent is not DocumentPluginModel document)
                return;

            if (document.View is not FrameworkElement documentView || documentView.DataContext is not IDockingDocument dockingDocument)
                return;

            dockingDocument.Redo();
        }

        #endregion

        #region UndoCommand -- 撤销命令

        /// <summary>
        /// 撤销命令
        /// </summary>
        public RelayCommand UndoCommand { get; private set; }

        /// <summary>
        /// 撤销
        /// </summary>
        private void Undo()
        {
            if (this.View is not MainView view || view.docking.ActiveContent is not DocumentPluginModel document)
                return;

            if (document.View is not FrameworkElement documentView || documentView.DataContext is not IDockingDocument dockingDocument)
                return;

            dockingDocument.Undo();
        }

        #endregion

        #region ClosingCommand -- 文档关闭之前命令

        /// <summary>
        /// 文档关闭之前命令
        /// </summary>
        public RelayCommand<AvalonDock.DocumentClosingEventArgs> ClosingCommand { get; private set; }

        /// <summary>
        /// 文档关闭之前
        /// </summary>
        /// <param name="e"></param>
        private void Closing(AvalonDock.DocumentClosingEventArgs? e)
        {
            if (e == null || e.Document.Content is not DocumentPluginModel document)
                return;

            if (document.View is not FrameworkElement view || view.DataContext is not IDockingDocument dockingDocument)
                return;

            if (!dockingDocument.IsModify)
                return;

            if (DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, $"是否保存文件: {document.File}", DanceMessageBoxAction.YES | DanceMessageBoxAction.NO) == DanceMessageBoxAction.YES)
            {
                dockingDocument.Save();
            }
        }

        #endregion

        #region ClosedCommand -- 文档关闭命令

        /// <summary>
        /// 文档关闭命令
        /// </summary>
        public RelayCommand<AvalonDock.DocumentClosedEventArgs> ClosedCommand { get; private set; }

        /// <summary>
        /// 文档关闭之后命令
        /// </summary>
        private void Closed(AvalonDock.DocumentClosedEventArgs? e)
        {
            if (e == null || e.Document.Content is not DocumentPluginModel document)
                return;

            this.Documents?.Remove(document);
        }

        #endregion

        // ========================================================================================
        // Message

        #region FileOpenMessage -- 文件打开消息

        /// <summary>
        /// 文件打开
        /// </summary>
        private void OnFileOpen(object sender, FileOpenMessage msg)
        {
            if (DanceDomain.Current is not ArtDomain domain)
                return;

            DocumentPluginModel? vm = domain.Documents.FirstOrDefault(p => p.File == msg.FileModel.Path);
            if (vm != null)
            {
                vm.IsActive = true;
                return;
            }

            DocumentPluginInfo? pluginModel = domain.DocumentPlugins.FirstOrDefault(p =>
            {
                if (p is not DocumentPluginInfo documentPlugin || documentPlugin.FileInfos == null)
                    return false;

                return documentPlugin.FileInfos.Any(p => string.Equals(p.Extension, msg.FileModel.Extension, StringComparison.OrdinalIgnoreCase));
            }) as DocumentPluginInfo;

            if (pluginModel == null)
                return;

            vm = new DocumentPluginModel(msg.FileModel.Path, msg.FileModel.FileName, pluginModel, msg.FileModel.Path);
            domain.Documents.Add(vm);
            vm.IsActive = true;
        }

        #endregion
    }
}
