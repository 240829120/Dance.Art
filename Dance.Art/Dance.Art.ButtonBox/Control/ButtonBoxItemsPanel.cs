using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CommunityToolkit.Mvvm.Input;
using Dance.Art.Domain;
using Dance.Wpf;

namespace Dance.Art.ButtonBox
{
    /// <summary>
    /// 按钮文档面板
    /// </summary>
    public class ButtonBoxItemsPanel : Panel
    {
        public ButtonBoxItemsPanel()
        {
            this.Loaded += ButtonBoxItemsPanel_Loaded; ;
        }

        // =================================================================================
        // Field

        /// <summary>
        /// 画笔
        /// </summary>
        private readonly Pen Pen = new(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFEFEFEF")), 1);

        /// <summary>
        /// 所属控件
        /// </summary>
        private ButtonBoxItemsControl? Owner;

        // =================================================================================
        // Property

        #region HighlightBrush -- 高亮画刷

        /// <summary>
        /// 高亮画刷
        /// </summary>
        public SolidColorBrush HighlightBrush
        {
            get { return (SolidColorBrush)GetValue(HighlightBrushProperty); }
            set { SetValue(HighlightBrushProperty, value); }
        }

        /// <summary>
        /// 高亮画刷
        /// </summary>
        public static readonly DependencyProperty HighlightBrushProperty =
            DependencyProperty.Register("HighlightBrush", typeof(SolidColorBrush), typeof(ButtonBoxItemsPanel), new PropertyMetadata(Brushes.Red));

        #endregion

        #region HighlightRow -- 高亮行

        /// <summary>
        /// 高亮行
        /// </summary>
        public int? HighlightRow
        {
            get { return (int?)GetValue(HighlightRowProperty); }
            set { SetValue(HighlightRowProperty, value); }
        }

        /// <summary>
        /// 高亮行
        /// </summary>
        public static readonly DependencyProperty HighlightRowProperty =
            DependencyProperty.Register("HighlightRow", typeof(int?), typeof(ButtonBoxItemsPanel), new PropertyMetadata(null));

        #endregion

        #region HighlightColumn -- 高亮列

        /// <summary>
        /// 高亮列
        /// </summary>
        public int? HighlightColumn
        {
            get { return (int?)GetValue(HighlightColumnProperty); }
            set { SetValue(HighlightColumnProperty, value); }
        }

        /// <summary>
        /// 高亮列
        /// </summary>
        public static readonly DependencyProperty HighlightColumnProperty =
            DependencyProperty.Register("HighlightColumn", typeof(int?), typeof(ButtonBoxItemsPanel), new PropertyMetadata(null));

        #endregion

        // =================================================================================
        // Override

        /// <summary>
        /// 测量
        /// </summary>
        /// <param name="availableSize">可用区域</param>
        /// <returns>测量结果</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            this.Owner ??= DanceXamlExpansion.GetVisualTreeParent<ButtonBoxItemsControl>(this);
            if (this.Owner == null || this.Owner.ItemsSource == null)
                return availableSize;

            foreach (FrameworkElement element in Children)
            {
                if (element == null || element.DataContext is not ButtonBoxItemModelBase model)
                    continue;

                element.Measure(new Size(this.Owner.UnitWidth, this.Owner.UnitHeight));
            }

            return new Size(this.Owner.Columns * this.Owner.UnitWidth, this.Owner.Rows * this.Owner.UnitHeight);
        }

        /// <summary>
        /// 布局
        /// </summary>
        /// <param name="finalSize">可用区域</param>
        /// <returns>布局结果</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            this.Owner ??= DanceXamlExpansion.GetVisualTreeParent<ButtonBoxItemsControl>(this);
            if (this.Owner == null || this.Owner.ItemsSource == null)
                return finalSize;

            foreach (FrameworkElement element in Children)
            {
                if (element == null || element.DataContext is not ButtonBoxItemModelBase model)
                    continue;

                element.Arrange(new Rect(model.Column * this.Owner.UnitWidth, model.Row * this.Owner.UnitHeight, this.Owner.UnitWidth, this.Owner.UnitHeight));
            }

