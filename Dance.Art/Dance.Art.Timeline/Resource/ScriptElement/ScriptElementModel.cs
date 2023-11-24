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

namespace Dance.Art.Timeline
{
    /// <summary>
    /// 脚本
    /// </summary>
    [DisplayName("脚本")]
    public class ScriptElementModel : TimelineElementModelBase
    {
        /// <summary>
        /// 脚本按钮
        /// </summary>
        public ScriptElementModel() : base(typeof(ScriptElement))
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
