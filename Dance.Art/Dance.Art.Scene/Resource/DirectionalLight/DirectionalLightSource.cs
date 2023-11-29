using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Scene
{
    /// <summary>
    /// 平行光
    /// </summary>
    public class DirectionalLightSource : IResourceSource
    {
        /// <summary>
        /// 资源ID
        /// </summary>
        public string ID { get; } = SceneResourceDefines.DirectionalLight;

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; } = "pack://application:,,,/Dance.Art.Scene;component/Themes/Resources/Icons/directional_light.svg";

        /// <summary>
        /// 分组
        /// </summary>
        public string Group { get; } = SceneResourceGroupDefines.LIGHT;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; } = "平行光";

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; } = "平行光";

        /// <summary>
        /// 资源类型
        /// </summary>
        public Type ResourceType { get; } = typeof(DirectionalLightModel);

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="projectDomain">项目领域</param>
        /// <returns>实例</returns>
        public ResourceItemModelBase CreateInstance(ProjectDomain projectDomain)
        {
            return new DirectionalLightModel();
        }
    }
}
