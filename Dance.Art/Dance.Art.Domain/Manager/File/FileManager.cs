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
using System.Windows;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 文件管理器
    /// </summary>
    [DanceSingleton(typeof(IFileManager))]
    public class FileManager : DanceObject, IFileManager
    {
        // ===============================================================================================
        // Field

        /// <summary>
        /// 跳过文件
        /// </summary>
        private readonly List<string> PASS_FILES =
        [
            FileSuffixCategory.PROJECT,
            FileSuffixCategory.PROJECT_CACHE
        ];

        // ===============================================================================================
        // Field

        /// <summary>
        /// 延时管理器
        /// </summary>
        private readonly IDanceDelayManager DelayManager = DanceDomain.Current.LifeScope.Resolve<IDanceDelayManager>();

        /// <summary>
        /// 文件系统监视器
        /// </summary>
        private FileSystemWatcher? FileSystemWatcher;

        // ===============================================================================================
        // Property

        /// <summary>
        /// 项目文件根路径
        /// </summary>
        public FileModel? Root { get; private set; }

        /// <summary>
        /// 分隔符
        /// </summary>
        private static readonly char[] SEPARATOR = ['\\', '/'];

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
                    NotifyFilter = NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastWrite
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
        public static FileModel? FindFileModel(FileModel ancestor, string path)
        {
            if (string.Equals(ancestor.Path, path, StringComparison.OrdinalIgnoreCase))
                return ancestor;

            string relativePath = Path.GetRelativePath(ancestor.Path, path);
            string? name = relativePath.Split(SEPARATOR, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
            if (string.IsNullOrWhiteSpace(name))
                return null;

            FileModel? next = ancestor.Items.FirstOrDefault(p => string.Equals(p.FileName, name, StringComparison.OrdinalIgnoreCase));
            if (next == null)
                return null;

            return FindFileModel(next, path);
        }

        /// <summary>
        /// 为文件操作过滤模型
        /// </summary>
        /// <param name="files">待过滤的文件模型</param>
        /// <returns>过滤后的文件模型</returns>
        public List<FileModel> FilterFileModelForOperate(List<FileModel> files)
        {
            List<FileModel> result = [.. files];

            foreach (var file in files)
            {
                if (file.Category == FileModelCategory.File)
                    continue;

                RemoveChildrenItemsForOperate(result, file);
            }

            return result;
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="files">文件集合</param>
        public void Sort(IList<FileModel> files)
        {
            for (int i = 0; i < files.Count - 1; i++)
            {
                for (int j = 0; j < files.Count - i - 1; j++)
                {
                    if (IsFileNameLargeThan(files[j], files[j + 1]))
                    {
                        (files[j + 1], files[j]) = (files[j], files[j + 1]);
                    }
                }
            }
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="action">保存行为</param>
        public void SaveFile(string path, Action action)
        {
            try
            {
                if (this.FileSystemWatcher != null)
                {
                    this.FileSystemWatcher.EnableRaisingEvents = false;
                }

                action();
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            finally
            {
                if (this.FileSystemWatcher != null)
                {
                    this.FileSystemWatcher.EnableRaisingEvents = true;
                }
            }
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
            Application.Current?.Dispatcher.BeginInvoke(() =>
            {
                this.CreateFileModel(e.FullPath);

                DanceDomain.Current.Messenger.Send(new FileCreateMessage(e.FullPath));
            });
        }

        /// <summary>
        /// 文件删除
        /// </summary>
        private void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            Application.Current?.Dispatcher.BeginInvoke(() =>
            {
                this.RemoveFileModel(e.FullPath);

                DanceDomain.Current.Messenger.Send(new FileDeleteMessage(e.FullPath));
            });
        }

        /// <summary>
        /// 文件改名
        /// </summary>
        private void FileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            Application.Current?.Dispatcher.BeginInvoke(() =>
            {
                this.RemoveFileModel(e.OldFullPath);
                this.CreateFileModel(e.FullPath);

                DanceDomain.Current.Messenger.Send(new FileRenameMessage(e.OldFullPath, e.FullPath));
            });
        }

        /// <summary>
        /// 其他改变
        /// </summary>
        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
                return;

            this.DelayManager.Wait($"FileManager__{e.FullPath}", 0.5, () =>
            {
                Application.Current?.Dispatcher.BeginInvoke(() =>
                {
                    DanceDomain.Current.Messenger.Send(new FileChangeMessage(e.FullPath));
                });
            });
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

            FileModel? parent = FindFileModel(this.Root, parentPath);
            if (parent == null)
                return;

            FileModel? model;

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

            parent.Items.Add(model);
            this.Sort(parent.Items);
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

            FileModel? parent = FindFileModel(this.Root, parentPath);
            if (parent == null)
                return;

            FileModel? model = parent.Items.FirstOrDefault(p => string.Equals(p.Path, path, StringComparison.OrdinalIgnoreCase));
            if (model == null)
                return;

            parent.Items.Remove(model);
        }

        /// <summary>
        /// 移除子项节点
        /// </summary>
        /// <param name="files">文件集合</param>
        /// <param name="folder">文件夹</param>
        private static void RemoveChildrenItemsForOperate(List<FileModel> files, FileModel folder)
        {
            foreach (FileModel file in folder.Items)
            {
                if (file.Category != FileModelCategory.File)
                {
                    RemoveChildrenItemsForOperate(files, file);
                }

                files.Remove(file);
            }
        }

        /// <summary>
        /// 比较文件名大小
        /// </summary>
        /// <param name="file1">文件1</param>
        /// <param name="file2">文件2</param>
        /// <returns>是否大于</returns>
        private static bool IsFileNameLargeThan(FileModel file1, FileModel file2)
        {
            if (file1.Category != FileModelCategory.File && file2.Category == FileModelCategory.File)
                return false;

            if (file1.Category == FileModelCategory.File && file2.Category != FileModelCategory.File)
                return true;

            return string.Compare(file1.FileName, file2.FileName, StringComparison.OrdinalIgnoreCase) == 1;
        }
    }
}
