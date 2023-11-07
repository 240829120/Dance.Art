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
using System.Windows;

namespace Dance.Art.Panel
{
    /// <summary>
    /// 新建文件窗口模型
    /// </summary>
    public class FileSourceNewFileWindowModel : DanceViewModel
    {
        public FileSourceNewFileWindowModel()
        {
            this.LoadedCommand = new(this.Loaded);
            this.EnterCommand = new(this.Enter);
            this.CancelCommand = new(this.Cancel);
        }

        // ======================================================================================
        // Field

        /// <summary>
        /// 文档文件信息管理器
        /// </summary>
        private readonly IDocumentFileInfoManager DocumentFileInfoManager = DanceDomain.Current.LifeScope.Resolve<IDocumentFileInfoManager>();

        // ======================================================================================
        // Property

        #region FileModel -- 文件模型

        private FileModel? fileModel;
        /// <summary>
        /// 文件模型
        /// </summary>
        public FileModel? FileModel
        {
            get { return fileModel; }
            set { fileModel = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Folder -- 文件夹

        private string? folder;
        /// <summary>
        /// 文件夹
        /// </summary>
        public string? Folder
        {
            get { return folder; }
            set { folder = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region FileName -- 文件名

        private string? fileName;
        /// <summary>
        /// 文件名
        /// </summary>
        public string? FileName
        {
            get { return fileName; }
            set { fileName = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region GroupInfos -- 分组信息集合

        private List<DocumentFileGroupInfo>? groupInfos;
        /// <summary>
        /// 分组信息集合
        /// </summary>
        public List<DocumentFileGroupInfo>? GroupInfos
        {
            get { return groupInfos; }
            set { groupInfos = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region FileInfos -- 文件信息集合

        private IReadOnlyList<DocumentFileInfo>? fileInfos;
        /// <summary>
        /// 文件信息集合
        /// </summary>
        public IReadOnlyList<DocumentFileInfo>? FileInfos
        {
            get { return fileInfos; }
            set { fileInfos = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region SelectedGroupInfo -- 当前选中的分组

        private DocumentFileGroupInfo? selectedGroupInfo;
        /// <summary>
        /// 当前选中的分组
        /// </summary>
        public DocumentFileGroupInfo? SelectedGroupInfo
        {
            get { return selectedGroupInfo; }
            set
            {
                selectedGroupInfo = value;
                this.OnPropertyChanged();

                this.FileInfos = value?.FileInfos;
                this.SelectedFileInfo = value?.FileInfos?.FirstOrDefault();
            }
        }

        #endregion

        #region SelectedFileInfo -- 当前选中的文件信息

        private DocumentFileInfo? selectedFileInfo;
        /// <summary>
        /// 当前选中的文件信息
        /// </summary>
        public DocumentFileInfo? SelectedFileInfo
        {
            get { return selectedFileInfo; }
            set
            {
                selectedFileInfo = value;
                this.OnPropertyChanged();

                this.UpdateFileExtension();
            }
        }

        #endregion

        // ======================================================================================
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
            this.GroupInfos = this.DocumentFileInfoManager.DocumentFileGroupInfos;
            this.SelectedGroupInfo = this.GroupInfos?.FirstOrDefault();
        }

        #endregion

        #region EnterCommand -- 确定命令

        /// <summary>
        /// 确定命令
        /// </summary>
        public RelayCommand EnterCommand { get; private set; }

        /// <summary>
        /// 确定
        /// </summary>
        private void Enter()
        {
            try
            {
                if (this.View is not Window window || this.FileModel == null || string.IsNullOrWhiteSpace(this.Folder))
                    return;

                if (string.IsNullOrWhiteSpace(this.FileName))
                {
                    DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "请输入文件名", DanceMessageBoxAction.YES);
                    return;
                }

                if (!Directory.Exists(this.Folder))
                {
                    DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, $"文件夹: {this.Folder} 不存在", DanceMessageBoxAction.YES);
                    return;
                }

                string path = Path.Combine(this.Folder, this.FileName);
                if (File.Exists(path))
                {
                    DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, $"文件: {path} 已经存在", DanceMessageBoxAction.YES);
                    return;
                }

                File.Create(path).Dispose();

                ArtDomain.Current.Messenger.Send(new FileOpenMessage(path));

                window.DialogResult = true;
                window.Close();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                DanceMessageExpansion.ShowMessageBox("错误", DanceMessageBoxIcon.Failure, ex.Message, DanceMessageBoxAction.YES);
            }
        }

        #endregion

        #region CancelCommand -- 取消命令

        /// <summary>
        /// 取消命令
        /// </summary>
        public RelayCommand CancelCommand { get; private set; }

        /// <summary>
        /// 取消
        /// </summary>
        private void Cancel()
        {
            if (this.View is not Window window)
                return;

            window.DialogResult = false;
            window.Close();
        }

        #endregion

        // ======================================================================================
        // Private Function

        /// <summary>
        /// 更新文件后缀
        /// </summary>
        private void UpdateFileExtension()
        {
            if (this.SelectedFileInfo == null || string.IsNullOrWhiteSpace(this.SelectedFileInfo.Extension))
                return;

            int index = this.FileName?.LastIndexOf('.') ?? -1;
            if (index < 0)
            {
                this.FileName = $"{this.FileName}{this.SelectedFileInfo.Extension}";
            }
            else
            {
                this.FileName = $"{this.FileName?[..index]}{this.SelectedFileInfo.Extension}";
            }
        }
    }
}
