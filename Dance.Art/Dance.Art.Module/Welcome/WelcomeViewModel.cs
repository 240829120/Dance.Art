using CommunityToolkit.Mvvm.Input;
using Dance.Art.Domain;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.Module
{
    /// <summary>
    /// 欢迎视图模型
    /// </summary>
    [DanceSingleton]
    public class WelcomeViewModel : DanceViewModel
    {
        public WelcomeViewModel()
        {
            this.LoadedCommand = new(this.Loaded);
        }

        // =========================================================================================
        // Field

        /// <summary>
        /// 插件管理器
        /// </summary>
        private readonly IDancePluginManager PluginManager = DanceDomain.Current.LifeScope.Resolve<IDancePluginManager>();

        /// <summary>
        /// 窗口管理器
        /// </summary>
        private readonly IWindowManager WindowManager = DanceDomain.Current.LifeScope.Resolve<IWindowManager>();

        // =========================================================================================
        // Property 

        #region ProgressValue -- 进度值

        private double progressValue;
        /// <summary>
        /// 进度值
        /// </summary>
        public double ProgressValue
        {
            get { return progressValue; }
            set { progressValue = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region ProgressMessage -- 进度消息

        private string? progressMessage;
        /// <summary>
        /// 进度消息
        /// </summary>
        public string? ProgressMessage
        {
            get { return progressMessage; }
            set { progressMessage = value; this.OnPropertyChanged(); }
        }

        #endregion

        // =========================================================================================
        // Command

        #region LoadedCommand -- 加载命令

        /// <summary>
        /// 加载命令
        /// </summary>
        public AsyncRelayCommand LoadedCommand { get; private set; }

        /// <summary>
        /// 加载
        /// </summary>
        private async Task Loaded()
        {
            if (DanceDomain.Current is not ArtDomain domain)
                return;

            this.WindowManager.WelcomeWindow.Closed -= WelcomeWindow_Closed;
            this.WindowManager.WelcomeWindow.Closed += WelcomeWindow_Closed;

            this.ProgressValue = 0;
            this.ProgressMessage = "准备初始化";

            PluginManager.LoadPlugin("Dance.Art.Plugin");
            PluginManager.LoadPlugin("Dance.Art.Template");

            for (int i = 0; i < PluginManager.PluginDomains.Count; ++i)
            {
                IDancePluginInfo info = PluginManager.PluginDomains[i].PluginInfo;

                this.ProgressValue = (double)i / PluginManager.PluginDomains.Count;
                this.ProgressMessage = $"正在加载: {info.Name}";

                PluginManager.InitializePlugin(info.ID);

                // 面板插件
                if (info is PanelPluginInfo panel)
                {
                    domain.PanelPlugins.Add(panel);
                }
                // 文档插件
                else if (info is DocumentPluginInfo document)
                {
                    domain.DocumentPlugins.Add(document);
                }
                // 设置插件
                else if (info is SettingPluginInfo setting)
                {
                    domain.SettingPlugins.Add(setting);
                }
                // 模板插件
                else if (info is TemplatePluginInfo template)
                {
                    domain.TemplatePlugins.Add(template);
                }

                await Task.Delay(100);
            }

            this.ProgressValue = 1;
            this.ProgressMessage = "准备启动";

            await Task.Delay(2000);

            this.WindowManager.WelcomeWindow.Closed -= WelcomeWindow_Closed;
            WindowManager.WelcomeWindow.Close();
            WindowManager.MainWindow.Show();
        }

        /// <summary>
        /// 欢迎窗口关闭
        /// </summary>
        private void WelcomeWindow_Closed(object? sender, EventArgs e)
        {
            DanceDomain.Current?.Dispose();
            Application.Current.Shutdown();
        }

        #endregion
    }
}
