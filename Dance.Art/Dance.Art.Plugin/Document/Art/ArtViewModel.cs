using CommunityToolkit.Mvvm.Input;
using Dance.Art.Domain;
using Dance.Art.Plugin.Document;
using Dance.Wpf;
using ICSharpCode.AvalonEdit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.Plugin
{
    /// <summary>
    /// .art项目文件视图模型
    /// </summary>
    public class ArtViewModel : AvaloneditDocumentViewModelBase
    {
        /// <summary>
        /// 获取编辑器
        /// </summary>
        /// <returns>编辑器</returns>
        protected override TextEditor? GetEditor()
        {
            if (this.View is not TxtView view)
                return null;

            return view.edit;
        }
    }
}