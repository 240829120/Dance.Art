using Dance.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
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
    public abstract class ResourceItemModelBase : DocumentItemModelBase, IResourceItemModel
    {
        /// <summary>
        /// 资源项基类
        /// </summary>
        /// <param name="dataTemplate">模板</param>
        public ResourceItemModelBase(Type? dataTemplate)
        {
            if (dataTemplate != null)
            {
                this.DataTemplate = DanceXamlExpansion.CreateDataTemplate(dataTemplate) ?? throw new Exception($"can`t create DataTemplate, type: {dataTemplate}");
            }
        }

        /// <summary>
        /// 模板
        /// </summary>
        [Browsable(false), JsonIgnore]
        [NotNull]
        public DataTemplate? DataTemplate { get; protected set; }

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
