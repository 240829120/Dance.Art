using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Plugin
{
    /// <summary>
    /// json文件插件生命周期
    /// </summary>
    public class JsonPluginLifescope : DanceObject, IDancePluginLifescope
    {
        /// <summary>
        /// 编号
        /// </summary>
        public const string ID = "[Dance.Art.Plugin]:Json";

        /// <summary>
        /// 名称
        /// </summary>
        public const string NAME = "json文件";

        /// <summary>
        /// 支持的文件后缀
        /// </summary>
        public readonly static string[] Extensions = new string[] { ".json" };

        /// <summary>
        /// 注册插件
        /// </summary>
        /// <returns>插件信息</returns>
        public IDancePluginInfo Register()
        {
            return new DocumentPluginModel(ID, NAME, typeof(JsonView), Extensions);
        }

        /// <summary>
        /// 初始化插件
        /// </summary>
        public void Initialize()
        {

        }
    }
}
