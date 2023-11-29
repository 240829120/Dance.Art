using Dance.Art.Domain;
using Dance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Scene
{
    /// <summary>
    /// 场景文档插件生命周期
    /// </summary>
    public class SceneDocumentPluginLifescope : DanceObject, IDancePluginLifescope
    {
        /// <summary>
        /// 编号
        /// </summary>
        public const string ID = "[Dance.Art.Scene]:Scene";

        /// <summary>
        /// 名称
        /// </summary>
        public const string NAME = "场景";

        /// <summary>
        /// 描述
        /// </summary>
        public const string Description = @"三维场景";

        /// <summary>
        /// 注册插件
        /// </summary>
        /// <returns>插件信息</returns>
        public IDancePluginInfo Register()
        {
            return new ResourceDocumentPluginInfo(ID, NAME, typeof(SceneDocumentView), typeof(ISceneItemModel),
                                                  new DocumentFileInfo(DocumentFileGroupDefines.TEMPLATE, true, FileSuffixCategory.SCENE,
                                                                       "pack://application:,,,/Dance.Art.Scene;component/Themes/Resources/Icons/scene.svg",
                                                                       Description));
        }

        /// <summary>
        /// 初始化插件
        /// </summary>
        public void Initialize()
        {

        }
    }
}

