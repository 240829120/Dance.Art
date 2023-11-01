using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 文档文件信息分组定义
    /// </summary>
    public static class DocumentFileGroupInfoDefines
    {
        /// <summary>
        /// 常规文件
        /// </summary>
        public readonly static DocumentFileGroupInfo NORMAL_FILE = new("NORMAL_FILE", "常规");

        /// <summary>
        /// 数据
        /// </summary>
        public readonly static DocumentFileGroupInfo DATA_FILE = new("DATA_FILE", "数据");

        /// <summary>
        /// 脚本
        /// </summary>
        public readonly static DocumentFileGroupInfo SCRIPT_FILE = new("SCRIPT_FILE", "脚本");
    }
}
