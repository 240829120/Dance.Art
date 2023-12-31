﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 文件模型
    /// </summary>
    /// <param name="parent">父基元素</param>
    /// <param name="path">路径</param>
    public class FileModel(FileModel? parent, string path) : DanceModel
    {
        // =============================================================================================
        // Property

        /// <summary>
        /// 分类
        /// </summary>
        public virtual FileModelCategory Category { get; internal set; } = FileModelCategory.File;

        #region Parent -- 父级元素

        private FileModel? parent = parent;
        /// <summary>
        /// 父级元素
        /// </summary>
        public FileModel? Parent
        {
            get { return parent; }
            set { parent = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region FileName -- 文件名称

        private string fileName = System.IO.Path.GetFileName(path);
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Path -- 路径

        private string path = path;
        /// <summary>
        /// 路径
        /// </summary>
        public string Path
        {
            get { return path; }
            set { path = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Extension -- 扩展名

        private string extension = System.IO.Path.GetExtension(path);
        /// <summary>
        /// 扩展名
        /// </summary>
        public string Extension
        {
            get { return extension; }
            set { extension = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Items -- 子项集合

        private ObservableCollection<FileModel> items = [];
        /// <summary>
        /// 子项集合
        /// </summary>
        public ObservableCollection<FileModel> Items
        {
            get { return items; }
            private set { items = value; this.OnPropertyChanged(); }
        }

        #endregion

        // =============================================================================================
        // Expand Property

        #region IsExpanded -- 是否展开

        private bool isExpanded;
        /// <summary>
        /// 是否展开
        /// </summary>
        public bool IsExpanded
        {
            get { return isExpanded; }
            set { isExpanded = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region IsSelected -- 当前是否选中

        private bool isSelected;
        /// <summary>
        /// 当前是否选中
        /// </summary>
        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region IsWaitForCut -- 是否等待剪切

        private bool isWaitForCut;
        /// <summary>
        /// 是否等待剪切
        /// </summary>
        public bool IsWaitForCut
        {
            get { return isWaitForCut; }
            set { isWaitForCut = value; this.OnPropertyChanged(); }
        }

        #endregion
    }
}
