using CommunityToolkit.Mvvm.Input;
using Dance.Wpf;
using SharpVectors.Dom;
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
    [TemplatePart(Name = nameof(PART_Panel), Type = typeof(ButtonBoxItemsPanel))]
    public class ButtonBoxItemsControl : ListBox
    {
        static ButtonBoxItemsControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ButtonBoxItemsControl), new FrameworkPropertyMetadata(typeof(ButtonBoxItemsControl)));
        }

        // =================================================================================
        // Field

        /// <summary>
        /// 项面板
        /// </summary>
        internal ButtonBoxItemsPanel? PART_Panel;

        // =================================================================================
        // Property

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
            DependencyProperty.Register("IsDesignMode", typeof(bool), typeof(ButtonBoxItemsControl), new PropertyMetadata(false));

        #endregion

        #region IsSelectedCanvas -- 是否选中画布

        /// <summary>
        /// 是否选中画布
        /// </summary>
        public bool IsSelectedCanvas
        {
            get { return (bool)GetValue(IsSelectedCanvasProperty); }
            set { SetValue(IsSelectedCanvasProperty, value); }
        }

        /// <summary>
        /// 是否选中画布
        /// </summary>
        public static readonly DependencyProperty IsSelectedCanvasProperty =
            DependencyProperty.Register("IsSelectedCanvas", typeof(bool), typeof(ButtonBoxItemsControl), new PropertyMetadata(false));

        #endregion

        #region UnitWidth -- 单位宽度

        /// <summary>
        /// 单位宽度
        /// </summary>
        public int UnitWidth
        {
            get { return (int)GetValue(UnitWidthProperty); }
            set { SetValue(UnitWidthProperty, value); }
        }

        /// <summary>
        /// 单位宽度
        /// </summary>
        public static readonly DependencyProperty UnitWidthProperty =
            DependencyProperty.Register("UnitWidth", typeof(int), typeof(ButtonBoxItemsControl), new PropertyMetadata(120, new PropertyChangedCallback((s, e) =>
            {
                if (s is not ButtonBoxItemsControl element)
                    return;

                element.PART_Panel?.UpdateCanvasSize();
            })));

        #endregion

        #region UnitHeight -- 单位高度

        /// <summary>
        /// 单位高度
        /// </summary>
        public int UnitHeight
        {
            get { return (int)GetValue(UnitHeightProperty); }
            set { SetValue(UnitHeightProperty, value); }
        }

        /// <summary>
        /// 单位高度
        /// </summary>
        public static readonly DependencyProperty UnitHeightProperty =
            DependencyProperty.Register("UnitHeight", typeof(int), typeof(ButtonBoxItemsControl), new PropertyMetadata(40, new PropertyChangedCallback((s, e) =>
            {
                if (s is not ButtonBoxItemsControl element)
                    return;

                element.PART_Panel?.UpdateCanvasSize();
            })));

        #endregion

        #region Rows -- 行数

        /// <summary>
        /// 行数
        /// </summary>
        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        /// <summary>
        /// 行数
        /// </summary>
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(ButtonBoxItemsControl), new PropertyMetadata(6, new PropertyChangedCallback((s, e) =>
            {
                if (s is not ButtonBoxItemsControl element)
                    return;

                element.PART_Panel?.UpdateCanvasSize();
            })));

        #endregion

        #region Columns -- 列数

        /// <summary>
        /// 列数
        /// </summary>
        public int Columns
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        /// <summary>
        /// 列数
        /// </summary>
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register("Columns", typeof(int), typeof(ButtonBoxItemsControl), new PropertyMetadata(6, new PropertyChangedCallback((s, e) =>
            {
                if (s is not ButtonBoxItemsControl element)
                    return;

                element.PART_Panel?.UpdateCanvasSize();
            })));

        #endregion

        // =================================================================================
        // Command

        #region DropCommand -- 拖拽释放命令

        /// <summary>
        /// 拖拽释放命令
        /// </summary>
        public RelayCommand<ButtonBoxItemsControlDropEventArgs> DropCommand
        {
            get { return (RelayCommand<ButtonBoxItemsControlDropEventArgs>)GetValue(DropCommandProperty); }
            set { SetValue(DropCommandProperty, value); }
        }

        /// <summary>
        /// 拖拽释放命令
        /// </summary>
        public static readonly DependencyProperty DropCommandProperty =
            DependencyProperty.Register("DropCommand", typeof(RelayCommand<ButtonBoxItemsControlDropEventArgs>), typeof(ButtonBoxItemsControl), new PropertyMetadata(null));

        #endregion

        // =================================================================================
        // Override

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is ButtonBoxItem;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ButtonBoxItem();
        }

        // =================================================================================
        // Public Function

        /// <summary>
        /// 刷新
        /// </summary>
        public void Refresh()
        {
            Application.Current.Dispatcher.BeginInvoke(() =>
            {
                this.PART_Panel?.InvalidateVisual();
            });
        }
    }
}
