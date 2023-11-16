using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Art.Module
{
    /// <summary>
    /// 脚本编辑器
    /// </summary>
    public class ScriptMultiLineEditor : MultiLineEditor
    {
        static ScriptMultiLineEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScriptMultiLineEditor), new FrameworkPropertyMetadata(typeof(ScriptMultiLineEditor)));
        }
    }
}
