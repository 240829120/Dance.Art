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
        /// <param name="templateName">模板名称</param>
        /// <param name="templateFolder">模板文件夹</param>
        /// <param name="icon">图标</param>
        public TemplatePluginInfo(string id, string name, string templateName, string templateFolder, string icon) : base(id, name)
        {
            this.TemplateName = templateName;
            this.TemplateFolder = templateFolder;
            this.Icon = icon;
        }

        /// <summary>
        /// 模板文件夹
        /// </summary>
        public string TemplateFolder { get; private set; }

        /// <summary>
        /// 模板名称
        /// </summary>
        public string TemplateName { get; private set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; private set; }
    }
}
