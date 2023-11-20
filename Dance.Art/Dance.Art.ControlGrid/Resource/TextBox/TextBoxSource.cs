using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.ControlGrid
{
    /// <summary>
    /// 文本框资源
    /// </summary>
    public class TextBoxSource : IResourceSource
    {
        /// <summary>
        /// 资源ID
        /// </summary>
        public string ID { get; } = ControlGridResourceDefines.TextBox;

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; } = "pack://application:,,,/Dance.Art.ControlGrid;component/Themes/Resources/Icons/textbox.svg";

        /// <summary>
        /// 分组
        /// </summary>
        public string Group { get; } = ResourceGroupDefines.COMMON;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; } = "文本框";

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; } = "文本框";

        /// <summary>
        /// 资源类型
        /// </summary>
        public Type ResourceType { get; } = typeof(TextBoxModel);

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="projectDomain">项目领域</param>
        /// <returns>实例</returns>
        public ResourceItemModelBase CreateInstance(ProjectDomain projectDomain)
        {
            return new TextBoxModel();
        }
    }
}
