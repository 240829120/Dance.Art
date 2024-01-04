using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ClearScript.V8;
using Dance.Art.Domain;

namespace Dance.Art
{
    /// <summary>
    /// 脚本领域
    /// </summary>
    /// <param name="indexFile">入口文件</param>
    public class ScriptDomain(string indexFile) : DanceDomain
    {
        /// <summary>
        /// 入口文件
        /// </summary>
        public string IndexFile { get; private set; } = indexFile;

        /// <summary>
        /// 脚本引擎
        /// </summary>
        public V8ScriptEngine? Engine { get; set; }

        /// <summary>
        /// 脚本宿主
        /// </summary>
        public ScriptHost Host { get; private set; } = new();

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            this.Engine?.DocumentSettings?.Loader?.DiscardCachedDocuments();
            this.Engine?.CancelInterrupt();
            this.Engine?.CancelAwaitDebugger();
            this.Engine?.Dispose();
            this.Host?.Dispose();
        }
    }
}
