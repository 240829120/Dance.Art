using CommunityToolkit.Mvvm.Input;
using Dance.Art.Domain;
using Dance.Art.Plugin.Document;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.Plugin
{
    /// <summary>
    /// Json视图模型
    /// </summary>
    public class JsonViewModel : DocumentViewModelBase
    {
        // ==========================================================================================
        // Public Function

        /// <summary>
        /// 加载
        /// </summary>
        public override void Load()
        {
            if (this.View is not JsonView view || this.DocumentModel == null)
                return;

            view.edit.Load(this.DocumentModel.File);
        }

        /// <summary>
        /// 保存
        /// </summary>
        public override void Save()
        {
            if (this.View is not JsonView view || this.DocumentModel == null)
                return;

            if (!view.edit.IsModified)
                return;

            view.edit.Save(this.DocumentModel.File);
        }

        /// <summary>
        /// 重做
        /// </summary>
        public override void Redo()
        {
            if (this.View is not JsonView view)
                return;

            view.edit.Redo();
        }

        /// <summary>
        /// 撤销
        /// </summary>
        public override void Undo()
        {
            if (this.View is not JsonView view)
                return;

            view.edit.Undo();
        }

        // ==========================================================================================
        // Override Function

        /// <summary>
        /// 更新文档状态
        /// </summary>
        protected override void UpdateDocumentStatus()
        {
            if (this.View is not JsonView view)
                return;

            this.IsModify = view.edit.IsModified;
            this.CanRedo = view.edit.CanRedo;
            this.CanUndo = view.edit.CanUndo;
        }

        /// <summary>
        /// 是否可以拷贝
        /// </summary>
        /// <returns>是否可以拷贝</returns>
        protected override bool CanCopy()
        {
            if (this.View is not JsonView view)
                return false;

            return view.edit.SelectionLength != 0;
        }

        /// <summary>
        /// 拷贝
        /// </summary>
        protected override void Copy()
        {
            if (this.View is not JsonView view)
                return;

            view.edit.Copy();
        }

        /// <summary>
        /// 是否可以剪切
        /// </summary>
        protected override bool CanCut()
        {
            if (this.View is not JsonView view)
                return false;

            return view.edit.SelectionLength != 0;
        }

        /// <summary>
        /// 剪切
        /// </summary>
        protected override void Cut()
        {
            if (this.View is not JsonView view)
                return;

            view.edit.Cut();
        }

        /// <summary>
        /// 粘贴
        /// </summary>
        protected override void Paste()
        {
            if (this.View is not JsonView view)
                return;

            view.edit.Paste();
        }
    }
}