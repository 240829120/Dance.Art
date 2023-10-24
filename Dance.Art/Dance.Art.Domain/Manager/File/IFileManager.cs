using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 文件管理器
    /// </summary>
    public interface IFileManager
    {
        /// <summary>
        /// 项目文件根路径
        /// </summary>
        public FileModel? Root { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="domain">项目领域</param>
        void Initialize(ProjectDomain domain);
    }
}
