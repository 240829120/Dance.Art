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
    /// InputStringTemplateWindow.xaml 的交互逻辑
    /// </summary>
    public partial class InputStringTemplateWindow : Window
    {
        public InputStringTemplateWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 输入
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="inputLabel">原始标签</param>
        /// <param name="inputValue">新值</param>
        /// <param name="data">数据</param>
        /// <param name="executeFunc">执行方法</param>
        public InputStringTemplateWindow(string title, string? inputLabel, string? inputValue, object? data, Func<InputStringTemplateWindowModel, bool> executeFunc) : this()
        {
            this.Title = title;

            InputStringTemplateWindowModel vm = new()
            {
                View = this,
                InputLabel = inputLabel,
                InputValue = inputValue,
                Data = data,
                ExecuteFunc = executeFunc
            };

            this.DataContext = vm;
        }
    }
}
