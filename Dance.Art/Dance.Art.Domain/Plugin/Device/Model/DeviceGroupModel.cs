using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 设备分组模型
    /// </summary>
    public class DeviceGroupModel : DanceModel
    {
        #region Name -- 名称

        private string? name;
        /// <summary>
        /// 名称
        /// </summary>
        public string? Name
        {
            get { return name; }
            set { name = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Items -- 子项集合

        /// <summary>
        /// 子项集合
        /// </summary>
        public ObservableCollection<DeviceModel> Items { get; } = new();

        #endregion
    }
}
