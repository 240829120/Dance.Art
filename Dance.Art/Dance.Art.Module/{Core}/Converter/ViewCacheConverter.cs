using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
            ViewPluginViewModelBase? vm = null;

            if (value is ContentPresenter control)
            {
                vm = control.Content as ViewPluginViewModelBase;
            }
            else if (value is ViewPluginViewModelBase)
            {
                vm = value as ViewPluginViewModelBase;
            }

            if (vm == null || vm.PluginModel is not ViewPluginModelBase plugin)
                return null;

            if (vm.View != null)
            {
                return vm.View;
            }

            if (plugin.ViewType == null || string.IsNullOrWhiteSpace(plugin.ViewType.FullName))
                return null;

            vm.View = plugin.ViewType.Assembly.CreateInstance(plugin.ViewType.FullName) as FrameworkElement;
            if (vm.View is FrameworkElement view && view.DataContext is IDockingDocument dockingDocument)
            {
                dockingDocument.DocumentModel = vm as DocumentViewModel;
            }

            return vm.View;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
