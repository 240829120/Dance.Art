using CommunityToolkit.Mvvm.Input;
using Dance.Art.Domain;
using Dance.Art.Module;
using Dance.Wpf;
using Microsoft.ClearScript.JavaScript;
using Microsoft.ClearScript;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Diagnostics;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace Dance.Art.Timeline
{
    /// <summary>
    /// 命令元素
    /// </summary>
    [DisplayName("命令元素")]
    public class CommandElementModel : TimelineElementModelBase
    {
        /// <summary>
        /// 命令元素
        /// </summary>
        public CommandElementModel() : base(typeof(CommandElement))
        {
            this.BorderThickness = new Thickness(0);
            this.BorderColor = Colors.Transparent;
            this.BackgroundColor = Colors.Transparent;
        }

        // ================================================================================
        // Property

        #region DeviceNames -- 设备名称

        private string? deviceNames;
        /// <summary>
        /// 设备名称
        /// </summary>
        [Category(PropertyCategoryDefines.OTHER), Description("设备"), DisplayName("设备")]
        [Editor(typeof(SendDeviceChooseEditor), typeof(SendDeviceChooseEditor))]
        public string? DeviceNames
        {
            get { return deviceNames; }
            set { deviceNames = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        #region BeginCommand -- 开始命令

        private string? beginCommand;
        /// <summary>
        /// 开始命令
        /// </summary>
        [Category(PropertyCategoryDefines.OTHER), PropertyOrder(4), Description("开始命令"), DisplayName("开始命令")]
        [Editor(typeof(ScriptMultiLineEditor), typeof(ScriptMultiLineEditor))]
        public string? BeginCommand
        {
            get { return beginCommand; }
            set { beginCommand = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        #region EndCommand -- 结束命令

        private string? endCommand;
        /// <summary>
        /// 结束命令
        /// </summary>
        [Category(PropertyCategoryDefines.OTHER), PropertyOrder(5), Description("结束命令"), DisplayName("结束命令")]
        [Editor(typeof(ScriptMultiLineEditor), typeof(ScriptMultiLineEditor))]
        public string? EndCommand
        {
            get { return endCommand; }
            set { endCommand = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        // ================================================================================
        // Override

        /// <summary>
        /// 当开始时触发
        /// </summary>
        public override void OnPlay()
        {
            // nothing todo.
        }

        /// <summary>
        /// 当停止时触发
        /// </summary>
        public override void OnStop()
        {
            // nothing todo.
        }

        /// <summary>
        /// 当开始时触发
        /// </summary>
        public override void OnBegin()
        {
            if (ArtDomain.Current.ProjectDomain == null || string.IsNullOrWhiteSpace(this.DeviceNames) || string.IsNullOrWhiteSpace(this.BeginCommand))
                return;

            List<string> devices = this.DeviceNames.Split(DeviceChooseEditor.SEPARATOR, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (devices.Count == 0)
                return;

            List<DeviceModel> deviceModels = ArtDomain.Current.ProjectDomain.GetDeviceModel(devices);
            if (deviceModels.Count == 0)
                return;

            byte[] buffer = Encoding.UTF8.GetBytes(this.BeginCommand);

            foreach (DeviceModel model in deviceModels)
            {
                if (model == null || model.Source == null || model.Source is not ISendDeviceSource source)
                    return;

                Task.Run(() =>
                {
                    try
                    {
                        source.Send(buffer);
                        this.OutputManager.WriteLine($"设备: [{model.Name}] 发送 [{this.BeginCommand}]");
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);
                    }
                });
            }
        }

        /// <summary>
        /// 当结束时触发
        /// </summary>
        public override void OnEnd()
        {
            if (ArtDomain.Current.ProjectDomain == null || string.IsNullOrWhiteSpace(this.DeviceNames) || string.IsNullOrWhiteSpace(this.EndCommand))
                return;

            List<string> devices = this.DeviceNames.Split(DeviceChooseEditor.SEPARATOR, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (devices.Count == 0)
                return;

            List<DeviceModel> deviceModels = ArtDomain.Current.ProjectDomain.GetDeviceModel(devices);
            if (deviceModels.Count == 0)
                return;

            byte[] buffer = Encoding.UTF8.GetBytes(this.EndCommand);

            foreach (DeviceModel model in deviceModels)
            {
                if (model == null || model.Source == null || model.Source is not ISendDeviceSource source)
                    return;

                Task.Run(() =>
                {
                    try
                    {
                        source.Send(buffer);
                        this.OutputManager.WriteLine($"设备: [{model.Name}] 发送 [{this.EndCommand}]");
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);
                    }
                });
            }
        }
    }
}