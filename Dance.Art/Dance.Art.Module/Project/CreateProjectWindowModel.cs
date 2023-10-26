using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Module
{
    /// <summary>
    /// 创建项目视图模型
    /// </summary>
    public class CreateProjectWindowModel : DanceViewModel
    {
        public CreateProjectWindowModel()
        {
            this.CreateCommand = new(this.Create);
            this.CancelCommand = new(this.Cancel);
            this.SelectFolderCommand = new(this.SelectFolder);
        }

        // ==============================================================================
        // Property

        #region ProjectFolder -- 项目文件夹

        private string? projectFolder;
        /// <summary>
        /// 文件夹
        /// </summary>
        public string? ProjectFolder
        {
            get { return projectFolder; }
            set { projectFolder = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region ProjectName -- 项目名称

        private string? projectName;
        /// <summary>
        /// 项目名称
        /// </summary>
        public string? ProjectName
        {
            get { return projectName; }
            set { projectName = value; this.OnPropertyChanged(); }
        }

        #endregion



        // ==============================================================================
        // Command

        #region CreateCommand -- 创建命令

        /// <summary>
        /// 创建命令
        /// </summary>
        public RelayCommand CreateCommand { get; private set; }

        /// <summary>
        /// 创建
        /// </summary>
        private void Create()
        {
            if (this.View is not CreateProjectWindow window)
                return;

            window.DialogResult = true;
            window.Close();
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
            if (this.View is not CreateProjectWindow window)
                return;

            window.DialogResult = false;
            window.Close();
        }

        #endregion

        #region SelectFolderCommand -- 选择文件夹命令

        /// <summary>
        /// 选择文件夹命令
        /// </summary>
        public RelayCommand SelectFolderCommand { get; private set; }

        /// <summary>
        /// 选择文件夹
        /// </summary>
        private void SelectFolder()
        {

        }

        #endregion
    }
}
