using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Dance.Art.Document.Document;
using Dance.Art.Domain;
using Dance.Wpf;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.ButtonBox
{
    /// <summary>
    /// 按钮组文档视图模型
    /// </summary>
    public class ButtonBoxDocumentViewModel : DocumentViewModelBase
    {
        public ButtonBoxDocumentViewModel()
        {
            this.ResourceDropCommand = new(this.ResourceDrop);
        }

        // ====================================================================================
        // Field

        // ====================================================================================
        // Property

        // -------------------------------------------------------------------------

        #region CanvasModel -- 画布模型

        /// <summary>
        /// 画布模型
        /// </summary>
        public ButtonBoxDocumentViewCanvasModel CanvasModel { get; } = new();

        #endregion

        #region Items -- 项集合

        /// <summary>
        /// 项集合
        /// </summary>
        public DanceWrapperCollection<ButtonBoxItemModelBase> Items { get; } = new();

        #endregion

        #region SelectedValue -- 当前选中项

        private ButtonBoxItemModelBase? selectedValue;
        /// <summary>
        /// 当前选中项
        /// </summary>
        public ButtonBoxItemModelBase? SelectedValue
        {
            get { return selectedValue; }
            set
            {
                selectedValue = value;
                this.OnPropertyChanged();

                DanceDomain.Current.Messenger.Send(new PropertySelectedChangedMessage(this, null, value));
            }
        }

        #endregion

        #region IsSelectedCanvas -- 是否选中画布

        private bool isSelectedCanvas;
        /// <summary>
        /// 是否选中画布
        /// </summary>
        public bool IsSelectedCanvas
        {
            get { return isSelectedCanvas; }
            set
            {
                isSelectedCanvas = value;
                this.OnPropertyChanged();

                DanceDomain.Current.Messenger.Send(new PropertySelectedChangedMessage(this, null, value ? this.CanvasModel : null));
            }
        }

        #endregion

        // ====================================================================================
        // Command

        #region ResourceDropCommand -- 资源拖拽结束命令

        /// <summary>
        /// 资源拖拽结束命令
        /// </summary>
        public RelayCommand<ButtonBoxItemsControlDropEventArgs> ResourceDropCommand { get; private set; }

        /// <summary>
        /// 资源拖拽结束
        /// </summary>
        /// <param name="e">事件参数</param>
        private void ResourceDrop(ButtonBoxItemsControlDropEventArgs? e)
        {
            if (e == null || ArtDomain.Current.ProjectDomain == null || e.Model == null)
                return;

            e.Model.Row = e.Row;
            e.Model.Column = e.Column;

            if (!this.Items.Contains(e.Model))
            {
                this.Items.Add(e.Model);
            }

            DanceDomain.Current.Messenger.Send(new PropertySelectedChangedMessage(this, null, e.Model));
        }

        #endregion

        // ====================================================================================
        // Override

        /// <summary>
        /// 是否可以拷贝
        /// </summary>
        protected override bool CanCopy()
        {
            return false;
        }

        /// <summary>
        /// 是否可以剪切
        /// </summary>
        /// <returns></returns>
        protected override bool CanCut()
        {
            return false;
        }

        /// <summary>
        /// 复制
        /// </summary>
        protected override void Copy()
        {
            // nothing todo.
        }

        /// <summary>
        /// 粘贴
        /// </summary>
        protected override void Cut()
        {
            // nothing todo.
        }

        /// <summary>
        /// 粘贴
        /// </summary>
        protected override void Paste()
        {
            // nothing todo.
        }
    }
}
