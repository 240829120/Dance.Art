using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace Dance.Art.Timeline
{
    /// <summary>
    /// 时间线模型
    /// </summary>
    [DisplayName("时间线面板")]
    public class TimelineModel : DocumentItemModelBase
    {
        #region Duration -- 持续时间

        private TimeSpan duration = TimeSpan.FromHours(1);
        /// <summary>
        /// 持续时间
        /// </summary>
        [Category(PropertyCategoryDefines.OTHER), PropertyOrder(0), Description("时长"), DisplayName("时长")]
        public TimeSpan Duration
        {
            get { return duration; }
            set { duration = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion
    }
}
