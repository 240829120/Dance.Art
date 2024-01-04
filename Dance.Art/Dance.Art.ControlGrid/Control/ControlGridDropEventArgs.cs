using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.ControlGrid
{
    /// <summary>
    /// 拖拽结束事件参数
    /// </summary>
    /// <param name="row">行</param>
    /// <param name="column">列</param>
    /// <param name="resourceInfo">资源信息</param>
    /// <param name="model">模型</param>
    public class ControlGridDropEventArgs(int row, int column, ResourceInfoItemModel? resourceInfo, ControlGridItemModelBase model) : EventArgs
    {
        /// <summary>
        /// 行
        /// </summary>
        public int Row { get; private set; } = row;

        /// <summary>
        /// 列
        /// </summary>
        public int Column { get; private set; } = column;

        /// <summary>
        /// 资源信息
        /// </summary>
        public ResourceInfoItemModel? ResourceInfo { get; private set; } = resourceInfo;

        /// <summary>
        /// 模型
        /// </summary>
        public ControlGridItemModelBase Model { get; private set; } = model;
    }
}
