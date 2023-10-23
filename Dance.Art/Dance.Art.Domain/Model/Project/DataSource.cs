using Dance.Art.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 数据源
    /// </summary>
    public class DataSource : DanceModel
    {
        // ===========================================================================
        // Project Property -- 项目属性

        #region Name -- 名称

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

        #region Description -- 描述

        private string? description;
        /// <summary>
        /// 描述
        /// </summary>
        public string? Description
        {
            get { return description; }
            set { description = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Type -- 数据源类型

        private DataSourceType type;
        /// <summary>
        /// 数据源类型
        /// </summary>
        public DataSourceType Type
        {
            get { return type; }
            set { type = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Uri -- 地址

        private string? uri;
        /// <summary>
        /// 地址
        /// </summary>
        public string? Uri
        {
            get { return uri; }
            set { uri = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Parameters -- 参数集合

        private Dictionary<string, string> parameters = new();
        /// <summary>
        /// 参数集合
        /// </summary>
        public Dictionary<string, string> Parameters
        {
            get { return parameters; }
            private set { parameters = value; this.OnPropertyChanged(); }
        }


        #endregion
    }
}
