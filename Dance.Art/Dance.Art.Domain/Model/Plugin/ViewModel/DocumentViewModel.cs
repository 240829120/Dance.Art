using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 文档视图模型
    /// </summary>
    public class DocumentViewModel : ViewPluginViewModelBase
    {
        /// <summary>
        /// 面板视图模型
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        /// <param name="pluginModel">插件模型</param>
        /// <param name="file">文件</param>
        public DocumentViewModel(string id, string name, DocumentPluginModel pluginModel, string file) : base(id, name, pluginModel)
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
