using CommunityToolkit.Mvvm.Input;
using Dance.Art.Domain;
using Dance.Wpf;
using ICSharpCode.AvalonEdit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Dance.Art.Module
{
    /// <summary>
    /// 脚本编辑窗口模型
    /// </summary>
    public class DeviceChooseWindowModel : DanceViewModel
    {
        public DeviceChooseWindowModel()
        {

            this.EnterCommand = new(this.Enter);
            this.CancelCommand = new(this.Cancel);
        }

        /// <summary>
        /// 延时管理器
        /// </summary>
        private readonly IDanceDelayManager DelayManager = DanceDomain.Current.LifeScope.Resolve<IDanceDelayManager>();

        // ====================================================================================================
        // Property

        #region Filter -- 筛选器

        private string? filter;
        /// <summary>
        /// 筛选器
        /// </summary>
        public string? Filter
        {
            get { return filter; }
            set
            {
                filter = value;
                this.OnPropertyChanged();

                this.DelayManager.Wait("Dance.Art.Module.DeviceChooseWindowModel.Filter", 0.5, () =>
                {
                    Application.Current.Dispatcher.BeginInvoke(() =>
                    {
                        this.DevicesView?.Refresh();
                    });
                });
            }
        }

        #endregion

        #region DevicesView -- 设备视图

        private ICollectionView? devicesView;
        /// <summary>
        /// 设备视图
        /// </summary>
        public ICollectionView? DevicesView
        {
            get { return devicesView; }
            set { devicesView = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Devices -- 设备集合

        private List<DeviceChooseModel>? devices;
        /// <summary>
        /// 设备集合
        /// </summary>
        public List<DeviceChooseModel>? Devices
        {
            get { return devices; }
            set
            {
                devices = value;
                this.OnPropertyChanged();

                this.DevicesView = value == null ? null : CollectionViewSource.GetDefaultView(value);
                if (this.DevicesView != null)
                {
                    this.DevicesView.Filter = this.ExecuteFilter;
                }
            }
        }

        #endregion

        // ====================================================================================================
        // Command

        #region EnterCommand -- 确定命令

        /// <summary>
        /// 确定命令
        /// </summary>
        public RelayCommand EnterCommand { get; private set; }

        /// <summary>
        /// 确定
        /// </summary>
        private void Enter()
        {
            if (this.View is not Window window)
                return;

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

        // ====================================================================================================
        // Private Function

        /// <summary>
        /// 执行筛选
        /// </summary>
        private bool ExecuteFilter(object obj)
        {
            if (obj is not DeviceChooseModel model)
                return false;

            if (string.IsNullOrWhiteSpace(this.Filter) || string.IsNullOrWhiteSpace(model.Device.Name))
                return true;

            return model.Device.Name.Contains(this.Filter);
        }
    }
}
