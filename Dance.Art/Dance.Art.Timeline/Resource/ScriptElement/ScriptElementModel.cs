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
using System.Diagnostics;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace Dance.Art.Timeline
{
    /// <summary>
    /// 脚本元素
    /// </summary>
    [DisplayName("脚本元素")]
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
        // Property

        #region BeginScript -- 开始脚本

        private string? beginScript;
        /// <summary>
        /// 点击脚本
        /// </summary>
        [Category(PropertyCategoryDefines.OTHER), PropertyOrder(4), Description("开始脚本"), DisplayName("开始脚本")]
        [Editor(typeof(ScriptMultiLineEditor), typeof(ScriptMultiLineEditor))]
        public string? BeginScript
        {
            get { return beginScript; }
            set { beginScript = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        #region EndScript -- 结束脚本

        private string? endScript;
        /// <summary>
        /// 结束脚本
        /// </summary>
        [Category(PropertyCategoryDefines.OTHER), PropertyOrder(5), Description("结束脚本"), DisplayName("结束脚本")]
        [Editor(typeof(ScriptMultiLineEditor), typeof(ScriptMultiLineEditor))]
        public string? EndScript
        {
            get { return endScript; }
            set { endScript = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        // ================================================================================
        // Override

        /// <summary>
        /// 当开始时触发
        /// </summary>
        public override void OnPlay()
        {
            // nothing todo.
        }

        /// <summary>
        /// 当停止时触发
        /// </summary>
        public override void OnStop()
        {
            // nothing todo.
        }

        /// <summary>
        /// 当开始时触发
        /// </summary>
        public override void OnBegin()
        {
            if (string.IsNullOrWhiteSpace(this.BeginScript))
                return;

            MainViewModel vm = DanceDomain.Current.LifeScope.Resolve<MainViewModel>();
            if (vm == null || vm.ScriptDomain == null || vm.ScriptDomain.Engine == null || (vm.ScriptStatus != ScriptStatus.Running && vm.ScriptStatus != ScriptStatus.Debugging))
                return;

            Task.Run(() =>
            {
                try
                {
                    vm.ScriptDomain.Engine.Evaluate(new DocumentInfo() { Category = ModuleCategory.Standard }, this.BeginScript);
                }
                catch (Exception ex)
                {
                    this.OutputManager.WriteLine(ex.Message);
                }
            });
        }

        /// <summary>
        /// 当结束时触发
        /// </summary>
        public override void OnEnd()
        {
            if (string.IsNullOrWhiteSpace(this.EndScript))
                return;

            MainViewModel vm = DanceDomain.Current.LifeScope.Resolve<MainViewModel>();
            if (vm == null || vm.ScriptDomain == null || vm.ScriptDomain.Engine == null || (vm.ScriptStatus != ScriptStatus.Running && vm.ScriptStatus != ScriptStatus.Debugging))
                return;

            Task.Run(() =>
            {
                try
                {
                    vm.ScriptDomain.Engine.Evaluate(new DocumentInfo() { Category = ModuleCategory.Standard }, this.EndScript);
                }
                catch (Exception ex)
                {
                    this.OutputManager.WriteLine(ex.Message);
                }
            });
        }
    }
}