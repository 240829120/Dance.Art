using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CommunityToolkit.Mvvm.Input;
using Dance.Art.Domain;
using Dance.Wpf;

namespace Dance.Art.ButtonBox
{
    /// <summary>
    /// 按钮文档面板
    /// </summary>
    public class ButtonBoxPanel : Panel
    {
        // =================================================================================
        // Field

        /// <summary>
        /// 画笔
        /// </summary>
        private readonly Pen Pen = new(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFEFEFEF")), 1);

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
            DependencyProperty.Register("HighlightBrush", typeof(SolidColorBrush), typeof(ButtonBoxPanel), new PropertyMetadata(Brushes.Red));

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
            DependencyProperty.Register("UnitWidth", typeof(int), typeof(ButtonBoxPanel), new PropertyMetadata(120, new PropertyChangedCallback((s, e) =>
            {
                if (s is not ButtonBoxPanel panel)
                    return;

                panel.InvalidateVisual();
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
            DependencyProperty.Register("UnitHeight", typeof(int), typeof(ButtonBoxPanel), new PropertyMetadata(40, new PropertyChangedCallback((s, e) =>
            {
                if (s is not ButtonBoxPanel panel)
                    return;

                panel.InvalidateVisual();
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
            DependencyProperty.Register("Rows", typeof(int), typeof(ButtonBoxPanel), new PropertyMetadata(6, new PropertyChangedCallback((s, e) =>
            {
                if (s is not ButtonBoxPanel panel)
                    return;

                panel.InvalidateVisual();
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
            DependencyProperty.Register("Columns", typeof(int), typeof(ButtonBoxPanel), new PropertyMetadata(6, new PropertyChangedCallback((s, e) =>
            {
                if (s is not ButtonBoxPanel panel)
                    return;

                panel.InvalidateVisual();
            })));

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
            DependencyProperty.Register("HighlightRow", typeof(int?), typeof(ButtonBoxPanel), new PropertyMetadata(null));

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
            DependencyProperty.Register("HighlightColumn", typeof(int?), typeof(ButtonBoxPanel), new PropertyMetadata(null));

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
            return new Size(this.Columns * this.UnitWidth, this.Rows * this.UnitHeight);
        }

        /// <summary>
        /// 布局
        /// </summary>
        /// <param name="finalSize">可用区域</param>
        /// <returns>布局结果</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (FrameworkElement element in Children)
            {
                if (element == null || element.DataContext is not ButtonBoxItemModelBase model)
                    continue;

                element.Arrange(new Rect(model.Column * this.UnitWidth, model.Row * this.UnitHeight, this.UnitWidth, this.UnitHeight));
            }

            return finalSize;
        }

        /// <summary>
        /// 渲染
        /// </summary>
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            for (int r = 0; r <= this.Rows; ++r)
            {
                dc.DrawSnappedLinesBetweenPoints(this.Pen, 1, new(0, r * this.UnitHeight), new(this.Columns * this.UnitWidth, r * this.UnitHeight));
            }
            for (int c = 0; c <= this.Columns; c++)
            {
                dc.DrawSnappedLinesBetweenPoints(this.Pen, 1, new(c * this.UnitWidth, 0), new(c * this.UnitWidth, this.Rows * this.UnitHeight));
            }

            if (this.HighlightRow != null && this.HighlightColumn != null)
            {
                dc.DrawRectangle(this.HighlightBrush, this.Pen, new Rect(this.HighlightColumn.Value * this.UnitWidth, this.HighlightRow.Value * this.UnitHeight, this.UnitWidth, this.UnitHeight));
            }
        }

        /// <summary>
        /// 拖拽进入
        /// </summary>
        protected override void OnDragEnter(DragEventArgs e)
        {
            if (e.Data.GetData(typeof(ResourceInfoItemModel)) is not ResourceInfoItemModel model)
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
            if (e.Data.GetData(typeof(ResourceInfoItemModel)) is not ResourceInfoItemModel model)
                return;

            this.SetHighlight(e);
        }

        /// <summary>
        /// 释放拖拽
        /// </summary>
        protected override void OnDrop(DragEventArgs e)
        {
            if (e.Data.GetData(typeof(ResourceInfoItemModel)) is not ResourceInfoItemModel model)
                return;

            this.ClearHighlight();

            Point point = e.GetPosition(this);
            int highlightRow = (int)(point.Y / this.UnitHeight);
            int highlightColumn = (int)(point.X / this.UnitWidth);

            if (DanceXamlExpansion.GetVisualTreeParent<ButtonBoxItemsControl>(this) is not ButtonBoxItemsControl owner)
                return;

            if ((highlightRow >= 0 && highlightRow < this.Rows) && (highlightColumn >= 0 && highlightColumn < this.Columns))
            {
                owner.DropCommand?.Execute(new ButtonBoxPanelDropEventArgs(highlightRow, highlightColumn, model));
            }
        }

        /// <summary>
        /// 设置高亮
        /// </summary>
        /// <param name="e"></param>
        private void SetHighlight(DragEventArgs e)
        {
            if (e.Data.GetData(typeof(ResourceInfoItemModel)) is not ResourceInfoItemModel model)
                return;

            Point point = e.GetPosition(this);
            int highlightRow = (int)(point.Y / this.UnitHeight);
            int highlightColumn = (int)(point.X / this.UnitWidth);

            this.HighlightRow = (highlightRow >= 0 && highlightRow < this.Rows) ? highlightRow : null;
            this.HighlightColumn = (highlightColumn >= 0 && highlightColumn < this.Columns) ? highlightColumn : null;

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
