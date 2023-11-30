using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
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
    /// 打开文件编辑器
    /// </summary>
    [TemplatePart(Name = nameof(PART_OpenButton), Type = typeof(Button))]
    public class OpenFileEditor : TextBox
    {
        static OpenFileEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(OpenFileEditor), new FrameworkPropertyMetadata(typeof(OpenFileEditor)));
        }

        // ===================================================================================
        // Field

        /// <summary>
        /// 打开按钮
        /// </summary>
        private Button? PART_OpenButton;

        // ===================================================================================
        // Property

        #region Filter -- 过滤器

        /// <summary>
        /// 过滤器
        /// </summary>
        public string Filter
        {
            get { return (string)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }

        /// <summary>
        /// 过滤器
        /// </summary>
        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(string), typeof(OpenFileEditor), new PropertyMetadata("all|*.*"));

        #endregion

        // ===================================================================================
        // Override

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.PART_OpenButton = this.Template.FindName(nameof(PART_OpenButton), this) as Button;
            if (this.PART_OpenButton != null)
            {
                this.PART_OpenButton.Click -= PART_OpenButton_Click;
                this.PART_OpenButton.Click += PART_OpenButton_Click;
            }
        }

        // ===================================================================================
        // Private Function

        /// <summary>
        /// 打开文件
        /// </summary>
        private void PART_OpenButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new()
            {
                Multiselect = false,
                Filter = this.Filter
            };

            if (ofd.ShowDialog() != true)
                return;

            this.Text = ofd.FileName;
        }
    }
}
