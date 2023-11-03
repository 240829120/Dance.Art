using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Dance.Art.Domain;
using Dance.Art.Module;
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
            // 命令 -- 分组
            this.AddGroupCommand = new(this.AddGroup);
            this.RenameGroupCommand = new(this.RenameGroup);
            this.DeleteGroupCommand = new(this.DeleteGroup);
            this.AddItemFromGroupCommand = new(this.AddItemFromGroup);
            // 命令 -- 项
            this.AddItemCommand = new(this.AddItem);
            this.EditItemCommand = new(this.EditItem);
            this.DeleteItemCommand = new(this.DeleteItem);
            // 命令 -- 拖拽
            this.DragBeginCommand = new(this.DragBegin);
            this.DropCommand = new(this.Drop);

            // 消息
            DanceDomain.Current.Messenger.Register<ProjectOpenMessage>(this, this.OnProjectOpen);
        }

        // ==========================================================================================
        // Field

        /// <summary>
        /// 窗口管理器
        /// </summary>
        private readonly IWindowManager WindowManager = DanceDomain.Current.LifeScope.Resolve<IWindowManager>();

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

        /// <summary>
        /// 分组集合
        /// </summary>
        public ObservableCollection<ConnectionGroupModel> Groups { get; } = new();

        #endregion

        // ==========================================================================================
        // Command

        #region AddGroupCommand -- 添加分组命令

        /// <summary>
        /// 添加分组命令
        /// </summary>
        public RelayCommand<ConnectionModel> AddGroupCommand { get; private set; }

        /// <summary>
        /// 添加分组
        /// </summary>
        /// <param name="item">项</param>
        private void AddGroup(ConnectionModel? item)
        {
            InputStringTemplateWindow window = new("添加分组", "名称:", "新分组", item, this.ExecuteAddGroup)
            {
                Owner = this.WindowManager.MainWindow
            };

            window.ShowDialog();
        }

        /// <summary>
        /// 执行添加分组
        /// </summary>
        /// <param name="vm">模型</param>
        /// <returns>是否成功</returns>
        private bool ExecuteAddGroup(InputStringTemplateWindowModel vm)
        {
            if (string.IsNullOrWhiteSpace(vm.InputValue))
            {
                DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "请输入名称", DanceMessageBoxAction.YES);
                return false;
            }

            ConnectionGroupModel group = new()
            {
                Name = vm.InputValue
            };

            this.Groups.Add(group);
            this.SaveGroups();

            return true;
        }

        #endregion

        #region RenameGroupCommand -- 重命名分组命令

        /// <summary>
        /// 重命名分组命令
        /// </summary>
        public RelayCommand<ConnectionGroupModel> RenameGroupCommand { get; private set; }

        /// <summary>
        /// 重命名分组
        /// </summary>
        /// <param name="group">分组</param>
        private void RenameGroup(ConnectionGroupModel? group)
        {
            if (group == null)
                return;

            EditStringTemplateWindow window = new("重命名分组", "分组名:", group.Name, "新分组名:", group.Name, group, this.ExecuteRenameGroup)
            {
                Owner = this.WindowManager.MainWindow
            };

            window.ShowDialog();
        }

        /// <summary>
        /// 执行重命名分组
        /// </summary>
        /// <param name="vm">模型</param>
        /// <returns>是否通过验证</returns>
        private bool ExecuteRenameGroup(EditStringTemplateWindowModel vm)
        {
            if (vm.Data is not ConnectionGroupModel group)
                return false;

            if (string.IsNullOrWhiteSpace(vm.NewValue))
            {
                DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "请输入名称", DanceMessageBoxAction.YES);
                return false;
            }

            group.Name = vm.NewValue;

            this.SaveGroups();

            return true;
        }

        #endregion

        #region DeleteGroupCommand -- 删除分组命令

        /// <summary>
        /// 删除分组命令
        /// </summary>
        public RelayCommand<ConnectionGroupModel> DeleteGroupCommand { get; private set; }

        /// <summary>
        /// 删除分组
        /// </summary>
        /// <param name="group">分组</param>
        private void DeleteGroup(ConnectionGroupModel? group)
        {
            if (group == null)
                return;

            if (DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, $"是否删除分组: [ {group.Name} ]", DanceMessageBoxAction.YES | DanceMessageBoxAction.NO) != DanceMessageBoxAction.YES)
                return;

            this.Groups.Remove(group);
        }

        #endregion

        #region AddItemFromGroupCommand -- 从分组中添加项命令

        /// <summary>
        /// 从分组中添加项命令
        /// </summary>
        public RelayCommand<ConnectionGroupModel> AddItemFromGroupCommand { get; private set; }

        /// <summary>
        /// 从分组中添加项
        /// </summary>
        /// <param name="group">分组</param>
        private void AddItemFromGroup(ConnectionGroupModel? group)
        {

        }

        #endregion

        #region AddItemCommand -- 添加项命令

        /// <summary>
        /// 添加项命令
        /// </summary>
        public RelayCommand<ConnectionModel> AddItemCommand { get; private set; }

        /// <summary>
        /// 添加项
        /// </summary>
        /// <param name="item">项</param>
        private void AddItem(ConnectionModel? item)
        {

        }

        #endregion

        #region EditItemCommand -- 编辑项命令

        /// <summary>
        /// 编辑项命令
        /// </summary>
        public RelayCommand<ConnectionModel> EditItemCommand { get; private set; }

        /// <summary>
        /// 编辑项
        /// </summary>
        /// <param name="item">项</param>
        private void EditItem(ConnectionModel? item)
        {

        }

        #endregion

        #region DeleteItemCommand -- 删除项命令

        /// <summary>
        /// 删除项命令
        /// </summary>
        public RelayCommand<ConnectionModel> DeleteItemCommand { get; private set; }

        /// <summary>
        /// 删除项
        /// </summary>
        /// <param name="item">项</param>
        private void DeleteItem(ConnectionModel? item)
        {
            if (item == null || item.Group == null)
                return;

            if (DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, $"是否删除连接: {item.Name}", DanceMessageBoxAction.YES | DanceMessageBoxAction.NO) != DanceMessageBoxAction.YES)
                return;

            item.Group.Connections.Remove(item);
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

            ConnectionPluginInfo pluginInfo = new(null, null, null);

            ObservableCollection<ConnectionGroupModel> groupModels = new();

            groupModels.Add(new ConnectionGroupModel() { Name = "设备" });
            groupModels.Add(new ConnectionGroupModel() { Name = "引擎" });
            groupModels.Add(new ConnectionGroupModel() { Name = "网络" });
            groupModels.Add(new ConnectionGroupModel() { Name = "空" });

            groupModels[0].Connections.Add(new ConnectionModel(pluginInfo) { Name = "设备1", Group = groupModels[0], Status = ConnectionStatus.Connected });
            groupModels[0].Connections.Add(new ConnectionModel(pluginInfo) { Name = "设备2", Group = groupModels[0], Status = ConnectionStatus.Waiting });
            groupModels[0].Connections.Add(new ConnectionModel(pluginInfo) { Name = "设备3", Group = groupModels[0] });

            groupModels[1].Connections.Add(new ConnectionModel(pluginInfo) { Name = "UE", Group = groupModels[1] });
            groupModels[1].Connections.Add(new ConnectionModel(pluginInfo) { Name = "Unity", Group = groupModels[1] });

            groupModels[2].Connections.Add(new ConnectionModel(pluginInfo) { Name = "主机房", Group = groupModels[2] });
            groupModels[2].Connections.Add(new ConnectionModel(pluginInfo) { Name = "副机房1", Group = groupModels[2] });
            groupModels[2].Connections.Add(new ConnectionModel(pluginInfo) { Name = "副机房2", Group = groupModels[2] });
            groupModels[2].Connections.Add(new ConnectionModel(pluginInfo) { Name = "副机房3", Group = groupModels[2] });

            this.Groups.Clear();
            this.Groups.AddRange(groupModels);

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

        /// <summary>
        /// 保存分组
        /// </summary>
        private void SaveGroups()
        {

        }

        /// <summary>
        /// 加载分组
        /// </summary>
        private void LoadGroups()
        {

        }
    }
}
