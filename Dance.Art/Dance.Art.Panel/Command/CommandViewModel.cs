using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Dance.Art.Domain;
using Dance.Art.Domain.Message;
using Dance.Art.Module;
using Dance.Art.Storage;
using Dance.Wpf;
using Microsoft.ClearScript;
using Microsoft.ClearScript.JavaScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Panel
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
            this.ClearCommand = new(this.Clear);

            DanceDomain.Current.Messenger.Register<ProjectOpenMessage>(this, this.OnProjectOpen);
            DanceDomain.Current.Messenger.Register<ProjectClosedMessage>(this, this.OnProjectClosed);
            DanceDomain.Current.Messenger.Register<ScriptRunningMessage>(this, this.OnScriptRunning);
            DanceDomain.Current.Messenger.Register<ScriptStopMessage>(this, this.OnScriptStop);
        }

        // ==========================================================================================
        // Field

        /// <summary>
        /// 输出管理器
        /// </summary>
        private readonly IOutputManager OutputManager = DanceDomain.Current.LifeScope.Resolve<IOutputManager>();

        // ==========================================================================================
        // Property

        #region IsEnabled -- 是否启用

        private bool isEnabled;
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled
        {
            get { return isEnabled; }
            set { isEnabled = value; this.OnPropertyChanged(); }
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
            if (this.View is not CommandView view)
                return false;

            MainViewModel vm = DanceDomain.Current.LifeScope.Resolve<MainViewModel>();

            return vm != null && vm.ScriptDomain != null && vm.ScriptDomain.Engine != null && (vm.ScriptStatus == ScriptStatus.Running || vm.ScriptStatus == ScriptStatus.Debugging);
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

        #region ClearCommand -- 清理命令

        /// <summary>
        /// 清理命令
        /// </summary>
        public RelayCommand ClearCommand { get; private set; }

        /// <summary>
        /// 清理
        /// </summary>
        private void Clear()
        {
            if (this.View is not CommandView view)
                return;

            view.edit.Clear();
        }

        #endregion

        // ==========================================================================================
        // Message

        #region ProjectOpenMessage -- 项目打开前消息

        /// <summary>
        /// 项目打开前
        /// </summary>
        private void OnProjectOpen(object sender, ProjectOpenMessage msg)
        {
            if (this.View is not CommandView view)
                return;

            CommandCacheEntity? entity = msg.ProjectDomain.CacheContext.CommandCaches.FindAll().FirstOrDefault();
            if (entity == null)
                return;

            view.edit.Text = entity.Command;
            this.IsEnabled = true;

            this.UpdateToolStatus();
        }

        #endregion

        #region ProjectClosedMessage -- 项目关闭消息 

        /// <summary>
        /// 项目关闭消息
        /// </summary>
        private void OnProjectClosed(object sender, ProjectClosedMessage msg)
        {
            if (this.View is not CommandView view)
                return;

            CommandCacheEntity? entity = msg.ProjectDomain.CacheContext.CommandCaches.FindAll().FirstOrDefault() ?? new();
            entity.Command = view.edit.Text;
            msg.ProjectDomain.CacheContext.CommandCaches.Upsert(entity);

            view.edit.Clear();
            this.IsEnabled = false;

            this.UpdateToolStatus();
        }

        #endregion

        #region ScriptRunningMessage -- 脚本运行消息

        /// <summary>
        /// 脚本运行消息
        /// </summary>
        private void OnScriptRunning(object sender, ScriptRunningMessage msg)
        {
            this.UpdateToolStatus();
        }

        #endregion

        #region ScriptStopMessage -- 脚本停止消息

        /// <summary>
        /// 脚本停止消息
        /// </summary>
        private void OnScriptStop(object sender, ScriptStopMessage msg)
        {
            this.UpdateToolStatus();
        }

        #endregion

        // ==========================================================================================
        // Private Function

        /// <summary>
        /// 更新工具状态
        /// </summary>
        private void UpdateToolStatus()
        {
            this.RunCommand?.NotifyCanExecuteChanged();
        }
    }
}
