﻿using CommunityToolkit.Mvvm.Input;
using Dance.Art.Domain;
using Dance.Art.Document.Document;
using Dance.Wpf;
using ICSharpCode.AvalonEdit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.Document
{
    /// <summary>
    /// 文本视图模型
    /// </summary>
    public class TxtViewModel : AvaloneditDocumentViewModelBase
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