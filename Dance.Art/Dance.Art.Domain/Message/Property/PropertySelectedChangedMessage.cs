using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 属性选择改变消息
    /// </summary>
    /// <param name="source">源</param>
    /// <param name="data">数据</param>
    /// <param name="selectedObject">选中对象</param>
    public class PropertySelectedChangedMessage(object? source, object? data, object? selectedObject)
    {
        /// <summary>
        /// 源
        /// </summary>
        public object? Source { get; } = source;

        /// <summary>
        /// 数据
        /// </summary>
        public object? Data { get; } = data;

        /// <summary>
        /// 选中对象
        /// </summary>
        public object? SelectedObject { get; } = selectedObject;
    }
}
