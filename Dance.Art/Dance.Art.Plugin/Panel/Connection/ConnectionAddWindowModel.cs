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
    /// 添加连接窗口模型
    /// </summary>
    public class ConnectionAddWindowModel : DanceViewModel
    {
        public ConnectionAddWindowModel(ConnectionGroupModel connectionGroup)
        {
            this.ConnectionGroup = connectionGroup;

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

        private string? id;
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

        private string? name;
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

        #region PluginInfos -- 插件信息集合

        private IList<ConnectionPluginInfoBase>? pluginInfos;
        /// <summary>
        /// 插件信息集合
        /// </summary>
        public IList<ConnectionPluginInfoBase>? PluginInfos
        {
            get { return pluginInfos; }
            set { pluginInfos = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region SelectedPluginInfo -- 当前选中的插件信息

        private ConnectionPluginInfoBase? selectedPluginInfo;
        /// <summary>
        /// 当前选中的插件信息
        /// </summary>
        public ConnectionPluginInfoBase? SelectedPluginInfo
        {
            get { return selectedPluginInfo; }
            set { selectedPluginInfo = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region ConnectionGroup -- 连接分组

        /// <summary>
        /// 连接分组
        /// </summary>
        public ConnectionGroupModel ConnectionGroup { get; private set; }

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
            this.PluginInfos = ArtDomain.Current.GetPluginCollection<ConnectionPluginInfoBase>();
            this.SelectedPluginInfo = this.PluginInfos.FirstOrDefault();
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
            if (DanceDomain.Current is not ArtDomain artDomain || artDomain.ProjectDomain == null || this.View is not ConnectionAddWindow window || this.SelectedPluginInfo == null)
                return;

            if (window.editView.Content is not FrameworkElement editView || editView.DataContext is not IConnectionEditViewModel editViewModel)
                return;

            if (string.IsNullOrWhiteSpace(this.ID))
            {
                DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "请输入ID", DanceMessageBoxAction.YES);
                return;
            }

            string id = this.ID.Trim();

            if (artDomain.ProjectDomain.ConnectionGroups.Any(p => p.Connections.Any(p => string.Equals(p.ID, id))))
            {
                DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "ID重复", DanceMessageBoxAction.YES);
                return;
            }

            if (string.IsNullOrWhiteSpace(this.Name))
            {
                DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "请输入名称", DanceMessageBoxAction.YES);
                return;
            }

            //ConnectionModel model = new(this.ConnectionGroup, this.SelectedPluginInfo)
            //{
            //    ID = id,
            //    Name = this.Name.Trim(),
            //    Description = this.Description
            //};

            //editViewModel.Save(model);

            //this.ConnectionGroup.Connections.Add(model);
            //this.ConnectionGroup.Connections.SortSelf((a, b) => string.Compare(a.Name, b.Name));
            //this.ConnectionStorage.SaveConnectionGroups(artDomain.ProjectDomain);

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
