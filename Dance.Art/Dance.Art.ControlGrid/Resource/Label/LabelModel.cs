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
using System.Windows;
using System.Windows.Media;

namespace Dance.Art.ControlGrid
{
    /// <summary>
    /// 脚本按钮
    /// </summary>
    [DisplayName("标签")]
    public class LabelModel : ControlGridItemModelBase
    {
        /// <summary>
        /// 脚本按钮
        /// </summary>
        public LabelModel() : base(typeof(Label))
        {
            this.BorderThickness = new Thickness(0);
            this.BorderColor = Colors.Transparent;
            this.BackgroundColor = Colors.Transparent;
        }

        // ================================================================================
        // Field

        // ================================================================================
        // Property

        #region Content -- 内容

        private string? content = "标签";
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
    }
}
