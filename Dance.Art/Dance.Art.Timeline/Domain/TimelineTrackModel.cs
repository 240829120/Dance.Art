using Dance.Art.Domain;
using Dance.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace Dance.Art.Timeline
{
    /// <summary>
    /// 时间线模型
    /// </summary>
    [DisplayName("轨道")]
    public class TimelineTrackModel : DocumentItemModelBase, IDanceTimelineTrack<DocumentWrapperCollection<TimelineElementModelBase>, TimelineElementModelBase>, IDanceJsonObject
    {
        // ==========================================================================================
        // Property

        #region Name -- 名称

        private string? name;
        /// <summary>
        /// 名称
        /// </summary>
        [Category(PropertyCategoryDefines.BASE), PropertyOrder(0), Description("名称"), DisplayName("名称")]
        public string? Name
        {
            get { return name; }
            set { name = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        #region ForegroundColor -- 前景色

        private Color foregroundColor = Colors.Black;
        /// <summary>
        /// 前景色
        /// </summary>
        [Category(PropertyCategoryDefines.STYLE), Description("前景色"), DisplayName("前景色")]
        public Color ForegroundColor
        {
            get { return foregroundColor; }
            set { foregroundColor = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        #region BackgroundColor -- 背景颜色

        private Color backgroundColor = (Color)ColorConverter.ConvertFromString("#FFFAFAFA");
        /// <summary>
        /// 背景颜色
        /// </summary>
        [Category(PropertyCategoryDefines.STYLE), Description("背景颜色"), DisplayName("背景颜色")]
        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        #region Items -- 子项集合

        private DocumentWrapperCollection<TimelineElementModelBase> items = new();
        /// <summary>
        /// 子项集合
        /// </summary>
        [Browsable(false)]
        public DocumentWrapperCollection<TimelineElementModelBase> Items
        {
            get { return items; }
            set { items = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        // -------------------------------------------------------------------
        // Control

        #region IsSelected -- 是否被选中

        private bool isSelected;
        /// <summary>
        /// 是否被选中
        /// </summary>
        [Browsable(false), JsonIgnore]
        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; this.OnPropertyChanged(); }
        }

        #endregion

        // -------------------------------------------------------------------
        // Override

        protected override void Destroy()
        {
            foreach (TimelineElementModelBase item in this.Items)
            {
                item.Dispose();
            }
        }
    }
}
