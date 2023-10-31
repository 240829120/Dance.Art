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

        /// <summary>
        /// 为文件操作过滤模型
        /// </summary>
        /// <param name="files">待过滤的文件模型</param>
        /// <returns>过滤后的文件模型</returns>
        List<FileModel> FilterFileModelForOperate(List<FileModel> files);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="files">文件集合</param>
        void Sort(IList<FileModel> files);

        /// <summary>
        /// 清理
        /// </summary>
        void Clear();
    }
}
