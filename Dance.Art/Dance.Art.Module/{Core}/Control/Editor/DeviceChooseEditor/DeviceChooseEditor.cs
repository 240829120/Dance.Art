using Dance.Art.Domain;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dance.Art.Module
{
    /// <summary>
    /// 设备选择编辑器
    /// </summary>
    public class DeviceChooseEditor : Button
    {
        static DeviceChooseEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DeviceChooseEditor), new FrameworkPropertyMetadata(typeof(DeviceChooseEditor)));
        }

        /// <summary>
        /// 窗口管理器
        /// </summary>
        private readonly IWindowManager WindowManager = DanceDomain.Current.LifeScope.Resolve<IWindowManager>();

        /// <summary>
        /// 分隔符
        /// </summary>
        public const string SEPARATOR = ",";

        #region Icon -- 图标

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        /// <summary>
        /// 图标
        /// </summary>
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(string), typeof(DeviceChooseEditor), new PropertyMetadata("pack://application:,,,/Dance.Art.Module;component/Themes/Resources/Icons/device.svg"));

        #endregion

        #region EditValue -- 编辑值

        /// <summary>
        /// 编辑值
        /// </summary>
        public string EditValue
        {
            get { return (string)GetValue(EditValueProperty); }
            set { SetValue(EditValueProperty, value); }
        }

        /// <summary>
        /// 编辑值
        /// </summary>
        public static readonly DependencyProperty EditValueProperty =
            DependencyProperty.Register("EditValue", typeof(string), typeof(DeviceChooseEditor), new PropertyMetadata(null));

        #endregion

        /// <summary>
        /// 点击
        /// </summary>
        protected override void OnClick()
        {
            if (ArtDomain.Current.ProjectDomain == null)
                return;

            List<string> devices = string.IsNullOrWhiteSpace(this.EditValue) ? [] : [.. this.EditValue.Split(SEPARATOR)];
            List<DeviceChooseModel> list = [];

            foreach (DeviceGroupModel group in ArtDomain.Current.ProjectDomain.DeviceGroups)
            {
                foreach (DeviceModel item in group.Items)
                {
                    if (item == null || !this.Filter(item))
                        continue;

                    DeviceChooseModel model = new(item);
                    if (!string.IsNullOrWhiteSpace(item.Name))
                    {
                        model.IsSelected = devices.Contains(item.Name);
                    }

                    list.Add(model);
                }
            }

            DeviceChooseWindow window = new()
            {
                Owner = this.WindowManager.MainWindow
            };
            if (window.DataContext is not DeviceChooseWindowModel vm)
                return;

            vm.Devices = list;
            if (window.ShowDialog() != true)
                return;

            this.EditValue = string.Join(SEPARATOR, list.Where(p => p.IsSelected && !string.IsNullOrWhiteSpace(p.Device.Name)).Select(p => p.Device.Name));
        }

        /// <summary>
        /// 过滤设备
        /// </summary>
        /// <param name="device">设备模型</param>
        /// <returns>是否符合要求</returns>
        protected virtual bool Filter(DeviceModel device)
        {
            return true;
        }
    }
}
