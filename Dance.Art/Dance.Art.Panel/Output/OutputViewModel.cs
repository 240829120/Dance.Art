﻿using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Dance.Art.Domain;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace Dance.Art.Panel
{
    /// <summary>
    /// 输出视图模型
    /// </summary>
    public class OutputViewModel : PanelViewModelBase
    {
        /// <summary>
        /// 输出视图模型
        /// </summary>
        public OutputViewModel()
        {
            // 命令
            this.LoadedCommand = new(this.Loaded);
            this.CopyCommand = new(this.Copy);
            this.ClearCommand = new(this.Clear);

            // 消息
            DanceDomain.Current.Messenger.Register<ProjectClosedMessage>(this, this.OnProjectClosed);

            // 输出管理器
            this.OutputManager.OnOutput -= OutputManager_OnOutput;
            this.OutputManager.OnOutput += OutputManager_OnOutput;

            this.OutputManager.OnClear -= OutputManager_OnClear;
            this.OutputManager.OnClear += OutputManager_OnClear;
        }

        // ============================================================================================
        // Field -- 字段

        /// <summary>
        /// 输出管理器
        /// </summary>
        private readonly IOutputManager OutputManager = DanceDomain.Current.LifeScope.Resolve<IOutputManager>();

        /// <summary>
        /// 输出缓存
        /// </summary>
        private readonly StringBuilder OutputCache = new();

        // ============================================================================================
        // Command

        #region LoadedCommand -- 加载命令

        /// <summary>
        /// 加载命令
        /// </summary>
        public RelayCommand LoadedCommand { get; private set; }

        /// <summary>
        /// 加载
        /// </summary>
        private void Loaded()
        {
            if (this.View is not OutputView view)
                return;

            view.edit.AppendText(this.OutputCache.ToString());
            this.OutputCache.Clear();
        }

        #endregion

        #region CopyCommand -- 拷贝命令

        /// <summary>
        /// 拷贝命令
        /// </summary>
        public RelayCommand CopyCommand { get; private set; }

        /// <summary>
        /// 拷贝
        /// </summary>
        private void Copy()
        {
            if (this.View is not OutputView view)
                return;

            view.edit.Copy();
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
            if (this.View is not OutputView view)
                return;

            view.edit.Clear();
        }

        #endregion

        // ============================================================================================
        // Message

        #region ProjectClosedMessage -- 项目关闭消息

        /// <summary>
        /// 项目关闭
        /// </summary>
        private void OnProjectClosed(object sender, ProjectClosedMessage msg)
        {
            if (this.View is not OutputView view)
                return;

            view.edit.Clear();
        }

        #endregion

        // ============================================================================================
        // Private Function

        /// <summary>
        /// 输出消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OutputManager_OnOutput(object? sender, OutputEventArgs e)
        {
            string msg = $"[{DateTime.Now:HH:mm:ss.fff}] ===> {e.Message}\r\n";

            if (this.View == null || this.View is not OutputView view)
            {
                this.OutputCache.Append(msg);
                return;
            }

            view.Dispatcher.BeginInvoke(() =>
            {
                view.edit.AppendText(msg);
                view.edit.ScrollToEnd();
            });
        }

        /// <summary>
        /// 清理消息
        /// </summary>
        private void OutputManager_OnClear(object? sender, EventArgs e)
        {
            if (this.View == null || this.View is not OutputView view)
            {
                return;
            }

            view.Dispatcher.BeginInvoke(() =>
            {
                view.edit.Clear();
            });
        }
    }
}