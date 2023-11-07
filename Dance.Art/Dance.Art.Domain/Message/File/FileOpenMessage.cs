using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 文件打开消息
    /// </summary>
    public class FileOpenMessage
    {
        /// <summary>
        /// 文件打开消息
        /// </summary>
        /// <param name="path">文件路径</param>
        public FileOpenMessage(string path)
        {
            this.Path = path;
            this.FileName = System.IO.Path.GetFileName(path);
            this.Extension = System.IO.Path.GetExtension(path);
        }

        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string Extension { get; private set; }

        /// <summary>
        /// 附加数据
        /// </summary>
        public object? Data { get; set; }
    }
}
