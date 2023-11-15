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
    /// 设计模式标志
    /// </summary>
    public class DesignModeTip : Button
    {
        static DesignModeTip()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DesignModeTip), new FrameworkPropertyMetadata(typeof(DesignModeTip)));
        }

        #region DesignMode -- 设计模式

        /// <summary>
        /// 设计模式
        /// </summary>
        public DocumentDesignMode DesignMode
        {
            get { return (DocumentDesignMode)GetValue(DesignModeProperty); }
            set { SetValue(DesignModeProperty, value); }
        }

        /// <summary>
        /// 设计模式
        /// </summary>
        public static readonly DependencyProperty DesignModeProperty =
            DependencyProperty.Register("DesignMode", typeof(DocumentDesignMode), typeof(DesignModeTip), new PropertyMetadata(DocumentDesignMode.NotSupport));

        #endregion
    }
}
