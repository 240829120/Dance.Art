using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Dance.Art.Domain;

namespace Dance.Art.Module
{
    /// <summary>
    /// 连接状态小部件
    /// </summary>
    public class ConnectionStatusTip : Control
    {
        static ConnectionStatusTip()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ConnectionStatusTip), new FrameworkPropertyMetadata(typeof(ConnectionStatusTip)));
        }

        #region Status -- 状态

        /// <summary>
        /// 状态
        /// </summary>
        public ConnectionStatus Status
        {
            get { return (ConnectionStatus)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(ConnectionStatus), typeof(ConnectionStatusTip), new PropertyMetadata(ConnectionStatus.Disconnected));

        #endregion
    }
}
