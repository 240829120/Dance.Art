using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 文档插件模型
    /// </summary>
    public class DocumentPluginModel : ViewPluginModelBase
    {
        /// <summary>
        /// 文档插件模型
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        /// <param name="pluginInfo">插件信息</param>
        /// <param name="file">文件</param>
        public DocumentPluginModel(string id, string name, DocumentPluginInfo pluginInfo, string file) : base(id, name, pluginInfo)
        {
            this.file = file;
        }

        #region File -- 文件路径

        private string file;
        /// <summary>
        /// 文件路径
        /// </summary>
        public string File
        {
            get { return file; }
            set { file = value; this.OnPropertyChanged(); }
        }

        #endregion
    }
}
