using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 文档项模型
    /// </summary>
    public abstract class DocumentItemModelBase : DanceWrapperModel, IDanceJsonObject
    {
        /// <summary>
        /// 对象类型
        /// </summary>
        [Browsable(false)]
        public string PART_DanceObjectType => this.GetType().AssemblyQualifiedName ?? string.Empty;

        /// <summary>
        /// 所属文档
        /// </summary>
        [JsonIgnore]
        [Browsable(false)]
        public virtual IDocumentViewModel? OwnerDocument { get; set; }

        /// <summary>
        /// 属性改变之后出发
        /// </summary>
        protected override void OnWrapperPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            base.OnWrapperPropertyChanged(propertyName);

            if (this.OwnerDocument == null)
                return;

            this.OwnerDocument.IsModify = true;
        }
    }
}
