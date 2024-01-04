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

namespace Dance.Art.ControlGrid
{
    /// <summary>
    /// 控制面板
    /// </summary>
    public class ControlGridPanel : Panel
    {
        public ControlGridPanel()
        {
            this.Loaded += ControlGridPanel_Loaded;
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
        private ControlGrid? Owner;

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
            DependencyProperty.Register("HighlightBrush", typeof(SolidColorBrush), typeof(ControlGridPanel), new PropertyMetadata(Brushes.Red));

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
            DependencyProperty.Register("HighlightRow", typeof(int?), typeof(ControlGridPanel), new PropertyMetadata(null));

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
            DependencyProperty.Register("HighlightColumn", typeof(int?), typeof(ControlGridPanel), new PropertyMetadata(null));

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
            if (this.Owner == null || this.Owner.ItemsSource == null)
            {
                double destWidth = availableSize.Width == double.PositiveInfinity ? 0 : availableSize.Width;
                double destHeight = availableSize.Height == double.PositiveInfinity ? 0 : availableSize.Height;

                return new Size(destWidth, destHeight);
            }

            foreach (FrameworkElement element in Children)
            {
                if (element == null)
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
            if (this.Owner == null || this.Owner.ItemsSource == null)
            {
                double destWidth = finalSize.Width == double.PositiveInfinity ? 0 : finalSize.Width;
                double destHeight = finalSize.Height == double.PositiveInfinity ? 0 : finalSize.Height;

                return new Size(destWidth, destHeight);
            }

            foreach (FrameworkElement element in Children)
            {
                if (element == null || element.DataContext is not ControlGridItemModelBase model)
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
            if (this.Owner == null || !this.Owner.IsDesignMode)
                return;

            ResourceInfoItemModel? resource = e.Data.GetData(typeof(ResourceInfoItemModel)) as ResourceInfoItemModel;
            ControlGridItem? item = e.Data.GetData(typeof(ControlGridItem)) as ControlGridItem;

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
            if (this.Owner == null || !this.Owner.IsDesignMode)
                return;

            ResourceInfoItemModel? resource = e.Data.GetData(typeof(ResourceInfoItemModel)) as ResourceInfoItemModel;
            ControlGridItem? item = e.Data.GetData(typeof(ControlGridItem)) as ControlGridItem;

            if (resource == null && item == null)
                return;

            this.SetHighlight(e);
        }

        /// <summary>
        /// 释放拖拽
        /// </summary>
        protected override void OnDrop(DragEventArgs e)
        {
            if (this.Owner == null || !this.Owner.IsDesignMode)
                return;

            this.ClearHighlight();

            if (ArtDomain.Current.ProjectDomain == null)
                return;

            ResourceInfoItemModel? resource = e.Data.GetData(typeof(ResourceInfoItemModel)) as ResourceInfoItemModel;

            ControlGridItemModelBase? model = null;

            if (resource != null)
            {
                model = resource.Source?.CreateInstance(ArtDomain.Current.ProjectDomain) as ControlGridItemModelBase;
            }

            if (e.Data.GetData(typeof(ControlGridItem)) is ControlGridItem item)
            {
                model = item.DataContext as ControlGridItemModelBase;
            }

            if (model == null || e.Source is FrameworkElement source && source.DataContext == model)
                return;

            Point point = e.GetPosition(this);
            int highlightRow = (int)(point.Y / this.Owner.UnitHeight);
            int highlightColumn = (int)(point.X / this.Owner.UnitWidth);

            if ((highlightRow >= 0 && highlightRow < this.Owner.Rows) && (highlightColumn >= 0 && highlightColumn < this.Owner.Columns))
            {
                this.Owner.DropCommand?.Execute(new ControlGridDropEventArgs(highlightRow, highlightColumn, resource, model));
            }
        }

        /// <summary>
        /// 鼠标左键按下
        /// </summary>
        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (this.Owner == null || this.Owner.ItemsSource == null || !this.Owner.IsDesignMode)
            {
                base.OnPreviewMouseLeftButtonDown(e);
                return;
            }

            Point point = e.GetPosition(this);
            int row = (int)(point.Y / this.Owner.UnitHeight);
            int column = (int)(point.X / this.Owner.UnitWidth);

            foreach (ControlGridItemModelBase item in this.Owner.ItemsSource)
            {
                if (item == null)
                    continue;

                if (item.Row == row && item.Column == column)
                {
                    this.Owner.IsSelectedCanvas = false;
                    this.Owner.SelectedValue = item;

                    e.Handled = true;

                    return;
                }
            }
        }

        // =================================================================================
        // Public Function

        /// <summary>
        /// 更新画布大小
        /// </summary>
        public void UpdateCanvasSize()
        {
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
        private void ControlGridPanel_Loaded(object sender, RoutedEventArgs e)
        {
            if (DanceXamlExpansion.GetVisualTreeParent<ControlGrid>(this) is not ControlGrid owner)
                return;

            this.Owner = owner;
            this.Owner.PART_Panel = this;

            this.InvalidateVisual();
        }

        /// <summary>
        /// 设置高亮
        /// </summary>
        /// <param name="e"></param>
        private void SetHighlight(DragEventArgs e)
        {
            this.Owner ??= DanceXamlExpansion.GetVisualTreeParent<ControlGrid>(this);
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