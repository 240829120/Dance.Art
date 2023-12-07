using Dance.Wpf;
using Microsoft.ClearScript;
using Microsoft.ClearScript.JavaScript;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 脚本宿主
    /// </summary>
    public class ScriptHost : DanceObject
    {
        // ===========================================================================================
        // Field

        /// <summary>
        /// 服务管理器
        /// </summary>
        private readonly IDanceServiceManager ServiceManager = DanceDomain.Current.LifeScope.Resolve<IDanceServiceManager>();

        // ===========================================================================================
        // Public Function

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="route">路由</param>
        /// <param name="args">参数</param>
        /// <returns>执行结果</returns>
        public string? Invoke(string route, IEnumerable args)
        {
            try
            {
                return this.ServiceManager.InvokeJson(route, args.Cast<string>().ToArray());
            }
            catch (Exception ex)
            {
                log.Error(ex);

                return null;
            }
        }
    }
}