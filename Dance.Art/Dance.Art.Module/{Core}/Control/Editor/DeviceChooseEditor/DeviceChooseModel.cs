using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Module
{
    /// <summary>
    /// 设备选择模型
    /// </summary>
    /// <param name="device">设备</param>
    public class DeviceChooseModel(DeviceModel device) : DanceModel
    {
        #region IsSelected -- 是否被选中

        private bool isSelected;
        /// <summary>
        /// 是否被选中
        /// </summary>
        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Device -- 设备

        private DeviceModel device = device;
        /// <summary>
        /// 设备
        /// </summary>
        public DeviceModel Device
        {
            get { return device; }
            set { device = value; this.OnPropertyChanged(); }
        }

        #endregion
    }
}
