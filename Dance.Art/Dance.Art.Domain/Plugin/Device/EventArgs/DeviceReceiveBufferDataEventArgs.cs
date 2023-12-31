﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Art.Domain
{
    /// <summary>
    /// 设备接收二进制数据事件参数
    /// </summary>
    /// <param name="source">源</param>
    /// <param name="buffer">二进制数据</param>
    /// <param name="length">数据长度</param>
    public class DeviceReceiveBufferDataEventArgs(IDeviceSource source, byte[] buffer, int length) : DeviceReceiveDataEventArgsBase(source)
    {
        /// <summary>
        /// 二进制数据
        /// </summary>
        public byte[] Buffer { get; private set; } = buffer;

        /// <summary>
        /// 数据长度
        /// </summary>
        public int Length { get; private set; } = length;

        /// <summary>
        /// 获取字符数据
        /// </summary>
        /// <returns></returns>
        public string GetBufferString()
        {
            return Encoding.UTF8.GetString(this.Buffer);
        }
    }
}
