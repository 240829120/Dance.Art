using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 脚本服务信息
    /// </summary>
    public class ScriptServiceInfo
    {
        /// <summary>
        /// 脚本服务信息
        /// </summary>
        /// <param name="nameSpace">命名空间</param>
        /// <param name="name">名称</param>
        /// <param name="type">类型</param>
        public ScriptServiceInfo(string nameSpace, string name, Type type)
        {
            this.NameSpace = nameSpace;
            this.Name = name;
            this.Type = type;
        }

        /// <summary>
        /// 命名空间
        /// </summary>
        public string NameSpace { get; private set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 服务类型
        /// </summary>
        public Type Type { get; private set; }
    }
}
