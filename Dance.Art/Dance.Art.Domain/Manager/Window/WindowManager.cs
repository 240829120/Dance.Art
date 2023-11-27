using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 窗口管理器
    /// </summary>
    [DanceSingleton(typeof(IWindowManager))]
    public class WindowManager : IWindowManager
    {
        /// <summary>
        /// 欢迎窗口
        /// </summary>
        [NotNull]
        public Window? WelcomeWindow { get; set; }

        /// <summary>
        /// 主窗口
        /// </summary>
        [NotNull]
        public Window? MainWindow { get; set; }
    }
}