            double width = this.Owner.Columns * this.Owner.UnitWidth;
            double height = this.Owner.Rows * this.Owner.UnitHeight;

            return new Size(width, height);
        }

        /// <summary>
        /// 渲染
        /// </summary>
        protected override void OnRender(DrawingContext dc)
        {
            this.Owner ??= DanceXamlExpansion.GetVisualTreeParent<ButtonBoxItemsControl>(this);
            if (this.Owner == null)
                return;

            base.OnRender(dc);

            for (int r = 0; r <= this.Owner.Rows; ++r)
            {
                dc.DrawSnappedLinesBetweenPoints(this.Pen, 1, new(0, r * this.Owner.UnitHeight), new(this.Owner.Columns * this.Owner.UnitWidth, r * this.Owner.UnitHeight));
            }
            for (int c = 0; c <= this.Owner.Columns; ++c)
            {
                dc.DrawSnappedLinesBetweenPoints(this.Pen, 1, new(c * this.Owner.UnitWidth, 0), new(c * this.Owner.UnitWidth, this.Owner.Rows * this.Owner.UnitHeight));
            }

            if (!this.Owner.IsDesignMode)
                return;

            if (this.HighlightRow != null && this.HighlightColumn != null)
            {
                dc.DrawRectangle(this.HighlightBrush, this.Pen, new Rect(this.HighlightColumn.Value * this.Owner.UnitWidth, this.HighlightRow.Value * this.Owner.UnitHeight, this.Owner.UnitWidth, this.Owner.UnitHeight));
            }
        }

        /// <summary>
        /// 拖拽进入
        /// </summary>
        protected override void OnDragEnter(DragEventArgs e)
        {
            this.Owner ??= DanceXamlExpansion.GetVisualTreeParent<ButtonBoxItemsControl>(this);
            if (this.Owner == null || !this.Owner.IsDesignMode)
                return;

            ResourceInfoItemModel? resource = e.Data.GetData(typeof(ResourceInfoItemModel)) as ResourceInfoItemModel;
            ButtonBoxItem? item = e.Data.GetData(typeof(ButtonBoxItem)) as ButtonBoxItem;

            if (resource == null && item == null)
                return;

            this.SetHighlight(e);
        }

        /// <summary>
        /// 拖拽离开
        /// </summary>
        protected override void OnDragLeave(DragEventArgs e)
        {
            this.ClearHighlight();
        }

        /// <summary>
        /// 拖拽经过
        /// </summary>
        protected override void OnDragOver(DragEventArgs e)
        {
            this.Owner ??= DanceXamlExpansion.GetVisualTreeParent<ButtonBoxItemsControl>(this);
            if (this.Owner == null || !this.Owner.IsDesignMode)
                return;

            ResourceInfoItemModel? resource = e.Data.GetData(typeof(ResourceInfoItemModel)) as ResourceInfoItemModel;
            ButtonBoxItem? item = e.Data.GetData(typeof(ButtonBoxItem)) as ButtonBoxItem;

            if (resource == null && item == null)
                return;

            this.SetHighlight(e);
        }

        /// <summary>
        /// 释放拖拽
        /// </summary>
        protected override void OnDrop(DragEventArgs e)
        {
            this.Owner ??= DanceXamlExpansion.GetVisualTreeParent<ButtonBoxItemsControl>(this);
            if (this.Owner == null || !this.Owner.IsDesignMode)
                return;

            this.ClearHighlight();

            if (ArtDomain.Current.ProjectDomain == null)
                return;

            ResourceInfoItemModel? resource = e.Data.GetData(typeof(ResourceInfoItemModel)) as ResourceInfoItemModel;
            ButtonBoxItem? item = e.Data.GetData(typeof(ButtonBoxItem)) as ButtonBoxItem;

            ButtonBoxItemModelBase? model = null;

            if (resource != null)
            {
                model = resource.Source?.CreateInstance(ArtDomain.Current.ProjectDomain) as ButtonBoxItemModelBase;
            }

            if (item != null)
            {
                model = item.DataContext as ButtonBoxItemModelBase;
            }

            if (model == null || e.Source is FrameworkElement source && source.DataContext == model)
                return;

            Point point = e.GetPosition(this);
            int highlightRow = (int)(point.Y / this.Owner.UnitHeight);
            int highlightColumn = (int)(point.X / this.Owner.UnitWidth);

            if ((highlightRow >= 0 && highlightRow < this.Owner.Rows) && (highlightColumn >= 0 && highlightColumn < this.Owner.Columns))
            {
                this.Owner.DropCommand?.Execute(new ButtonBoxItemsControlDropEventArgs(highlightRow, highlightColumn, resource, model));
            }
        }

