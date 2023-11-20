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
    /// 文本框
    /// </summary>
    [DisplayName("文本框")]
    public class TextBoxModel : ControlGridItemModelBase
    {
        /// <summary>
        /// 文本框
        /// </summary>
        public TextBoxModel() : base(typeof(TextBox))
        {

        }

        // ================================================================================
        // Field

        // ================================================================================
        // Property

        #region Value -- 值

        private string? _value = "文本框";
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
    }
}
