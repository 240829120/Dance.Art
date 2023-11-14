using Dance.Art.Domain;
using Dance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.ButtonBox
{
    /// <summary>
    /// 按钮组文档插件生命周期
    /// </summary>
    public class ButtonBoxDocumentPluginLifescope : DanceObject, IDancePluginLifescope
    {
        /// <summary>
        /// 编号
        /// </summary>
        public const string ID = "[Dance.Art.Document]:ButtonBox";

        /// <summary>
        /// 名称
        /// </summary>
        public const string NAME = "按钮面板";

        /// <summary>
        /// 注册插件
        /// </summary>
        /// <returns>插件信息</returns>
        public IDancePluginInfo Register()
        {
            List<string> resouceIDs = new()
            {
                ResourceDefines.CommandButton
            };

            return new ResourceDocumentPluginInfo(ID, NAME, typeof(ButtonBoxDocumentView), resouceIDs,
                                                  new DocumentFileInfo(DocumentFileGroupDefines.TEMPLATE, true, ".art_bp",
                                                                       "pack://application:,,,/Dance.Art.ButtonBox;component/Themes/Resources/Icons/button_box.svg",
                                                                       "按钮面板"));
        }

        /// <summary>
        /// 初始化插件
        /// </summary>
        public void Initialize()
        {

        }
    }
}

