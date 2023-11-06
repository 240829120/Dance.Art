using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Document
{
    /// <summary>
    /// art项目文件插件生命周期
    /// </summary>
    public class ArtPluginLifescope : DanceObject, IDancePluginLifescope
    {
        /// <summary>
        /// 编号
        /// </summary>
        public const string ID = "[Dance.Art.Document]:Art";

        /// <summary>
        /// 名称
        /// </summary>
        public const string NAME = "项目文件";

        /// <summary>
        /// 注册插件
        /// </summary>
        /// <returns>插件信息</returns>
        public IDancePluginInfo Register()
        {
            return new DocumentPluginInfo(ID, NAME, typeof(ArtView), new DocumentFileInfo(DocumentFileGroupInfoDefines.PRIVATE_FILE, ".art", "pack://application:,,,/Dance.Art.Module;component/Themes/Resources/Icons/project.svg",
                                                                                          "项目文件"));
        }

        /// <summary>
        /// 初始化插件
        /// </summary>
        public void Initialize()
        {

        }
    }
}
