using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 模板插件信息
    /// </summary>
    public class TemplatePluginInfo : PluginInfoBase
    {
        /// <summary>
        /// 模板插件信息
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="name">名称</param>
        /// <param name="templateFolder">模板文件夹</param>
        public TemplatePluginInfo(string id, string name, string templateFolder) : base(id, name)
        {
            this.TemplateFolder = templateFolder;
        }

        /// <summary>
        /// 模板文件夹
        /// </summary>
        public string TemplateFolder { get; private set; }
    }
}
