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
    /// 时间线面板文档插件生命周期
    /// </summary>
    public class TimelineDocumentPluginLifescope : DanceObject, IDancePluginLifescope
    {
        /// <summary>
        /// 编号
        /// </summary>
        public const string ID = "[Dance.Art.Timeline]:Timeline";

        /// <summary>
        /// 名称
        /// </summary>
        public const string NAME = "时间线面板";

        /// <summary>
        /// 描述
        /// </summary>
        public const string Description = @"根据时间线指定的时间触发相应事件的面板";

        /// <summary>
        /// 注册插件
        /// </summary>
        /// <returns>插件信息</returns>
        public IDancePluginInfo Register()
        {
            return new ResourceDocumentPluginInfo(ID, NAME, typeof(TimelineDocumentView), typeof(ITimelineElementModel),
                                                  new DocumentFileInfo(DocumentFileGroupDefines.TEMPLATE, true, FileSuffixCategory.TIME_LINE_PANEL,
                                                                       "pack://application:,,,/Dance.Art.Timeline;component/Themes/Resources/Icons/timeline.svg",
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

