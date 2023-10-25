using CommunityToolkit.Mvvm.Input;
using Dance.Art.Domain;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Dance.Art.Plugin.Document
{
    /// <summary>
    /// 文档视图模型基类
    /// </summary>
    public abstract class DocumentViewModelBase : DanceViewModel, IDockingDocument
    {
        /// <summary>
        /// 文本视图模型
        /// </summary>
        public DocumentViewModelBase()
        {
            // 初始化命令
            this.CopyCommand = new(this.Copy, this.CanCopy);
            this.CutCommand = new(this.Cut, this.CanCut);
            this.PasteCommand = new(this.Paste);
            this.LoadedCommand = new(this.Load);

            // 注册状态更新
            this.LoopKey = $"DocumentViewModelBase_{this.GetHashCode()}";
            this.LoopManager.Register(this.LoopKey, 1, this.ExecuteUdateDocumentStatus);
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

        #region DocumentViewModel -- 文档模型

        private DocumentViewModel? documentModel;
        /// <summary>
        /// 文档模型
        /// </summary>
        public DocumentViewModel? DocumentModel
        {
            get { return documentModel; }
            set { documentModel = value; this.OnPropertyChanged(); }
        }

        #endregion

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

        #region LoadedCommand -- 加载命令

        /// <summary>
        /// 加载命令
        /// </summary>
        public RelayCommand LoadedCommand { get; private set; }

        #endregion

        #region CopyCommand -- 复制命令

        /// <summary>
        /// 复制命令
        /// </summary>
        public RelayCommand CopyCommand { get; private set; }

        /// <summary>
        /// 是否可以复制
        /// </summary>
        /// <returns>是否可以复制</returns>
        protected abstract bool CanCopy();

        /// <summary>
        /// 复制
        /// </summary>
        protected abstract void Copy();

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
        protected abstract bool CanCut();

        /// <summary>
        /// 剪切
        /// </summary>
        protected abstract void Cut();

        #endregion

        #region PasteCommand -- 粘贴命令

        /// <summary>
        /// 粘贴命令
        /// </summary>
        public RelayCommand PasteCommand { get; private set; }

        /// <summary>
        /// 粘贴
        /// </summary>
        protected abstract void Paste();

        #endregion

        // ==========================================================================================
        // Public Function

        /// <summary>
        /// 加载
        /// </summary>
        public abstract void Load();

        /// <summary>
        /// 保存
        /// </summary>
        public abstract void Save();

        /// <summary>
        /// 重做
        /// </summary>
        public abstract void Redo();

        /// <summary>
        /// 撤销
        /// </summary>
        public abstract void Undo();

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
        // Protected Function

        /// <summary>
        /// 更新文档状态
        /// </summary>
        protected abstract void UpdateDocumentStatus();

        // ==========================================================================================
        // Private Function

        /// <summary>
        /// 执行更新文档状态
        /// </summary>
        private void ExecuteUdateDocumentStatus()
        {
            if (this.View is not FrameworkElement view)
                return;

            view.Dispatcher.BeginInvoke(() =>
            {
                this.UpdateDocumentStatus();
            });
        }
    }
}
