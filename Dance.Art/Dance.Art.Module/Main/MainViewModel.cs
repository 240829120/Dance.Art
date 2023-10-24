using AvalonDock.Layout.Serialization;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Dance.Art.Domain;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            this.SaveLayoutCommand = new(this.SaveLayout);
            this.LoadLayoutCommand = new(this.LoadLayout);

            this.OpenProjectCommand = new(this.OpenProject);
        }

        // ========================================================================================
        // Field

        /// <summary>
        /// 文件管理器
        /// </summary>
        private readonly IFileManager FileManager = DanceDomain.Current.LifeScope.Resolve<IFileManager>();

        // ========================================================================================
        // Property

        #region Panels -- 面板集合

        private ObservableCollection<PluginViewModel>? panels;

        /// <summary>
        /// 面板集合
        /// </summary>
        public ObservableCollection<PluginViewModel>? Panels
        {
            get { return panels; }
            private set { panels = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Documents -- 文档集合

        private ObservableCollection<PluginViewModel>? documents;

        /// <summary>
        /// 面板集合
        /// </summary>
        public ObservableCollection<PluginViewModel>? Documents
        {
            get { return documents; }
            private set { documents = value; this.OnPropertyChanged(); }
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
            if (DanceDomain.Current is not ArtDomain domain)
                return;

            this.Panels = domain.Panels;
            this.Documents = domain.Documents;

            this.LoadLayout();
        }

        #endregion

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
            if (DanceDomain.Current is not ArtDomain artDomain)
                return;

            ProjectDomain domain = new(@"E:\test_project\test.art");

            this.FileManager.Initialize(domain);

            ProjectOpenMessage msg = new()
            {
                OldProject = artDomain.ProjectDomain,
                NewProject = domain,
            };

            artDomain.ProjectDomain = domain;

            artDomain.Messenger.Send(msg);
        }

        #endregion
    }
}
