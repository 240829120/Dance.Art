using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Storage
{
    /// <summary>
    /// 实体基类
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        /// 编号
        /// </summary>
        [LiteDB.BsonId(true)]
        public int ID { get; set; }
    }
}
