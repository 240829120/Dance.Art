using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Dance.Art.Domain;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dance.Art.Plugin
{
    /// <summary>
    /// 连接视图模型
    /// </summary>
    public class ConnectionViewModel : PanelViewModelBase
    {
        public ConnectionViewModel()
        {
            // 命令
            this.AddGroupCommand = new(this.AddGroup);
            this.DeleteGroupCommand = new(this.DeleteGroup);
            this.AddItemCommand = new(this.AddItem);
            this.DeleteItemCommand = new(this.DeleteItem);
            this.DragBeginCommand = new(this.DragBegin);
            this.DropCommand = new(this.Drop);

            // 消息
            DanceDomain.Current.Messenger.Register<ProjectOpenMessage>(this, this.OnProjectOpen);
        }

        // ==========================================================================================
        // Field

        // ==========================================================================================
        // Property

        #region IsEnabled -- 是否启用

        private bool isEnabled = true;
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled
        {
            get { return isEnabled; }
            set { isEnabled = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Groups -- 分组集合

        private ObservableCollection<ConnectionGroupModel>? groups;
        /// <summary>
        /// 分组集合
        /// </summary>
        public ObservableCollection<ConnectionGroupModel>? Groups
        {
            get { return groups; }
            set { groups = value; this.OnPropertyChanged(); }
        }

        #endregion

        // ==========================================================================================
        // Command

        #region AddGroupCommand -- 添加分组命令

        /// <summary>
        /// 添加分组命令
        /// </summary>
        public RelayCommand AddGroupCommand { get; private set; }

        /// <summary>
        /// 添加分组
        /// </summary>
        private void AddGroup()
        {

        }

        #endregion

        #region DeleteGroupCommand -- 删除分组命令

        /// <summary>
        /// 删除分组命令
        /// </summary>
        public RelayCommand DeleteGroupCommand { get; private set; }

        /// <summary>
        /// 删除分组
        /// </summary>
        private void DeleteGroup()
        {

        }

        #endregion

        #region AddItemCommand -- 添加项命令

        /// <summary>
        /// 添加项命令
        /// </summary>
        public RelayCommand AddItemCommand { get; private set; }

        /// <summary>
        /// 添加项
        /// </summary>
        private void AddItem()
        {

        }

        #endregion

        #region DeleteItemCommand -- 删除项命令

        /// <summary>
        /// 删除项命令
        /// </summary>
        public RelayCommand DeleteItemCommand { get; private set; }

        /// <summary>
        /// 删除项
        /// </summary>
        private void DeleteItem()
        {

        }

        #endregion

        #region DragBeginCommand -- 拖拽开始命令

        /// <summary>
        /// 拖拽开始命令
        /// </summary>
        public RelayCommand<DanceDragBeginEventArgs> DragBeginCommand { get; private set; }

        /// <summary>
        /// 拖拽开始命令
        /// </summary>
        private void DragBegin(DanceDragBeginEventArgs? e)
        {
            if (e == null)
                return;

            e.Data = "this is a try.";
        }

        #endregion

        #region DropCommand 拖拽结束命令

        /// <summary>
        /// 拖拽结束命令
        /// </summary>
        public RelayCommand<DanceDragEventArgs> DropCommand { get; private set; }

        /// <summary>
        /// 拖拽结束命令
        /// </summary>
        private void Drop(DanceDragEventArgs? e)
        {
            if (e == null)
                return;

            string? data = e.EventArgs.Data.GetData(typeof(string))?.ToString();

        }

        #endregion

        // ==========================================================================================
        // Message

        #region ProjectOpenMessage -- 项目打开消息

        /// <summary>
        /// 项目打开消息
        /// </summary>
        private void OnProjectOpen(object sender, ProjectOpenMessage msg)
        {
            if (DanceDomain.Current is not ArtDomain artDomain)
                return;

            ConnectionPluginInfo pluginInfo = new(null, null, null, "pack://application:,,,/Dance.Art.Plugin;component/Themes/Resources/Icons/udp.svg");

            ObservableCollection<ConnectionGroupModel> groupModels = new();

            groupModels.Add(new ConnectionGroupModel() { Name = "设备" });
            groupModels.Add(new ConnectionGroupModel() { Name = "引擎" });
            groupModels.Add(new ConnectionGroupModel() { Name = "网络" });
            groupModels.Add(new ConnectionGroupModel() { Name = "空" });

            groupModels[0].Connections.Add(new ConnectionModel(pluginInfo) { Name = "设备1" });
            groupModels[0].Connections.Add(new ConnectionModel(pluginInfo) { Name = "设备2" });
            groupModels[0].Connections.Add(new ConnectionModel(pluginInfo) { Name = "设备3" });

            groupModels[1].Connections.Add(new ConnectionModel(pluginInfo) { Name = "UE" });
            groupModels[1].Connections.Add(new ConnectionModel(pluginInfo) { Name = "Unity" });

            groupModels[2].Connections.Add(new ConnectionModel(pluginInfo) { Name = "主机房" });
            groupModels[2].Connections.Add(new ConnectionModel(pluginInfo) { Name = "副机房1" });
            groupModels[2].Connections.Add(new ConnectionModel(pluginInfo) { Name = "副机房2" });
            groupModels[2].Connections.Add(new ConnectionModel(pluginInfo) { Name = "副机房3" });

            this.Groups = groupModels;

            //ObservableCollection<ConnectionGroupModel> groupModels = new();
            //var groups = msg.ProjectDomain.CacheContext.ConnectionGroups.FindAll();
            //foreach (var group in groups)
            //{
            //    ConnectionGroupModel groupModel = new();
            //    groupModel.Name = group.Name;
            //    groupModel.Description = group.Description;

            //    groupModels.Add(groupModel);

            //    if (group.Connections == null || group.Connections.Count == 0)
            //        continue;

            //    foreach (var connection in group.Connections)
            //    {
            //        ConnectionPluginInfo? pluginInfo = artDomain.ConnectionPlugins.FirstOrDefault(p => string.Equals(p.ID, connection.PluginID));
            //        if (pluginInfo == null)
            //        {
            //            log.Info($"未找到连接插件: {connection.PluginID}");
            //            continue;
            //        }

            //        ConnectionModel connectionModel = new(pluginInfo)
            //        {
            //            ID = connection.ID,
            //            Name = connection.Name,
            //            Description = connection.Description
            //        };

            //        groupModel.Connections.Add(connectionModel);

            //        if (connection.Parameters == null || connection.Parameters.Count == 0)
            //            continue;

            //        foreach (var kv in connection.Parameters)
            //        {
            //            connectionModel.Parameters.Add(kv.Key, kv.Value);
            //        }
            //    }
            //}

            //this.Groups = groupModels;
        }

        #endregion

        // ==========================================================================================
        // Private Function

    }
}
