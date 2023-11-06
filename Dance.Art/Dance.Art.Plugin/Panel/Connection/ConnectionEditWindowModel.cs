using CommunityToolkit.Mvvm.Input;
using Dance.Art.Domain;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.Plugin
{
    /// <summary>
    /// 编辑连接窗口模型
    /// </summary>
    public class ConnectionEditWindowModel : DanceViewModel
    {
        public ConnectionEditWindowModel(ConnectionModel model)
        {
            this.Model = model;

            this.LoadedCommand = new(this.Loaded);
            this.EnterCommand = new(this.Enter);
            this.CancelCommand = new(this.Cancel);
        }

        // =====================================================================
        // Field 

        /// <summary>
        /// 连接仓储
        /// </summary>
        private readonly IConnectionStorage ConnectionStorage = DanceDomain.Current.LifeScope.Resolve<IConnectionStorage>();

        // =====================================================================
        // Property 

        #region ID -- 编号

        private string? id = "新建连接";
        /// <summary>
        /// 编号
        /// </summary>
        public string? ID
        {
            get { return id; }
            set { id = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Name -- 名称

        private string? name = "新建连接";
        /// <summary>
        /// 名称
        /// </summary>
        public string? Name
        {
            get { return name; }
            set { name = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Description -- 描述

        private string? description;
        /// <summary>
        /// 描述
        /// </summary>
        public string? Description
        {
            get { return description; }
            set { description = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Model -- 连接分组

        /// <summary>
        /// 连接模型
        /// </summary>
        public ConnectionModel Model { get; private set; }

        #endregion

        // =====================================================================
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
            if (this.View is not ConnectionEditWindow window || window.editView.Content is not FrameworkElement editView || editView.DataContext is not IConnectionEditViewModel vm)
                return;

            vm.LoadFromModel(this.Model);
        }

        #endregion

        #region EnterCommand -- 确认命令

        /// <summary>
        /// 确认命令
        /// </summary>
        public RelayCommand EnterCommand { get; private set; }

        /// <summary>
        /// 确认
        /// </summary>
        private void Enter()
        {
            // 校验
            if (ArtDomain.Current.ProjectDomain == null || this.View is not ConnectionEditWindow window || this.Model.Group == null)
                return;

            if (window.editView.Content is not FrameworkElement editView || editView.DataContext is not IConnectionEditViewModel editViewModel)
                return;

            if (string.IsNullOrWhiteSpace(this.ID))
            {
                DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "请输入ID", DanceMessageBoxAction.YES);
                return;
            }

            string id = this.ID.Trim();
            if (ArtDomain.Current.ProjectDomain.ConnectionGroups.Any(p => p.Connections.Any(p => string.Equals(p.ID, id))))
            {
                DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "ID重复", DanceMessageBoxAction.YES);
                return;
            }

            if (string.IsNullOrWhiteSpace(this.Name))
            {
                DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "请输入名称", DanceMessageBoxAction.YES);
                return;
            }

            if (!editViewModel.SaveToModel(this.Model, out string error))
            {
                DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, error, DanceMessageBoxAction.YES);
                return;
            }

            // 停止
            this.Model.PluginInfo.Destory(this.Model);

            // 修改
            this.Model.ID = id;
            this.Model.Name = this.Name.Trim();
            this.Model.Description = this.Description;

            // 保存至仓储，并且完成初始化
            this.Model.SourceID = this.Model.PluginInfo.SaveToStorage(this.Model);
            this.Model.PluginInfo.Initialize(this.Model);

            this.Model.Group.Connections.SortSelf((a, b) => string.Compare(a.Name, b.Name));
            this.ConnectionStorage.SaveConnectionGroups(ArtDomain.Current.ProjectDomain);

            // 关闭窗口
            window.DialogResult = true;
            window.Close();
        }

        #endregion

        #region CancelCommand -- 取消命令

        /// <summary>
        /// 取消命令
        /// </summary>
        public RelayCommand CancelCommand { get; private set; }

        /// <summary>
        /// 取消
        /// </summary>
        private void Cancel()
        {
            if (this.View is not Window window)
                return;

            window.DialogResult = false;
            window.Close();
        }

        #endregion
    }
}
