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
    public interface ISceneResourceManager
    {
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="key">键<see cref="SceneResourceDefines"/></param>
        /// <returns>数据模板</returns>
        DataTemplate? Get(string key);
    }
}
