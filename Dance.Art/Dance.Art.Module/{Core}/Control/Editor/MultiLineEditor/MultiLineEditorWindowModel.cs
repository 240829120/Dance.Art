using CommunityToolkit.Mvvm.Input;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Highlighting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.Module
{
    /// <summary>
    /// 多行文本编辑窗口模型
    /// </summary>
    public class MultiLineEditorWindowModel : DanceViewModel
    {
        public MultiLineEditorWindowModel()
        {
            this.CopyCommand = new(this.Copy, this.CanCopy);
            this.CutCommand = new(this.Cut, this.CanCut);
            this.PasteCommand = new(this.Paste);
            this.EnterCommand = new(this.Enter);
            this.CancelCommand = new(this.Cancel);
        }

        /// <summary>
        /// 高亮转化器
        /// </summary>
        private readonly HighlightingDefinitionTypeConverter HighlightingConverter = new();

        // ====================================================================================================
        // Property

        #region Script -- 脚本

        /// <summary>
        /// 脚本
        /// </summary>
        public string? Script
        {
            get { return this.GetEditor()?.Text; }
            set
            {
                TextEditor? editor = this.GetEditor();
                if (editor == null)
                    return;

                editor.Text = value;
            }
        }

        #endregion

        #region SyntaxHighlighting -- 高亮策略

        /// <summary>
        /// 高亮策略
        /// </summary>
        public string? SyntaxHighlighting
        {
            get { return this.GetEditor()?.SyntaxHighlighting?.ToString(); }
            set
            {
                TextEditor? editor = this.GetEditor();
                if (editor == null)
                    return;

                if (string.IsNullOrWhiteSpace(value))
                    editor.SyntaxHighlighting = null;
                else
                    editor.SyntaxHighlighting = HighlightingConverter.ConvertFromString(value) as IHighlightingDefinition;
            }
        }

        #endregion

        #region IsEnter -- 是否确定

        private bool isEnter;
        /// <summary>
        /// 是否确定
        /// </summary>
        public bool IsEnter
        {
            get { return isEnter; }
            set { isEnter = value; this.OnPropertyChanged(); }
        }

        #endregion

        // ====================================================================================================
        // Command

        #region CopyCommand -- 拷贝命令

        /// <summary>
        /// 拷贝命令
        /// </summary>
        public RelayCommand CopyCommand { get; private set; }

        /// <summary>
        /// 是否可以拷贝
        /// </summary>
        private bool CanCopy()
        {
            return this.GetEditor()?.SelectionLength != 0;
        }

        /// <summary>
        /// 拷贝
        /// </summary>
        private void Copy()
        {
            this.GetEditor()?.Copy();
        }

        #endregion

        #region CutCommand -- 剪切命令

        /// <summary>
        /// 剪切命令
        /// </summary>
        public RelayCommand CutCommand { get; private set; }

        /// <summary>
        /// 是否可以剪切
        /// </summary>
        private bool CanCut()
        {
            return this.GetEditor()?.SelectionLength != 0;
        }

        /// <summary>
        /// 剪切
        /// </summary>
        private void Cut()
        {
            this.GetEditor()?.Cut();
        }

        #endregion

        #region PasteCommand -- 粘贴命令

        /// <summary>
        /// 粘贴命令
        /// </summary>
        public RelayCommand PasteCommand { get; private set; }

        /// <summary>
        /// 粘贴
        /// </summary>
        private void Paste()
        {
            this.GetEditor()?.Paste();
        }

        #endregion

        #region EnterCommand -- 确定命令

        /// <summary>
        /// 确定命令
        /// </summary>
        public RelayCommand EnterCommand { get; private set; }

        /// <summary>
        /// 确定
        /// </summary>
        private void Enter()
        {
            if (this.View is not Window window)
                return;

            this.IsEnter = true;
            window.Close();
        }

        #endregion

        #region CancelCommand -- 取消命令

        /// <summary>
        /// 取消命令
        /// </summary>
        public RelayCommand CancelCommand { get; private set; }

        /// <summary>
        /// 取消
        /// </summary>
        private void Cancel()
        {
            if (this.View is not Window window)
                return;

            this.IsEnter = false;
            window.Close();
        }

        #endregion

        // ====================================================================================================
        // Private Function

        /// <summary>
        /// 获取编辑器
        /// </summary>
        /// <returns>编辑器</returns>
        protected TextEditor? GetEditor()
        {
            if (this.View is not MultiLineEditorWindow view)
                return null;

            return view.edit;
        }
    }
}
