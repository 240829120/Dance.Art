using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// <param name="viewType">视图类型</param>
        /// <param name="extensions">能处理文件的扩展名</param>
        public DocumentPluginModel(string id, string name, Type viewType, params string[] extensions) : base(id, name, viewType)
        {
            this.Extensions = extensions;
        }

        /// <summary>
        /// 能处理文件的扩展名
        /// </summary>
        public string[] Extensions { get; private set; }
    }
}
