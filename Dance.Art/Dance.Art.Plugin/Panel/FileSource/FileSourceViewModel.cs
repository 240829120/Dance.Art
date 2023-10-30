using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Dance.Art.Domain;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Plugin
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

            // 初始化消息
            DanceDomain.Current.Messenger.Register<ProjectOpenMessage>(this, this.OnProjectOpen);
            DanceDomain.Current.Messenger.Register<ProjectCloseMessage>(this, this.OnProjectClose);

        }

        // ==================================================================================
        // Field

        /// <summary>
        /// 文件管理器
        /// </summary>
        private readonly IFileManager FileManager = DanceDomain.Current.LifeScope.Resolve<IFileManager>();

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

            DanceDomain.Current.Messenger.Send(new FileOpenMessage(fileModel));
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

                foreach (FileModel source in sources)
                {
                    string targetPath = Path.Combine(target.Path, source.FileName);

                    if (source.Category == FileModelCategory.Folder)
                    {
                        Directory.Move(source.Path, targetPath);
                    }
                    else if (source.Category == FileModelCategory.File)
                    {
                        File.Move(source.Path, targetPath);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);

                DanceMessageExpansion.ShowMessageBox("错误", DanceMessageBoxIcon.Failure, ex.Message, DanceMessageBoxAction.YES);
            }
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
            this.FileManager.Initialize(msg.NewProject);
            this.Files = this.FileManager.Root == null ? null : new() { this.FileManager.Root };
        }

        #endregion

        #region ProjectCloseMessage -- 项目关闭消息

        /// <summary>
        /// 执行项目关闭消息
        /// </summary>
        private void OnProjectClose(object sender, ProjectCloseMessage msg)
        {
            this.FileManager.Clear();
            this.Files = null;
        }

        #endregion

        // ==================================================================================
        // Private Function

        /// <summary>
        /// 获取选中的节点
        /// </summary>
        /// <returns>选中的节点</returns>
        //private List<FileModel> GetSelectedFileModel(FileModel root)
        //{

        //}
    }
}
