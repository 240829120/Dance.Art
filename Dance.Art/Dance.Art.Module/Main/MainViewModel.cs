using AvalonDock.Layout.Serialization;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Dance.Art.Domain;
using Dance.Art.Domain.Message;
using Dance.Art.Storage;
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
            this.ActiveContentChangedCommand = new(this.ActiveContentChanged);
            // -----------------------------------------------------
            // Script
            this.OpenInVSCodeCommand = new(this.OpenInVSCode);
            this.RunScriptCommand = new(this.RunScript);
            this.DebugScriptCommand = new(this.DebugScript);
            this.StopScriptCommand = new(this.StopScript);

            // -----------------------------------------------------
            // Message
            DanceDomain.Current.Messenger.Register<ApplicationClosingMessage>(this, this.OnApplicationClosing);
            DanceDomain.Current.Messenger.Register<ProjectOpenMessage>(this, this.OnProjectOpen);
            DanceDomain.Current.Messenger.Register<ProjectClosingMessage>(this, this.OnProjectClosing);
            DanceDomain.Current.Messenger.Register<FileOpenMessage>(this, this.OnFileOpen);
            DanceDomain.Current.Messenger.Register<FileRenameMessage>(this, this.OnFileRename);
            DanceDomain.Current.Messenger.Register<FileChangeMessage>(this, this.OnFileChange);
            DanceDomain.Current.Messenger.Register<FileStatusChangeMessage>(this, this.OnFileStatusChange);
            DanceDomain.Current.Messenger.Register<PropertySelectedChangedMessage>(this, this.OnPropertySelectedChanged);

        }

        // ========================================================================================
        // Field

        /// <summary>
        /// 输出管理器
        /// </summary>
        private readonly IOutputManager OutputManager = DanceDomain.Current.LifeScope.Resolve<IOutputManager>();

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

        #region IsSaveAllEnabled -- 保存全部是否可用

        private bool isSaveAllEnabled;
        /// <summary>
        /// 保存全部是否可用
        /// </summary>
        public bool IsSaveAllEnabled
        {
            get { return isSaveAllEnabled; }
            set { isSaveAllEnabled = value; this.OnPropertyChanged(); }
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
            foreach (PanelPluginInfo plugin in ArtDomain.Current.GetPluginCollection<PanelPluginInfo>())
            {
                PanelPluginModel vm = new(plugin.ID, plugin.Name, plugin);

                ArtDomain.Current.Panels.Add(vm);
            }

            this.Panels = ArtDomain.Current.Panels;
            this.Documents = ArtDomain.Current.Documents;

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
            if (ArtDomain.Current.ProjectDomain != null)
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
            ArtDomain.Current.ProjectDomain = domain;
            domain.Build();

            ProjectOpenMessage msg = new(domain);
            this.ProjectDomain = domain;
            ArtDomain.Current.Messenger.Send(msg);
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
            if (ArtDomain.Current.ProjectDomain != null)
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
            ArtDomain.Current.ProjectDomain = domain;
            domain.Build();

            ProjectOpenMessage msg = new(domain);
            this.ProjectDomain = domain;
            ArtDomain.Current.Messenger.Send(msg);
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
            if (ArtDomain.Current.ProjectDomain == null)
                return;

            ProjectClosingMessage closingMsg = new(ArtDomain.Current.ProjectDomain);
            DanceDomain.Current.Messenger.Send(closingMsg);

            if (closingMsg.IsCancel)
                return;

            ProjectClosedMessage closedMsg = new(ArtDomain.Current.ProjectDomain);
            DanceDomain.Current.Messenger.Send(closedMsg);

            ArtDomain.Current.ProjectDomain.Dispose();
            ArtDomain.Current.ProjectDomain = null;
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
            try
            {
                if (this.View is not MainView view || view.docking.ActiveContent is not ViewPluginModelBase pluginModel)
                    return;

                if (pluginModel.View is not FrameworkElement pluginView || pluginView.DataContext is not IPanelViewModel panel)
                    return;

                panel.Save();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                DanceMessageExpansion.ShowMessageBox("错误", DanceMessageBoxIcon.Failure, ex.Message, DanceMessageBoxAction.YES);
            }
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
            try
            {
                foreach (DocumentPluginModel document in ArtDomain.Current.Documents)
                {
                    if (document.View is not FrameworkElement documentView || documentView.DataContext is not IDocumentViewModel dockingDocument)
                        continue;

                    dockingDocument.Save();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                DanceMessageExpansion.ShowMessageBox("错误", DanceMessageBoxIcon.Failure, ex.Message, DanceMessageBoxAction.YES);
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

            if (document.View is not FrameworkElement documentView || documentView.DataContext is not IDocumentViewModel dockingDocument)
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

            if (document.View is not FrameworkElement documentView || documentView.DataContext is not IDocumentViewModel dockingDocument)
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

            if (document.View is not FrameworkElement view || view.DataContext is not IDocumentViewModel dockingDocument)
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

        #region ActiveContentChangedCommand -- 激活内容改变命令

        /// <summary>
        /// 激活内容改变命令
        /// </summary>
        public RelayCommand ActiveContentChangedCommand { get; private set; }

        /// <summary>
        /// 激活内容改变
        /// </summary>
        private void ActiveContentChanged()
        {
            if (this.View is not MainView view)
                return;

            ArtDomain.Current.CurrentActiveContent = view.docking.ActiveContent;
            DanceDomain.Current.Messenger.Send(new DockingActiveContentChangedMessage(view.docking.ActiveContent));
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

            this.OutputManager.Clear();
            this.OutputManager.WriteLine($"运行脚本 ----- {file}");

            await this.CreateAndRunScriptDomain(file, V8ScriptEngineFlags.EnableDynamicModuleImports);

            if (this.ScriptDomain == null)
                return;

            DanceDomain.Current.Messenger.Send(new ScriptRunningMessage(this.ProjectDomain, this.ScriptDomain, false));
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

            this.OutputManager.Clear();
            this.OutputManager.WriteLine($"调试脚本 ----- {file}");
            this.OutputManager.WriteLine($"调试脚本 ----- 正在等待调试器连接......");

            await this.CreateAndRunScriptDomain(file, V8ScriptEngineFlags.EnableDynamicModuleImports | V8ScriptEngineFlags.EnableDebugging |
                                                      V8ScriptEngineFlags.EnableRemoteDebugging | V8ScriptEngineFlags.AwaitDebuggerAndPauseOnStart);

            if (this.ScriptDomain == null)
                return;

            DanceDomain.Current.Messenger.Send(new ScriptRunningMessage(this.ProjectDomain, this.ScriptDomain, false));
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
            this.ScriptStatus = ScriptStatus.WaitingStop;

            await Task.Run(() =>
            {
                if (this.ScriptDomain == null)
                    return;

                this.ScriptDomain.Dispose();
                this.ScriptDomain = null;
                ArtDomain.Current.ScriptDomain = null;
                this.ScriptStatus = ScriptStatus.None;
                this.OutputManager.WriteLine($"停止脚本");
            });

            if (this.ProjectDomain == null)
                return;

            DanceDomain.Current.Messenger.Send(new ScriptStopMessage(this.ProjectDomain));
        }

        #endregion

        // ========================================================================================
        // Message

        #region ApplicationClosingMessage -- 应用程序关闭消息

        /// <summary>
        /// 应用程序关闭
        /// </summary>
        private void OnApplicationClosing(object sender, ApplicationClosingMessage msg)
        {
            if (ArtDomain.Current.ProjectDomain == null)
                return;

            ProjectClosingMessage closingMsg = new(ArtDomain.Current.ProjectDomain);
            ArtDomain.Current.Messenger.Send(closingMsg);

            if (closingMsg.IsCancel)
            {
                msg.IsCancel = true;
                return;
            }

            ProjectClosedMessage closedMsg = new(ArtDomain.Current.ProjectDomain);
            ArtDomain.Current.Messenger.Send(closedMsg);

            ArtDomain.Current.ProjectDomain.Dispose();
            ArtDomain.Current.ProjectDomain = null;
            this.ProjectDomain = null;
        }

        #endregion

        #region ProjectOpenMessage -- 项目打开消息

        /// <summary>
        /// 项目打开消息
        /// </summary>
        private void OnProjectOpen(object sender, ProjectOpenMessage msg)
        {
            if (string.IsNullOrWhiteSpace(msg.ProjectDomain.ProjectFolderPath) || !Directory.Exists(msg.ProjectDomain.ProjectFolderPath))
                return;

            List<OpendDocumentEntity> documents = msg.ProjectDomain.CacheContext.OpendDocuments.FindAll().ToList();
            if (documents.Count == 0)
                return;

            foreach (OpendDocumentEntity document in documents)
            {
                if (document == null || string.IsNullOrWhiteSpace(document.Path))
                    continue;

                string path = Path.Combine(msg.ProjectDomain.ProjectFolderPath, document.Path);
                if (!File.Exists(path))
                    continue;

                DanceDomain.Current.Messenger.Send(new FileOpenMessage(path));
            }

            OpendDocumentEntity? first = documents.FirstOrDefault();
            if (first == null || string.IsNullOrWhiteSpace(first.Path))
                return;

            DanceDomain.Current.Messenger.Send(new FileOpenMessage(Path.Combine(msg.ProjectDomain.ProjectFolderPath, first.Path)));
        }

        #endregion

        #region ProjectClosingMessage -- 项目关闭前消息

        /// <summary>
        /// 项目关闭前
        /// </summary>
        private void OnProjectClosing(object sender, ProjectClosingMessage msg)
        {
            // 停止脚本
            if (ArtDomain.Current.ScriptDomain != null && this.ScriptStatus != ScriptStatus.None)
            {
                ArtDomain.Current.ScriptDomain.Dispose();
                ArtDomain.Current.ScriptDomain = null;

                this.ScriptDomain = null;
                this.ScriptStatus = ScriptStatus.None;
            }

            // 关闭文档
            if (this.Documents == null)
                return;

            List<IPanelViewModel> unSaveDocuments = new();
            foreach (DocumentPluginModel document in this.Documents)
            {
                if (document.View is FrameworkElement view && view.DataContext is IPanelViewModel panel && panel.IsModify)
                {
                    unSaveDocuments.Add(panel);
                }
            }

            if (unSaveDocuments.Count > 0)
            {
                DanceMessageBoxAction result = DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "是否保存未保存的文件?", DanceMessageBoxAction.YES | DanceMessageBoxAction.NO | DanceMessageBoxAction.CANCEL);
                if (result == DanceMessageBoxAction.CANCEL)
                {
                    msg.IsCancel = true;
                    return;
                }

                if (result == DanceMessageBoxAction.YES)
                {
                    unSaveDocuments.ForEach(p => p.Save());
                }
            }

            this.Documents.ForEach(p =>
            {
                if (p.View is FrameworkElement view && view.DataContext is IPanelViewModel panel && panel.IsModify)
                {
                    panel.Dispose();
                }
            });

            // 保存打开的文档
            if (!string.IsNullOrWhiteSpace(msg.ProjectDomain.ProjectFolderPath) && Directory.Exists(msg.ProjectDomain.ProjectFolderPath))
            {
                msg.ProjectDomain.CacheContext.OpendDocuments.DeleteAll();
                msg.ProjectDomain.CacheContext.OpendDocuments.InsertBulk(this.Documents.Select(p => new OpendDocumentEntity() { Path = Path.GetRelativePath(msg.ProjectDomain.ProjectFolderPath, p.File) }));
            }

            this.Documents.Clear();
        }

        #endregion

        #region FileOpenMessage -- 文件打开消息

        /// <summary>
        /// 文件打开
        /// </summary>
        private void OnFileOpen(object sender, FileOpenMessage msg)
        {
            DocumentPluginModel? vm = ArtDomain.Current.Documents.FirstOrDefault(p => p.File == msg.Path);
            if (vm != null)
            {
                vm.IsActive = true;
                return;
            }

            DocumentPluginInfo? pluginInfo = msg.PluginInfo ?? ArtDomain.Current.GetPluginCollection<DocumentPluginInfo>().FirstOrDefault(p =>
            {
                if (p is not DocumentPluginInfo documentPlugin || documentPlugin.FileInfos == null)
                    return false;

                return documentPlugin.FileInfos.Any(p => string.Equals(p.Extension, msg.Extension, StringComparison.OrdinalIgnoreCase));
            });

            if (pluginInfo == null || pluginInfo.ViewType == null)
            {
                if (DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, $"是否使用默认程序打开: {msg.Path}", DanceMessageBoxAction.YES | DanceMessageBoxAction.CANCEL) != DanceMessageBoxAction.YES)
                    return;

                Process.Start("explorer", msg.Path);

                return;
            }

            vm = new DocumentPluginModel(msg.Path, msg.FileName, pluginInfo, msg.Path) { Data = msg.Data };
            ArtDomain.Current.Documents.Add(vm);
            vm.IsActive = true;
        }

        #endregion

        #region FileRenameMessage -- 文件改名消息

        /// <summary>
        /// 文件改名消息
        /// </summary>
        private void OnFileRename(object sender, FileRenameMessage msg)
        {
            DocumentPluginModel? documentModel = this.Documents?.FirstOrDefault(p => string.Equals(p.File, msg.OldPath, StringComparison.OrdinalIgnoreCase));
            if (documentModel == null)
                return;

            documentModel.File = msg.Path;
            documentModel.Name = Path.GetFileName(msg.Path);
        }

        #endregion

        #region FileChangeMessage -- 文件改变消息

        /// <summary>
        /// 文件改变
        /// </summary>
        private void OnFileChange(object sender, FileChangeMessage msg)
        {
            DocumentPluginModel? documentModel = this.Documents?.FirstOrDefault(p => string.Equals(p.File, msg.Path, StringComparison.OrdinalIgnoreCase));
            if (documentModel == null || documentModel.View is not FrameworkElement documentView || documentView.DataContext is not IPanelViewModel panel)
                return;

            if (DanceMessageExpansion.ShowMessageBox("提升", DanceMessageBoxIcon.Info, $"文件: {documentModel.File} 发生改变，是否重新加载?", DanceMessageBoxAction.YES | DanceMessageBoxAction.NO) != DanceMessageBoxAction.YES)
                return;

            panel.Load();
        }

        #endregion

        #region FileStatusChangeMessage -- 文件状态改变消息

        /// <summary>
        /// 文件状态改变
        /// </summary>
        private void OnFileStatusChange(object sender, FileStatusChangeMessage msg)
        {
            this.IsSaveAllEnabled = this.Documents?.Any(p => p.View is FrameworkElement view && view.DataContext is IDocumentViewModel document && document.IsModify) ?? false;
        }

        #endregion

        #region PropertySelectedChangedMessage -- 属性选择改变消息

        /// <summary>
        /// 属性选择改变消息
        /// </summary>
        private void OnPropertySelectedChanged(object sender, PropertySelectedChangedMessage msg)
        {
            ArtDomain.Current.CurrentSelectedObject = msg.SelectedObject;
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
            await Task.Run(() =>
            {
                try
                {
                    if (this.ProjectDomain == null || string.IsNullOrWhiteSpace(this.ProjectDomain.ProjectFolderPath))
                        return;

                    this.ScriptDomain = new(file)
                    {
                        Engine = new(flags)
                    };
                    ArtDomain.Current.ScriptDomain = this.ScriptDomain;
                    this.ScriptDomain.Engine.DocumentSettings.AccessFlags = DocumentAccessFlags.EnableFileLoading;
                    this.ScriptDomain.Engine.DocumentSettings.SearchPath = this.ProjectDomain.ProjectFolderPath;
                    this.ScriptDomain.Engine.DocumentSettings.Loader.DiscardCachedDocuments();
                    this.ScriptDomain.Engine.AddHostObject("DANCE_ART_HOST", this.ScriptDomain.Host);
                    this.ScriptDomain.Engine.ExecuteDocument(file, ModuleCategory.Standard);
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
