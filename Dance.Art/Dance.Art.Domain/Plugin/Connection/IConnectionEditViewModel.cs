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
        /// 从模型中加载数据
        /// </summary>
        /// <param name="model">连接模型</param>
        void LoadFromModel(ConnectionModel model);

        /// <summary>
        /// 保存至模型
        /// </summary>
        /// <param name="model">连接模型</param>
        /// <param name="error">错误信息</param>
        /// <returns>是否成功保存</returns>
        bool SaveToModel(ConnectionModel model, out string error);
    }
}
