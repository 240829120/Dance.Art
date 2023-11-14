using CommunityToolkit.Mvvm.Input;
using Dance.Art.Domain;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.ButtonBox
{
    /// <summary>
    /// 命令按钮
    /// </summary>
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
