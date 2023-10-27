using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Script
{
    /// <summary>
    /// 消息脚本插件生命周期
    /// </summary>
    public class MessageScriptPluginLifescope : DanceObject, IDancePluginLifescope
    {
        /// <summary>
        /// 编号
        /// </summary>
        public const string ID = "[Dance.Art.Script]:Message";

        /// <summary>
        /// 名称
        /// </summary>
        public const string NAME = "消息脚本";

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
            return new ScriptPluginInfo(ID, NAME, new ScriptServiceInfo(NAME_SPACE, "MessageService", typeof(MessageScriptService)));
        }

        /// <summary>
        /// 初始化插件
        /// </summary>
        public void Initialize()
        {

        }
    }
}
