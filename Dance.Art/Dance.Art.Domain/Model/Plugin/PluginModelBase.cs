using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 插件模型基类
    /// </summary>
    public abstract class PluginModelBase : DanceModel, IDancePluginInfo
    {
        /// <summary>
        /// 插件模型基类
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        public PluginModelBase(string id, string name)
        {
            this.ID = id;
            this.Name = name;
        }

        /// <summary>
        /// 编号
        /// </summary>
        public string ID { get; private set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }

        #region IsVisible -- 是否可见

        private bool isVisible = true;
        /// <summary>
        /// 是否可见
        /// </summary>
        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; this.OnPropertyChanged(); }
        }

        #endregion
    }
}
