using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 文件夹模型
    /// </summary>
    public class FolderModel : FileModel
    {
        /// <summary>
        /// 文件夹模型
        /// </summary>
        /// <param name="path">路径</param>
        public FolderModel(string path) : base(path)
        {

        }

        /// <summary>
        /// 分类
        /// </summary>
        public override FileModelCategory Category { get; internal set; } = FileModelCategory.Folder;

        #region IsExpand -- 是否展开

        private bool isExpand;
        /// <summary>
        /// 是否展开
        /// </summary>
        public bool IsExpand
        {
            get { return isExpand; }
            set { isExpand = value; this.OnPropertyChanged(); }
        }

        #endregion
    }
}
