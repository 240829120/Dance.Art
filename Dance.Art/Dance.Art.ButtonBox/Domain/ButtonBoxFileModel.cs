using Dance.Art.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.ButtonBox
{
    /// <summary>
    /// 按钮文件模型
    /// </summary>
    public class ButtonBoxFileModel : DocumentItemModelBase
    {
        /// <summary>
        /// 画布对象
        /// </summary>
        public ButtonBoxDocumentViewCanvasModel? CanvasModel { get; set; }

        /// <summary>
        /// 项对象
        /// </summary>
        public List<ButtonBoxItemModelBase>? Items { get; set; }
    }
}
