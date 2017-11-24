using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitFileProcess
{
    /// <summary>
    /// 通道定义偏移量类
    /// </summary>
    public class DataChannelOffset
    {
        /// <summary>
        /// 轨检通道偏移位置（0）
        /// </summary>
        public static int ID = 0;

        /// <summary>
        /// 通道名称英文偏移位置（4）
        /// </summary>
        public static int NameEn = 4;

        /// <summary>
        /// 通道名称中文偏移位置（25）
        /// </summary>
        public static int NameCh = 25;

        /// <summary>
        /// 通道比例偏移位置（46）
        /// </summary>
        public static int Scale = 46;

        /// <summary>
        /// 通道基线值偏移位置（50）
        /// </summary>
        public static int Offset = 50;

        /// <summary>
        /// 通道单位偏移位置（54）
        /// </summary>
        public static int Unit = 54;
    }
}
