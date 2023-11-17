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
    /// 脚本按钮
    /// </summary>
    [DisplayName("脚本按钮")]
    public class ScriptButtonModel : ControlGridItemModelBase
    {
        /// <summary>
        /// 脚本按钮
        /// </summary>
        public ScriptButtonModel() : base(typeof(ScriptButton))
        {
            this.ClickCommand = new(this.Click);
        }

        // ================================================================================
        // Field

        /// <summary>
        /// 输出管理器
        /// </summary>
        private readonly IOutputManager OutputManager = DanceDomain.Current.LifeScope.Resolve<IOutputManager>();

        // ================================================================================
        // Property

        #region Content -- 内容

        private string? content = "脚本按钮";
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

        #region OnClick -- 点击脚本

        private string? onClick;
        /// <summary>
        /// 点击脚本
        /// </summary>
        [Category(PropertyCategoryDefines.OTHER), Description("点击执行脚本"), DisplayName("点击脚本")]
        [Editor(typeof(ScriptMultiLineEditor), typeof(ScriptMultiLineEditor))]
        public string? OnClick
        {
            get { return onClick; }
            set { onClick = value; this.OnWrapperPropertyChanged(); }
        }

        #endregion

        // ================================================================================
        // Command

        #region ClickCommand -- 点击命令

        /// <summary>
        /// 点击命令
        /// </summary>
        [Browsable(false), JsonIgnore]
        public RelayCommand ClickCommand { get; private set; }

        /// <summary>
        /// 点击
        /// </summary>
        private void Click()
        {
            if (string.IsNullOrWhiteSpace(this.OnClick))
                return;

            MainViewModel vm = DanceDomain.Current.LifeScope.Resolve<MainViewModel>();
            if (vm == null || vm.ScriptDomain == null || vm.ScriptDomain.Engine == null || (vm.ScriptStatus != ScriptStatus.Running && vm.ScriptStatus != ScriptStatus.Debugging))
            {
                DanceMessageExpansion.ShowMessageBox("提示", DanceMessageBoxIcon.Info, "脚本未运行", DanceMessageBoxAction.YES);
                return;
            }

            Task.Run(() =>
            {
                try
                {
                    vm.ScriptDomain.Engine.Evaluate(new DocumentInfo() { Category = ModuleCategory.Standard }, this.OnClick);
                }
                catch (Exception ex)
                {
                    this.OutputManager.WriteLine(ex.Message);
                }
            });
        }

        #endregion
    }
}
