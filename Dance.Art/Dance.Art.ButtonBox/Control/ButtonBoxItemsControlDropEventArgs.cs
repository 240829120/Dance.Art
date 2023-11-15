using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.ButtonBox
{
    /// <summary>
    /// 拖拽结束事件参数
    /// </summary>
    public class ButtonBoxItemsControlDropEventArgs : EventArgs
    {
        /// <summary>
        /// 拖拽结束事件参数
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="column">列</param>
        /// <param name="resourceInfo">资源信息</param>
        /// <param name="model">模型</param>
        public ButtonBoxItemsControlDropEventArgs(int row, int column, ResourceInfoItemModel? resourceInfo, ButtonBoxItemModelBase model)
        {
            this.Row = row;
            this.Column = column;
            this.ResourceInfo = resourceInfo;
            this.Model = model;
        }

        /// <summary>
        /// 行
        /// </summary>
        public int Row { get; private set; }

        /// <summary>
        /// 列
        /// </summary>
        public int Column { get; private set; }

        /// <summary>
        /// 资源信息
        /// </summary>
        public ResourceInfoItemModel? ResourceInfo { get; private set; }

        /// <summary>
        /// 模型
        /// </summary>
        public ButtonBoxItemModelBase Model { get; private set; }
    }
}
