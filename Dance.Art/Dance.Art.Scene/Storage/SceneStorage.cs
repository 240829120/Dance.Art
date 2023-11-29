using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Scene
{
    /// <summary>
    /// 场景仓储
    /// </summary>
    public class SceneStorage
    {
        /// <summary>
        /// 场景模型
        /// </summary>
        public SceneModel? SceneModel { get; set; }

        /// <summary>
        /// 项集合
        /// </summary>
        public List<ISceneItemModel>? Items { get; set; }
    }
}
