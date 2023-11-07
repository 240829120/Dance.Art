using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 设备文档视图模型
    /// </summary>
    public interface IDeviceDocumentViewModel : IDocumentViewModel
    {
        /// <summary>
        /// 关联模型
        /// </summary>
        DeviceModel? Model { get; }

        /// <summary>
        /// 确定命令
        /// </summary>
        RelayCommand EnterCommand { get; }

        /// <summary>
        /// 还原命令
        /// </summary>
        RelayCommand ReloadCommand { get; }
    }
}
