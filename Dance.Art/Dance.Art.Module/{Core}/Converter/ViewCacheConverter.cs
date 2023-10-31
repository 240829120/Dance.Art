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
            ViewPluginModelBase? pluginModel = value is ContentPresenter control ? control.Content as ViewPluginModelBase : value as ViewPluginModelBase;

            if (pluginModel == null || pluginModel.PluginInfo is not ViewPluginInfoBase pluginInfo)
                return null;

            if (pluginModel.View != null)
            {
                return pluginModel.View;
            }

            if (pluginInfo.ViewType == null || string.IsNullOrWhiteSpace(pluginInfo.ViewType.FullName))
                return null;

            pluginModel.View = pluginInfo.ViewType.Assembly.CreateInstance(pluginInfo.ViewType.FullName) as FrameworkElement;
            if (pluginModel.View is FrameworkElement view && view.DataContext is IDockingPanel panel)
            {
                panel.ViewPluginModel = pluginModel;
            }

            return pluginModel.View;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
