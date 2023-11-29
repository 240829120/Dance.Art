using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.Scene
{
    /// <summary>
    /// 场景资源管理器
    /// </summary>
    [DanceSingleton(typeof(ISceneResourceManager))]
    public class SceneResourceManager : ISceneResourceManager
    {
        public SceneResourceManager()
        {
            this.ResourceDictionary = new ResourceDictionary() { Source = new Uri("pack://application:,,,/Dance.Art.Scene;component/Resource/SceneResourceDictionary.xaml", UriKind.RelativeOrAbsolute) };
        }

        /// <summary>
        /// 资源字典
        /// </summary>
        private readonly ResourceDictionary ResourceDictionary;

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="key">键<see cref="SceneResourceDefines"/></param>
        /// <returns>数据模板</returns>
        public DataTemplate? Get(string key)
        {
            return this.ResourceDictionary[key] as DataTemplate;
        }
    }
}
