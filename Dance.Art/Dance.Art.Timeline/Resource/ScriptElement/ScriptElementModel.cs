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
        // Override

        /// <summary>
        /// 当开始时触发
        /// </summary>
        public override void OnBegin()
        {
            Console.WriteLine("OnBegin");
        }

        /// <summary>
        /// 当结束时触发
        /// </summary>
        public override void OnEnd()
        {
            Console.WriteLine("OnEnd");
        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            Console.WriteLine("Destroy");
        }
    }
}
