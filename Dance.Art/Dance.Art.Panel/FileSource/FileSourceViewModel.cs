using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Dance.Art.Domain;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace Dance.Art.Panel
{
    /// <summary>
    /// 文件源视图模型
    /// </summary>
    public class FileSourceViewModel : PanelViewModelBase
    {
        public FileSourceViewModel()
        {
            // 初始化命令
            this.LoadedCommand = new(this.Loaded);
            this.FileDoubleClickCommand = new(this.FileDoubleClick);
            this.FileDragBeginCommand = new(this.FileDragBegin);
            this.FileDragEnterCommand = new(this.FileDragEnter);
            this.FileDragLeaveCommand = new(this.FileDragLeave);
            this.FileDropCommand = new(this.FileDrop);
            this.FileOpenInBrowserCommand = new(this.FileOpenInBrowser, this.CanFileOpenInBrowser);
            this.FileOpenInDefaultCommand = new(this.FileOpenInDefault, this.CanFileOpenInDefault);
            this.FileCopyCommand = new(this.FileCopy, this.CanFileCopy);
            this.FileCutCommand = new(this.FileCut, this.CanFileCut);
            this.FilePasteCommand = new(this.FilePaste, this.CanFilePaste);
            this.FileDeleteCommand = new(this.FileDelete, this.CanFileDelete);
            this.FileRenameCommand = new(this.FileRename, this.CanFileRename);
            this.FileNewFolderCommand = new(this.FileNewFolder, this.CanFileNewFolder);
            this.FileNewCommand = new(this.FileNew, this.CanFileNew);
            this.ProjectSettingCommand = new(this.ProjectSetting, this.CanProjectSetting);

            // 初始化消息
            DanceDomain.Current.Messenger.Register<ProjectOpenMessage>(this, this.OnProjectOpen);
            DanceDomain.Current.Messenger.Register<ProjectClosedMessage>(this, this.OnProjectClosed);
        }

        // ==================================================================================
        // Field

        /// <summary>
        /// 文件管理器
        /// </summary>
        private readonly IFileManager FileManager = DanceDomain.Current.LifeScope.Resolve<IFileManager>();

        /// <summary>
        /// 窗口管理器
        /// </summary>
        private readonly IWindowManager WindowManager = DanceDomain.Current.LifeScope.Resolve<IWindowManager>();

        /// <summary>
        /// 等待剪切的文件集合
        /// </summary>
        private readonly List<FileModel> WaitForCutFiles = new();

        // ==================================================================================
        // Property

        #region Files -- 文件集合

        private ObservableCollection<FileModel>? files;
        /// <summary>
        /// 文件集合
        /// </summary>
        public ObservableCollection<FileModel>? Files
        {
            get { return files; }
            set { files = value; this.OnPropertyChanged(); }
        }

        #endregion

        // ==================================================================================
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
            if (ArtDomain.Current.ProjectDomain == null)
                return;

            this.FileManager.Initialize(ArtDomain.Current.ProjectDomain);
            this.Files = this.FileManager.Root == null ? null : new() { this.FileManager.Root };
        }

        #endregion

        #region FileDoubleClickCommand -- 文件双击命令

        /// <summary>
        /// 文件双击命令
        /// </summary>
        public RelayCommand<FileModel> FileDoubleClickCommand { get; private set; }

        /// <summary>
        /// 文件双击
        /// </summary>
        /// <param name="fileModel">文件模型</param>
        private void FileDoubleClick(FileModel? fileModel)
        {
            if (fileModel == null || fileModel.Category != FileModelCategory.File)
                return;

            DanceDomain.Current.Messenger.Send(new FileOpenMessage(fileModel.Path));
        }

        #endregion

        #region FileDragBeginCommand -- 文件拖拽开始命令

        /// <summary>
        /// 文件拖拽开始命令
        /// </summary>
        public RelayCommand<DanceDragBeginEventArgs> FileDragBeginCommand { get; private set; }

        /// <summary>
        /// 文件拖拽开始
        /// </summary>
        private void FileDragBegin(DanceDragBeginEventArgs? e)
        {
            if (e == null)
                return;

            if (e.Element.DataContext is not FileModel fileModel || fileModel == this.FileManager.Root)
            {
                e.IsCancel = true;
                return;
            }

            e.Data = this.FileManager.Root;
        }

        #endregion

        #region FileDragEnterCommand -- 文件拖拽进入命令

        /// <summary>
        /// 文件拖拽进入命令
        /// </summary>
        public RelayCommand<DanceDragEventArgs> FileDragEnterCommand { get; private set; }

        /// <summary>
        /// 文件拖拽进入
        /// </summary>
        private void FileDragEnter(DanceDragEventArgs? e)
        {
            if (e == null)
                return;

            e.EventArgs.Handled = true;
        }

        #endregion

        #region FileDragLeaveCommand -- 文件拖拽离开命令

        /// <summary>
        /// 文件拖拽离开命令
        /// </summary>
        public RelayCommand<DanceDragEventArgs> FileDragLeaveCommand { get; private set; }

        /// <summary>
        /// 文件拖拽离开
        /// </summary>
        private void FileDragLeave(DanceDragEventArgs? e)
        {
            if (e == null)
                return;

            e.EventArgs.Handled = true;
        }

        #endregion

        #region FileDropCommand -- 文件放置命令

        /// <summary>
        /// 文件放置命令
        /// </summary>
        public RelayCommand<DanceDragEventArgs> FileDropCommand { get; private set; }

        /// <summary>
        /// 文件放置
        /// </summary>
        private void FileDrop(DanceDragEventArgs? e)
        {
            try
            {
                if (e == null)
                    return;

                e.EventArgs.Handled = true;

                if (e.EventArgs.Data.GetData(typeof(FileModel)) is not FileModel dragData || dragData != this.FileManager.Root)
                    return;

                if (this.View is not FileSourceView view || e.Element.DataContext is not FileModel target || target.Category == FileModelCategory.File)
                    return;

                List<FileModel> sources = view.tree.GetSelectedValues().Cast<FileModel>().ToList();
                if (sources.Count == 0 || sources.Contains(target))
                    return;

                List<FileModel> files = this.FileManager.FilterFileModelForOperate(sources);

                DanceWin32Helper.Move(files.Select(p => p.Path).ToList(), target.Path);

                view.tree.ClearSelected();
                this.WaitForCutFiles.ForEach(p => p.IsWaitForCut = false);
                this.WaitForCutFiles.Clear();
            }
            catch (Exception ex)
            {
                log.Error(ex);

                DanceMessageExpansion.ShowMessageBox("错误", DanceMessageBoxIcon.Failure, ex.Message, DanceMessageBoxAction.YES);
            }
        }

        #endregion

        #region FileOpenInBrowserCommand -- 在资源管理器中打开文件命令

        /// <summary>
        /// 在资源管理器中打开文件命令
        /// </summary>
        public RelayCommand FileOpenInBrowserCommand { get; private set; }

        /// <summary>
        /// 是否可以在资源管理器中打开文件
        /// </summary>
        /// <returns></returns>
        private bool CanFileOpenInBrowser()
        {
            if (this.View is not FileSourceView view)
                return false;

            return view.tree.GetSelectedValues().Count == 1;
        }

        /// <summary>
        /// 在资源管理器中打开文件
        /// </summary>
        private void FileOpenInBrowser()
        {
            try
            {
                if (this.View is not FileSourceView view)
                    return;

                if (view.tree.GetSelectedValues().FirstOrDefault() is not FileModel file)
                    return;

                Process.Start("explorer.exe", $"/select,{file.Path}");
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        #endregion

        #region FileOpenInDefaultCommand -- 使用默认程序打开文件命令

        /// <summary>
        /// 使用默认程序打开文件命令
        /// </summary>
        public RelayCommand FileOpenInDefaultCommand { get; private set; }

        /// <summary>
        /// 是否可以使用默认程序打开文件
        /// </summary>
        /// <returns></returns>
        private bool CanFileOpenInDefault()
        {
            if (this.View is not FileSourceView view)
                return false;

            List<FileModel> files = view.tree.GetSelectedValues().Cast<FileModel>().ToList();

            return files.Count == 1 && files.First().Category == FileModelCategory.File;
        }

        /// <summary>
        /// 使用默认程序打开文件
        /// </summary>
        private void FileOpenInDefault()
        {
            try
            {
                if (this.View is not FileSourceView view)
                    return;

                if (view.tree.GetSelectedValues().FirstOrDefault() is not FileModel file || file.Category != FileModelCategory.File)
                    return;

                Process.Start("explorer", file.Path);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        #endregion

        #region FileCopyCommand -- 文件拷贝命令

        /// <summary>
        /// 文件拷贝命令
        /// </summary>
        public RelayCommand FileCopyCommand { get; private set; }

        /// <summary>
        /// 文件是否可以拷贝
        /// </summary>
        /// <returns></returns>
        private bool CanFileCopy()
        {
            if (this.ViewPluginModel == null || !this.ViewPluginModel.IsActive || this.FileManager.Root == null || this.View is not FileSourceView view)
                return false;

            List<FileModel> sources = view.tree.GetSelectedValues().Cast<FileModel>().ToList();

            return !sources.Contains(this.FileManager.Root) && sources.Count > 0;
        }

        /// <summary>
        /// 文件拷贝命令
        /// </summary>
        private void FileCopy()
        {
            try
            {
                if (this.ViewPluginModel == null || !this.ViewPluginModel.IsActive || this.View is not FileSourceView view)
                    return;

                this.WaitForCutFiles.ForEach(p => p.IsWaitForCut = false);
                this.WaitForCutFiles.Clear();

                List<FileModel> surces = view.tree.GetSelectedValues().Cast<FileModel>().ToList();
                List<FileModel> files = this.FileManager.FilterFileModelForOperate(surces);
                if (files.Count == 0)
                    return;

                StringCollection fileDropList = new();
                fileDropList.AddRange(files.Select(p => p.Path).ToArray());

                Clipboard.SetFileDropList(fileDropList);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                DanceMessageExpansion.ShowMessageBox("错误", DanceMessageBoxIcon.Failure, ex.Message, DanceMessageBoxAction.YES);
            }
        }

        #endregion

        #region FileCutCommand -- 文件剪切命令

        /// <summary>
        /// 文件剪切命令
        /// </summary>
        public RelayCommand FileCutCommand { get; private set; }

        /// <summary>
        /// 文件是否可以剪切
        /// </summary>
        /// <returns></returns>
        private bool CanFileCut()
        {
            if (this.ViewPluginModel == null || !this.ViewPluginModel.IsActive || this.FileManager.Root == null || this.View is not FileSourceView view)
                return false;

            List<FileModel> sources = view.tree.GetSelectedValues().Cast<FileModel>().ToList();

            return !sources.Contains(this.FileManager.Root) && sources.Count > 0;
        }

        /// <summary>
        /// 文件剪切
        /// </summary>
        private void FileCut()
        {
            try
            {
                if (this.ViewPluginModel == null || !this.ViewPluginModel.IsActive || this.View is not FileSourceView view)
                    return;

                List<FileModel> surces = view.tree.GetSelectedValues().Cast<FileModel>().ToList();
                List<FileModel> files = this.FileManager.FilterFileModelForOperate(surces);
                if (files.Count == 0)
                    return;

                this.WaitForCutFiles.ForEach(p => p.IsWaitForCut = false);
                this.WaitForCutFiles.Clear();
                this.WaitForCutFiles.AddRange(files);
                this.WaitForCutFiles.ForEach(p => p.IsWaitForCut = true);

                StringCollection fileDropList = new();
                fileDropList.AddRange(files.Select(p => p.Path).ToArray());

                Clipboard.SetFileDropList(fileDropList);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                DanceMessageExpansion.ShowMessageBox("错误", DanceMessageBoxIcon.Failure, ex.Message, DanceMessageBoxAction.YES);
            }
        }

        #endregion

        #region FilePasteCommand -- 文件粘贴命令

        /// <summary>
        /// 文件粘贴命令
        /// </summary>
        public RelayCommand FilePasteCommand { get; private set; }

        /// <summary>
        /// 文件是否可以被粘贴
        /// </summary>
        /// <returns></returns>
        private bool CanFilePaste()
        {
            if (this.ViewPluginModel == null || !this.ViewPluginModel.IsActive || this.View is not FileSourceView view)
                return false;

            if (view.tree.GetSelectedValues().Count != 1)
                return false;

            return Clipboard.ContainsFileDropList();
        }

        /// <summary>
        /// 文件粘贴
        /// </summary>
        private void FilePaste()
        {
            try
            {
                if (this.ViewPluginModel == null || !this.ViewPluginModel.IsActive || this.View is not FileSourceView view)
                    return;

                if (view.tree.GetSelectedValues().FirstOrDefault() is not FileModel file)
                    return;

                StringCollection fileDropList = Clipboard.GetFileDropList();
                if (fileDropList.Count == 0)
                    return;

                string? dst = file.Category == FileModelCategory.File ? file.Parent?.Path : file.Path;
                if (string.IsNullOrWhiteSpace(dst))
                    return;

                List<string> sources = new();
                foreach (string? source in fileDropList)
                {
                    if (string.IsNullOrWhiteSpace(source))
                        continue;

                    sources.Add(source);
                }

                if (sources.Count == 0)
                    return;

                if (sources.Any(p => string.Equals(Path.GetDirectoryName(p), dst)))
                {
                    DanceWin32Helper.Copy(sources, dst, true);
                }
                else
                {
                    DanceWin32Helper.Copy(sources, dst, false);
                }

                view.tree.ClearSelected();

                if (this.WaitForCutFiles.Count == 0)
                    return;

                List<FileModel> waitDeleteFiles = this.FileManager.FilterFileModelForOperate(this.WaitForCutFiles);

                this.WaitForCutFiles.ForEach(p => p.IsWaitForCut = false);
                this.WaitForCutFiles.Clear();

                if (waitDeleteFiles.Count == 0)
                    return;

                DanceWin32Helper.Delete(waitDeleteFiles.Select(p => p.Path).ToArray());
            }
            catch (Exception ex)
            {
                log.Error(ex);
                DanceMessageExpansion.ShowMessageBox("错误", DanceMessageBoxIcon.Failure, ex.Message, DanceMessageBoxAction.YES);
            }
        }

        #endregion

        #region FileDeleteCommand -- 文件删除命令

        /// <summary>
        /// 文件删除命令
        /// </summary>
        public RelayCommand FileDeleteCommand { get; private set; }

        /// <summary>
        /// 文件是否可以删除
        /// </summary>
        /// <returns></returns>
        private bool CanFileDelete()
        {
            if (this.ViewPluginModel == null || !this.ViewPluginModel.IsActive || this.FileManager.Root == null || this.View is not FileSourceView view)
                return false;

            List<FileModel> sources = view.tree.GetSelectedValues().Cast<FileModel>().ToList();

            return !sources.Contains(this.FileManager.Root) && sources.Count > 0;
        }

        /// <summary>
        /// 文件删除
        /// </summary>
        private void FileDelete()
        {
            try
            {
                if (this.ViewPluginModel == null || !this.ViewPluginModel.IsActive)
                    return;

                this.WaitForCutFiles.ForEach(p => p.IsWaitForCut = false);
                this.WaitForCutFiles.Clear();

                if (this.View is not FileSourceView view)
                    return;

                List<FileModel> sources = view.tree.GetSelectedValues().Cast<FileModel>().ToList();
                List<FileModel> files = this.FileManager.FilterFileModelForOperate(sources);

                if (files.Count == 0)
                    return;

                if (DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, $"是否删除选中项", DanceMessageBoxAction.YES | DanceMessageBoxAction.NO) != DanceMessageBoxAction.YES)
                    return;

                DanceWin32Helper.Delete(files.Select(p => p.Path).ToList());
                view.tree.ClearSelected();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                DanceMessageExpansion.ShowMessageBox("错误", DanceMessageBoxIcon.Failure, ex.Message, DanceMessageBoxAction.YES);
            }
        }

        #endregion

        #region FileRenameCommand -- 文件重命名命令

        /// <summary>
        /// 文件重命名命令
        /// </summary>
        public RelayCommand FileRenameCommand { get; private set; }

        /// <summary>
        /// 是否可以文件重命名
        /// </summary>
        /// <returns></returns>
        private bool CanFileRename()
        {
            if (this.ViewPluginModel == null || !this.ViewPluginModel.IsActive || this.FileManager.Root == null || this.View is not FileSourceView view)
                return false;

            return view.tree.GetSelectedValues().Count == 1;
        }

        /// <summary>
        /// 文件重命名
        /// </summary>
        private void FileRename()
        {
            if (this.ViewPluginModel == null || !this.ViewPluginModel.IsActive || this.FileManager.Root == null || this.View is not FileSourceView view)
                return;

            if (view.tree.GetSelectedValues().FirstOrDefault() is not FileModel file)
                return;

            FileSourceRenameWindow window = new(file)
            {
                Owner = this.WindowManager.MainWindow
            };

            window.ShowDialog();
        }

        #endregion

        #region FileNewFolderCommand -- 新建文件夹命令

        /// <summary>
        /// 新建文件夹命令
        /// </summary>
        public RelayCommand FileNewFolderCommand { get; private set; }

        /// <summary>
        /// 是否可以新建文件夹
        /// </summary>
        /// <returns></returns>
        private bool CanFileNewFolder()
        {
            if (this.ViewPluginModel == null || !this.ViewPluginModel.IsActive || this.FileManager.Root == null || this.View is not FileSourceView view)
                return false;

            List<FileModel> sources = view.tree.GetSelectedValues().Cast<FileModel>().ToList();

            return sources.Count == 1 && sources[0].Category != FileModelCategory.File;
        }

        /// <summary>
        /// 新建文件夹
        /// </summary>
        private void FileNewFolder()
        {
            if (this.ViewPluginModel == null || !this.ViewPluginModel.IsActive || this.FileManager.Root == null || this.View is not FileSourceView view)
                return;

            List<FileModel> sources = view.tree.GetSelectedValues().Cast<FileModel>().ToList();
            if (sources.Count != 1)
                return;

            FileModel file = sources[0];
            if (file.Category == FileModelCategory.File)
                return;

            FileSourceNewFolderWindow window = new(file)
            {
                Owner = this.WindowManager.MainWindow
            };

            window.ShowDialog();
        }

        #endregion

        #region FileNewCommand -- 新建文件命令

        /// <summary>
        /// 新建文件命令
        /// </summary>
        public RelayCommand FileNewCommand { get; private set; }

        /// <summary>
        /// 是否可以新建文件
        /// </summary>
        /// <returns></returns>
        private bool CanFileNew()
        {
            if (this.ViewPluginModel == null || !this.ViewPluginModel.IsActive || this.FileManager.Root == null || this.View is not FileSourceView view)
                return false;

            List<FileModel> sources = view.tree.GetSelectedValues().Cast<FileModel>().ToList();

            return sources.Count == 1 && sources[0].Category != FileModelCategory.File;
        }

        /// <summary>
        /// 新建文件命令
        /// </summary>
        private void FileNew()
        {
            if (this.ViewPluginModel == null || !this.ViewPluginModel.IsActive || this.FileManager.Root == null || this.View is not FileSourceView view)
                return;

            if (view.tree.GetSelectedValues().FirstOrDefault() is not FileModel file || file.Category == FileModelCategory.File)
                return;

            FileSourceNewFileWindow window = new(file)
            {
                Owner = this.WindowManager.MainWindow
            };
            window.ShowDialog();
        }

        #endregion

        #region ProjectSettingCommand -- 项目设置命令

        /// <summary>
        /// 项目设置命令
        /// </summary>
        public RelayCommand ProjectSettingCommand { get; private set; }

        /// <summary>
        /// 是否可以项目设置
        /// </summary>
        /// <returns></returns>
        private bool CanProjectSetting()
        {
            return DanceDomain.Current is ArtDomain artDomain && artDomain.ProjectDomain != null;
        }

        /// <summary>
        /// 项目设置
        /// </summary>
        private void ProjectSetting()
        {
            if (ArtDomain.Current.ProjectDomain == null || string.IsNullOrWhiteSpace(ArtDomain.Current.ProjectDomain.ProjectFilePath) || !File.Exists(ArtDomain.Current.ProjectDomain.ProjectFilePath))
                return;

            ArtDomain.Current.Messenger.Send(new FileOpenMessage(ArtDomain.Current.ProjectDomain.ProjectFilePath));
        }

        #endregion

        // ==================================================================================
        // Message

        #region ProjectOpenMessage -- 项目打开消息

        /// <summary>
        /// 执行项目打开消息
        /// </summary>
        private void OnProjectOpen(object sender, ProjectOpenMessage msg)
        {
            this.FileManager.Initialize(msg.ProjectDomain);
            this.Files = this.FileManager.Root == null ? null : new() { this.FileManager.Root };
        }

        #endregion

        #region ProjectClosedMessage -- 项目关闭消息

        /// <summary>
        /// 执行项目关闭消息
        /// </summary>
        private void OnProjectClosed(object sender, ProjectClosedMessage msg)
        {
            this.FileManager.Clear();
            this.Files = null;
        }

        #endregion

        // ==================================================================================
        // Private Function
    }
}
