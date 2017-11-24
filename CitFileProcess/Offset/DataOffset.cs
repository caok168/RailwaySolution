using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitFileProcess
{

    public class DataOffset
    {
        /// <summary>
        /// 文件头 文件信息长度（120）
        /// </summary>
        public static int DataHeadLength = 120;

        /// <summary>
        /// 文件头 通道定义长度（65）
        /// </summary>
        public static int DataChannelLength = 65;

        /// <summary>
        /// 文件头 补充信息长度（4）
        /// </summary>
        public static int ExtraLength = 4;
    }
}
