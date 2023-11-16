using Dance.Art.Domain;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dance.Art.Module
{
    /// <summary>
    /// 脚本编辑器
    /// </summary>
    public class ScriptEditor : Button
    {
        static ScriptEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScriptEditor), new FrameworkPropertyMetadata(typeof(ScriptEditor)));
        }

        /// <summary>
        /// 窗口管理器
        /// </summary>
        private readonly IWindowManager WindowManager = DanceDomain.Current.LifeScope.Resolve<IWindowManager>();

        #region Script -- 脚本

        /// <summary>
        /// 脚本
        /// </summary>
        public string? Script
        {
            get { return (string)GetValue(ScriptProperty); }
            set { SetValue(ScriptProperty, value); }
        }

        /// <summary>
        /// 脚本
        /// </summary>
        public static readonly DependencyProperty ScriptProperty =
            DependencyProperty.Register("Script", typeof(string), typeof(ScriptEditor), new PropertyMetadata(null));

        #endregion

        /// <summary>
        /// 点击
        /// </summary>
        protected override void OnClick()
        {
            ScriptEditorWindow window = new()
            {
                Owner = this.WindowManager.MainWindow
            };
            if (window.DataContext is not ScriptEditorWindowModel vm)
                return;

            vm.Script = this.Script;
            window.Closed += (s, e) =>
            {
                if (!vm.IsEnter)
                    return;

                this.Script = vm.Script;
            };

            window.Show();
        }
    }
}
