using CommunityToolkit.Mvvm.Input;
using Dance.Art.Domain;
using Dance.Art.Storage;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Dance.Art.Module
{
    /// <summary>
    /// 创建项目视图模型
    /// </summary>
    public class CreateProjectWindowModel : DanceViewModel
    {
        public CreateProjectWindowModel()
        {
            this.LoadedCommand = new(this.Loaded);
            this.CreateCommand = new(this.Create);
            this.CancelCommand = new(this.Cancel);
            this.SelectFolderCommand = new(this.SelectFolder);
        }

        // ==============================================================================
        // Property

        #region IsEnabled -- 当前窗口是否可用

        private bool isEnabled = true;
        /// <summary>
        /// 当前窗口是否可用
        /// </summary>
        public bool IsEnabled
        {
            get { return isEnabled; }
            set { isEnabled = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region ProjectRoot -- 项目位置

        private string? projectRoot;
        /// <summary>
        /// 项目位置
        /// </summary>
        public string? ProjectRoot
        {
            get { return projectRoot; }
            set { projectRoot = value; this.OnPropertyChanged(); }
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

        #region ProjectDescription -- 项目描述

        private string? projectDescription;
        /// <summary>
        /// 项目描述
        /// </summary>
        public string? ProjectDescription
        {
            get { return projectDescription; }
            set { projectDescription = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region ProjectPath -- 项目路径

        private string? projectPath;
        /// <summary>
        /// 项目路径
        /// </summary>
        public string? ProjectPath
        {
            get { return projectPath; }
            set { projectPath = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Templates -- 模板集合

        private ObservableCollection<TemplatePluginInfo>? templates;
        /// <summary>
        /// 模板集合
        /// </summary>
        public ObservableCollection<TemplatePluginInfo>? Templates
        {
            get { return templates; }
            set { templates = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region SelectedTemplate -- 选中的模板

        private TemplatePluginInfo? selectedTemplate;
        /// <summary>
        /// 选中的模板
        /// </summary>
        public TemplatePluginInfo? SelectedTemplate
        {
            get { return selectedTemplate; }
            set { selectedTemplate = value; this.OnPropertyChanged(); }
        }

        #endregion

        // ==============================================================================
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

            this.Templates = domain.TemplatePlugins;
            this.SelectedTemplate = this.Templates.FirstOrDefault();
        }

        #endregion

        #region CreateCommand -- 创建命令

        /// <summary>
        /// 创建命令
        /// </summary>
        public AsyncRelayCommand CreateCommand { get; private set; }

        /// <summary>
        /// 创建
        /// </summary>
        private async Task Create()
        {
            try
            {
                if (this.View is not CreateProjectWindow window)
                    return;

                if (string.IsNullOrWhiteSpace(this.ProjectRoot))
                {
                    DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "请输入项目位置", DanceMessageBoxAction.YES);
                    return;
                }

                if (string.IsNullOrWhiteSpace(this.ProjectName))
                {
                    DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "请输入项目名称", DanceMessageBoxAction.YES);
                    return;
                }

                if (this.SelectedTemplate == null)
                {
                    DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "请输项目类型", DanceMessageBoxAction.YES);
                    return;
                }

                string folder = Path.Combine(this.ProjectRoot, this.ProjectName);
                if (Directory.Exists(folder) && (Directory.GetFiles(folder).Length > 0 || Directory.GetDirectories(folder).Length > 0))
                {
                    if (DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "项目文件夹不为空，是否继续创建?", DanceMessageBoxAction.YES | DanceMessageBoxAction.CANCEL) != DanceMessageBoxAction.YES)
                        return;
                }

                this.IsEnabled = false;

                await this.CreateProject(this.ProjectRoot, folder, this.ProjectName);

                window.DialogResult = true;
                window.Close();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                DanceMessageExpansion.ShowMessageBox("错误", DanceMessageBoxIcon.Warning, $"{ex.Message}", DanceMessageBoxAction.YES);
            }
            finally
            {
                this.IsEnabled = true;
            }
        }

        /// <summary>
        /// 创建项目
        /// </summary>
        /// <param name="root">项目位置</param>
        /// <param name="folder">项目文件夹</param>
        /// <param name="name">项目名称</param>
        private async Task CreateProject(string root, string folder, string name)
        {
            await Task.Run(() =>
            {
                if (this.SelectedTemplate == null)
                    return;

                // 创建项目文件夹
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                // 拷贝项目模板
                DanceFileHelper.CopyDirectory(this.SelectedTemplate.TemplateFolder, folder);

                // 创建项目文件
                ProjectNode project = new()
                {
                    Name = name,
                    Description = this.ProjectDescription,
                    ProjectTemplate = this.SelectedTemplate.ID,
                };

                this.ProjectPath = Path.Combine(folder, $"{name}.art");

                DanceFileHelper.WriteJson(project, this.ProjectPath);
            });
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
            FolderBrowserDialog fbd = new();

            if (fbd.ShowDialog() != DialogResult.OK)
                return;

            this.ProjectRoot = fbd.SelectedPath;
        }

        #endregion
    }
}
