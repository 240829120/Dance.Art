using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dance.Art.Module
{
    /// <summary>
    /// 数据状态小部件
    /// </summary>
    public class DataSourceStatusTip : Control
    {
        static DataSourceStatusTip()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DataSourceStatusTip), new FrameworkPropertyMetadata(typeof(DataSourceStatusTip)));
        }

        #region Status -- 状态

        /// <summary>
        /// 状态
        /// </summary>
        public DataSourceStatus Status
        {
            get { return (DataSourceStatus)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(DataSourceStatus), typeof(DataSourceStatusTip), new PropertyMetadata(DataSourceStatus.Waiting));

        #endregion
    }
}
