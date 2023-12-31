﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dance.Art.ControlGrid
{
    /// <summary>
    /// 按钮组项数据模板选择器
    /// </summary>
    public class ControlGridDataTemplateSelecter : DataTemplateSelector
    {
        public override DataTemplate? SelectTemplate(object item, DependencyObject container)
        {
            if (item is not ControlGridItemModelBase model)
                return null;

            return model.DataTemplate;
        }
    }
}
