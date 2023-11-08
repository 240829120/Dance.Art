using Dance.Art.Domain;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Dance.Art.Script
{
    /// <summary>
    /// 消息脚本服务
    /// </summary>
    public class MessageScriptService : DanceWrapperModel
    {
        // ============================================================================================
        // Public Function

        /// <summary>
        /// 显示消息
        /// </summary>
        /// <remarks>
        /// <code>
        /// -------------------------------------------------------------------------------
        /// icon   取值: None, Failure, Success, Warning, Info
        /// action 取值: YES, NO, CANCEL
        /// 
        /// 返回值: YES, NO, CANCEL
        /// -------------------------------------------------------------------------------
        /// messageService.showMessageBox("提示", "Info", "提示内容", "YES | NO");
        /// -------------------------------------------------------------------------------
        /// </code>
        /// </remarks>
        /// <param name="header">头部</param>
        /// <param name="icon">图标</param>
        /// <param name="content">内容</param>
        /// <param name="action">行为</param>
        public string ShowMessageBox(string header, string icon, string content, string action)
        {
            if (!Enum.TryParse(icon, out DanceMessageBoxIcon enumIcon))
                enumIcon = DanceMessageBoxIcon.None;

            DanceMessageBoxAction enumAction = DanceMessageBoxAction.YES;
            if (!string.IsNullOrWhiteSpace(action))
            {
                string[] parts = action.Split('|');
                for (int i = 0; i < parts.Length; ++i)
                {
                    string part = parts[i];
                    if (!Enum.TryParse(part.Trim(), out DanceMessageBoxAction ac))
                    {
                        enumAction = DanceMessageBoxAction.YES;
                        break;
                    }

                    if (i == 0)
                    {
                        enumAction = ac;
                    }
                    else
                    {
                        enumAction |= ac;
                    }
                }
            }

            DanceMessageBoxAction enumResult = DanceMessageExpansion.ShowMessageBox(header, enumIcon, content, enumAction);

            return $"{enumResult}";
        }

        /// <summary>
        /// 显示通知
        /// </summary>
        /// <param name="header">标题</param>
        /// <param name="icon">图标</param>
        /// <param name="content">内容</param>
        public void ShowNotify(string header, string icon, string content)
        {
            if (!Enum.TryParse(icon, out ToolTipIcon enumIcon))
                enumIcon = ToolTipIcon.None;

            DanceMessageExpansion.ShowNotify(enumIcon, header, content);
        }
    }
}
