using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 数据源类别模型
    /// </summary>
    public class DataSourceCategoryModel : DanceModel
    {
        #region Category -- 类别

        private string? category;
        /// <summary>
        /// 类别
        /// </summary>
        public string? Category
        {
            get { return category; }
            set { category = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Items -- 项集合

        private List<DataSourcePluginInfo>? items;
        /// <summary>
        /// 项集合
        /// </summary>
        public List<DataSourcePluginInfo>? Items
        {
            get { return items; }
            set { items = value; this.OnPropertyChanged(); }
        }

        #endregion
    }
}
