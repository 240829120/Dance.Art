using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Scene
{
    /// <summary>
    /// 点光源
    /// </summary>
    public class PointLightSource : IResourceSource
    {
        /// <summary>
        /// 资源ID
        /// </summary>
        public string ID { get; } = SceneResourceDefines.PointLight;

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; } = "pack://application:,,,/Dance.Art.Scene;component/Themes/Resources/Icons/point_light.svg";

        /// <summary>
        /// 分组
        /// </summary>
        public string Group { get; } = SceneResourceGroupDefines.LIGHT;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; } = "点光源";

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; } = "点光源";

        /// <summary>
        /// 资源类型
        /// </summary>
        public Type ResourceType { get; } = typeof(PointLightModel);

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="projectDomain">项目领域</param>
        /// <returns>实例</returns>
        public ResourceItemModelBase CreateInstance(ProjectDomain projectDomain)
        {
            return new PointLightModel();
        }
    }
}
