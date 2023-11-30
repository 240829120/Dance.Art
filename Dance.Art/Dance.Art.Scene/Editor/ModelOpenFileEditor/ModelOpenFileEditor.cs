using Dance.Art.Module;
using HelixToolkit.SharpDX.Core.Assimp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Scene
{
    /// <summary>
    /// 打开文件编辑器
    /// </summary>
    public class ModelOpenFileEditor : OpenFileEditor
    {
        public ModelOpenFileEditor()
        {
            this.Filter = Importer.SupportedFormatsString;
        }
    }
}
