using CommunityToolkit.Mvvm.Input;
using Dance.Art.Domain;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.Plugin
{
    /// <summary>
    /// 新建文件夹窗口模型
    /// </summary>
    public class FileSourceNewFolderWindowModel : DanceViewModel
    {
        public FileSourceNewFolderWindowModel()
        {
            this.EnterCommand = new(this.Enter);
            this.CancelCommand = new(this.Cancel);
        }

        // ======================================================================================
        // Property

        #region FileModel -- 文件名模型

        private FileModel? fileModel;

        public FileModel? FileModel
        {
            get { return fileModel; }
            set { fileModel = value; this.OnPropertyChanged(); }
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

        // ======================================================================================
        // Command

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
                if (this.View is not Window window || this.FileModel == null || this.FileModel.Category == FileModelCategory.File)
                    return;

                if (string.IsNullOrWhiteSpace(this.FileName))
                {
                    DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "请输入名称", DanceMessageBoxAction.YES);
                    return;
                }

                string newPath = Path.Combine(this.FileModel.Path, this.FileName);
                if (Directory.Exists(newPath))
                {
                    DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, $"文件夹: {newPath} 已经存在", DanceMessageBoxAction.YES);
                    return;
                }

                Directory.CreateDirectory(newPath);

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
    }
}
