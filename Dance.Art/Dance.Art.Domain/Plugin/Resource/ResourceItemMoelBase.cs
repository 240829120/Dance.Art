using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 资源项基类
    /// </summary>
    public abstract class ResourceItemMoelBase : DanceWrapperModel
    {
        /// <summary>
        /// 资源项基类
        /// </summary>
        /// <param name="dataTemplate">模板</param>
        public ResourceItemMoelBase(Type dataTemplate)
        {
            this.DataTemplate = DanceXamlExpansion.CreateDataTemplate(dataTemplate) ?? throw new Exception($"can`t create DataTemplate, type: {dataTemplate}");
        }

        /// <summary>
        /// 模板
        /// </summary>
        [Browsable(false)]
        public DataTemplate DataTemplate { get; }

        #region ID -- 编号

        private string? id;
        /// <summary>
        /// 编号
        /// </summary>
        [Category(PropertyCategoryDefines.BASE), PropertyOrder(0), Description("编号"), DisplayName("编号")]
        public string? ID
        {
            get { return id; }
            set { id = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion
    }
}
