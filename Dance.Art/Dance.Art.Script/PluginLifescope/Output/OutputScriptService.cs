using Dance.Art.Domain;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.Script
{
    /// <summary>
    /// 输出脚本服务
    /// </summary>
    public class OutputScriptService : DanceWrapperModel
    {
        // ============================================================================================
        // Field

        /// <summary>
        /// 输出管理器
        /// </summary>
        private readonly IOutputManager OutputManager = DanceDomain.Current.LifeScope.Resolve<IOutputManager>();

        // ============================================================================================
        // Public Function

        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="txt">日志</param>
        public void Log(string txt)
        {
            this.OutputManager.WriteLine(txt);
        }
    }
}
