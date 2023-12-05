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


    [ApiController]
    [Route("[controller]")]
    public class OutputController : ControllerBase
    {
        /// <summary>
        /// 输出管理器
        /// </summary>
        private readonly IOutputManager OutputManager = DanceDomain.Current.LifeScope.Resolve<IOutputManager>();

        [HttpPost]
        [Route("WriteLine")]
        public WriteLineResponse WriteLine([FromBody] WriteLineRequest request)
        {
            this.OutputManager.WriteLine(request.msg ?? string.Empty);

            return new WriteLineResponse { message = "输出日志成功" };
        }
    }
}