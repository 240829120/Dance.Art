using CommunityToolkit.Mvvm.Input;
using Dance.Art.Document.Document;
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

        #region Items -- 项集合

        /// <summary>
        /// 项集合
        /// </summary>
        public DanceWrapperCollection<ButtonBoxItemModelBase> Items { get; } = new();

        #endregion

        // ====================================================================================
        // Command

        #region ResourceDropCommand -- 资源拖拽结束命令

        /// <summary>
        /// 资源拖拽结束命令
        /// </summary>
        public RelayCommand<ButtonBoxPanelDropEventArgs> ResourceDropCommand { get; private set; }

        /// <summary>
        /// 资源拖拽结束
        /// </summary>
        /// <param name="e">事件参数</param>
        private void ResourceDrop(ButtonBoxPanelDropEventArgs? e)
        {
            if (e == null || e.ResourceInfo.Source == null || ArtDomain.Current.ProjectDomain == null)
                return;

            if (e.ResourceInfo.Source.CreateInstance(ArtDomain.Current.ProjectDomain) is not ButtonBoxItemModelBase model)
                return;

            model.Row = e.Row;
            model.Column = e.Column;

            this.Items.Add(model);
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
