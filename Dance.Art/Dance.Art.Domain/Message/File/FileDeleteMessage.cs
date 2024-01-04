using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 文件删除消息
    /// </summary>
    /// <param name="fullPath">文件完整路径</param>
    public class FileDeleteMessage(string fullPath)
    {
        /// <summary>
        /// 文件完整路径
        /// </summary>
        public string FullPath { get; private set; } = fullPath;
    }
}
