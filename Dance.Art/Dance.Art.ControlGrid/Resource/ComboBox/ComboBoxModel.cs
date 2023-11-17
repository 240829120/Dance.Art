using CommunityToolkit.Mvvm.Input;
using Dance.Art.Domain;
using Dance.Art.Module;
using Dance.Wpf;
using Microsoft.ClearScript.JavaScript;
using Microsoft.ClearScript;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Dance.Art.ControlGrid
{
    /// <summary>
    /// 下拉选框
    /// </summary>
    [DisplayName("下拉选框")]
    public class ComboBoxModel : ControlGridItemModelBase
    {
        /// <summary>
        /// 下拉选框
        /// </summary>
        public ComboBoxModel() : base(typeof(ComboBox))
        {

        }

        // ================================================================================
        // Field

        // ================================================================================
        // Property

        #region OwnerDocument -- 所属文档

        private IDocumentViewModel? ownerDocument;

        /// <summary>
        /// 所属文档
        /// </summary>
        public override IDocumentViewModel? OwnerDocument
        {
            get { return this.ownerDocument; }
            set
            {
                this.ownerDocument = value;
                this.items.OwnerDocument = value;
            }
        }

        #endregion

        #region Value -- 值

        private string? _value;
        /// <summary>
        /// 值
        /// </summary>
        [Category(PropertyCategoryDefines.OTHER), Description("值"), DisplayName("值")]
        public string? Value
        {
            get { return _value; }
            set { _value = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        #region Items -- 项集合

        private DocumentWrapperCollection<string> items = new();
        /// <summary>
        /// 项集合
        /// </summary>
        [Category(PropertyCategoryDefines.OTHER), Description("选项"), DisplayName("选项")]
        public DocumentWrapperCollection<string> Items
        {
            get { return items; }
            set { items = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion
    }
}
