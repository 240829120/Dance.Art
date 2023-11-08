using Dance.Art.Domain;
using Dance.Wpf;
using Newtonsoft.Json;
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
    /// 文件脚本服务
    /// </summary>
    public class FileScriptService : DanceWrapperModel
    {
        // ============================================================================================
        // Public Function

        /// <summary>
        /// 读取文本文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>文件内容</returns>
        public string ReadTxtFile(string path)
        {
            if (!File.Exists(path))
                return string.Empty;

            using StreamReader sr = new(path, Encoding.UTF8);
            string content = sr.ReadToEnd();

            return content;
        }

        /// <summary>
        /// 写入文本文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="content">内容</param>
        public void WriteTxtFile(string path, string content)
        {
            using StreamWriter sw = new(path, false, Encoding.UTF8);
            sw.Write(content);
        }
    }
}
