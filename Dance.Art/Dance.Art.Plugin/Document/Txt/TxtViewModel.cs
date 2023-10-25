using CommunityToolkit.Mvvm.Input;
using Dance.Art.Domain;
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
    /// 文本视图模型
    /// </summary>
    public class TxtViewModel : DanceViewModel, IDockingDocument
    {
        /// <summary>
        /// 文本视图模型
        /// </summary>
        public TxtViewModel()
        {
            // 初始化命令
            this.CopyCommand = new(this.Copy, this.CanCopy);
            this.CutCommand = new(this.Cut, this.CanCut);
            this.PasteCommand = new(this.Paste);

            // 注册状态更新
            this.LoopKey = $"TxtViewModel_{this.GetHashCode()}";
            this.LoopManager.Register(this.LoopKey, 1, this.UpdateDocumentStatus);
        }

        // ==========================================================================================
        // Field

        /// <summary>
        /// 循环键
        /// </summary>
        private readonly string LoopKey;

        /// <summary>
        /// 循环管理器
        /// </summary>
        private readonly IDanceLoopManager LoopManager = DanceDomain.Current.LifeScope.Resolve<IDanceLoopManager>();

        // ==========================================================================================
        // Property

        #region IsModify -- 是否修改

        private bool isModify;
        /// <summary>
        /// 是否修改
        /// </summary>
        public bool IsModify
        {
            get { return isModify; }
            set { isModify = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region CanRedo -- 是否可以重做

        private bool canRedo;
        /// <summary>
        /// 是否可以重做
        /// </summary>
        public bool CanRedo
        {
            get { return canRedo; }
            set { canRedo = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region CanUndo -- 是否可以撤销

        private bool canUndo;
        /// <summary>
        /// 是否可以撤销
        /// </summary>
        public bool CanUndo
        {
            get { return canUndo; }
            set { canUndo = value; this.OnPropertyChanged(); }
        }

        #endregion

        // ==========================================================================================
        // Command

        #region CopyCommand -- 复制命令

        /// <summary>
        /// 复制命令
        /// </summary>
        public RelayCommand CopyCommand { get; private set; }

        /// <summary>
        /// 是否可以复制
        /// </summary>
        /// <returns>是否可以复制</returns>
        private bool CanCopy()
        {
            if (this.View is not TxtView view)
                return false;

            return view.edit.SelectionLength > 0;
        }

        /// <summary>
        /// 复制
        /// </summary>
        private void Copy()
        {
            if (this.View is not TxtView view)
                return;

            view.edit.Copy();
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
        /// <returns>是否可以剪切</returns>
        private bool CanCut()
        {
            if (this.View is not TxtView view)
                return false;

            return view.edit.SelectionLength > 0;
        }

        /// <summary>
        /// 剪切
        /// </summary>
        private void Cut()
        {
            if (this.View is not TxtView view)
                return;

            view.edit.Cut();
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
            if (this.View is not TxtView view)
                return;

            view.edit.Paste();
        }

        #endregion

        // ==========================================================================================
        // Public Function

        /// <summary>
        /// 保存
        /// </summary>
        public void Save()
        {
            if (this.View is not TxtView view)
                return;


        }

        /// <summary>
        /// 重做
        /// </summary>
        public void Redo()
        {
            if (this.View is not TxtView view)
                return;

            view.edit.Redo();
        }

        /// <summary>
        /// 撤销
        /// </summary>
        public void Undo()
        {
            if (this.View is not TxtView view)
                return;

            view.edit.Undo();
        }

        // ==========================================================================================
        // Override Function

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            this.LoopManager.UnRegister(this.LoopKey);
        }

        // ==========================================================================================
        // Private Function

        /// <summary>
        /// 更新文档状态
        /// </summary>
        private void UpdateDocumentStatus()
        {
            if (this.View is not TxtView view)
                return;

            view.Dispatcher.BeginInvoke(() =>
            {
                this.IsModify = view.edit.IsModified;
                this.CanRedo = view.edit.CanRedo;
                this.CanUndo = view.edit.CanUndo;
            });
        }
    }
}
