using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
using Dance.Wpf;
using CommunityToolkit.Mvvm.Messaging;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using System.Windows;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 文件管理器
    /// </summary>
    [DanceSingleton(typeof(IFileManager))]
    public class FileManager : IFileManager
    {
        // ===============================================================================================
        // Field

        /// <summary>
        /// 跳过文件
        /// </summary>
        private readonly List<string> PASS_FILES = new()
        {
            FileSuffixCategory.PROJECT,
            FileSuffixCategory.PROJECT_CACHE
        };

        // ===============================================================================================
        // Property

        /// <summary>
        /// 项目文件根路径
        /// </summary>
        public FileModel? Root { get; private set; }

        /// <summary>
        /// 文件系统监视器
        /// </summary>
        public FileSystemWatcher? FileSystemWatcher { get; private set; }

        // ===============================================================================================
        // Public Function

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
            // 监视文件
            if (Directory.Exists(domain.ProjectFolderPath) && File.Exists(domain.ProjectFilePath))
            {
                this.FileSystemWatcher = new(domain.ProjectFolderPath)
                {
                    IncludeSubdirectories = true,
                    EnableRaisingEvents = true,
                    NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size
                };
                this.FileSystemWatcher.Created += FileSystemWatcher_Created;
                this.FileSystemWatcher.Deleted += FileSystemWatcher_Deleted;
                this.FileSystemWatcher.Renamed += FileSystemWatcher_Renamed;
                this.FileSystemWatcher.Changed += FileSystemWatcher_Changed;
            }
        }

        /// <summary>
        /// 清理
        /// </summary>
        public void Clear()
        {
            if (this.FileSystemWatcher != null)
            {
                this.FileSystemWatcher.Created -= FileSystemWatcher_Created;
                this.FileSystemWatcher.Deleted -= FileSystemWatcher_Deleted;
                this.FileSystemWatcher.Renamed -= FileSystemWatcher_Renamed;
                this.FileSystemWatcher.Changed -= FileSystemWatcher_Changed;

                this.FileSystemWatcher.Dispose();
                this.FileSystemWatcher = null;
            }

            this.Root = null;
        }

        /// <summary>
        /// 根据路径查找文件模型
        /// </summary>
        /// <param name="ancestor">祖先节点</param>
        /// <param name="path">路径</param>
        /// <returns>文件模型</returns>
        public FileModel? FindFileModel(FileModel ancestor, string path)
        {
            if (string.Equals(ancestor.Path, path, StringComparison.OrdinalIgnoreCase))
                return ancestor;

            string relativePath = Path.GetRelativePath(ancestor.Path, path);
            string? name = relativePath.Split(new char[] { '\\', '/' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
            if (string.IsNullOrWhiteSpace(name))
                return null;

            FileModel? next = ancestor.Items.FirstOrDefault(p => string.Equals(p.FileName, name, StringComparison.OrdinalIgnoreCase));
            if (next == null)
                return null;

            return FindFileModel(next, path);
        }

        // ===============================================================================================
        // Private Function

        // ---------------------------------------------------------------------------
        // Build File Tree

        /// <summary>
        /// 构建文件树
        /// </summary>
        /// <param name="domain">项目领域</param>
        private void BuildFileTree(ProjectDomain domain)
        {
            if (!Directory.Exists(domain.ProjectFolderPath) || !File.Exists(domain.ProjectFilePath))
                return;

            this.Root = this.BuildFileTree_Folder(null, domain.ProjectFolderPath);
            if (this.Root == null)
                return;

            this.Root.Category = FileModelCategory.Project;
            this.Root.IsExpanded = true;
        }

        /// <summary>
        /// 构建文件树 -- 文件
        /// </summary>
        /// <param name="parent">父节点</param>
        /// <param name="path">路径</param>
        private FileModel? BuildFileTree_File(FileModel? parent, string path)
        {
            if (PASS_FILES.Contains(Path.GetExtension(path)))
                return null;

            FileModel fileModel = new(parent, path);

            return fileModel;
        }

        /// <summary>
        /// 构建文件树 -- 文件夹
        /// </summary>
        /// <param name="parent">父节点</param>
        /// <param name="path">路径</param>
        private FileModel BuildFileTree_Folder(FileModel? parent, string path)
        {
            FileModel folderModel = new(parent, path)
            {
                Category = FileModelCategory.Folder
            };

            foreach (string folder in Directory.GetDirectories(path))
            {
                folderModel.Items.Add(this.BuildFileTree_Folder(folderModel, folder));
            }

            foreach (string file in Directory.GetFiles(path))
            {
                FileModel? fileModel = this.BuildFileTree_File(folderModel, file);
                if (fileModel == null)
                    continue;

                fileModel.Parent = folderModel;
                folderModel.Items.Add(fileModel);
            }

            return folderModel;
        }

        // ---------------------------------------------------------------------------
        // File Changed

        /// <summary>
        /// 文件创建
        /// </summary>
        private void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            this.CreateFileModel(e.FullPath);
            DanceDomain.Current.Messenger.Send(new FileCreateMessage(e.FullPath));
        }

        /// <summary>
        /// 文件删除
        /// </summary>
        private void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            this.RemoveFileModel(e.FullPath);
            DanceDomain.Current.Messenger.Send(new FileDeleteMessage(e.FullPath));
        }

        /// <summary>
        /// 文件改名
        /// </summary>
        private void FileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            this.RemoveFileModel(e.OldFullPath);
            this.CreateFileModel(e.FullPath);

            DanceDomain.Current.Messenger.Send(new FileRenameMessage(e.OldFullPath, e.FullPath));
        }

        /// <summary>
        /// 其他改变
        /// </summary>
        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
                return;

            DanceDomain.Current.Messenger.Send(new FileChangeMessage(e.FullPath));
        }

        /// <summary>
        /// 创建文件模型
        /// </summary>
        /// <param name="path">路径</param>
        private void CreateFileModel(string path)
        {
            if (this.Root == null)
                return;

            string? parentPath = Path.GetDirectoryName(path);
            if (string.IsNullOrWhiteSpace(parentPath))
                return;

            FileModel? parent = this.FindFileModel(this.Root, parentPath);
            if (parent == null)
                return;

            FileModel? model = null;

            if (Directory.Exists(path))
            {
                model = this.BuildFileTree_Folder(parent, path);
            }
            else
            {
                model = this.BuildFileTree_File(parent, path);
            }

            if (model == null)
                return;

            Application.Current.Dispatcher.Invoke(() =>
            {
                parent.Items.Add(model);
            });
        }

        /// <summary>
        /// 移除文件模型
        /// </summary>
        /// <param name="path">路径</param>
        private void RemoveFileModel(string path)
        {
            if (this.Root == null)
                return;

            string? parentPath = Path.GetDirectoryName(path);
            if (string.IsNullOrWhiteSpace(parentPath))
                return;

            FileModel? parent = this.FindFileModel(this.Root, parentPath);
            if (parent == null)
                return;

            FileModel? model = parent.Items.FirstOrDefault(p => string.Equals(p.Path, path, StringComparison.OrdinalIgnoreCase));
            if (model == null)
                return;

            Application.Current.Dispatcher.Invoke(() =>
            {
                parent.Items.Remove(model);
            });
        }
    }
}
