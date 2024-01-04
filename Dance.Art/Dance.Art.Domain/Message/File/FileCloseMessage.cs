using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 文件关闭消息
    /// </summary>
    /// <param name="path">路径</param>
    public class FileCloseMessage(string path)
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        public string Path { get; private set; } = path;
    }
}
