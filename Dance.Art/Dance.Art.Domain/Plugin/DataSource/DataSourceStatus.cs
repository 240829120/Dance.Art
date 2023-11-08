using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 数据源状态
    /// </summary>
    public enum DataSourceStatus
    {
        /// <summary>
        /// 等待
        /// </summary>
        Waiting,

        /// <summary>
        /// 准备完成
        /// </summary>
        Ready,

        /// <summary>
        /// 错误
        /// </summary>
        Error
    }
}
