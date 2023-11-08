using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dance.Art.DataSource
{
    /// <summary>
    /// 数据源文档面板模板
    /// </summary>
    public class DataSourceDocumentPanelTemplate : ContentControl
    {
        static DataSourceDocumentPanelTemplate()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DataSourceDocumentPanelTemplate), new FrameworkPropertyMetadata(typeof(DataSourceDocumentPanelTemplate)));
        }
    }
}
