using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitFileProcess
{
    /// <summary>
    /// 文件信息偏移量类
    /// </summary>
    public class DataHeadOffset
    {
        /// <summary>
        /// 文件头偏移位置
        /// </summary>
        public static int Header = 0;

        /// <summary>
        /// 文件类型数据偏移位置（0）
        /// </summary>
        public static int DataType = 0;

        /// <summary>
        /// 文件版本号数据偏移位置（4）
        /// </summary>
        public static int DataVersion = 4;

        /// <summary>
        /// 线路代码数据偏移位置（25）
        /// </summary>
        public static int TrackCode = 25;

        /// <summary>
        /// 线路名数据偏移位置（30）
        /// </summary>
        public static int TrackName = 30;

        /// <summary>
        /// 行别数据偏移位置（51）
        /// </summary>
        public static int Dir = 51;

        /// <summary>
        /// 检测车号数据偏移位置（55）
        /// </summary>
        public static int TrainCode = 55;

        /// <summary>
        /// 检测日期数据偏移位置（76）
        /// </summary>
        public static int Date = 76;

        /// <summary>
        /// 检测起始时间数据偏移位置（87）
        /// </summary>
        public static int Time = 87;

        /// <summary>
        /// 检测方向数据偏移位置（96）
        /// </summary>
        public static int RunDir = 96;

        /// <summary>
        /// 增减里程数据偏移位置（100）
        /// </summary>
        public static int KmInc = 100;

        /// <summary>
        /// 开始里程数据偏移位置（104）
        /// </summary>
        public static int KmFrom = 104;

        /// <summary>
        /// 结束里程数据偏移位置（108）
        /// </summary>
        public static int KmTo = 108;

        /// <summary>
        /// 采样频率数据偏移位置（112）
        /// </summary>
        public static int SmaleRate = 112;

        /// <summary>
        /// 通道个数数据偏移位置（116）
        /// </summary>
        public static int ChannelNumber = 116;

        /// <summary>
        /// 通道定义数据偏移位置（120）
        /// </summary>
        public static int ChannelDef = 120;
    }
}
