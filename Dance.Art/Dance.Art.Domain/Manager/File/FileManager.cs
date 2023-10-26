using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 文件管理器
    /// </summary>
    [DanceSingleton(typeof(IFileManager))]
    public class FileManager : IFileManager
    {
        /// <summary>
        /// 跳过文件
        /// </summary>
        private readonly List<string> PASS_FILES = new()
        {
            FileSuffixCategory.PROJECT,
            FileSuffixCategory.PROJECT_CACHE
        };

        /// <summary>
        /// 项目文件根路径
        /// </summary>
        public FileModel? Root { get; private set; }

        /// <summary>
        /// 文件系统监视器
        /// </summary>
        public FileSystemWatcher? FileSystemWatcher { get; private set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="domain">项目领域</param>
        public void Initialize(ProjectDomain domain)
        {
            this.FileSystemWatcher?.Dispose();
            this.FileSystemWatcher = null;

            // 构建文件树
            this.BuildFileTree(domain);
        }

        /// <summary>
        /// 清理
        /// </summary>
        public void Clear()
        {
            this.FileSystemWatcher?.Dispose();
            this.FileSystemWatcher = null;

            this.Root = null;
        }

        /// <summary>
        /// 构建文件树
        /// </summary>
        /// <param name="domain">项目领域</param>
        private void BuildFileTree(ProjectDomain domain)
        {
            if (!Directory.Exists(domain.ProjectFolderPath) || !File.Exists(domain.ProjectFilePath))
                return;

            this.Root = this.BuildFileTree_Folder(domain.ProjectFolderPath);
            if (this.Root == null)
                return;

            this.Root.Category = FileModelCategory.Project;
            this.Root.IsExpanded = true;
        }

        /// <summary>
        /// 构建文件树 -- 文件
        /// </summary>
        /// <param name="path">路径</param>
        private FileModel? BuildFileTree_File(string path)
        {
            if (PASS_FILES.Contains(Path.GetExtension(path)))
                return null;

            FileModel fileModel = new(path);

            return fileModel;
        }

        /// <summary>
        /// 构建文件树 -- 文件夹
        /// </summary>
        /// <param name="path">路径</param>
        private FileModel BuildFileTree_Folder(string path)
        {
            FileModel folderModel = new(path)
            {
                Category = FileModelCategory.Folder
            };

            foreach (string folder in Directory.GetDirectories(path))
            {
                folderModel.Items.Add(this.BuildFileTree_Folder(folder));
            }

            foreach (string file in Directory.GetFiles(path))
            {
                FileModel? fileModel = this.BuildFileTree_File(file);
                if (fileModel == null)
                    continue;

                folderModel.Items.Add(fileModel);
            }

            return folderModel;
        }
    }
}
