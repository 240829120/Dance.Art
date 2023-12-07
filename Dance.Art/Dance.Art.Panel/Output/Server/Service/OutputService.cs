using Dance.Art.Domain;
using Dance.Wpf;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Panel
{
    /// <summary>
    /// 输出服务
    /// </summary>
    [DanceServiceRoute]
    public class OutputService
    {
        /// <summary>
        /// 输出管理器
        /// </summary>
        private readonly IOutputManager OutputManager = DanceDomain.Current.LifeScope.Resolve<IOutputManager>();

        /// <summary>
        /// 输出行
        /// </summary>
        /// <param name="request">请求</param>
        /// <returns></returns>
        [DanceServiceRoute]
        public ServerResponse WriteLine(WriteLineRequest request)
        {
            this.OutputManager.WriteLine(request.msg ?? string.Empty);

            return new ServerResponse(ServerResponseCode.SUCCESS, "输出日志成功");
        }
    }
}
