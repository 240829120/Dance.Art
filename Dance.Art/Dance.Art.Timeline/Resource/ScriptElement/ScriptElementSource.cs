using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Timeline
{
    /// <summary>
    /// 脚本元素
    /// </summary>
    public class ScriptElementSource : IResourceSource
    {
        /// <summary>
        /// 资源ID
        /// </summary>
        public string ID { get; } = TimelineResourceDefines.ScriptElement;

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; } = "pack://application:,,,/Dance.Art.Timeline;component/Themes/Resources/Icons/script_element.svg";

        /// <summary>
        /// 分组
        /// </summary>
        public string Group { get; } = ResourceGroupDefines.COMMON;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; } = "脚本元素";

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; } = "脚本元素";

        /// <summary>
        /// 资源类型
        /// </summary>
        public Type ResourceType { get; } = typeof(ScriptElementModel);

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="projectDomain">项目领域</param>
        /// <returns>实例</returns>
        public ResourceItemModelBase CreateInstance(ProjectDomain projectDomain)
        {
            return new ScriptElementModel();
        }
    }
}
