using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Dance.Art.Domain;

namespace Dance.Art
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            if (DateTime.Now >= DateTime.Parse("2024/3/1"))
            {
                MessageBox.Show("软件过期");
                this.Shutdown();
                return;
            }

            DanceDomain.Current = new ArtDomain();
            ArtDomain.Current.IocBuilder.AddAssemblys("Dance.Art.");
            ArtDomain.Current.PluginAssemblyPrefixes.Add("Dance.Art.");
            ArtDomain.Current.Build();

            IDanceMonitorManager monitorManager = DanceDomain.Current.LifeScope.Resolve<IDanceMonitorManager>();
            monitorManager.MonitorInfo = new DanceMonitorInfo();

            IWindowManager windowManager = DanceDomain.Current.LifeScope.Resolve<IWindowManager>();
            windowManager.WelcomeWindow = new WelcomeWindow();
            windowManager.MainWindow = new MainWindow();
            this.MainWindow = windowManager.MainWindow;

            windowManager.WelcomeWindow.Show();
        }
    }
}
