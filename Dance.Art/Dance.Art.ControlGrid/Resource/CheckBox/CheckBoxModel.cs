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
    /// 复选框
    /// </summary>
    [DisplayName("复选框")]
    public class CheckBoxModel : ControlGridItemModelBase
    {
        /// <summary>
        /// 复选框
        /// </summary>
        public CheckBoxModel() : base(typeof(CheckBox))
        {

        }

        // ================================================================================
        // Field

        // ================================================================================
        // Property

        #region Content -- 内容

        private string? content = "命令按钮";
        /// <summary>
        /// 内容
        /// </summary>
        [Category(PropertyCategoryDefines.OTHER), Description("内容"), DisplayName("内容")]
        public string? Content
        {
            get { return content; }
            set { content = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        #region IsChecked -- 勾选

        private bool isChecked;
        /// <summary>
        /// 值
        /// </summary>
        [Category(PropertyCategoryDefines.OTHER), Description("值"), DisplayName("值")]
        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion
    }
}
