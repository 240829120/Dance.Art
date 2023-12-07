using Dance.Art.Domain;
using Dance.Wpf;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Dance.Art.Panel
{
    /// <summary>
    /// 输出控制器
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class OutputController : ControllerBase
    {
        /// <summary>
        /// 服务管理器
        /// </summary>
        private readonly IDanceServiceManager ServiceManager = DanceDomain.Current.LifeScope.Resolve<IDanceServiceManager>();

        [HttpPost]
        [Route("WriteLine")]
        public ServerResponse WriteLine(WriteLineRequest request)
        {
            if (this.ServiceManager.Invoke("ControlGrid/AddItem", request) is not ServerResponse response)
                return new ServerResponse(ServerResponseCode.FAIL, "输出日志失败");

            return response;
        }
    }
}