using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dance.Art.Device
{
    /// <summary>
    /// 设备文档面板模板
    /// </summary>
    public class DeviceDocumentPanelTemplate : ContentControl
    {
        static DeviceDocumentPanelTemplate()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DeviceDocumentPanelTemplate), new FrameworkPropertyMetadata(typeof(DeviceDocumentPanelTemplate)));
        }
    }
}
