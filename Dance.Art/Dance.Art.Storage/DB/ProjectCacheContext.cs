using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Dance.Art.Storage
{
    /// <summary>
    /// 项目缓存上下文
    /// </summary>
    public class ProjectCacheContext : DanceObject
    {
        /// <summary>
        /// 项目缓存上下文
        /// </summary>
        /// <param name="path">项目上下文路径</param>
        public ProjectCacheContext(string path)
        {
            this.Path = path;
            this.Database = new LiteDatabase(path);

            this.OpendDocuments = this.Database.GetCollection<OpendDocumentEntity>();
            this.CommandCaches = this.Database.GetCollection<CommandCacheEntity>();
        }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// 数据库
        /// </summary>
        public LiteDatabase Database { get; private set; }

        /// <summary>
        /// 打开的文档
        /// </summary>
        public ILiteCollection<OpendDocumentEntity> OpendDocuments { get; private set; }

        /// <summary>
        /// 命令缓存
        /// </summary>
        public ILiteCollection<CommandCacheEntity> CommandCaches { get; private set; }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            this.Database?.Dispose();
        }
    }
}
