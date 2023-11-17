using Dance.Art.Domain;
using Dance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.ControlGrid
{
    /// <summary>
    /// 按钮组文档插件生命周期
    /// </summary>
    public class ControlGridDocumentPluginLifescope : DanceObject, IDancePluginLifescope
    {
        /// <summary>
        /// 编号
        /// </summary>
        public const string ID = "[Dance.Art.ControlGrid]:ControlGrid";

        /// <summary>
        /// 名称
        /// </summary>
        public const string NAME = "控制面板";

        /// <summary>
        /// 描述
        /// </summary>
        public const string Description = @"由一组控件按照表格分布的控制面板";

        /// <summary>
        /// 注册插件
        /// </summary>
        /// <returns>插件信息</returns>
        public IDancePluginInfo Register()
        {
            List<string> resouceIDs = new()
            {
                ControlGridResourceDefines.CommandButton,
                ControlGridResourceDefines.ScriptButton
            };

            return new ResourceDocumentPluginInfo(ID, NAME, typeof(ControlGridDocumentView), resouceIDs,
                                                  new DocumentFileInfo(DocumentFileGroupDefines.TEMPLATE, true, FileSuffixCategory.BUTTON_PANEL,
                                                                       "pack://application:,,,/Dance.Art.ControlGrid;component/Themes/Resources/Icons/button_box.svg",
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

