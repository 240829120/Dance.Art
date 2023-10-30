using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 文件该名消息
    /// </summary>
    public class FileRenameMessage
    {
        /// <summary>
        /// 文件关闭消息
        /// </summary>
        /// <param name="oldPath">原始路径</param>
        /// <param name="path">路径</param>
        public FileRenameMessage(string oldPath, string path)
        {
            this.OldPath = oldPath;
            this.Path = path;
        }

        /// <summary>
        /// 原始路径
        /// </summary>
        public string OldPath { get; private set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string Path { get; private set; }
    }
}
