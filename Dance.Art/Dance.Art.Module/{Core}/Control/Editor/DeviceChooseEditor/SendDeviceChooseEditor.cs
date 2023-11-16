using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.Module
{
    /// <summary>
    /// 发送设备选择器
    /// </summary>
    public class SendDeviceChooseEditor : DeviceChooseEditor
    {
        static SendDeviceChooseEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SendDeviceChooseEditor), new FrameworkPropertyMetadata(typeof(SendDeviceChooseEditor)));
        }

        /// <summary>
        /// 过滤设备
        /// </summary>
        /// <param name="device">设备模型</param>
        /// <returns>是否符合要求</returns>
        protected override bool Filter(DeviceModel device)
        {
            return device.Source is ISendDeviceSource;
        }
    }
}
