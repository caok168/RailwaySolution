using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitFileProcess
{
    /// <summary>
    /// 通道定义信息结构体---65个字节
    /// </summary>
    public class DataChannelInfo
    {
        #region 通道Id
        /// <summary>
        /// 通道Id：轨检通道从1～1000定义；动力学从1001~2000；弓网从2001~3000-----4个字节
        /// </summary>
        public int sID;
        #endregion

        #region 通道名称英文
        /// <summary>
        /// 通道名称英文，不足补空格-----1+20个字节
        /// </summary>
        public string sNameEn;
        #endregion

        #region 通道名称中文
        /// <summary>
        /// 通道名称中文，不足补空格-----1+20个字节
        /// </summary>
        public string sNameCh;
        #endregion

        #region 通道比例
        /// <summary>
        /// 通道比例-----4个字节
        /// </summary>
        public float fScale;
        #endregion

        #region 通道基线值
        /// <summary>
        /// 通道基线值-----4个字节
        /// </summary>
        public float fOffset;
        #endregion

        #region 通道单位
        /// <summary>
        /// 通道单位，不足补空格-----1+10个字节
        /// </summary>
        public string sUnit;
        #endregion
    }
}
