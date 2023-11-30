using Dance.Art.Domain;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace Dance.Art.Scene
{
    /// <summary>
    /// 场景项模型
    /// </summary>
    public interface ISceneItemModel : IResourceItemModel, IDanceModel3D
    {
        /// <summary>
        /// 图标
        /// </summary>
        string? Icon { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        string? Name { get; set; }
    }
}
