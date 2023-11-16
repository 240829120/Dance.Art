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

namespace Dance.Art.ButtonBox
{
    /// <summary>
    /// 命令按钮
    /// </summary>
    [DisplayName("命令按钮")]
    public class CommandButtonModel : ButtonBoxItemModelBase
    {
        /// <summary>
        /// 命令按钮
        /// </summary>
        public CommandButtonModel() : base(typeof(CommandButton))
        {
            this.ClickCommand = new(this.Click);
        }

        // ================================================================================
        // Field

        /// <summary>
        /// 输出管理器
        /// </summary>
        private readonly IOutputManager OutputManager = DanceDomain.Current.LifeScope.Resolve<IOutputManager>();

        // ================================================================================
        // Property

        #region Content -- 内容

        private string? content = "命令按钮";
        /// <summary>
        /// 内容
        /// </summary>
        [Category(PropertyCategoryDefines.OTHER), Description("内容"), DisplayName("内容")]
        public string? Content
        {
            get { return content; }
            set { content = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

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

        #region Command -- 命令

        private string? command;
        /// <summary>
        /// 命令
        /// </summary>
        [Category(PropertyCategoryDefines.OTHER), Description("命令"), DisplayName("命令")]
        [Editor(typeof(MultiLineEditor), typeof(MultiLineEditor))]
        public string? Command
        {
            get { return command; }
            set { command = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        // ================================================================================
        // Command

        #region ClickCommand -- 点击命令

        /// <summary>
        /// 点击命令
        /// </summary>
        [Browsable(false), JsonIgnore]
        public RelayCommand ClickCommand { get; private set; }

        /// <summary>
        /// 点击
        /// </summary>
        private void Click()
        {
            if (ArtDomain.Current.ProjectDomain == null || string.IsNullOrWhiteSpace(this.DeviceNames) || string.IsNullOrWhiteSpace(this.Command))
                return;

            List<string> devices = this.DeviceNames.Split(DeviceChooseEditor.SEPARATOR, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (devices.Count == 0)
                return;

            List<DeviceModel> deviceModels = ArtDomain.Current.ProjectDomain.GetDeviceModel(devices);
            if (deviceModels.Count == 0)
                return;

            byte[] buffer = Encoding.UTF8.GetBytes(this.Command);

            foreach (DeviceModel model in deviceModels)
            {
                if (model == null || model.Source == null || model.Source is not ISendDeviceSource source)
                    return;

                Task.Run(() =>
                {
                    try
                    {
                        source.Send(buffer);
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);
                    }
                });
            }
        }

        #endregion
    }
}
