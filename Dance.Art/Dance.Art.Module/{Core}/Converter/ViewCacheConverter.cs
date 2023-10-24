using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Dance.Art.Module
{
    /// <summary>
    /// 视图缓存转化器
    /// </summary>
    public class ViewCacheConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not PluginViewModel vm)
                return null;

            if (vm.View != null)
            {
                return vm.View;
            }

            if (vm.ViewType == null || string.IsNullOrWhiteSpace(vm.ViewType.FullName))
                return null;

            vm.View = vm.ViewType.Assembly.CreateInstance(vm.ViewType.FullName) as FrameworkElement;

            return vm.View;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
