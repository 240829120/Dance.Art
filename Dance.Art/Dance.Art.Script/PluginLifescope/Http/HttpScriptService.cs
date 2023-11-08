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
    /// HTTP脚本服务
    /// </summary>
    public class HttpScriptService : DanceWrapperModel
    {
        // ============================================================================================
        // Public Function

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="data">数据</param>
        /// <returns>返回数据</returns>
        public string? Post(string url, string data)
        {
            string? result = DanceHttpHelper.Post(url, data, null, null).Result;

            return result;
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <returns>返回数据</returns>
        public string? Get(string url)
        {
            string? result = DanceHttpHelper.Get(url, null, null).Result;

            return result;
        }
    }
}
