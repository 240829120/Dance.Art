using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 连接分组
    /// </summary>
    public class ConnectionGroupModel : DanceModel
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

        #region Connections -- 连接集合

        /// <summary>
        /// 连接集合
        /// </summary>
        public ObservableCollection<ConnectionModel> Connections { get; } = new();

        #endregion

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            base.Destroy();

            this.Connections.ForEach(p => p.Dispose());
        }
    }
}
