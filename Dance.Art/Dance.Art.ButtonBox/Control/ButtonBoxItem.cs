using CommunityToolkit.Mvvm.Input;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Dance.Art.ButtonBox
{
    /// <summary>
    /// 按钮组文档项
    /// </summary>
    public class ButtonBoxItem : ListBoxItem
    {
        static ButtonBoxItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ButtonBoxItem), new FrameworkPropertyMetadata(typeof(ButtonBoxItem)));
        }

        public ButtonBoxItem()
        {
            this.DragBeginCommand = new(this.DragBegin);
        }

        // ===========================================================================================================
        // Property

        #region Row -- 行

        /// <summary>
        /// 行
        /// </summary>
        public int Row
        {
            get { return (int)GetValue(RowProperty); }
            set { SetValue(RowProperty, value); }
        }

        /// <summary>
        /// 行
        /// </summary>
        public static readonly DependencyProperty RowProperty =
            DependencyProperty.Register("Row", typeof(int), typeof(ButtonBoxItem), new PropertyMetadata(0, new PropertyChangedCallback((s, e) =>
            {
                if (s is not ButtonBoxItem item)
                    return;

                if (DanceXamlExpansion.GetVisualTreeParent<ButtonBoxItemsControl>(item) is not ButtonBoxItemsControl owner)
                    return;

                owner.PART_Panel?.InvalidateVisual();
            })));

        #endregion

        #region Column -- 列

        /// <summary>
        /// 列
        /// </summary>
        public int Column
        {
            get { return (int)GetValue(ColumnProperty); }
            set { SetValue(ColumnProperty, value); }
        }

        /// <summary>
        /// 列
        /// </summary>
        public static readonly DependencyProperty ColumnProperty =
            DependencyProperty.Register("Column", typeof(int), typeof(ButtonBoxItem), new PropertyMetadata(0, new PropertyChangedCallback((s, e) =>
            {
                if (s is not ButtonBoxItem item)
                    return;

                if (DanceXamlExpansion.GetVisualTreeParent<ButtonBoxItemsControl>(item) is not ButtonBoxItemsControl owner)
                    return;

                owner.PART_Panel?.InvalidateVisual();
            })));

        #endregion

        #region IsDesignMode -- 是否是设计模式

        /// <summary>
        /// 是否是设计模式
        /// </summary>
        public bool IsDesignMode
        {
            get { return (bool)GetValue(IsDesignModeProperty); }
            set { SetValue(IsDesignModeProperty, value); }
        }

        /// <summary>
        /// 是否是设计模式
        /// </summary>
        public static readonly DependencyProperty IsDesignModeProperty =
            DependencyProperty.Register("IsDesignMode", typeof(bool), typeof(ButtonBoxItem), new PropertyMetadata(false));

        #endregion

        // ===========================================================================================================
        // Command

        #region DragBeginCommand -- 拖拽开始命令

        /// <summary>
        /// 拖拽开始命令
        /// </summary>
        public RelayCommand<DanceDragBeginEventArgs> DragBeginCommand { get; private set; }

        /// <summary>
        /// 拖拽开始命令
        /// </summary>
        private void DragBegin(DanceDragBeginEventArgs? e)
        {
            if (e == null)
                return;

            e.Data = this;
        }

        #endregion
    }
}
