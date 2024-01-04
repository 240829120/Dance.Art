using Dance.Art.Domain;
using Dance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Timeline
{
    /// <summary>
    /// 按钮组资源插件生命周期
    /// </summary>
    public class TimelineResourcePluginLifescope : DanceObject, IDancePluginLifescope
    {
        /// <summary>
        /// 编号
        /// </summary>
        public const string ID = "[Dance.Art.Timeline]:Resource";

        /// <summary>
        /// 名称
        /// </summary>
        public const string NAME = "按钮面板资源";

        /// <summary>
        /// 注册插件
        /// </summary>
        /// <returns>插件信息</returns>
        public IDancePluginInfo Register()
        {
            List<IResourceSource> resources =
            [
                new ScriptElementSource(),
                new CommandElementSource(),
            ];

            return new ResourcePluginInfo(ID, NAME, [.. resources]);
        }

        /// <summary>
        /// 初始化插件
        /// </summary>
        public void Initialize()
        {

        }
    }
}

