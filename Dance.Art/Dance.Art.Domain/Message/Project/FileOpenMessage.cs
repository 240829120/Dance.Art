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
        /// <param name="fileModel">文件模型</param>
        public FileOpenMessage(FileModel fileModel)
        {
            this.FileModel = fileModel;
        }

        /// <summary>
        /// 文件模型
        /// </summary>
        public FileModel FileModel { get; private set; }
    }
}
