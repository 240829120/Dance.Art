using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Dance.Art.Domain;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Plugin
{
    /// <summary>
    /// 文件源视图模型
    /// </summary>
    [DanceSingleton]
    public class FileSourceViewModel : DanceViewModel
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

        #region SelectedFile -- 当前选中的文件

        private FileModel? selectedFile;
        /// <summary>
        /// 当前选中的文件
        /// </summary>
        public FileModel? SelectedFile
        {
            get { return selectedFile; }
            set { selectedFile = value; this.OnPropertyChanged(); }
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
            if (e == null || e.Element.DataContext is not FileModel fileModel)
                return;

            e.Data = fileModel;
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
            if (e == null || e.Element.DataContext is not FileModel fileModel)
                return;

            fileModel.IsHighlight = true;
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
            if (e == null || e.Element.DataContext is not FileModel fileModel)
                return;

            fileModel.IsHighlight = false;
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
            if (e == null || e.Element.DataContext is not FileModel fileModel)
                return;

            fileModel.IsHighlight = false;
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
            this.Files = this.FileManager.Root == null ? null : new() { this.FileManager.Root };
        }

        #endregion
    }
}
