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

namespace Dance.Art.Panel
{
    /// <summary>
    /// 重命名窗口模型
    /// </summary>
    public class FileSourceRenameWindowModel : DanceViewModel
    {
        public FileSourceRenameWindowModel()
        {
            this.EnterCommand = new(this.Enter);
            this.CancelCommand = new(this.Cancel);
        }

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

        #region NewFileName -- 新文件名

        private string? newFileName;
        /// <summary>
        /// 新文件名
        /// </summary>
        public string? NewFileName
        {
            get { return newFileName; }
            set { newFileName = value; this.OnPropertyChanged(); }
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
                if (this.View is not Window window || this.FileModel == null)
                    return;

                if (string.IsNullOrWhiteSpace(this.NewFileName))
                {
                    DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "请输入新的名称", DanceMessageBoxAction.YES);
                    return;
                }

                if (this.FileModel.Category == FileModelCategory.File && !File.Exists(this.FileModel.Path))
                {
                    DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Failure, $"文件: {this.FileModel.Path} 不存在", DanceMessageBoxAction.YES);
                    return;
                }

                if (this.FileModel.Category != FileModelCategory.File && !Directory.Exists(this.FileModel.Path))
                {
                    DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Failure, $"文件夹: {this.FileModel.Path} 不存在", DanceMessageBoxAction.YES);
                    return;
                }

                string? dir = Path.GetDirectoryName(this.FileModel.Path);
                if (string.IsNullOrWhiteSpace(dir))
                {
                    DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Failure, $"路径: {this.FileModel.Path} 不正确", DanceMessageBoxAction.YES);
                    return;
                }
                string newPath = Path.Combine(dir, this.NewFileName);

                if (this.FileModel.Category == FileModelCategory.File && File.Exists(newPath))
                {
                    DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Failure, $"文件: {newPath} 已经存在", DanceMessageBoxAction.YES);
                    return;
                }

                if (this.FileModel.Category != FileModelCategory.File && Directory.Exists(newPath))
                {
                    DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Failure, $"文件夹: {newPath} 已经存在", DanceMessageBoxAction.YES);
                    return;
                }

                DanceWin32Helper.Rename(this.FileModel.Path, newPath);

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
