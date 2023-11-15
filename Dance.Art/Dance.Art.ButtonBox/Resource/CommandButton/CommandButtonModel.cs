using CommunityToolkit.Mvvm.Input;
using Dance.Art.Domain;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Dance.Art.ButtonBox
{
    /// <summary>
    /// 命令按钮
    /// </summary>
    [DisplayName("命令按钮")]
    public class CommandButtonModel : ButtonBoxItemModelBase
    {
        /// <summary>
        /// 命令按钮
        /// </summary>
        public CommandButtonModel() : base(typeof(CommandButton))
        {
            this.ClickCommand = new(this.Click);
        }

        // ================================================================================
        // Property

        #region Content -- 内容

        private string? content;
        /// <summary>
        /// 内容
        /// </summary>
        [Category(PropertyCategoryDefines.OTHER), Description("内容"), DisplayName("内容")]
        public string? Content
        {
            get { return content; }
            set { content = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        #region OnClick -- 点击脚本

        private string? onClick;
        /// <summary>
        /// 点击脚本
        /// </summary>
        [Category(PropertyCategoryDefines.OTHER), Description("点击执行脚本"), DisplayName("点击脚本"))]
        public string? OnClick
        {
            get { return onClick; }
            set { onClick = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        // ================================================================================
        // Command

        #region ClickCommand -- 点击命令

        /// <summary>
        /// 点击命令
        /// </summary>
        [Browsable(false)]
        public RelayCommand ClickCommand { get; private set; }

        /// <summary>
        /// 点击
        /// </summary>
        private void Click()
        {
            DanceMessageExpansion.ShowMessageBox("点击命令按钮", $"{this.Content}");
        }

        #endregion
    }
}
