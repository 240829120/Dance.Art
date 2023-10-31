using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.Domain
{
    /// <summary>
    /// Docking 面板
    /// </summary>
    public interface IDockingPanel : IDisposable
    {
        /// <summary>
        /// 是否修改
        /// </summary>
        bool IsModify { get; }

        /// <summary>
        /// 是否可以重做
        /// </summary>
        bool CanRedo { get; }

        /// <summary>
        /// 是否可以撤销
        /// </summary>
        bool CanUndo { get; }

        /// <summary>
        /// 加载
        /// </summary>
        void Load();

        /// <summary>
        /// 保存命令
        /// </summary>
        void Save();

        /// <summary>
        /// 重做
        /// </summary>
        void Redo();

        /// <summary>
        /// 撤销
        /// </summary>
        void Undo();

        /// <summary>
        /// 视图插件模型
        /// </summary>
        ViewPluginModelBase? ViewPluginModel { get; set; }
    }
}
