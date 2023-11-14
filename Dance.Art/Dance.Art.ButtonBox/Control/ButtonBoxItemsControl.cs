using CommunityToolkit.Mvvm.Input;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dance.Art.ButtonBox
{
    /// <summary>
    /// 按钮组项控件
    /// </summary>
    public class ButtonBoxItemsControl : ListBox
    {
        static ButtonBoxItemsControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ButtonBoxItemsControl), new FrameworkPropertyMetadata(typeof(ButtonBoxItemsControl)));
        }

        // =================================================================================
        // Command

        #region DropCommand -- 拖拽释放命令

        /// <summary>
        /// 拖拽释放命令
        /// </summary>
        public RelayCommand<ButtonBoxPanelDropEventArgs> DropCommand
        {
            get { return (RelayCommand<ButtonBoxPanelDropEventArgs>)GetValue(DropCommandProperty); }
            set { SetValue(DropCommandProperty, value); }
        }

        /// <summary>
        /// 拖拽释放命令
        /// </summary>
        public static readonly DependencyProperty DropCommandProperty =
            DependencyProperty.Register("DropCommand", typeof(RelayCommand<ButtonBoxPanelDropEventArgs>), typeof(ButtonBoxItemsControl), new PropertyMetadata(null));

        #endregion


        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is ButtonBoxItem;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ButtonBoxItem();
        }
    }
}
