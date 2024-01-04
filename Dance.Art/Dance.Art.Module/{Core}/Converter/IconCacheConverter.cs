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
    /// 图标缓存转化器
    /// </summary>
    public class IconCacheConverter : IValueConverter
    {
        /// <summary>
        /// 日志
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(typeof(FileModelIconConverter));

        /// <summary>
        /// 缓存
        /// </summary>
        private static readonly Dictionary<string, ImageSource> CACHE = [];

        /// <summary>
        /// 转化器
        /// </summary>
        private static readonly SvgImageConverterExtension SvgImageConverterExtension = new();

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
            if (value is not string uri || string.IsNullOrWhiteSpace(uri))
                return null;

            return GetImageSource(uri);
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
        public static ImageSource? GetImageSource(string? uri)
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
