using CommunityToolkit.Mvvm.Input;
using Dance.Art.Domain;
using Dance.Art.Module;
using Dance.Wpf;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.ClearScript;
using Microsoft.ClearScript.JavaScript;
using Microsoft.ClearScript.V8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Plugin
{
    /// <summary>
    /// 命令视图模型
    /// </summary>
    public class CommandViewModel : PanelViewModelBase
    {
        /// <summary>
        /// 命令视图模型
        /// </summary>
        public CommandViewModel()
        {
            this.CopyCommand = new(this.Copy, this.CanCopy);
            this.CutCommand = new(this.Cut, this.CanCut);
            this.PasteCommand = new(this.Paste);
            this.RunCommand = new(this.Run, this.CanRun);
        }

        // ==========================================================================================
        // Field

        /// <summary>
        /// 输出管理器
        /// </summary>
        private readonly IOutputManager OutputManager = DanceDomain.Current.LifeScope.Resolve<IOutputManager>();

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
        protected bool CanCopy()
        {
            if (this.View is not CommandView view)
                return false;

            return view.edit.SelectionLength > 0;
        }

        /// <summary>
        /// 复制
        /// </summary>
        protected void Copy()
        {
            if (this.View is not CommandView view)
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
        protected bool CanCut()
        {
            if (this.View is not CommandView view)
                return false;

            return view.edit.SelectionLength > 0;
        }

        /// <summary>
        /// 剪切
        /// </summary>
        protected void Cut()
        {
            if (this.View is not CommandView view)
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
        protected void Paste()
        {
            if (this.View is not CommandView view)
                return;

            view.edit.Paste();
        }

        #endregion

        #region RunCommand -- 运行命令

        /// <summary>
        /// 运行命令
        /// </summary>
        public RelayCommand RunCommand { get; private set; }

        /// <summary>
        /// 是否可以运行
        /// </summary>
        /// <returns>是否可以运行</returns>
        private bool CanRun()
        {
            if (this.View is not CommandView view || string.IsNullOrWhiteSpace(view.edit.Text))
                return false;

            MainViewModel vm = DanceDomain.Current.LifeScope.Resolve<MainViewModel>();

            return vm != null && vm.ScriptDomain != null && vm.ScriptDomain.Engine != null && (vm.ScriptStatus == ScriptStatus.Running || vm.ScriptStatus == ScriptStatus.Debugging) && !string.IsNullOrWhiteSpace(view.edit.Text);
        }

        /// <summary>
        /// 运行
        /// </summary>
        private void Run()
        {
            if (this.View is not CommandView view || string.IsNullOrWhiteSpace(view.edit.Text))
                return;

            MainViewModel vm = DanceDomain.Current.LifeScope.Resolve<MainViewModel>();
            if (vm == null || vm.ScriptDomain == null || vm.ScriptDomain.Engine == null || (vm.ScriptStatus != ScriptStatus.Running && vm.ScriptStatus != ScriptStatus.Debugging))
                return;

            try
            {
                object? result = vm.ScriptDomain.Engine.Evaluate(new DocumentInfo() { Category = ModuleCategory.Standard }, view.edit.Text);
                this.OutputManager.WriteLine(result?.ToString() ?? string.Empty);
            }
            catch (Exception ex)
            {
                this.OutputManager.WriteLine(ex.Message);
            }
        }

        #endregion
    }
}
