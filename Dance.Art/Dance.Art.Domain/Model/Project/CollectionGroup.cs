using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 连接分组
    /// </summary>
    public class CollectionGroup : DanceModel
    {
        #region Group -- 分组

        private string? group;
        /// <summary>
        /// 分组
        /// </summary>
        public string? Group
        {
            get { return group; }
            set { group = value; this.OnPropertyChanged(); }
        }

        #endregion


    }
}
