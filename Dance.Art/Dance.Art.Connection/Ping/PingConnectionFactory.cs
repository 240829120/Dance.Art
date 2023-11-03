using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Connection
{
    /// <summary>
    /// Ping连接工厂
    /// </summary>
    public class PingConnectionFactory : IConnectionFactory
    {
        /// <summary>
        /// 编辑视图类型
        /// </summary>
        public Type EdtiViewType => typeof(PingConnectionEditView);

        /// <summary>
        /// 创建连接控制器
        /// </summary>
        /// <returns>连接控制器</returns>
        public IConnectionController CreateController()
        {
            return new PingConnectionController();
        }
    }
}
