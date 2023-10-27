using Dance.Art.Plugin.Document;
using ICSharpCode.AvalonEdit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Plugin
{
    /// <summary>
    /// Avalonedit编辑文档视图模型
    /// </summary>
    public abstract class AvaloneditDocumentViewModelBase : DocumentViewModelBase
    {
        // ==========================================================================================
        // Property

        /// <summary>
        /// 是否修改
        /// </summary>
        public override bool IsModify { get { return this.GetEditor()?.IsModified ?? false; } }

        /// <summary>
        /// 是否可以重做
        /// </summary>
        public override bool CanRedo { get { return this.GetEditor()?.CanRedo ?? false; } }

        /// <summary>
        /// 是否可以撤销
        /// </summary>
        public override bool CanUndo { get { return this.GetEditor()?.CanUndo ?? false; } }

        // ==========================================================================================
        // Public Function

        /// <summary>
        /// 加载
        /// </summary>
        public override void Load()
        {
            if (this.DocumentModel == null)
                return;

            TextEditor? edit = this.GetEditor();
            if (edit == null)
                return;

            edit.TextChanged -= Edit_TextChanged;
            edit.TextChanged += Edit_TextChanged;

            edit.Load(this.DocumentModel.File);
            this.UdateDocumentStatus();
        }

        /// <summary>
        /// 保存
        /// </summary>
        public override void Save()
        {
            if (this.DocumentModel == null)
                return;

            this.GetEditor()?.Save(this.DocumentModel.File);
            this.UdateDocumentStatus();
        }

        /// <summary>
        /// 重做
        /// </summary>
        public override void Redo()
        {
            this.GetEditor()?.Redo();
        }

        /// <summary>
        /// 撤销
        /// </summary>
        public override void Undo()
        {
            this.GetEditor()?.Undo();
        }

        // ==========================================================================================
        // Override Function

        /// <summary>
        /// 是否可以拷贝
        /// </summary>
        /// <returns>是否可以拷贝</returns>
        protected override bool CanCopy()
        {
            return this.GetEditor()?.SelectionLength != 0;
        }

        /// <summary>
        /// 拷贝
        /// </summary>
        protected override void Copy()
        {
            this.GetEditor()?.Copy();
        }

        /// <summary>
        /// 是否可以剪切
        /// </summary>
        protected override bool CanCut()
        {
            return this.GetEditor()?.SelectionLength != 0;
        }

        /// <summary>
        /// 剪切
        /// </summary>
        protected override void Cut()
        {
            this.GetEditor()?.Cut();
        }

        /// <summary>
        /// 粘贴
        /// </summary>
        protected override void Paste()
        {
            this.GetEditor()?.Paste();
        }

        /// <summary>
        /// 获取编辑器
        /// </summary>
        /// <returns>编辑器</returns>
        protected abstract TextEditor? GetEditor();

        /// <summary>
        /// 文本更新
        /// </summary>
        private void Edit_TextChanged(object? sender, EventArgs e)
        {
            this.UdateDocumentStatus();
        }
    }
}
