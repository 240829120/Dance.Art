using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Dance.Art.Domain;

namespace Dance.Art.Module
{
    /// <summary>
    /// 设备状态小部件
    /// </summary>
    public class DeviceStatusTip : Control
    {
        static DeviceStatusTip()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DeviceStatusTip), new FrameworkPropertyMetadata(typeof(DeviceStatusTip)));
        }

        #region Status -- 状态

        /// <summary>
        /// 状态
        /// </summary>
        public DeviceStatus Status
        {
            get { return (DeviceStatus)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(DeviceStatus), typeof(DeviceStatusTip), new PropertyMetadata(DeviceStatus.Disconnected));

        #endregion
    }
}
