using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Plugin
{
    /// <summary>
    /// 面板视图模型基类
    /// </summary>
    public abstract class PanelViewModelBase : DanceViewModel, IDockingPanel
    {
        /// <summary>
        /// 是否修改
        /// </summary>
        public virtual bool IsModify { get; }

        /// <summary>
        /// 是否可以重做
        /// </summary>
        public virtual bool CanRedo { get; }

        /// <summary>
        /// 是否可以撤销
        /// </summary>
        public virtual bool CanUndo { get; }

        /// <summary>
        /// 加载
        /// </summary>
        public virtual void Load() { }

        /// <summary>
        /// 保存命令
        /// </summary>
        public virtual void Save() { }

        /// <summary>
        /// 重做
        /// </summary>
        public virtual void Redo() { }

        /// <summary>
        /// 撤销
        /// </summary>
        public virtual void Undo() { }
    }
}
