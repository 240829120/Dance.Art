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
    /// 多行文本编辑器
    /// </summary>
    public class MultiLineEditor : Button
    {
        static MultiLineEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MultiLineEditor), new FrameworkPropertyMetadata(typeof(MultiLineEditor)));
        }

        /// <summary>
        /// 窗口管理器
        /// </summary>
        private readonly IWindowManager WindowManager = DanceDomain.Current.LifeScope.Resolve<IWindowManager>();

        #region Icon -- 图标

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        /// <summary>
        /// 图标
        /// </summary>
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(string), typeof(MultiLineEditor), new PropertyMetadata(null));

        #endregion

        #region EditValue -- 编辑值

        /// <summary>
        /// 编辑值
        /// </summary>
        public string EditValue
        {
            get { return (string)GetValue(EditValueProperty); }
            set { SetValue(EditValueProperty, value); }
        }

        /// <summary>
        /// 编辑值
        /// </summary>
        public static readonly DependencyProperty EditValueProperty =
            DependencyProperty.Register("EditValue", typeof(string), typeof(MultiLineEditor), new PropertyMetadata(null));

        #endregion

        #region SyntaxHighlighting -- 高亮策略

        /// <summary>
        /// 高亮策略
        /// </summary>
        public string SyntaxHighlighting
        {
            get { return (string)GetValue(SyntaxHighlightingProperty); }
            set { SetValue(SyntaxHighlightingProperty, value); }
        }

        /// <summary>
        /// 高亮策略
        /// </summary>
        public static readonly DependencyProperty SyntaxHighlightingProperty =
            DependencyProperty.Register("SyntaxHighlighting", typeof(string), typeof(MultiLineEditor), new PropertyMetadata("Txt"));

        #endregion

        #region WindowTitle -- 窗口标题

        /// <summary>
        /// 窗口标题
        /// </summary>
        public string WindowTitle
        {
            get { return (string)GetValue(WindowTitleProperty); }
            set { SetValue(WindowTitleProperty, value); }
        }

        /// <summary>
        /// 窗口标题
        /// </summary>
        public static readonly DependencyProperty WindowTitleProperty =
            DependencyProperty.Register("WindowTitle", typeof(string), typeof(MultiLineEditor), new PropertyMetadata("脚本编辑"));

        #endregion

        /// <summary>
        /// 点击
        /// </summary>
        protected override void OnClick()
        {
            MultiLineEditorWindow window = new()
            {
                Title = this.WindowTitle,
                Owner = this.WindowManager.MainWindow
            };
            if (window.DataContext is not MultiLineEditorWindowModel vm)
                return;

            vm.SyntaxHighlighting = this.SyntaxHighlighting;
            vm.Script = this.EditValue;
            window.Closed += (s, e) =>
            {
                if (!vm.IsEnter)
                    return;

                this.EditValue = vm.Script;
            };

            window.Show();
        }
    }
}
