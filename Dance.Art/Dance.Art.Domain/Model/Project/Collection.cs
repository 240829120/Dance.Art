using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 连接
    /// </summary>
    public class Collection : DanceModel
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

        #region Group -- 分组

        private CollectionGroup? group;
        /// <summary>
        /// 分组
        /// </summary>
        public CollectionGroup? Group
        {
            get { return group; }
            set { group = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region IP -- IP地址

        private string? ip;
        /// <summary>
        /// IP地址
        /// </summary>
        public string? IP
        {
            get { return ip; }
            set { ip = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region Port -- 端口

        private int port;
        /// <summary>
        /// 端口
        /// </summary>
        public int Port
        {
            get { return port; }
            set { port = value; this.OnPropertyChanged(); }
        }

        #endregion
    }
}
