using Dance.Art.Domain;
using Dance.Wpf;
using log4net;
using SharpVectors.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Dance.Art.Module
{
    /// <summary>
    /// 文件图标转化器
    /// </summary>
    public class FileModelIconConverter : IValueConverter
    {
        /// <summary>
        /// 项目
        /// </summary>
        public string? Project { get; set; }

        /// <summary>
        /// 文件夹
        /// </summary>
        public string? Folder { get; set; }

        /// <summary>
        /// 未知
        /// </summary>
        public string? Unknow { get; set; }

        /// <summary>
        /// 转化
        /// </summary>
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not FileModel fileModel)
                return null;

            if (fileModel.Category == FileModelCategory.Project)
                return IconCacheConverter.GetImageSource(this.Project);

            if (fileModel.Category == FileModelCategory.Folder)
                return IconCacheConverter.GetImageSource(this.Folder);

            foreach (DocumentPluginInfo pluginInfo in ArtDomain.Current.GetPluginCollection<DocumentPluginInfo>())
            {
                DocumentFileInfo? fileInfo = pluginInfo.FileInfos.FirstOrDefault(p => string.Equals(p.Extension, fileModel.Extension, StringComparison.OrdinalIgnoreCase));
                if (fileInfo == null)
                    continue;

                return IconCacheConverter.GetImageSource(fileInfo.Icon);
            }

            return IconCacheConverter.GetImageSource(this.Unknow);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
