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
        /// 日志
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(FileModelIconConverter));

        /// <summary>
        /// 缓存
        /// </summary>
        private readonly Dictionary<string, ImageSource> CACHE = new();

        /// <summary>
        /// 转化器
        /// </summary>
        private readonly SvgImageConverterExtension SvgImageConverterExtension = new();

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
            if (DanceDomain.Current is not ArtDomain domain || value is not FileModel fileModel)
                return null;

            if (fileModel.Category == FileModelCategory.Project)
                return this.GetImageSource(this.Project);

            if (fileModel.Category == FileModelCategory.Folder)
                return this.GetImageSource(this.Folder);

            foreach (DocumentPluginInfo pluginInfo in domain.DocumentPlugins)
            {
                DocumentFileInfo? fileInfo = pluginInfo.FileInfos.FirstOrDefault(p => string.Equals(p.Extension, fileModel.Extension, StringComparison.OrdinalIgnoreCase));
                if (fileInfo == null)
                    continue;

                return this.GetImageSource(fileInfo.Icon);
            }

            return this.GetImageSource(this.Unknow);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取图片源
        /// </summary>
        /// <param name="uri">地址</param>
        /// <returns>图片源</returns>
        private ImageSource? GetImageSource(string? uri)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(uri))
                    return null;

                lock (CACHE)
                {
                    if (CACHE.TryGetValue(uri, out ImageSource? source))
                        return source;

                    source = SvgImageConverterExtension.Convert(uri, null, null, null) as ImageSource;

                    if (source == null)
                        return null;

                    CACHE[uri] = source;

                    return source;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return null;
            }
        }
    }
}
