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
    /// <param name="id">编号</param>
    /// <param name="name">名称</param>
    /// <param name="pluginInfo">插件信息</param>
    /// <param name="file">文件</param>
    public class DocumentPluginModel(string id, string name, DocumentPluginInfo pluginInfo, string file) : ViewPluginModelBase(id, name, pluginInfo)
    {
        #region File -- 文件路径

        private string file = file;
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
