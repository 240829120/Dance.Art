using AvalonDock.Layout.Serialization;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Dance.Art.Domain;
using Dance.Wpf;
using Microsoft.ClearScript;
using Microsoft.ClearScript.JavaScript;
using Microsoft.ClearScript.V8;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
            // -----------------------------------------------------
            // Layout
            this.SaveLayoutCommand = new(this.SaveLayout);
            this.LoadLayoutCommand = new(this.LoadLayout);
            // -----------------------------------------------------
            // Project
            this.CreateProjectCommand = new(this.CreateProject);
            this.OpenProjectCommand = new(this.OpenProject);
            this.SaveProjectCommand = new(this.SaveProject, this.CanSaveProject);
            this.CloseProjectCommand = new(this.CloseProject, this.CanCloseProject);
            // -----------------------------------------------------
            // Edit
            this.SaveCommand = new(this.Save);
            this.SaveAllCommand = new(this.SaveAll);
            this.RedoCommand = new(this.Redo);
            this.UndoCommand = new(this.Undo);
            this.ClosingCommand = new(this.Closing);
            this.ClosedCommand = new(this.Closed);
            // -----------------------------------------------------
            // Script
            this.OpenInVSCodeCommand = new(this.OpenInVSCode);
            this.RunScriptCommand = new(this.RunScript);
            this.DebugScriptCommand = new(this.DebugScript);
            this.StopScriptCommand = new(this.StopScript);

            // -----------------------------------------------------
            // Message
            DanceDomain.Current.Messenger.Register<FileOpenMessage>(this, this.OnFileOpen);
        }

        // ========================================================================================
        // Field

        /// <summary>
        /// 输出管理器
        /// </summary>
        private readonly IOutputManager OutputManager = DanceDomain.Current.LifeScope.Resolve<IOutputManager>();

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

        #region ProjectDomain -- 项目领域

        private ProjectDomain? projectDomain;
        /// <summary>
        /// 项目领域
        /// </summary>
        public ProjectDomain? ProjectDomain
        {
            get { return projectDomain; }
            set { projectDomain = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region ScriptDomain -- 脚本领域

        private ScriptDomain? scriptDomain;
        /// <summary>
        /// 脚本领域
        /// </summary>
        public ScriptDomain? ScriptDomain
        {
            get { return scriptDomain; }
            set { scriptDomain = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region ScriptStatus -- 脚本状态

        private ScriptStatus scriptStatus = ScriptStatus.None;
        /// <summary>
        /// 脚本状态
        /// </summary>
        public ScriptStatus ScriptStatus
        {
            get { return scriptStatus; }
            set { scriptStatus = value; this.OnPropertyChanged(); }
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

        // ------------------------------------------------------------------------------------------
        // Layout

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

        // ------------------------------------------------------------------------------------------
        // Project

        #region CreateProjectCommand -- 创建项目命令

        /// <summary>
        /// 创建项目命令
        /// </summary>
        public RelayCommand CreateProjectCommand { get; private set; }

        /// <summary>
        /// 创建项目
        /// </summary>
        private void CreateProject()
        {
            if (DanceDomain.Current is not ArtDomain artDomain)
                return;

            if (artDomain.ProjectDomain != null)
            {
                if (DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "是否关闭当前项目?", DanceMessageBoxAction.YES | DanceMessageBoxAction.NO) == DanceMessageBoxAction.NO)
                    return;

                this.CloseProject();
            }

            CreateProjectWindow window = new()
            {
                Owner = Application.Current.MainWindow
            };

            if (window.ShowDialog() != true || window.DataContext is not CreateProjectWindowModel vm)
                return;

            if (string.IsNullOrWhiteSpace(vm.ProjectPath) || !File.Exists(vm.ProjectPath))
                return;

            ProjectDomain domain = new(vm.ProjectPath);
            ProjectOpenMessage msg = new(artDomain.ProjectDomain, domain);
            this.ProjectDomain = domain;
            artDomain.ProjectDomain = domain;

            artDomain.Messenger.Send(msg);
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

            if (artDomain.ProjectDomain != null)
            {
                if (DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "是否关闭当前项目?", DanceMessageBoxAction.YES | DanceMessageBoxAction.NO) == DanceMessageBoxAction.NO)
                    return;

                this.CloseProject();
            }

            OpenFileDialog ofd = new()
            {
                Filter = "项目文件|*.art",
                Multiselect = false
            };

            if (ofd.ShowDialog() != true || string.IsNullOrWhiteSpace(ofd.FileName) || !File.Exists(ofd.FileName))
                return;

            ProjectDomain domain = new(ofd.FileName);
            ProjectOpenMessage msg = new(artDomain.ProjectDomain, domain);
            this.ProjectDomain = domain;
            artDomain.ProjectDomain = domain;

            artDomain.Messenger.Send(msg);
        }

        #endregion

        #region SaveProjectCommand -- 保存项目命令

        /// <summary>
        /// 保存项目命令
        /// </summary>
        public RelayCommand SaveProjectCommand { get; private set; }

        /// <summary>
        /// 是否可以保存项目
        /// </summary>
        /// <returns>是否可以保存项目</returns>
        private bool CanSaveProject()
        {
            return this.ProjectDomain != null && this.ProjectDomain.IsModify;
        }

        /// <summary>
        /// 保存项目
        /// </summary>
        private void SaveProject()
        {

        }

        #endregion

        #region CloseProjectCommand -- 关闭项目命令

        /// <summary>
        /// 关闭项目命令
        /// </summary>
        public RelayCommand CloseProjectCommand { get; private set; }

        /// <summary>
        /// 是否可以关闭项目
        /// </summary>
        /// <returns>是否可以关闭项目</returns>
        private bool CanCloseProject()
        {
            return this.ProjectDomain != null;
        }

        /// <summary>
        /// 关闭项目
        /// </summary>
        private void CloseProject()
        {
            if (DanceDomain.Current is not ArtDomain domain || domain.ProjectDomain == null)
                return;

            List<IDockingDocument> unSavedDockingDocuments = new();
            foreach (DocumentPluginModel document in domain.Documents)
            {
                if (document.View is not FrameworkElement view || view.DataContext is not IDockingDocument dockingDocument)
                    continue;

                if (dockingDocument.IsModify)
                {
                    unSavedDockingDocuments.Add(dockingDocument);
                }
            }

            if (unSavedDockingDocuments.Count > 0)
            {
                if (DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "保存未保存的文档?", DanceMessageBoxAction.YES | DanceMessageBoxAction.NO) == DanceMessageBoxAction.YES)
                {
                    unSavedDockingDocuments.ForEach(p => p.Save());
                }
            }

            foreach (DocumentPluginModel document in domain.Documents)
            {
                if (document.View is not FrameworkElement view || view.DataContext is not IDockingDocument dockingDocument)
                    continue;

                dockingDocument.Dispose();
            }

            domain.Documents.Clear();

            ProjectCloseMessage msg = new(domain.ProjectDomain);
            domain.Messenger.Send(msg);
            domain.ProjectDomain?.Dispose();
            domain.ProjectDomain = null;
            this.ProjectDomain = null;
        }

        #endregion

        // ------------------------------------------------------------------------------------------
        // Edit

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

            dockingDocument.Dispose();
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

        // ------------------------------------------------------------------------------------------
        // Script

        #region OpenInVSCodeCommand -- 使用VSCode打开项目命令

        /// <summary>
        /// 使用VSCode打开项目命令
        /// </summary>
        public RelayCommand OpenInVSCodeCommand { get; private set; }

        /// <summary>
        /// 使用VSCode打开项目
        /// </summary>
        private void OpenInVSCode()
        {
            try
            {
                if (this.ProjectDomain == null || string.IsNullOrWhiteSpace(this.ProjectDomain.ProjectFolderPath))
                    return;

                string? setupPath = Registry.ClassesRoot.OpenSubKey("Applications\\Code.exe\\shell\\open")?.GetValue("Icon")?.ToString();
                if (string.IsNullOrWhiteSpace(setupPath))
                {
                    DanceMessageExpansion.ShowMessageBox("错误", DanceMessageBoxIcon.Warning, "未找到VSCode安装路径", DanceMessageBoxAction.YES);
                    return;
                }
                Process.Start(setupPath, $"\"{this.ProjectDomain.ProjectFolderPath}\"");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                DanceMessageExpansion.ShowMessageBox("错误", DanceMessageBoxIcon.Warning, ex.Message, DanceMessageBoxAction.YES);
            }
        }

        #endregion

        #region RunScriptCommand -- 运行脚本命令

        /// <summary>
        /// 运行脚本命令
        /// </summary>
        public AsyncRelayCommand RunScriptCommand { get; private set; }

        /// <summary>
        /// 运行脚本
        /// </summary>
        private async Task RunScript()
        {
            if (this.ProjectDomain == null || string.IsNullOrWhiteSpace(this.ProjectDomain.ProjectFolderPath) || !Directory.Exists(this.ProjectDomain.ProjectFolderPath))
                return;

            this.ScriptStatus = ScriptStatus.Running;
            string file = Path.Combine(this.ProjectDomain.ProjectFolderPath, "index.js");

            this.OutputManager.WriteLine($"运行脚本 ----- {file}");

            await this.CreateAndRunScriptDomain(file, V8ScriptEngineFlags.EnableDynamicModuleImports);
        }

        #endregion

        #region DebugScriptCommand -- 调试脚本命令

        /// <summary>
        /// 调试脚本命令
        /// </summary>
        public AsyncRelayCommand DebugScriptCommand { get; private set; }

        /// <summary>
        /// 调试脚本
        /// </summary>
        /// <returns></returns>
        private async Task DebugScript()
        {
            if (this.ProjectDomain == null || string.IsNullOrWhiteSpace(this.ProjectDomain.ProjectFolderPath) || !Directory.Exists(this.ProjectDomain.ProjectFolderPath))
                return;

            this.ScriptStatus = ScriptStatus.Debugging;
            string file = Path.Combine(this.ProjectDomain.ProjectFolderPath, "index.js");

            this.OutputManager.WriteLine($"调试脚本 ----- {file}");
            this.OutputManager.WriteLine($"调试脚本 ----- 正在等待调试器连接......");

            await this.CreateAndRunScriptDomain(file, V8ScriptEngineFlags.EnableDynamicModuleImports | V8ScriptEngineFlags.EnableDebugging |
                                                      V8ScriptEngineFlags.EnableRemoteDebugging | V8ScriptEngineFlags.AwaitDebuggerAndPauseOnStart);
        }

        #endregion

        #region StopScriptCommand -- 停止脚本命令

        /// <summary>
        /// 停止脚本命令
        /// </summary>
        public AsyncRelayCommand StopScriptCommand { get; private set; }

        /// <summary>
        /// 停止脚本
        /// </summary>
        private async Task StopScript()
        {
            if (DanceDomain.Current is not ArtDomain artDomain)
                return;

            await Task.Run(() =>
            {
                if (this.ScriptDomain == null)
                    return;

                this.ScriptDomain.Dispose();
                this.ScriptDomain = null;
                artDomain.ScriptDomain = null;
                this.ScriptStatus = ScriptStatus.None;
                this.OutputManager.WriteLine($"正在停止脚本......");
            });
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

            if (pluginModel == null || pluginModel.ViewType == null)
            {
                Process.Start("explorer", msg.FileModel.Path);

                return;
            }

            vm = new DocumentPluginModel(msg.FileModel.Path, msg.FileModel.FileName, pluginModel, msg.FileModel.Path);
            domain.Documents.Add(vm);
            vm.IsActive = true;
        }

        #endregion

        // ========================================================================================
        // Private Function

        /// <summary>
        /// 创建并运行脚本
        /// </summary>
        /// <param name="file">入口文件</param>
        /// <param name="flags">引擎标志</param>
        private async Task CreateAndRunScriptDomain(string file, V8ScriptEngineFlags flags)
        {
            if (DanceDomain.Current is not ArtDomain artDomain)
                return;

            await Task.Run(async () =>
            {
                try
                {
                    if (this.ProjectDomain == null || string.IsNullOrWhiteSpace(this.ProjectDomain.ProjectFolderPath))
                        return;

                    await this.StopScript();

                    this.ScriptDomain = new(file)
                    {
                        Engine = new(flags)
                    };
                    artDomain.ScriptDomain = this.ScriptDomain;

                    this.ScriptDomain.Engine.DocumentSettings.AccessFlags = DocumentAccessFlags.EnableFileLoading;
                    this.ScriptDomain.Engine.DocumentSettings.SearchPath = this.ProjectDomain.ProjectFolderPath;
                    this.ScriptDomain.Engine.AddHostObject("DANCE_ART_HOST", this.ScriptDomain.Host);
                    this.ScriptDomain.Engine.EvaluateDocument(file, ModuleCategory.Standard);
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                    this.ScriptStatus = ScriptStatus.None;
                    DanceMessageExpansion.ShowMessageBox("错误", DanceMessageBoxIcon.Failure, $"{ex.Message}", DanceMessageBoxAction.YES);
                }
            });
        }
    }
}
