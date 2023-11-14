using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 资源分组信息模型
    /// </summary>
    public class ResourceInfoGroupModel : ResourceInfoItemModel
    {
        #region IsExpanded -- 是否展开

        private bool isExpanded;
        /// <summary>
        /// 是否展开
        /// </summary>
        public bool IsExpanded
        {
            get { return isExpanded; }
            set { isExpanded = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Items -- 项集合

        /// <summary>
        /// 项集合
        /// </summary>
        public List<ResourceInfoItemModel> Items { get; } = new();

        #endregion
    }
}
