using Dance.Art.Domain;
using Dance.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace Dance.Art.Timeline
{
    /// <summary>
    /// 时间线元素模型基类
    /// </summary>
    public abstract class TimelineElementModelBase : ResourceElementModelBase, ITimelineElementModel, IDanceTimelineTrackElement, IDanceJsonObject
    {
        /// <summary>
        /// 时间线元素模型基类
        /// </summary>
        /// <param name="dataTemplate">数据模板</param>
        public TimelineElementModelBase(Type dataTemplate) : base(dataTemplate)
        {

        }

        // ==========================================================================================
        // Property

        #region BeginTime -- 开始时间

        private TimeSpan beginTime;
        /// <summary>
        /// 开始时间
        /// </summary>
        [Category(PropertyCategoryDefines.OTHER), Description("开始时间"), DisplayName("开始时间")]
        [Editor(typeof(TimelineBeginTimeEditor), typeof(TimelineBeginTimeEditor))]
        public TimeSpan BeginTime
        {
            get { return beginTime; }
            set { beginTime = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        #region EndTime -- 结束时间

        private TimeSpan endTime;
        /// <summary>
        /// 结束时间
        /// </summary>
        [Category(PropertyCategoryDefines.OTHER), Description("结束时间"), DisplayName("结束时间")]
        [Editor(typeof(TimelineEndTimeEditor), typeof(TimelineEndTimeEditor))]
        public TimeSpan EndTime
        {
            get { return endTime; }
            set { endTime = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        #region Content -- 内容

        private string? content = "标签";
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

        #region HorizontalAlignment -- 水平对齐

        private HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left;
        /// <summary>
        /// 水平对齐
        /// </summary>
        [Category(PropertyCategoryDefines.LAYOUT), PropertyOrder(2), Description("水平对齐"), DisplayName("水平对齐")]
        public HorizontalAlignment HorizontalAlignment
        {
            get { return horizontalAlignment; }
            set { horizontalAlignment = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        #region VerticalAlignment -- 垂直对齐

        private VerticalAlignment verticalAlignment = VerticalAlignment.Center;
        /// <summary>
        /// 垂直对齐
        /// </summary>
        [Category(PropertyCategoryDefines.LAYOUT), PropertyOrder(3), Description("垂直对齐"), DisplayName("垂直对齐")]
        public VerticalAlignment VerticalAlignment
        {
            get { return verticalAlignment; }
            set { verticalAlignment = value; this.OnWrapperPropertyChanged(); }
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

        #region IsTriggeiedBegin -- 是否已经触发了开始事件

        private bool isTriggeiedBegin;
        /// <summary>
        /// 是否已经触发了开始事件
        /// </summary>
        [Browsable(false), JsonIgnore]
        public bool IsTriggeiedBegin
        {
            get { return isTriggeiedBegin; }
            set { isTriggeiedBegin = value; }
        }

        #endregion

        #region IsTriggeiedEnd -- 是否已经触发了结束事件

        private bool isTriggeiedEnd;
        /// <summary>
        /// 是否已经触发了结束事件
        /// </summary>
        [Browsable(false), JsonIgnore]
        public bool IsTriggeiedEnd
        {
            get { return isTriggeiedEnd; }
            set { isTriggeiedEnd = value; }
        }


        #endregion

        // -------------------------------------------------------------------
        // Control

        /// <summary>
        /// 当开始时执行
        /// </summary>
        public abstract void OnBegin();

        /// <summary>
        /// 当结束时触发
        /// </summary>
        public abstract void OnEnd();
    }
}
