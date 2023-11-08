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

namespace Dance.Art.Panel
{
    /// <summary>
    /// 设备视图模型
    /// </summary>
    public class DeviceViewModel : PanelViewModelBase
    {
        public DeviceViewModel()
        {
            // 命令
            this.LoadedCommand = new(this.Loaded);

            // 命令 -- 分组
            this.AddGroupCommand = new(this.AddGroup);
            this.RenameGroupCommand = new(this.RenameGroup);
            this.DeleteGroupCommand = new(this.DeleteGroup);
            this.AddItemFromGroupCommand = new(this.AddItemFromGroup);
            // 命令 -- 项
            this.AddItemCommand = new(this.AddItem);
            this.EditItemCommand = new(this.EditItem);
            this.DeleteItemCommand = new(this.DeleteItem);
            this.RefreshItemCommand = new(this.RefreshItem);
            // 命令 -- 拖拽
            this.DragBeginCommand = new(this.DragBegin);
            this.DropCommand = new(this.Drop);

            // 消息
            DanceDomain.Current.Messenger.Register<ProjectOpenMessage>(this, this.OnProjectOpen);
            DanceDomain.Current.Messenger.Register<ProjectClosedMessage>(this, this.OnProjectClosed);
        }

        // ==========================================================================================
        // Field

        /// <summary>
        /// 窗口管理器
        /// </summary>
        private readonly IWindowManager WindowManager = DanceDomain.Current.LifeScope.Resolve<IWindowManager>();

        /// <summary>
        /// 设备仓储
        /// </summary>
        private readonly IDeviceStorage DeviceStorage = DanceDomain.Current.LifeScope.Resolve<IDeviceStorage>();

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

        private ObservableCollection<DeviceGroupModel>? groups;
        /// <summary>
        /// 分组集合
        /// </summary>
        public ObservableCollection<DeviceGroupModel>? Groups
        {
            get { return groups; }
            set { groups = value; this.OnPropertyChanged(); }
        }

        #endregion

        // ==========================================================================================
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
            if (ArtDomain.Current.ProjectDomain == null)
                return;

            this.Groups = ArtDomain.Current.ProjectDomain.DeviceGroups;
        }

        #endregion

        // --------------------------------------------------------
        // 分组

        #region AddGroupCommand -- 添加分组命令

        /// <summary>
        /// 添加分组命令
        /// </summary>
        public RelayCommand<DeviceModel> AddGroupCommand { get; private set; }

        /// <summary>
        /// 添加分组
        /// </summary>
        /// <param name="item">项</param>
        private void AddGroup(DeviceModel? item)
        {
            if (ArtDomain.Current.ProjectDomain == null)
                return;

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
            if (this.Groups == null)
                return false;

            if (string.IsNullOrWhiteSpace(vm.InputValue))
            {
                DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "请输入名称", DanceMessageBoxAction.YES);
                return false;
            }

