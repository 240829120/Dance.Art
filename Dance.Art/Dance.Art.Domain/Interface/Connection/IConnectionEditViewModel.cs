using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 连接编辑视图模型
    /// </summary>
    public interface IConnectionEditViewModel
    {
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="model">模型</param>
        void Load(ConnectionModel model);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model">模型</param>
        /// <returns>是否保存成功</returns>
        bool Save(ConnectionModel model);
    }
}
