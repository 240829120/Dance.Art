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
        private readonly Dictionary<string, DataSetCellModel> Dic = new();

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

        /// <summary>
        /// 单元格集合
        /// </summary>
        public DanceWrapperCollection<DataSetCellModel> Cells { get; } = new();

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
