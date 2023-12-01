using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 面板视图模型基类
    /// </summary>
    public abstract class PanelViewModelBase : DanceViewModel, IPanelViewModel
    {
        /// <summary>
        /// 是否修改
        /// </summary>
        public virtual bool IsModify { get; set; }

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

        #region ViewPluginModel -- 视图插件模型

        private ViewPluginModelBase? viewPluginModel;
        /// <summary>
        /// 视图插件模型
        /// </summary>
        public ViewPluginModelBase? ViewPluginModel
        {
            get { return viewPluginModel; }
            set
            {
                if (this.viewPluginModel != null)
                {
                    this.viewPluginModel.OnDestory -= ViewPluginModel_OnDestory;
                }

                this.viewPluginModel = value;

                if (this.viewPluginModel != null)
                {
                    this.viewPluginModel.OnDestory -= ViewPluginModel_OnDestory;
                    this.viewPluginModel.OnDestory += ViewPluginModel_OnDestory;
                }
            }
        }

        /// <summary>
        /// 销毁
        /// </summary>
        private void ViewPluginModel_OnDestory(object? sender, EventArgs e)
        {
            this.Dispose();
        }

        #endregion
    }
}
