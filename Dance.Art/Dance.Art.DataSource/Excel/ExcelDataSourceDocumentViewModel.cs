using CommunityToolkit.Mvvm.Input;
using Dance.Art.Domain;
using Dance.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.DataSource
{
    /// <summary>
    /// Excel文档视图模型
    /// </summary>
    public class ExcelDataSourceDocumentViewModel : DataSourceDocumentViewModelBase, IDataSourceDocumentViewModel
    {
        public ExcelDataSourceDocumentViewModel()
        {
            this.SelectFileCommand = new(this.SelectFile);
        }

        // ================================================================================================
        // Property

        #region Path -- 文件路径

        private string? path;
        /// <summary>
        /// 文件路径
        /// </summary>
        public string? Path
        {
            get { return path; }
            set { path = value; this.OnPropertyChanged(); }
        }

        #endregion

        // ================================================================================================
        // Command

        #region SelectFileCommand -- 选择文件命令

        /// <summary>
        /// 选择文件命令
        /// </summary>
        public RelayCommand SelectFileCommand { get; private set; }

        /// <summary>
        /// 选择文件
        /// </summary>
        private void SelectFile()
        {
            if (ArtDomain.Current.ProjectDomain == null || string.IsNullOrWhiteSpace(ArtDomain.Current.ProjectDomain.ProjectFolderPath))
                return;

            OpenFileDialog ofd = new()
            {
                Filter = "xls files (*.xls)|*.xls",
                Multiselect = false
            };

            if (ofd.ShowDialog() != true || string.IsNullOrWhiteSpace(ofd.FileName) || !File.Exists(ofd.FileName))
                return;

            if (ofd.FileName.StartsWith(ArtDomain.Current.ProjectDomain.ProjectFolderPath))
            {
                this.Path = System.IO.Path.GetRelativePath(ArtDomain.Current.ProjectDomain.ProjectFolderPath, ofd.FileName);
            }
            else
            {
                this.Path = ofd.FileName;
            }
        }

        #endregion

        // ================================================================================================
        // Override

        /// <summary>
        /// 加载
        /// </summary>
        public override void Load()
        {
            base.Load();

            if (this.Model == null || this.Model.Source is not ExcelDataSourceModel sourceModel)
                return;

            this.Name = this.Model?.Name;
            this.Description = this.Model?.Description;
            this.Path = sourceModel.Path;
        }

        /// <summary>
        /// 确定
        /// </summary>
        protected override void Enter()
        {
            try
            {
                if (this.Model == null || this.Model.Source is not ExcelDataSourceModel sourceModel)
                    return;

                if (!this.CheckName())
                    return;

                this.ChangeDocumentTitle();
                this.Model.Name = this.Name;
                this.Model.Description = this.Description;
                sourceModel.Path = this.Path;

                this.SaveDataSourceGroups();
                sourceModel.SaveToStorage();

                sourceModel.Load();

                DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "应用成功", DanceMessageBoxAction.YES);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                DanceMessageExpansion.ShowMessageBox("错误", DanceMessageBoxIcon.Failure, ex.Message, DanceMessageBoxAction.YES);
            }
        }
    }
}