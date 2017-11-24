using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitFileProcess
{
    /// <summary>
    /// CIT文件头信息结构体，120字节
    /// </summary>
    public class DataHeadInfo
    {
        #region 文件类型
        /// <summary>
        /// 文件类型 iDataType：1轨检、2动力学、3弓网----4个字节
        /// </summary>
        public int iDataType;
        #endregion

        #region 文件版本号
        /// <summary>
        /// 文件版本号，用X.X.X表示 第一位大于等于3代表加密后,只加密数据块部分---1+20个字节，第一个字节表示实际长度，以下类同
        /// </summary>
        public string sDataVersion;
        #endregion

        #region 线路代码
        /// <summary>
        /// 线路代码，同PWMIS----1+4个字节
        /// </summary>
        public string sTrackCode;
        #endregion

        #region 线路名 英文最好
        /// <summary>
        /// 线路名  英文最好---1+20个字节
        /// </summary>
        public string sTrackName;
        #endregion

        #region 行别
        /// <summary>
        /// 行别：1上行、2下行、3单线----4个字节
        /// </summary>
        public int iDir;
        #endregion

        #region 检测车号
        /// <summary>
        /// 检测车号，不足补空格---1+20个字节
        /// </summary>
        public string sTrain;
        #endregion

        #region 检测日期
        /// <summary>
        /// 检测日期：yyyy-MM-dd---1+10个字节
        /// </summary>
        public string sDate;
        #endregion

        #region 检测起始时间
        /// <summary>
        /// 检测起始时间：HH:mm:ss---1+8个字节
        /// </summary>
        public string sTime;
        #endregion

        #region 检测方向
        /// <summary>
        /// 检测方向，正0，反1----4个字节
        /// </summary>
        public int iRunDir;
        #endregion

        #region 增里程0，减里程1
        /// <summary>
        /// 增里程0，减里程1----4个字节
        /// </summary>
        public int iKmInc;
        #endregion

        #region 开始里程
        /// <summary>
        /// 开始里程----4个字节
        /// </summary>
        public float fkmFrom;
        #endregion

        #region 结束里程
        /// <summary>
        /// 结束里程，检测结束后更新----4个字节
        /// </summary>
        public float fkmTo;
        #endregion

        #region 采样数，（距离采样>0, 时间采样<0)
        /// <summary>
        /// 采样数，距离采样>0, 时间采样小于0 ----4个字节
        /// </summary>
        public int iSmaleRate;
        #endregion

        #region 数据块中通道总数
        /// <summary>
        /// 数据块中通道总数----4个字节
        /// </summary>
        public int iChannelNumber;
        #endregion
    }
}
