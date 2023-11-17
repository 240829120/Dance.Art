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

namespace Dance.Art.ControlGrid
{
    /// <summary>
    /// 控制面板项
    /// </summary>
    public class ControlGridItem : ListBoxItem
    {
        static ControlGridItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ControlGridItem), new FrameworkPropertyMetadata(typeof(ControlGridItem)));
        }

        public ControlGridItem()
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
            DependencyProperty.Register("Row", typeof(int), typeof(ControlGridItem), new PropertyMetadata(0, new PropertyChangedCallback((s, e) =>
            {
                if (s is not ControlGridItem item)
                    return;

                if (DanceXamlExpansion.GetVisualTreeParent<ControlGrid>(item) is not ControlGrid owner)
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
            DependencyProperty.Register("Column", typeof(int), typeof(ControlGridItem), new PropertyMetadata(0, new PropertyChangedCallback((s, e) =>
            {
                if (s is not ControlGridItem item)
                    return;

                if (DanceXamlExpansion.GetVisualTreeParent<ControlGrid>(item) is not ControlGrid owner)
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
            DependencyProperty.Register("IsDesignMode", typeof(bool), typeof(ControlGridItem), new PropertyMetadata(false));

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

            if (!this.IsDesignMode)
            {
                e.IsCancel = true;
                return;
            }

            e.Data = this;
        }

        #endregion
    }
}
