using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Scene
{
    /// <summary>
    /// 场景插件生命周期
    /// </summary>
    public class SceneScriptPluginLifescope : DanceObject, IDancePluginLifescope
    {
        /// <summary>
        /// 编号
        /// </summary>
        public const string ID = "[Dance.Art.Scene]:Script";

        /// <summary>
        /// 名称
        /// </summary>
        public const string NAME = "场景脚本";

        /// <summary>
        /// 命名空间
        /// </summary>
        public const string NAME_SPACE = ScriptNameSpace.DANCE_ART_SCRIPT;

        /// <summary>
        /// 注册插件
        /// </summary>
        /// <returns>插件信息</returns>
        public IDancePluginInfo Register()
        {
            return new ScriptPluginInfo(ID, NAME, new ScriptServiceInfo(NAME_SPACE, "SceneScriptService", typeof(SceneScriptService)));
        }

        /// <summary>
        /// 初始化插件
        /// </summary>
        public void Initialize()
        {

        }
    }
}