            if (this.Groups.Any(p => string.Equals(p.Name, vm.InputValue)))
            {
                DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "名称重复", DanceMessageBoxAction.YES);
                return false;
            }

            DeviceGroupModel group = new()
            {
                Name = vm.InputValue
            };

            this.Groups?.Add(group);
            this.SaveGroups();

            return true;
        }

        #endregion

        #region RenameGroupCommand -- 重命名分组命令

        /// <summary>
        /// 重命名分组命令
        /// </summary>
        public RelayCommand<DeviceGroupModel> RenameGroupCommand { get; private set; }

        /// <summary>
        /// 重命名分组
        /// </summary>
        /// <param name="group">分组</param>
        private void RenameGroup(DeviceGroupModel? group)
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
            if (vm.Data is not DeviceGroupModel group || this.Groups == null)
                return false;

            if (string.IsNullOrWhiteSpace(vm.NewValue))
            {
                DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "请输入名称", DanceMessageBoxAction.YES);
                return false;
            }

            if (this.Groups.Any(p => string.Equals(p.Name, vm.NewValue)))
            {
                DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "名称重复", DanceMessageBoxAction.YES);
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
        public RelayCommand<DeviceGroupModel> DeleteGroupCommand { get; private set; }

        /// <summary>
        /// 删除分组
        /// </summary>
        /// <param name="group">分组</param>
        private void DeleteGroup(DeviceGroupModel? group)
        {
            if (group == null)
                return;

            if (DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, $"是否删除分组: [ {group.Name} ]", DanceMessageBoxAction.YES | DanceMessageBoxAction.NO) != DanceMessageBoxAction.YES)
                return;

            this.Groups?.Remove(group);

            foreach (DeviceModel model in group.Items)
            {
                try
                {
                    model.Source?.Dispose();
                    model.Source?.Delete();
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }

            this.SaveGroups();
        }

        #endregion

        // --------------------------------------------------------
        // 连接

        #region AddItemFromGroupCommand -- 从分组中添加项命令

        /// <summary>
        /// 从分组中添加项命令
        /// </summary>
        public RelayCommand<DeviceGroupModel> AddItemFromGroupCommand { get; private set; }

        /// <summary>
        /// 从分组中添加项
        /// </summary>
        /// <param name="group">分组</param>
        private void AddItemFromGroup(DeviceGroupModel? group)
        {
            if (group == null)
                return;

            DeviceAddWindow window = new(group)
            {
                Owner = this.WindowManager.MainWindow
            };

            window.ShowDialog();
        }

        #endregion

        #region AddItemCommand -- 添加项命令

        /// <summary>
        /// 添加项命令
        /// </summary>
        public RelayCommand<DeviceModel> AddItemCommand { get; private set; }

        /// <summary>
        /// 添加项
        /// </summary>
        /// <param name="item">项</param>
        private void AddItem(DeviceModel? item)
        {
            if (item == null || item.Group == null)
                return;

            DeviceAddWindow window = new(item.Group)
            {
                Owner = this.WindowManager.MainWindow
            };

            window.ShowDialog();
        }

        #endregion

        #region EditItemCommand -- 编辑项命令

        /// <summary>
        /// 编辑项命令
        /// </summary>
        public RelayCommand<DeviceModel> EditItemCommand { get; private set; }

        /// <summary>
        /// 编辑项
        /// </summary>
        /// <param name="model">项</param>
        private void EditItem(DeviceModel? model)
        {
            if (model == null)
                return;

            ArtDomain.Current.Messenger.Send(new FileOpenMessage($"[设备]{model.Name}", model.PluginInfo, model));
        }

        #endregion

        #region DeleteItemCommand -- 删除项命令

        /// <summary>
        /// 删除项命令
        /// </summary>
        public RelayCommand<DeviceModel> DeleteItemCommand { get; private set; }

        /// <summary>
        /// 删除项
        /// </summary>
        /// <param name="model">项</param>
        private void DeleteItem(DeviceModel? model)
        {
            if (model == null || model.Group == null)
                return;

            if (DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, $"是否删除设备: [ {model.Name} ]", DanceMessageBoxAction.YES | DanceMessageBoxAction.NO) != DanceMessageBoxAction.YES)
                return;

            model.Group.Items.Remove(model);
            this.SaveGroups();

            model.Source?.Dispose();
            model.Source?.Delete();
        }

        #endregion

        #region RefreshItemCommand -- 刷新项命令

        /// <summary>
        /// 刷新项目命令
        /// </summary>
        public RelayCommand<DeviceModel> RefreshItemCommand { get; private set; }

        /// <summary>
        /// 刷新项
        /// </summary>
        /// <param name="model">项</param>
        private void RefreshItem(DeviceModel? model)
        {
            if (model == null || model.Group == null)
                return;

            model.Source.Disconnect();
            model.Source.Connect();
        }

        #endregion

        // --------------------------------------------------------
        // 拖拽

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
            if (e == null || e.Element.DataContext is not DeviceModel model)
                return;

            e.Data = model;
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
            if (e == null || e.Element.DataContext is not DeviceGroupModel dstGroup || e.EventArgs.Data.GetData(typeof(DeviceModel)) is not DeviceModel src)
                return;

            if (src.Group == null || !src.Group.Items.Contains(src) || dstGroup.Items.Contains(src))
                return;

            src.Group.Items.Remove(src);
            dstGroup.Items.Add(src);
            src.Group = dstGroup;
            dstGroup.Items.SortSelf((a, b) => string.Compare(a.Name, b.Name));

            this.SaveGroups();
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
            this.Groups = msg.ProjectDomain.DeviceGroups;
        }

        #endregion

        #region ProjectClosedMessage -- 项目关闭消息

        /// <summary>
        /// 项目关闭消息
        /// </summary>
        private void OnProjectClosed(object sender, ProjectClosedMessage msg)
        {
            this.Groups = null;
        }

        #endregion

        // ==========================================================================================
        // Private Function

        /// <summary>
        /// 保存分组
        /// </summary>
        private void SaveGroups()
        {
            if (ArtDomain.Current.ProjectDomain == null)
                return;

            this.DeviceStorage.SaveDeviceGroups(ArtDomain.Current.ProjectDomain);
        }
    }
}