        /// <summary>
        /// 鼠标左键按下
        /// </summary>
        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);

            this.Owner ??= DanceXamlExpansion.GetVisualTreeParent<ButtonBoxItemsControl>(this);
            if (this.Owner == null || this.Owner.ItemsSource == null || !this.Owner.IsDesignMode)
                return;

            this.Owner.IsSelectedCanvas = false;
        }

        /// <summary>
        /// 鼠标左键按下
        /// </summary>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            this.Owner ??= DanceXamlExpansion.GetVisualTreeParent<ButtonBoxItemsControl>(this);
            if (this.Owner == null || this.Owner.ItemsSource == null || !this.Owner.IsDesignMode)
                return;

            Point point = e.GetPosition(this);
            int row = (int)(point.Y / this.Owner.UnitHeight);
            int column = (int)(point.X / this.Owner.UnitWidth);

            foreach (ButtonBoxItemModelBase item in this.Owner.ItemsSource)
            {
                if (item == null)
                    continue;

                if (item.Row == row && item.Column == column)
                {
                    this.Owner.IsSelectedCanvas = false;
                    this.Owner.SelectedValue = item;

                    return;
                }
            }

            this.Owner.SelectedValue = null;
            this.Owner.IsSelectedCanvas = true;
        }

        // =================================================================================
        // Public Function

        /// <summary>
        /// 更新画布大小
        /// </summary>
        public void UpdateCanvasSize()
        {
            this.Owner ??= DanceXamlExpansion.GetVisualTreeParent<ButtonBoxItemsControl>(this);
            if (this.Owner == null || this.Owner.ItemsSource == null || !this.Owner.IsDesignMode)
                return;

            this.Width = this.Owner.Columns * this.Owner.UnitWidth;
            this.Height = this.Owner.Rows * this.Owner.UnitHeight;
        }

        // =================================================================================
        // Private Function

        /// <summary>
        /// 加载完成
        /// </summary>
        private void ButtonBoxItemsPanel_Loaded(object sender, RoutedEventArgs e)
        {
            if (DanceXamlExpansion.GetVisualTreeParent<ButtonBoxItemsControl>(this) is not ButtonBoxItemsControl owner)
                return;

            owner.PART_Panel = this;
        }

        /// <summary>
        /// 设置高亮
        /// </summary>
        /// <param name="e"></param>
        private void SetHighlight(DragEventArgs e)
        {
            this.Owner ??= DanceXamlExpansion.GetVisualTreeParent<ButtonBoxItemsControl>(this);
            if (this.Owner == null || this.Owner.ItemsSource == null)
                return;

            Point point = e.GetPosition(this);
            int highlightRow = (int)(point.Y / this.Owner.UnitHeight);
            int highlightColumn = (int)(point.X / this.Owner.UnitWidth);

            this.HighlightRow = (highlightRow >= 0 && highlightRow < this.Owner.Rows) ? highlightRow : null;
            this.HighlightColumn = (highlightColumn >= 0 && highlightColumn < this.Owner.Columns) ? highlightColumn : null;

            this.InvalidateVisual();
        }

        /// <summary>
        /// 清理高亮
        /// </summary>
        private void ClearHighlight()
        {
            this.HighlightRow = null;
            this.HighlightColumn = null;

            this.InvalidateVisual();
        }

    }
}