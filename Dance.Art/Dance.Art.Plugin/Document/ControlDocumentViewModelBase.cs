using Dance.Art.Plugin.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Plugin
{
    /// <summary>
    /// 控件文档视图模型基类
    /// </summary>
    public abstract class ControlDocumentViewModelBase : DocumentViewModelBase
    {
        // ==========================================================================================
        // Property

        #region IsModify -- 是否修改

        protected bool isModify;
        /// <summary>
        /// 是否修改
        /// </summary>
        public override bool IsModify
        {
            get { return isModify; }
        }

        #endregion

        // ==========================================================================================
        // Command

        #region CopyCommand -- 拷贝命令

        /// <summary>
        /// 是否可以拷贝
        /// </summary>
        protected override bool CanCopy()
        {
            return false;
        }

        /// <summary>
        /// 拷贝
        /// </summary>
        protected override void Copy()
        {

        }

        #endregion

        #region CutCommand -- 剪切命令

        /// <summary>
        /// 是否可以剪切
        /// </summary>
        protected override bool CanCut()
        {
            return false;
        }

        /// <summary>
        /// 剪切
        /// </summary>
        protected override void Cut()
        {

        }

        #endregion

        #region PasteCommand -- 粘贴命令

        /// <summary>
        /// 粘贴
        /// </summary>
        protected override void Paste()
        {

        }

        #endregion
    }
}
