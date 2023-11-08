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

namespace Dance.Art.Panel
{
    /// <summary>
    /// 添加数据窗口模型
    /// </summary>
    public class DataSourceAddWindowModel : DanceViewModel
    {
        /// <summary>
        /// 添加数据窗口模型
        /// </summary>
        /// <param name="group">分组</param>
        public DataSourceAddWindowModel(DataSourceGroupModel group)
        {
            this.Group = group;

            this.LoadedCommand = new(this.Loaded);
            this.EnterCommand = new(this.Enter);
            this.CancelCommand = new(this.Cancel);
        }

        // =====================================================================
        // Field 

        /// <summary>
        /// 数据仓储
        /// </summary>
        private readonly IDataSourceStorage DataSourceStorage = DanceDomain.Current.LifeScope.Resolve<IDataSourceStorage>();

        // =====================================================================
        // Property 

        /// <summary>
        /// 连接分组
        /// </summary>
        public DataSourceGroupModel Group { get; private set; }

        #region Name -- 名称

        private string? name = "新建数据";
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

        #region Categorys -- 分类集合

        private List<DataSourceCategoryModel>? categorys;
        /// <summary>
        /// 分类集合
        /// </summary>
        public List<DataSourceCategoryModel>? Categorys
        {
            get { return categorys; }
            set { categorys = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region SelectedCategory -- 当前选中的分类

        private DataSourceCategoryModel? selectedCategory;
        /// <summary>
        /// 当前选中的分类
        /// </summary>
        public DataSourceCategoryModel? SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                this.OnPropertyChanged();
                this.PluginInfos = value?.Items;
                this.SelectedPluginInfo = this.PluginInfos?.FirstOrDefault();
            }
        }

        #endregion

        #region PluginInfos -- 插件集合

        private List<DataSourcePluginInfo>? pluginInfos;
        /// <summary>
        /// 插件集合
        /// </summary>
        public List<DataSourcePluginInfo>? PluginInfos
        {
            get { return pluginInfos; }
            set { pluginInfos = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region SelectedPluginInfo -- 当前选中的插件信息

        private DataSourcePluginInfo? selectedPluginInfo;
        /// <summary>
        /// 当前选中的插件信息
        /// </summary>
        public DataSourcePluginInfo? SelectedPluginInfo
        {
            get { return selectedPluginInfo; }
            set { selectedPluginInfo = value; this.OnPropertyChanged(); }
        }

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
            IReadOnlyList<DataSourcePluginInfo> query = ArtDomain.Current.GetPluginCollection<DataSourcePluginInfo>();
            List<DataSourceCategoryModel> categorys = new();
            foreach (var group in query.GroupBy(p => p.Category))
            {
                DataSourceCategoryModel category = new()
                {
                    Category = group.Key,
                    Items = group.ToList()
                };

                categorys.Add(category);
            }

            this.Categorys = categorys;
            this.SelectedCategory = this.Categorys?.FirstOrDefault();
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
            if (ArtDomain.Current.ProjectDomain == null || this.SelectedPluginInfo == null || this.View is not Window window)
                return;

            if (string.IsNullOrWhiteSpace(this.Name))
            {
                DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "请输入名称", DanceMessageBoxAction.YES);
                return;
            }

            if (ArtDomain.Current.ProjectDomain.DataSourceGroups.Any(g => g.Items.Any(i => string.Equals(i.Name, this.Name))))
            {
                DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "名称重复", DanceMessageBoxAction.YES);
                return;
            }

            // 创建
            DataSourceModel model = new(this.SelectedPluginInfo)
            {
                Name = this.Name,
                Description = this.Description,
                Group = this.Group
            };

            // 保存
            model.Source.SaveToStorage();

            this.Group.Items.Add(model);
            this.Group.Items.SortSelf((a, b) => string.Compare(a.Name, b.Name));
            this.DataSourceStorage.SaveDataSourceGroups(ArtDomain.Current.ProjectDomain);

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
