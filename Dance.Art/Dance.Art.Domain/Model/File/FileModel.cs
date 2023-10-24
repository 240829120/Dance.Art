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
    public class FileModel : DanceModel
    {
        /// <summary>
        /// 文件模型
        /// </summary>
        /// <param name="path">路径</param>
        public FileModel(string path)
        {
            this.path = path;
            this.fileName = System.IO.Path.GetFileName(path);
            this.extension = System.IO.Path.GetExtension(path);
        }

        /// <summary>
        /// 分类
        /// </summary>
        public virtual FileModelCategory Category { get; internal set; } = FileModelCategory.File;

        #region FileName -- 文件名称

        private string fileName;
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

        private string path;
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

        private string extension;
        /// <summary>
        /// 扩展名
        /// </summary>
        public string Extension
        {
            get { return extension; }
            set { extension = value; this.OnPropertyChanged(); }
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

        #region Items -- 子项集合

        private ObservableCollection<FileModel> items = new();
        /// <summary>
        /// 子项集合
        /// </summary>
        public ObservableCollection<FileModel> Items
        {
            get { return items; }
            private set { items = value; this.OnPropertyChanged(); }
        }

        #endregion
    }
}
