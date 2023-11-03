using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Dance.Art.Module
{
    /// <summary>
    /// EditStringTemplateWindow.xaml 的交互逻辑
    /// </summary>
    public partial class EditStringTemplateWindow : Window
    {
        public EditStringTemplateWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="oldLabel">原始标签</param>
        /// <param name="oldValue">原始值</param>
        /// <param name="newLabel">新标签</param>
        /// <param name="newValue">新值</param>
        /// <param name="data">数据</param>
        /// <param name="executeFunc">执行方法</param>
        public EditStringTemplateWindow(string title, string? oldLabel, string? oldValue, string? newLabel, string? newValue, object? data, Func<EditStringTemplateWindowModel, bool> executeFunc) : this()
        {
            this.Title = title;

            EditStringTemplateWindowModel vm = new()
            {
                View = this,
                OldLabel = oldLabel,
                OldValue = oldValue,
                NewLabel = newLabel,
                NewValue = newValue,
                Data = data,
                ExecuteFunc = executeFunc
            };

            this.DataContext = vm;
        }
    }
}
