using Dance.Art.Domain;
using Dance.Art.Module;
using Dance.Wpf;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.ControlGrid
{
    /// <summary>
    /// 控制面板接口
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ControlGridController : ControllerBase
    {
        /// <summary>
        /// 服务管理器
        /// </summary>
        private readonly IDanceServiceManager ServiceManager = DanceDomain.Current.LifeScope.Resolve<IDanceServiceManager>();

        /// <summary>
        /// 添加项
        /// </summary>
        /// <param name="request">请求</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddItem")]
        public ServerResponse AddItem(ControlGridAddItemRequest request)
        {
            if (this.ServiceManager.Invoke("ControlGrid/AddItem", request) is not ServerResponse response)
                return new ServerResponse(ServerResponseCode.FAIL, "添加失败");

            return response;
        }

        /// <summary>
        /// 删除项
        /// </summary>
        /// <param name="request">请求</param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteItem")]
        public ServerResponse DeleteItem(ControlGridDeleteItemRequest request)
        {
            if (this.ServiceManager.Invoke("ControlGrid/DeleteItem", request) is not ServerResponse response)
                return new ServerResponse(ServerResponseCode.FAIL, "删除失败");

            return response;
        }

        /// <summary>
        /// 移动项
        /// </summary>
        /// <param name="request">请求</param>
        /// <returns></returns>
        [HttpPost]
        [Route("MoveItem")]
        public ServerResponse MoveItem(ControlGridMoveItemRequest request)
        {
            if (this.ServiceManager.Invoke("ControlGrid/MoveItem", request) is not ServerResponse response)
                return new ServerResponse(ServerResponseCode.FAIL, "移动失败");

            return response;
        }
    }
}