using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 数据集模型
    /// </summary>
    public class DataSetModel : DanceWrapperModel
    {
        // ====================================================================================
        // Field

        /// <summary>
        /// 单元格缓存
        /// </summary>
        private readonly Dictionary<string, DataSetCellModel> Dic = [];

        // ====================================================================================
        // Property

        #region Name -- 名称

        private string? name;
        /// <summary>
        /// 名称
        /// </summary>
        public string? Name
        {
            get { return name; }
            set { name = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        #region MinRow -- 最小行值

        private int minRow;
        /// <summary>
        /// 最小行值
        /// </summary>
        public int MinRow
        {
            get { return minRow; }
        }

        #endregion

        #region MaxRow -- 最大行值

        private int maxRow;
        /// <summary>
        /// 最大行值
        /// </summary>
        public int MaxRow
        {
            get { return maxRow; }
        }

        #endregion

        #region MinColumn -- 最小列值

        private int minColumn;
        /// <summary>
        /// 最小列值
        /// </summary>
        public int MinColumn
        {
            get { return minColumn; }
        }

        #endregion

        #region MaxColumn -- 最大列值

        private int maxColumn;
        /// <summary>
        /// 最大列值
        /// </summary>
        public int MaxColumn
        {
            get { return maxColumn; }
        }

        #endregion

        /// <summary>
        /// 单元格集合
        /// </summary>
        public DanceWrapperCollection<DataSetCellModel> Cells { get; } = [];

        // ====================================================================================
        // Public Function

        /// <summary>
        /// 构建索引
        /// </summary>
        public void BuildIndex()
        {
            this.Dic.Clear();
            foreach (DataSetCellModel cell in this.Cells)
            {
                this.minRow = Math.Min(cell.Row, this.minRow);
                this.maxRow = Math.Max(cell.Row, this.maxRow);
                this.minColumn = Math.Min(cell.Column, this.minColumn);
                this.maxColumn = Math.Max(cell.Column, this.maxColumn);

                this.Dic.Add($"{cell.Row}_{cell.Column}", cell);
            }
        }

        /// <summary>
        /// 获取单元格值
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="column">列</param>
        /// <returns>单元格</returns>
        public DataSetCellModel? GetCell(int row, int column)
        {
            this.Dic.TryGetValue($"{row}_{column}", out DataSetCellModel? cell);
            return cell;
        }
    }
}
