﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 资源信息项模型
    /// </summary>
    public class ResourceInfoItemModel : DanceModel
    {
        #region Icon -- 图标

        private string? icon;
        /// <summary>
        /// 图标
        /// </summary>
        public string? Icon
        {
            get { return icon; }
            set { icon = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Name -- 分组

        private string? name;
        /// <summary>
        /// 名称
        /// </summary>
        public string? Name
        {
            get { return name; }
            set { name = value; this.OnPropertyChanged(); }
        }

        #endregion

        /// <summary>
        /// 源
        /// </summary>
        public IResourceSource? Source { get; set; }
    }
}
