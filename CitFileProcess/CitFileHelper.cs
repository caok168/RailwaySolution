using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace CitFileProcess
{
    /// <summary>
    /// 读取Cit文件帮助类
    /// </summary>
    public partial class CitFileHelper
    {
        #region 全局变量

        /// <summary>
        /// cit文件的文件头信息
        /// </summary>
        public DataHeadInfo dhi { get; set; }

        /// <summary>
        /// cit文件的通道定义信息
        /// </summary>
        public List<DataChannelInfo> dciL { get; set; }

        #endregion

        #region 文件头部分

        #region 获取cit 文件头 文件信息

        /// <summary>
        /// 查询CIT文件头信息--返回文件头信息结构体，同时dhi全局变量赋值
        /// </summary>
        /// <param name="sFile"></param>
        /// <returns>结构体</returns>
        public DataHeadInfo GetDataInfoHead(string sFile)
        {
            using (FileStream fs = new FileStream(sFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                {
                    br.BaseStream.Position = 0;
                    dhi = GetDataInfoHead(br.ReadBytes(DataOffset.DataHeadLength));
                }
            }
            return dhi;
        }


        #endregion

        #region 获取cit 文件头 单个通道定义信息

        /// <summary>
        /// 查询CIT通道信息--返回通道定义结构体列表，同时dciL全局变量赋值
        /// 返回：通道定义信息结构体对象列表
        /// </summary>
        /// <param name="sFile">CIT文件名（全路径）</param>
        /// <returns>返回结构体</returns>
        public List<DataChannelInfo> GetDataChannelInfoHead(string sFile)
        {
            using (FileStream fs = new FileStream(sFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                {
                    br.BaseStream.Position = 0;

                    //br.ReadBytes(DataOffset.DataHeadLength);

                    DataHeadInfo m_dhi = GetDataInfoHead(br.ReadBytes(DataOffset.DataHeadLength));
                    dhi = m_dhi;

                    byte[] bChannelData = br.ReadBytes(dhi.iChannelNumber * DataOffset.DataChannelLength);
                    dciL = new List<DataChannelInfo>();
                    for (int i = 0; i < dhi.iChannelNumber * DataOffset.DataChannelLength; i += DataOffset.DataChannelLength)
                    {
                        DataChannelInfo dci = GetChannelInfo(bChannelData, i);
                        //if (i == DataOffset.DataChannelLength)
                        //{
                        //    dci.fScale = 4;
                        //}
                        dciL.Add(dci);
                    }
                }
            }

            return dciL;
        }

        #endregion

        #region 获取cit 文件头 补充信息（附加信息）
        /// <summary>
        /// 获取补充信息（附加信息）,返回值是字节流，客户负责解析附加信息到具体的类型
        /// </summary>
        /// <param name="citFilePath"></param>
        /// <returns></returns>
        public byte[] GetExtraInfo(string citFilePath)
        {
            FileStream fs = new FileStream(citFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BinaryReader br = new BinaryReader(fs, Encoding.Default);

            DataHeadInfo m_dhi = GetDataInfoHead(br.ReadBytes(DataOffset.DataHeadLength));
            br.BaseStream.Position = DataOffset.DataHeadLength + m_dhi.iChannelNumber;
            //br.ReadBytes(120);
            //br.ReadBytes(65 * m_dhi.iChannelNumber);
            byte[] extraByte = br.ReadBytes(BitConverter.ToInt32(br.ReadBytes(DataOffset.ExtraLength), 0));
            return extraByte;
        }

        #endregion

        #region 获取文件信息的指定字段信息

        /// <summary>
        /// 获取文件信息的文件类型
        /// </summary>
        /// <param name="citFile"></param>
        /// <returns>文件类型</returns>
        public int GetHeadDataType(string citFile)
        {
            DataHeadInfo headInfo = GetDataInfoHead(citFile);
            return headInfo.iDataType;
        }

        /// <summary>
        /// 获取文件信息的文件版本号
        /// </summary>
        /// <param name="citFile"></param>
        /// <returns>文件版本号</returns>
        public string GetHeadDataVersion(string citFile)
        {
            DataHeadInfo headInfo = GetDataInfoHead(citFile);
            return headInfo.sDataVersion;
        }

        /// <summary>
        /// 获取文件信息的线路代码，同PWMIS
        /// </summary>
        /// <param name="citFile"></param>
        /// <returns>线路代码，同PWMIS</returns>
        public string GetHeadTrackCode(string citFile)
        {
            DataHeadInfo headInfo = GetDataInfoHead(citFile);
            return headInfo.sTrackCode;
        }

        /// <summary>
        /// 获取文件信息的线路名 英文最好
        /// </summary>
        /// <param name="citFile"></param>
        /// <returns>线路名 英文最好</returns>
        public string GetHeadTrackName(string citFile)
        {
            DataHeadInfo headInfo = GetDataInfoHead(citFile);
            return headInfo.sTrackName;
        }

        /// <summary>
        /// 获取文件信息的行别：1上行、2下行、3单线
        /// </summary>
        /// <param name="citFile"></param>
        /// <returns>行别：1上行、2下行、3单线</returns>
        public int GetHeadDir(string citFile)
        {
            DataHeadInfo headInfo = GetDataInfoHead(citFile);
            return headInfo.iDir;
        }

        /// <summary>
        /// 获取文件信息的检测车号，不足补空格
        /// </summary>
        /// <param name="citFile"></param>
        /// <returns>检测车号，不足补空格</returns>
        public string GetHeadTrain(string citFile)
        {
            DataHeadInfo headInfo = GetDataInfoHead(citFile);
            return headInfo.sTrain;
        }

        /// <summary>
        /// 获取文件信息的检测日期：yyyy-MM-dd
        /// </summary>
        /// <param name="citFile"></param>
        /// <returns>检测日期：yyyy-MM-dd</returns>
        public string GetHeadDate(string citFile)
        {
            DataHeadInfo headInfo = GetDataInfoHead(citFile);
            return headInfo.sDate;
        }

        /// <summary>
        /// 获取文件信息的检测起始时间：HH:mm:ss
        /// </summary>
        /// <param name="citFile"></param>
        /// <returns>检测起始时间：HH:mm:ss</returns>
        public string GetHeadTime(string citFile)
        {
            DataHeadInfo headInfo = GetDataInfoHead(citFile);
            return headInfo.sTime;
        }

        /// <summary>
        /// 获取文件信息的检测方向，正0，反1
        /// </summary>
        /// <param name="citFile"></param>
        /// <returns>检测方向，正0，反1</returns>
        public int GetHeadRunDir(string citFile)
        {
            DataHeadInfo headInfo = GetDataInfoHead(citFile);
            return headInfo.iRunDir;
        }

        /// <summary>
        /// 获取文件信息的增里程0，减里程1
        /// </summary>
        /// <param name="citFile"></param>
        /// <returns>增里程0，减里程1</returns>
        public int GetHeadKmInc(string citFile)
        {
            DataHeadInfo headInfo = GetDataInfoHead(citFile);
            return headInfo.iKmInc;
        }

        /// <summary>
        /// 获取文件信息的开始里程
        /// </summary>
        /// <param name="citFile"></param>
        /// <returns>开始里程</returns>
        public float GetHeadKmFrom(string citFile)
        {
            DataHeadInfo m_dhi = GetDataInfoHead(citFile);
            dhi = m_dhi;

            FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BinaryReader br = new BinaryReader(fs, Encoding.Default);
            br.BaseStream.Position = 0;
            br.ReadBytes(DataOffset.DataHeadLength);
            br.ReadBytes(DataOffset.DataChannelLength * m_dhi.iChannelNumber);
            br.ReadBytes(BitConverter.ToInt32(br.ReadBytes(DataOffset.ExtraLength), 0));
            int iChannelNumberSize = m_dhi.iChannelNumber * 2;
            byte[] b = new byte[iChannelNumberSize];
            long iArray = (br.BaseStream.Length - br.BaseStream.Position) / iChannelNumberSize;

            b = br.ReadBytes(iChannelNumberSize);
            if (m_dhi.sDataVersion.StartsWith("3."))
            {
                b = ByteXORByte(b);
            }

            short km = BitConverter.ToInt16(b, 0);

            short m = BitConverter.ToInt16(b, 2);
            float fGL = km + (float)m / m_dhi.iSmaleRate / 1000;//单位为公里

            br.Close();
            fs.Close();

            return fGL;
        }

        /// <summary>
        /// 获取文件信息的结束里程，检测结束后更新
        /// </summary>
        /// <param name="citFile">结束里程，检测结束后更新</param>
        /// <returns></returns>
        public float GetHeadKmTo(string citFile)
        {
            DataHeadInfo m_dhi = GetDataInfoHead(citFile);
            dhi = m_dhi;

            FileStream fs = new FileStream(citFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BinaryReader br = new BinaryReader(fs, Encoding.Default);
            br.BaseStream.Position = 0;
            br.ReadBytes(DataOffset.DataHeadLength);
            br.ReadBytes(DataOffset.DataChannelLength * m_dhi.iChannelNumber);
            br.ReadBytes(BitConverter.ToInt32(br.ReadBytes(DataOffset.ExtraLength), 0));
            int iChannelNumberSize = m_dhi.iChannelNumber * 2;
            byte[] b = new byte[iChannelNumberSize];
            long iArray = (br.BaseStream.Length - br.BaseStream.Position) / iChannelNumberSize;

            float fGL = 0.0f;
            if (iArray > 1)
            {
                int num = (int)(iArray - 1);
                br.BaseStream.Position += iChannelNumberSize * num;
                b = br.ReadBytes(iChannelNumberSize);
                if (m_dhi.sDataVersion.StartsWith("3."))
                {
                    b = ByteXORByte(b);
                }

                short km = BitConverter.ToInt16(b, 0);

                short m = BitConverter.ToInt16(b, 2);
                fGL = km + (float)m / m_dhi.iSmaleRate / 1000;//单位为公里

            }


            br.Close();
            fs.Close();

            return fGL;
        }

        /// <summary>
        /// 获取文件信息的采样数，（距离采样>0, 时间采样<0）
        /// </summary>
        /// <param name="citFile"></param>
        /// <returns>采样数，（距离采样>0, 时间采样<0）</returns>
        public int GetHeadSmaleRate(string citFile)
        {
            DataHeadInfo headInfo = GetDataInfoHead(citFile);
            return headInfo.iSmaleRate;
        }

        /// <summary>
        /// 获取文件信息的数据块中通道总数
        /// </summary>
        /// <param name="citFile"></param>
        /// <returns>数据块中通道总数</returns>
        public int GetHeadChannelNumber(string citFile)
        {
            DataHeadInfo headInfo = GetDataInfoHead(citFile);
            return headInfo.iChannelNumber;
        }

        #endregion

        #region 获取通道定义的指定字段信息

        /// <summary>
        /// 根据通道名称获取通道序号
        /// </summary>
        /// <param name="channelName">通道名称（中文或者英文）</param>
        /// <param name="channelList">通道定义信息结构体对象列表</param>
        /// <returns>通道序号</returns>
        public int GetChannelId(string channelName, string sFile)
        {
            List<DataChannelInfo> channelList = GetDataChannelInfoHead(sFile);

            int channelNumber = -1;
            for (int i = 0; i < channelList.Count; i++)
            {
                if ((channelList[i].sNameEn.Equals(channelName) || channelList[i].sNameCh.Equals(channelName)) && (channelName != ""))
                {
                    channelNumber = i + 1;
                    //channelNumber = channelList[i].sID;
                    break;
                }
            }

            return channelNumber;
        }

        /// <summary>
        /// 根据通道序号获取通道名称英文
        /// </summary>
        /// <param name="id">通道序号</param>
        /// <param name="channelList">通道定义信息结构体对象列表</param>
        /// <returns>通道名称英文</returns>
        public string GetChannelNameEn(int id, string sFile)
        {
            List<DataChannelInfo> channelList = GetDataChannelInfoHead(sFile);
            string channelName = "";
            if (channelList.Count >= id)
            {
                channelName = channelList[id - 1].sNameEn;
            }
            return channelName;
        }

        /// <summary>
        /// 根据通道序号获取通道名称中文
        /// </summary>
        /// <param name="id">通道序号</param>
        /// <param name="channelList">通道定义信息结构体对象列表</param>
        /// <returns>通道名称中文</returns>
        public string GetChannelNameCn(int id, string sFile)
        {
            List<DataChannelInfo> channelList = GetDataChannelInfoHead(sFile);
            string channelName = "";
            if (channelList.Count >= id)
            {
                channelName = channelList[id - 1].sNameCh;
            }
            return channelName;
        }

        /// <summary>
        /// 根据通道序号获取通道比例
        /// </summary>
        /// <param name="id">通道序号</param>
        /// <param name="channelList">通道定义信息结构体对象列表</param>
        /// <returns>通道比例</returns>
        public float GetChannelScale(int id, string sFile)
        {
            List<DataChannelInfo> channelList = GetDataChannelInfoHead(sFile);
            float channelScale = 0;
            if (channelList.Count >= id)
            {
                channelScale = channelList[id - 1].fScale;
            }
            return channelScale;
        }

        /// <summary>
        /// 根据通道英文名称获取通道比例
        /// </summary>
        /// <param name="enname">通道英文名称</param>
        /// <param name="channelList">通道定义信息结构体对象列表</param>
        /// <returns>通道比例</returns>
        public float GetChannelScale(string enname, string sFile)
        {
            List<DataChannelInfo> channelList = GetDataChannelInfoHead(sFile);
            float channelScale = 0;
            for (int i = 0; i < channelList.Count; i++)
            {
                if (channelList[i].sNameEn == enname)
                {
                    channelScale = channelList[i].fScale;
                    break;
                }
            }
            return channelScale;
        }

        /// <summary>
        /// 根据通道序号获取通道基准线
        /// </summary>
        /// <param name="id">通道序号</param>
        /// <param name="channelList">通道定义信息结构体对象列表</param>
        /// <returns>通道基准线</returns>
        public float GetChannelOffset(int id, string sFile)
        {
            List<DataChannelInfo> channelList = GetDataChannelInfoHead(sFile);
            float channelOffset = 0;
            if (channelList.Count >= id)
            {
                channelOffset = channelList[id - 1].fOffset;
            }
            return channelOffset;
        }

        /// <summary>
        /// 根据通道英文名称获取通道基准线
        /// </summary>
        /// <param name="enname">通道英文名称</param>
        /// <param name="channelList">通道定义信息结构体对象列表</param>
        /// <returns>通道基准线</returns>
        public float GetChannelOffset(string enname, string sFile)
        {
            List<DataChannelInfo> channelList = GetDataChannelInfoHead(sFile);
            float channelOffset = 0;
            for (int i = 0; i < channelList.Count; i++)
            {
                if (channelList[i].sNameEn == enname)
                {
                    channelOffset = channelList[i].fOffset;
                    break;
                }
            }
            return channelOffset;
        }

        /// <summary>
        /// 根据通道序号获取通道单位
        /// </summary>
        /// <param name="id">通道序号</param>
        /// <param name="channelList">通道定义信息结构体对象列表</param>
        /// <returns>通道单位</returns>
        public string GetChannelUnit(int id, string sFile)
        {
            List<DataChannelInfo> channelList = GetDataChannelInfoHead(sFile);
            string channelUnit = "";
            if (channelList.Count >= id)
            {
                channelUnit = channelList[id - 1].sUnit;
            }
            return channelUnit;
        }

        /// <summary>
        /// 根据通道英文名称获取通道单位
        /// </summary>
        /// <param name="enname">通道英文名称</param>
        /// <param name="channelList">通道定义信息结构体对象列表</param>
        /// <returns>通道单位</returns>
        public string GetChannelUnit(string enname, string sFile)
        {
            List<DataChannelInfo> channelList = GetDataChannelInfoHead(sFile);
            string channelUnit = "";
            for (int i = 0; i < channelList.Count; i++)
            {
                if (channelList[i].sNameEn == enname)
                {
                    channelUnit = channelList[i].sUnit;
                    break;
                }
            }
            return channelUnit;
        }

        #endregion

        #endregion

        #region 数据块部分

        #region 获取指定通道数据

        /// <summary>
        /// 获取指定通道数据
        /// </summary>
        /// <param name="sSourceFile">cit文件</param>
        /// <param name="iChannelNumber">通道号（从1开始的）</param>
        /// <returns>通道数据</returns>
        public double[] GetSingleChannelData(string sSourceFile, int iChannelNumber)
        {
            try
            {
                List<DataChannelInfo> m_dcil = new List<DataChannelInfo>();

                FileStream fs = new FileStream(sSourceFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fs, Encoding.Default);
                br.BaseStream.Position = 0;

                DataHeadInfo m_dhi = GetDataInfoHead(br.ReadBytes(DataOffset.DataHeadLength));
                dhi = m_dhi;

                m_dcil = GetDataChannelInfoHead(sSourceFile);

                br.ReadBytes(DataOffset.DataChannelLength * m_dhi.iChannelNumber);
                br.ReadBytes(BitConverter.ToInt32(br.ReadBytes(DataOffset.ExtraLength), 0));
                int iChannelNumberSize = m_dhi.iChannelNumber * 2;
                byte[] b = new byte[iChannelNumberSize];
                long iArray = (br.BaseStream.Length - br.BaseStream.Position) / iChannelNumberSize;
                double[] fReturnArray = new double[iArray];
                for (int i = 0; i < iArray; i++)
                {
                    b = br.ReadBytes(iChannelNumberSize);
                    if (m_dhi.sDataVersion.StartsWith("3."))
                    {
                        b = ByteXORByte(b);
                    }

                    double fGL = (BitConverter.ToInt16(b, (iChannelNumber - 1) * 2) / m_dcil[iChannelNumber - 1].fScale + m_dcil[iChannelNumber - 1].fOffset);

                    fReturnArray[i] = fGL;
                }


                br.Close();
                fs.Close();

                return fReturnArray;

            }
            catch (Exception)
            {
                return new double[1];
            }
        }

        /// <summary>
        /// 获取指定通道数据---指定范围内
        /// </summary>
        /// <param name="sSourceFile">cit文件</param>
        /// <param name="iChannelNumber">通道号（从1开始的）</param>
        /// <param name="startPos">起始文件指针</param>
        /// <param name="endPos">结束文件指针</param>
        /// <returns>通道数据</returns>
        public double[] GetSingleChannelData(string sSourceFile, int iChannelNumber, long startPos, long endPos)
        {
            try
            {
                FileStream fs = new FileStream(sSourceFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fs, Encoding.Default);
                br.BaseStream.Position = 0;

                DataHeadInfo m_dhi = GetDataInfoHead(br.ReadBytes(DataOffset.DataHeadLength));
                dhi = m_dhi;

                List<DataChannelInfo> m_dcil = GetDataChannelInfoHead(sSourceFile);

                br.ReadBytes(DataOffset.DataChannelLength * m_dhi.iChannelNumber);
                br.ReadBytes(BitConverter.ToInt32(br.ReadBytes(DataOffset.ExtraLength), 0));
                int iChannelNumberSize = m_dhi.iChannelNumber * 2;
                byte[] b = new byte[iChannelNumberSize];

                br.BaseStream.Position = startPos;

                long iArray = (endPos - br.BaseStream.Position) / iChannelNumberSize;
                double[] fReturnArray = new double[iArray];
                for (int i = 0; i < iArray; i++)
                {
                    b = br.ReadBytes(iChannelNumberSize);
                    if (m_dhi.sDataVersion.StartsWith("3."))
                    {
                        b = ByteXORByte(b);
                    }

                    double fGL = (BitConverter.ToInt16(b, (iChannelNumber - 1) * 2) / m_dcil[iChannelNumber - 1].fScale + m_dcil[iChannelNumber - 1].fOffset);

                    fReturnArray[i] = fGL;
                }


                br.Close();
                fs.Close();

                return fReturnArray;

            }
            catch (Exception)
            {
                return new double[1];
            }
        }

        /// <summary>
        /// 根据二进制流和开始位置结束位置获取指定通道的数据
        /// </summary>
        /// <param name="br"></param>
        /// <param name="iChannelNumber">iChannelNumber从1开始计数</param>
        /// <param name="startPos">起始文件指针</param>
        /// <param name="endPos">结束文件指针</param>
        /// <returns>通道数据</returns>
        public double[] GetSingleChannelData(BinaryReader br, int iChannelNumber, long startPos, long endPos)
        {
            try
            {
                int iChannelNumberSize = dhi.iChannelNumber * 2;
                byte[] b = new byte[iChannelNumberSize];

                br.BaseStream.Position = startPos;

                long iArray = (endPos - br.BaseStream.Position) / iChannelNumberSize;
                double[] fReturnArray = new double[iArray];
                for (int i = 0; i < iArray; i++)
                {
                    b = br.ReadBytes(iChannelNumberSize);
                    if (dhi.sDataVersion.StartsWith("3."))
                    {
                        b = ByteXORByte(b);
                    }

                    double fGL = (BitConverter.ToInt16(b, (iChannelNumber - 1) * 2)
                                                                                     /

                                                                                     dciL[iChannelNumber - 1].fScale + dciL[iChannelNumber - 1].fOffset);

                    fReturnArray[i] = fGL;
                }
                return fReturnArray;

            }
            catch (Exception)
            {
                return new double[1];
            }
        }

        #endregion


        #region 获取指定通道名称的一段里程的通道数据

        public double[] GetSingleChannelData(string sSourceFile, string channelName, double startMile, double endMile)
        {
            try
            {
                int iChannelNumber = GetChannelId(channelName, sSourceFile);

                List<DataChannelInfo> m_dcil = new List<DataChannelInfo>();

                FileStream fs = new FileStream(sSourceFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fs, Encoding.Default);
                br.BaseStream.Position = 0;

                DataHeadInfo m_dhi = GetDataInfoHead(br.ReadBytes(DataOffset.DataHeadLength));
                dhi = m_dhi;

                m_dcil = GetDataChannelInfoHead(sSourceFile);

                br.ReadBytes(DataOffset.DataChannelLength * m_dhi.iChannelNumber);
                br.ReadBytes(BitConverter.ToInt32(br.ReadBytes(DataOffset.ExtraLength), 0));
                int iChannelNumberSize = m_dhi.iChannelNumber * 2;
                byte[] b = new byte[iChannelNumberSize];
                long iArray = (br.BaseStream.Length - br.BaseStream.Position) / iChannelNumberSize;
                double[] fReturnArray = new double[iArray];

                List<double> listValue = new List<double>();

                for (int i = 0; i < iArray; i++)
                {
                    b = br.ReadBytes(iChannelNumberSize);
                    if (m_dhi.sDataVersion.StartsWith("3."))
                    {
                        b = ByteXORByte(b);
                    }

                    short km = BitConverter.ToInt16(b, 0);

                    short m = BitConverter.ToInt16(b, 2);
                    float fGLMile = km + (float)m / m_dhi.iSmaleRate / 1000;//单位为公里

                    if (fGLMile > startMile && fGLMile < endMile)
                    {
                        double fGL = (BitConverter.ToInt16(b, (iChannelNumber - 1) * 2) / m_dcil[iChannelNumber - 1].fScale + m_dcil[iChannelNumber - 1].fOffset);

                        listValue.Add(fGL);
                    }

                    //if (Math.Abs(fGLMile - startMile) < tempStartMile)
                    //{
                    //    tempStartMile = Math.Abs(fGLMile - startMile);
                    //}
                    //else
                    //{
                    //    if (Math.Abs(fGLMile - endMile) < tempEndMile)
                    //    {
                    //        tempEndMile = Math.Abs(fGLMile - endMile);
                    //    }
                    //    else
                    //    {
                    //        break;
                    //    }
                    //    double fGL = (BitConverter.ToInt16(b, (iChannelNumber - 1) * 2) / m_dcil[iChannelNumber - 1].fScale + m_dcil[iChannelNumber - 1].fOffset);

                    //    listValue.Add(fGL);

                    //    fReturnArray[i] = fGL;
                    //}
                }

                br.Close();
                fs.Close();

                double[] result = listValue.ToArray();
                return result;

                //return fReturnArray;
            }
            catch (Exception)
            {
                return new double[1];
            }
        }

        #endregion


        #region 获取cit文件中的所有公里标

        /// <summary>
        /// 获取cit文件中的所有公里标---注意：是cit文件里的
        /// </summary>
        /// <param name="citFilePath">cit文件名</param>
        /// <returns>cit文件中的里程--单位为公里</returns>
        public double[] GetMilesData(String citFilePath)
        {
            double[] retVal = null;

            DataHeadInfo m_dhi = GetDataInfoHead(citFilePath);
            dhi = m_dhi;

            FileStream fs = new FileStream(citFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BinaryReader br = new BinaryReader(fs, Encoding.Default);
            br.BaseStream.Position = 0;
            br.ReadBytes(DataOffset.DataHeadLength);
            br.ReadBytes(DataOffset.DataChannelLength * m_dhi.iChannelNumber);
            br.ReadBytes(BitConverter.ToInt32(br.ReadBytes(DataOffset.ExtraLength), 0));
            int iChannelNumberSize = m_dhi.iChannelNumber * 2;
            byte[] b = new byte[iChannelNumberSize];
            long iArray = (br.BaseStream.Length - br.BaseStream.Position) / iChannelNumberSize;
            retVal = new double[iArray];
            for (int i = 0; i < iArray; i++)
            {
                b = br.ReadBytes(iChannelNumberSize);
                if (m_dhi.sDataVersion.StartsWith("3."))
                {
                    b = ByteXORByte(b);
                }

                short km = BitConverter.ToInt16(b, 0);

                short m = BitConverter.ToInt16(b, 2);
                float fGL = km + (float)m / m_dhi.iSmaleRate / 1000;//单位为公里

                retVal[i] = fGL;
            }

            br.Close();
            fs.Close();

            return retVal;
        }


        public double[] GetMilesData(String citFilePath, long startPos, long endPos)
        {
            double[] retVal = null;

            DataHeadInfo m_dhi = GetDataInfoHead(citFilePath);
            dhi = m_dhi;

            FileStream fs = new FileStream(citFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BinaryReader br = new BinaryReader(fs, Encoding.Default);
            br.BaseStream.Position = 0;
            br.ReadBytes(DataOffset.DataHeadLength);
            br.ReadBytes(DataOffset.DataChannelLength * m_dhi.iChannelNumber);
            br.ReadBytes(BitConverter.ToInt32(br.ReadBytes(DataOffset.ExtraLength), 0));
            int iChannelNumberSize = m_dhi.iChannelNumber * 2;
            byte[] b = new byte[iChannelNumberSize];

            br.BaseStream.Position = startPos;
            long iArray = (endPos - br.BaseStream.Position) / iChannelNumberSize;

            retVal = new double[iArray];
            for (int i = 0; i < iArray; i++)
            {
                b = br.ReadBytes(iChannelNumberSize);
                if (m_dhi.sDataVersion.StartsWith("3."))
                {
                    b = ByteXORByte(b);
                }

                short km = BitConverter.ToInt16(b, 0);

                short m = BitConverter.ToInt16(b, 2);
                float fGL = km + (float)m / m_dhi.iSmaleRate / 1000;//单位为公里

                retVal[i] = fGL;
            }


            br.Close();
            fs.Close();

            return retVal;
        }

        /// <summary>
        /// 根据指定的二进制流和开始位置、结束位置获取公里数据
        /// </summary>
        /// <param name="br"></param>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        /// <returns>公里数据</returns>
        public double[] GetMilesData(BinaryReader br, long startPos, long endPos)
        {
            double[] retVal = null;
            try
            {
                int iChannelNumberSize = dhi.iChannelNumber * 2;
                byte[] b = new byte[iChannelNumberSize];

                br.BaseStream.Position = startPos;

                long iArray = (endPos - startPos) / iChannelNumberSize;
                retVal = new double[iArray];
                for (int i = 0; i < iArray; i++)
                {
                    b = br.ReadBytes(iChannelNumberSize);
                    if (dhi.sDataVersion.StartsWith("3."))
                    {
                        b = ByteXORByte(b);
                    }

                    short km = BitConverter.ToInt16(b, 0);
                    short m = BitConverter.ToInt16(b, 2);

                    //单位为公里
                    float fGL = km + (float)m / 1000;

                    retVal[i] = fGL;
                }

            }
            catch (Exception)
            {
                return new double[1];
            }

            return retVal;
        }

        public double[] GetMilesData(String citFilePath, double startMile, double endMile)
        {
            double[] retVal = null;

            DataHeadInfo m_dhi = GetDataInfoHead(citFilePath);
            dhi = m_dhi;

            FileStream fs = new FileStream(citFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BinaryReader br = new BinaryReader(fs, Encoding.Default);
            br.BaseStream.Position = 0;
            br.ReadBytes(DataOffset.DataHeadLength);
            br.ReadBytes(DataOffset.DataChannelLength * m_dhi.iChannelNumber);
            br.ReadBytes(BitConverter.ToInt32(br.ReadBytes(DataOffset.ExtraLength), 0));
            int iChannelNumberSize = m_dhi.iChannelNumber * 2;
            byte[] b = new byte[iChannelNumberSize];
            long iArray = (br.BaseStream.Length - br.BaseStream.Position) / iChannelNumberSize;
            retVal = new double[iArray];

            List<double> listValue = new List<double>();

            for (int i = 0; i < iArray; i++)
            {
                b = br.ReadBytes(iChannelNumberSize);
                if (m_dhi.sDataVersion.StartsWith("3."))
                {
                    b = ByteXORByte(b);
                }

                short km = BitConverter.ToInt16(b, 0);

                short m = BitConverter.ToInt16(b, 2);
                float fGL = km + (float)m / m_dhi.iSmaleRate / 1000;//单位为公里

                if (fGL > startMile && fGL < endMile)
                {
                    listValue.Add(fGL);
                }

                retVal[i] = fGL;
            }

            br.Close();
            fs.Close();

            double[] result = listValue.ToArray();
            return result;

        }

        #endregion

        #endregion

        #region 针对通道数据的解密算法
        /// <summary>
        /// 针对通道数据的解密算法
        /// </summary>
        /// <param name="b">通道原数据</param>
        /// <returns>解密之后的通道数据</returns>
        public static byte[] ByteXORByte(byte[] b)
        {
            for (int iIndex = 0; iIndex < b.Length; iIndex++)
            {
                b[iIndex] = (byte)(b[iIndex] ^ 128);
            }
            return b;
        }
        #endregion


        #region 写操作

        public Byte[] GetBytesFromDataHeadInfo(DataHeadInfo structCITHead)
        {
            List<Byte> byteList = new List<Byte>();

            MemoryStream mStream = new MemoryStream();

            BinaryWriter bw = new BinaryWriter(mStream, Encoding.UTF8);

            //文件类型---1
            bw.Write(structCITHead.iDataType);
            //文件版本号----2
            Byte[] tmpBytes = UnicodeEncoding.Default.GetBytes(structCITHead.sDataVersion);
            bw.Write((Byte)(tmpBytes.Length));
            bw.Write(tmpBytes);
            if (tmpBytes.Length < 20)
            {
                for (int i = 0; i < (20 - tmpBytes.Length); i++)
                {
                    bw.Write((byte)0);
                }
            }

            //线路代码----3
            tmpBytes = UnicodeEncoding.Default.GetBytes(structCITHead.sTrackCode);
            bw.Write((Byte)(tmpBytes.Length));
            bw.Write(tmpBytes);
            if (tmpBytes.Length < 4)
            {
                for (int i = 0; i < (4 - tmpBytes.Length); i++)
                {
                    bw.Write((byte)0);
                }
            }
            //线路名，英文最好----4
            tmpBytes = UnicodeEncoding.Default.GetBytes(structCITHead.sTrackName);
            bw.Write((Byte)(tmpBytes.Length));
            bw.Write(tmpBytes);
            if (tmpBytes.Length < 20)
            {
                for (int i = 0; i < (20 - tmpBytes.Length); i++)
                {
                    bw.Write((byte)0);
                }
            }
            //行别：1-上，2-下，3-单线----5
            bw.Write(structCITHead.iDir);
            //检测车号---6
            tmpBytes = UnicodeEncoding.Default.GetBytes(structCITHead.sTrain);
            bw.Write((Byte)(tmpBytes.Length));
            bw.Write(tmpBytes);
            for (int i = 0; i < 20 - tmpBytes.Length; i++)
            {
                bw.Write((byte)0);
            }
            //检测日期：yyyy-MM-dd-----7
            tmpBytes = UnicodeEncoding.Default.GetBytes(structCITHead.sDate);
            bw.Write((Byte)(tmpBytes.Length));
            bw.Write(tmpBytes);
            for (int i = 0; i < 10 - tmpBytes.Length; i++)
            {
                bw.Write((byte)0);
            }
            //检测起始时间：HH:mm:ss----8
            tmpBytes = UnicodeEncoding.Default.GetBytes(structCITHead.sTime);
            bw.Write((Byte)(tmpBytes.Length));
            bw.Write(tmpBytes);
            for (int i = 0; i < 8 - tmpBytes.Length; i++)
            {
                bw.Write((byte)0);
            }
            //检测方向，正0，反1-----9
            bw.Write(structCITHead.iRunDir);
            //增里程0，减里程1-----10
            bw.Write(structCITHead.iKmInc);
            //开始里程-----11
            bw.Write(structCITHead.fkmFrom);
            //结束里程，检测结束后更新----12
            bw.Write(structCITHead.fkmTo);
            //采样数，（距离采样>0, 时间采样<0）----13
            bw.Write(structCITHead.iSmaleRate);
            //数据块中通道总数----14
            bw.Write(structCITHead.iChannelNumber);

            bw.Flush();
            bw.Close();

            byte[] tmp = mStream.ToArray();

            mStream.Flush();
            mStream.Close();

            byteList.AddRange(tmp);

            return byteList.ToArray();
        }

        public Byte[] GetBytesFromChannelDataInfoList(List<DataChannelInfo> m_dciL)
        {
            List<Byte> byteList = new List<Byte>();

            if (m_dciL == null || m_dciL.Count == 0)
            {
                //throw new Exception("通道结构体为空");
                return byteList.ToArray();
            }

            MemoryStream mStream = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(mStream, Encoding.UTF8);

            foreach (DataChannelInfo m_dci in m_dciL)
            {
                //1----轨检通道从1～1000定义,动力学从1001~2000,弓网从2001~3000
                bw.Write(m_dci.sID);
                //2----通道英文名，不足补空格
                Byte[] tmpBytes = UnicodeEncoding.Default.GetBytes(m_dci.sNameEn);
                bw.Write((Byte)(tmpBytes.Length));
                bw.Write(tmpBytes);
                if (tmpBytes.Length < 20)
                {
                    for (int i = 0; i < (20 - tmpBytes.Length); i++)
                    {
                        bw.Write((byte)0);
                    }
                }
                //3----通道中文名，不足补空格
                tmpBytes = UnicodeEncoding.Default.GetBytes(m_dci.sNameCh);
                bw.Write((Byte)(tmpBytes.Length));
                bw.Write(tmpBytes);
                if (tmpBytes.Length < 20)
                {
                    for (int i = 0; i < (20 - tmpBytes.Length); i++)
                    {
                        bw.Write((byte)0);
                    }
                }
                //4----通道比例
                bw.Write(m_dci.fScale);
                //5----通道基线值
                bw.Write(m_dci.fOffset);
                //6----通道单位
                tmpBytes = UnicodeEncoding.Default.GetBytes(m_dci.sUnit);
                bw.Write((Byte)(tmpBytes.Length));
                bw.Write(tmpBytes);
                if (tmpBytes.Length < 10)
                {
                    for (int i = 0; i < (10 - tmpBytes.Length); i++)
                    {
                        bw.Write((byte)0);
                    }
                }

            }

            bw.Flush();
            bw.Close();

            byte[] tmp = mStream.ToArray();

            mStream.Flush();
            mStream.Close();

            byteList.AddRange(tmp);


            return byteList.ToArray();
        }


        public bool WriteCitFileHeadInfo(string citFileName, DataHeadInfo dataHeadInfo, List<DataChannelInfo> channelList)
        {
            try
            {
                FileStream fsWrite = new FileStream(citFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8);
                bw.Write(GetBytesFromDataHeadInfo(dataHeadInfo));
                bw.Write(GetBytesFromChannelDataInfoList(channelList));
                bw.Close();
                fsWrite.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 向文件中写入头文件的文件信息
        /// </summary>
        /// <param name="citFileName"></param>
        /// <param name="dataHeadInfo"></param>
        /// <returns></returns>
        public bool WriteDataInfoHead(string citFileName, DataHeadInfo dataHeadInfo)
        {
            try
            {
                using (FileStream fsWrite = new FileStream(citFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8))
                    {
                        bw.Write(GetBytesFromDataHeadInfo(dataHeadInfo));
                        bw.Close();
                    }
                    fsWrite.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 向文件中写入头文件的通道定义信息
        /// </summary>
        /// <param name="citFileName"></param>
        /// <param name="channelList"></param>
        /// <returns></returns>
        public bool WriteDataChannelInfoHead(string citFileName, List<DataChannelInfo> channelList)
        {
            try
            {
                using (FileStream fsWrite = new FileStream(citFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8))
                    {
                        bw.BaseStream.Position = DataOffset.DataHeadLength;
                        bw.Write(GetBytesFromChannelDataInfoList(channelList));
                        bw.Close();
                    }
                    fsWrite.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 向文件中写入头文件的补充信息
        /// </summary>
        /// <param name="citFileName"></param>
        /// <param name="extraInfo"></param>
        /// <returns></returns>
        public bool WriteDataExtraInfo(string citFileName, string extraInfo)
        {
            try
            {
                using (FileStream fs = new FileStream(citFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs, Encoding.Default))
                    {
                        DataHeadInfo dhi = GetDataInfoHead(citFileName);
                        bw.BaseStream.Position = DataOffset.DataHeadLength + dhi.iChannelNumber * DataOffset.DataChannelLength;

                        byte[] tmpBytes = UnicodeEncoding.Default.GetBytes(extraInfo);
                        //bw.Write((Byte)(tmpBytes.Length));
                        bw.Write(tmpBytes);
                        if (tmpBytes.Length < 4)
                        {
                            for (int i = 0; i < (4 - tmpBytes.Length); i++)
                            {
                                bw.Write((byte)0);
                            }
                        }
                        bw.Close();
                    }
                    fs.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 向文件中写入通道数据
        /// </summary>
        /// <param name="citFileName"></param>
        /// <param name="channelData"></param>
        /// <returns></returns>
        public bool WriteChannelData(string citFileName, List<double[]> channelData)
        {
            try
            {
                using (FileStream fs = new FileStream(citFileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs, Encoding.Default))
                    {
                        DataHeadInfo dhi = GetDataInfoHead(citFileName);
                        List<DataChannelInfo> channelList = GetDataChannelInfoHead(citFileName);
                        int iChannelNumberSize = dhi.iChannelNumber * 2;
                        byte[] dataArray = new byte[iChannelNumberSize];

                        List<Byte> dataList = new List<Byte>();
                        short tmpRmsData = 0;
                        Byte[] tmpBytes = new Byte[2];

                        long iArrayLen = channelData[0].Length;
                        for (int k = 0; k < iArrayLen; k++)
                        {
                            if (IsEncrypt(dhi))
                            {
                                for (int iTmp = 0; iTmp < channelList.Count; iTmp++)
                                {
                                    tmpRmsData = (short)((channelData[iTmp][k] - channelList[iTmp].fOffset) * channelList[iTmp].fScale);
                                    tmpBytes = ByteXORByte(BitConverter.GetBytes(tmpRmsData));
                                    dataList.AddRange(tmpBytes);
                                }
                            }
                            else
                            {
                                for (int iTmp = 0; iTmp < channelList.Count; iTmp++)
                                {
                                    //tmpRmsData = (short)((channelData[iTmp][k] - channelList[iTmp].fOffset) * channelList[iTmp].fScale);
                                    tmpRmsData = (short)((double.Parse(channelData[iTmp][k].ToString("0.00")) - channelList[iTmp].fOffset) * channelList[iTmp].fScale);
                                    dataList.AddRange(BitConverter.GetBytes(tmpRmsData));
                                }
                            }
                            bw.Write(dataList.ToArray());
                            bw.Flush();
                            bw.Flush();

                            Application.DoEvents();

                            dataList.Clear();
                        }
                        bw.Close();
                    }
                    fs.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /*
        /// <summary>
        /// 向文件中写入通道数据
        /// </summary>
        /// <param name="citFileName"></param>
        /// <param name="channelData"></param>
        /// <returns></returns>
        public bool WriteChannelDataFloat(string citFileName, List<float[]> channelData)
        {
            try
            {
                using (FileStream fs = new FileStream(citFileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs, Encoding.Default))
                    {
                        DataHeadInfo dhi = GetDataInfoHead(citFileName);
                        List<DataChannelInfo> channelList = GetDataChannelInfoHead(citFileName);
                        int iChannelNumberSize = dhi.iChannelNumber * 2;
                        byte[] dataArray = new byte[iChannelNumberSize];

                        List<Byte> dataList = new List<Byte>();
                        short tmpRmsData = 0; ;
                        
                        Byte[] tmpBytes = new Byte[2];

                        long iArrayLen = channelData[0].Length;
                        for (int k = 0; k < iArrayLen; k++)
                        {
                            if (IsEncrypt(dhi))
                            {
                                for (int iTmp = 0; iTmp < channelList.Count; iTmp++)
                                {
                                    tmpRmsData = (short)((channelData[iTmp][k] - channelList[iTmp].fOffset) * channelList[iTmp].fScale);
                                    tmpBytes = ByteXORByte(BitConverter.GetBytes(tmpRmsData));
                                    dataList.AddRange(tmpBytes);
                                }
                            }
                            else
                            {
                                for (int iTmp = 0; iTmp < channelList.Count; iTmp++)
                                {
                                    try
                                    {
                                        tmpRmsData = (short)((channelData[iTmp][k] - channelList[iTmp].fOffset) * channelList[iTmp].fScale);
                                        dataList.AddRange(BitConverter.GetBytes(tmpRmsData));
                                    }
                                    catch (Exception ex)
                                    {
                                        throw ex;
                                    }
                                }
                            }
                            bw.Write(dataList.ToArray());
                            bw.Flush();

                            dataList.Clear();
                        }
                        bw.Close();
                    }
                    fs.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        */

        /// <summary>
        /// 向文件中写入通道数据
        /// </summary>
        /// <param name="citFileName"></param>
        /// <param name="channelData"></param>
        /// <returns></returns>
        public bool WriteChannelDataFloat(string citFileName, List<float[]> channelData)
        {
            try
            {
                using (FileStream fs = new FileStream(citFileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs, Encoding.Default))
                    {
                        DataHeadInfo dhi = GetDataInfoHead(citFileName);
                        List<DataChannelInfo> channelList = GetDataChannelInfoHead(citFileName);
                        int iChannelNumberSize = dhi.iChannelNumber * 2;
                        byte[] dataArray = new byte[iChannelNumberSize];

                        List<Byte> dataList = new List<Byte>();
                        short tmpRmsData = 0; ;

                        Byte[] tmpBytes = new Byte[2];

                        long iArrayLen = channelData[0].Length;
                        for (int k = 0; k < iArrayLen; k++)
                        {
                            if (IsEncrypt(dhi))
                            {
                                for (int iTmp = 0; iTmp < channelList.Count; iTmp++)
                                {
                                    tmpRmsData = (short)((channelData[iTmp][k] - channelList[iTmp].fOffset) * channelList[iTmp].fScale);
                                    

                                    tmpBytes = ByteXORByte(BitConverter.GetBytes(tmpRmsData));
                                    dataList.AddRange(tmpBytes);
                                }
                            }
                            else
                            {
                                for (int iTmp = 0; iTmp < channelList.Count; iTmp++)
                                {
                                    try
                                    {

                                        double value = double.Parse(((double.Parse(channelData[iTmp][k].ToString("0.00")) - channelList[iTmp].fOffset) * channelList[iTmp].fScale).ToString("0.00"));
                                        tmpRmsData = (short)value;

                                        //tmpRmsData = (short)((double.Parse(channelData[iTmp][k].ToString("0.00")) - channelList[iTmp].fOffset) * channelList[iTmp].fScale);

                                        //tmpRmsData = (short)((channelData[iTmp][k] - channelList[iTmp].fOffset) * channelList[iTmp].fScale);
                                        //double temp = ((channelData[iTmp][k] - channelList[iTmp].fOffset) * channelList[iTmp].fScale);

                                        dataList.AddRange(BitConverter.GetBytes(tmpRmsData));
                                        
                                    }
                                    catch (Exception ex)
                                    {
                                        throw ex;
                                    }
                                }
                            }
                            ///bw.BaseStream.Position = 100;
                            bw.Write(dataList.ToArray());
                            bw.Flush();

                            dataList.Clear();
                        }
                        long postin = bw.BaseStream.Position;
                        bw.Close();
                    }
                    fs.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 写整个cit文件
        /// </summary>
        /// <param name="citFileName"></param>
        /// <param name="dataHeadInfo"></param>
        /// <param name="channelList"></param>
        /// <param name="extraInfo"></param>
        /// <param name="channelData"></param>
        /// <returns></returns>
        public bool WriteCitFile(string citFileName, DataHeadInfo dataHeadInfo, List<DataChannelInfo> channelList, string extraInfo, List<byte[]> channelData)
        {
            try
            {
                using (FileStream fsWrite = new FileStream(citFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8))
                    {
                        //写文件头文件信息
                        bw.Write(GetBytesFromDataHeadInfo(dataHeadInfo));
                        //写文件头通道定义
                        bw.Write(GetBytesFromChannelDataInfoList(channelList));
                        //写文件头补充信息
                        byte[] tmpBytes = UnicodeEncoding.Default.GetBytes(extraInfo);
                        bw.Write((Byte)(tmpBytes.Length));
                        bw.Write(tmpBytes);
                        if (tmpBytes.Length < 4)
                        {
                            for (int i = 0; i < (4 - tmpBytes.Length); i++)
                            {
                                bw.Write((byte)0);
                            }
                        }

                        //写通道数据
                        for (int i = 0; i < channelData.Count; i++)
                        {
                            bw.Write(channelData[i]);
                        }

                        bw.Close();
                    }
                    fsWrite.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool WriteCitFile(string citFileName, string newCitFileName, List<double[]> arrayDone)
        {
            try
            {
                using (FileStream fs = new FileStream(citFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                    {
                        using (FileStream fsNewFile = new FileStream(newCitFileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                        {
                            using (BinaryWriter bwNewFile = new BinaryWriter(fsNewFile, Encoding.Default))
                            {
                                DataHeadInfo dhi = GetDataInfoHead(citFileName);
                                List<DataChannelInfo> channelList = GetDataChannelInfoHead(citFileName);
                                br.BaseStream.Position = 0;
                                bwNewFile.Write(br.ReadBytes(DataOffset.DataHeadLength));
                                bwNewFile.Write(br.ReadBytes(DataOffset.DataChannelLength * dhi.iChannelNumber));

                                byte[] last4 = br.ReadBytes(DataOffset.ExtraLength);
                                byte[] zh = br.ReadBytes(BitConverter.ToInt32(last4, 0));
                                bwNewFile.Write(last4);
                                if (zh.Length != 0)
                                {
                                    bwNewFile.Write(zh);
                                }
                                int iChannelNumberSize = dhi.iChannelNumber * 2;
                                byte[] dataArray = new byte[iChannelNumberSize];

                                List<Byte> dataList = new List<Byte>();
                                short tmpRmsData = 0;
                                Byte[] tmpBytes = new Byte[2];

                                long iArrayLen = arrayDone[0].Length;
                                for (int k = 0; k < iArrayLen; k++)
                                {
                                    if (IsEncrypt(dhi))
                                    {
                                        for (int iTmp = 0; iTmp < channelList.Count; iTmp++)
                                        {
                                            tmpRmsData = (short)((arrayDone[iTmp][k] - channelList[iTmp].fOffset) * channelList[iTmp].fScale);
                                            tmpBytes = ByteXORByte(BitConverter.GetBytes(tmpRmsData));
                                            dataList.AddRange(tmpBytes);
                                        }
                                    }
                                    else
                                    {
                                        for (int iTmp = 0; iTmp < channelList.Count; iTmp++)
                                        {
                                            tmpRmsData = (short)((arrayDone[iTmp][k] - channelList[iTmp].fOffset) * channelList[iTmp].fScale);
                                            dataList.AddRange(BitConverter.GetBytes(tmpRmsData));
                                        }
                                    }
                                    bwNewFile.Write(dataList.ToArray());
                                    bwNewFile.Flush();
                                    bwNewFile.Flush();

                                    Application.DoEvents();

                                    dataList.Clear();
                                }
                                bwNewFile.Close();
                            }
                            fsNewFile.Close();
                        }
                        br.Close();
                    }
                    fs.Close();

                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region 文件头文件信息的指定字段修改

        public bool WriteHeadDataType(string citFileName, int value)
        {
            try
            {
                FileStream fsWrite = new FileStream(citFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8);
                DataHeadInfo dataHeadInfo = GetDataInfoHead(citFileName);
                dataHeadInfo.iDataType = value;
                bw.Write(GetBytesFromDataHeadInfo(dataHeadInfo));
                bw.Close();
                fsWrite.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool WriteHeadDataVersion(string citFileName, string value)
        {
            try
            {
                FileStream fsWrite = new FileStream(citFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8);
                DataHeadInfo dataHeadInfo = GetDataInfoHead(citFileName);
                dataHeadInfo.sDataVersion = value;
                bw.Write(GetBytesFromDataHeadInfo(dataHeadInfo));
                bw.Close();
                fsWrite.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool WriteHeadTrackCode(string citFileName, string value)
        {
            try
            {
                FileStream fsWrite = new FileStream(citFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8);
                DataHeadInfo dataHeadInfo = GetDataInfoHead(citFileName);
                dataHeadInfo.sTrackCode = value;
                bw.Write(GetBytesFromDataHeadInfo(dataHeadInfo));
                bw.Close();
                fsWrite.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool WriteHeadTrackName(string citFileName, string value)
        {
            try
            {
                FileStream fsWrite = new FileStream(citFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8);
                DataHeadInfo dataHeadInfo = GetDataInfoHead(citFileName);
                dataHeadInfo.sTrackName = value;
                bw.Write(GetBytesFromDataHeadInfo(dataHeadInfo));
                bw.Close();
                fsWrite.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool WriteHeadDir(string citFileName, int value)
        {
            try
            {
                FileStream fsWrite = new FileStream(citFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8);
                DataHeadInfo dataHeadInfo = GetDataInfoHead(citFileName);
                dataHeadInfo.iDir = value;
                bw.Write(GetBytesFromDataHeadInfo(dataHeadInfo));
                bw.Close();
                fsWrite.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool WriteHeadTrain(string citFileName, string value)
        {
            try
            {
                FileStream fsWrite = new FileStream(citFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8);
                DataHeadInfo dataHeadInfo = GetDataInfoHead(citFileName);
                dataHeadInfo.sTrain = value;
                bw.Write(GetBytesFromDataHeadInfo(dataHeadInfo));
                bw.Close();
                fsWrite.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool WriteHeadDate(string citFileName, string value)
        {
            try
            {
                FileStream fsWrite = new FileStream(citFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8);
                DataHeadInfo dataHeadInfo = GetDataInfoHead(citFileName);
                dataHeadInfo.sDate = value;
                bw.Write(GetBytesFromDataHeadInfo(dataHeadInfo));
                bw.Close();
                fsWrite.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool WriteHeadTime(string citFileName, string value)
        {
            try
            {
                FileStream fsWrite = new FileStream(citFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8);
                DataHeadInfo dataHeadInfo = GetDataInfoHead(citFileName);
                dataHeadInfo.sTime = value;
                bw.Write(GetBytesFromDataHeadInfo(dataHeadInfo));
                bw.Close();
                fsWrite.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool WriteHeadRunDir(string citFileName, int value)
        {
            try
            {
                FileStream fsWrite = new FileStream(citFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8);
                DataHeadInfo dataHeadInfo = GetDataInfoHead(citFileName);
                dataHeadInfo.iRunDir = value;
                bw.Write(GetBytesFromDataHeadInfo(dataHeadInfo));
                bw.Close();
                fsWrite.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool WriteHeadKmInc(string citFileName, int value)
        {
            try
            {
                FileStream fsWrite = new FileStream(citFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8);
                DataHeadInfo dataHeadInfo = GetDataInfoHead(citFileName);
                dataHeadInfo.iKmInc = value;
                bw.Write(GetBytesFromDataHeadInfo(dataHeadInfo));
                bw.Close();
                fsWrite.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool WriteHeadKmFrom(string citFileName, float value)
        {
            try
            {
                FileStream fsWrite = new FileStream(citFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8);
                DataHeadInfo dataHeadInfo = GetDataInfoHead(citFileName);
                dataHeadInfo.fkmFrom = value;
                bw.Write(GetBytesFromDataHeadInfo(dataHeadInfo));
                bw.Close();
                fsWrite.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool WriteHeadKmTo(string citFileName, float value)
        {
            try
            {
                FileStream fsWrite = new FileStream(citFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8);
                DataHeadInfo dataHeadInfo = GetDataInfoHead(citFileName);
                dataHeadInfo.fkmTo = value;
                bw.Write(GetBytesFromDataHeadInfo(dataHeadInfo));
                bw.Close();
                fsWrite.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool WriteHeadSmaleRate(string citFileName, int value)
        {
            try
            {
                FileStream fsWrite = new FileStream(citFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                BinaryWriter bw = new BinaryWriter(fsWrite, Encoding.UTF8);
                DataHeadInfo dataHeadInfo = GetDataInfoHead(citFileName);
                dataHeadInfo.iSmaleRate = value;
                bw.Write(GetBytesFromDataHeadInfo(dataHeadInfo));
                bw.Close();
                fsWrite.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #endregion


        #region 私有方法


        /// <summary>
        /// 读取cit文件头中的文件信息信息，并返回文件头信息结构体
        /// </summary>
        /// <param name="bDataInfo">文件头中包含文件信息的120个字节 </param>
        /// <returns>文件信息结构体</returns>
        private DataHeadInfo GetDataInfoHead(byte[] bDataInfo)
        {
            DataHeadInfo dhi = new DataHeadInfo();
            StringBuilder sbDataVersion = new StringBuilder();
            StringBuilder sbTrackCode = new StringBuilder();
            StringBuilder sbTrackName = new StringBuilder();
            StringBuilder sbTrain = new StringBuilder();
            StringBuilder sbDate = new StringBuilder();
            StringBuilder sbTime = new StringBuilder();

            //数据类型
            dhi.iDataType = BitConverter.ToInt32(bDataInfo, 0); //iDataType：1轨检、2动力学、3弓网，

            //1+20个字节，数据版本
            for (int i = 1; i <= (int)bDataInfo[DataHeadOffset.DataVersion]; i++)
            {
                sbDataVersion.Append(UnicodeEncoding.Default.GetString(bDataInfo, DataHeadOffset.DataVersion + i, 1));
            }
            //1+4个字节，线路代码
            for (int i = 1; i <= (int)bDataInfo[DataHeadOffset.TrackCode]; i++)
            {
                sbTrackCode.Append(UnicodeEncoding.Default.GetString(bDataInfo, DataHeadOffset.TrackCode + i, 1));
            }
            //1+20个字节，线路名
            for (int i = 1; i <= (int)bDataInfo[DataHeadOffset.TrackName]; i+=2)
            {
                sbTrackName.Append(UnicodeEncoding.Default.GetString(bDataInfo, DataHeadOffset.TrackName + i, 2));
            }

            //检测方向
            dhi.iDir = BitConverter.ToInt32(bDataInfo, DataHeadOffset.Dir);

            //1+20个字节，检测车号
            for (int i = 1; i <= (int)bDataInfo[DataHeadOffset.TrainCode]; i++)
            {
                sbTrain.Append(UnicodeEncoding.Default.GetString(bDataInfo, DataHeadOffset.TrainCode + i, 1));
            }
            //1+10个字节，检测日期
            for (int i = 1; i <= (int)bDataInfo[DataHeadOffset.Date]; i++)
            {
                sbDate.Append(UnicodeEncoding.Default.GetString(bDataInfo, DataHeadOffset.Date + i, 1));
            }
            //1+8个字节，检测时间
            for (int i = 1; i <= (int)bDataInfo[DataHeadOffset.Time]; i++)
            {
                sbTime.Append(UnicodeEncoding.Default.GetString(bDataInfo, DataHeadOffset.Time + i, 1));
            }

            dhi.iRunDir = BitConverter.ToInt32(bDataInfo, DataHeadOffset.RunDir);
            dhi.iKmInc = BitConverter.ToInt32(bDataInfo, DataHeadOffset.KmInc);
            dhi.fkmFrom = BitConverter.ToSingle(bDataInfo, DataHeadOffset.KmFrom);
            dhi.fkmTo = BitConverter.ToSingle(bDataInfo, DataHeadOffset.KmTo);
            dhi.iSmaleRate = BitConverter.ToInt32(bDataInfo, DataHeadOffset.SmaleRate);
            //dhi.iSmaleRate = 4;
            dhi.iChannelNumber = BitConverter.ToInt32(bDataInfo, DataHeadOffset.ChannelNumber);
            dhi.sDataVersion = sbDataVersion.ToString();
            dhi.sDate = DateTime.Parse(sbDate.ToString()).ToString("yyyy-MM-dd");
            dhi.sTime = DateTime.Parse(sbTime.ToString()).ToString("HH:mm:ss");
            dhi.sTrackCode = sbTrackCode.ToString();
            dhi.sTrackName = sbTrackName.ToString();
            dhi.sTrain = sbTrain.ToString();

            return dhi;
        }


        /// <summary>
        /// 读取cit文件头中的文件信息信息，并返回文件头信息结构体 (线路名为读取字节数为2
        /// </summary>
        /// <param name="bDataInfo"></param>
        /// <returns></returns>
        private DataHeadInfo GetDataInfoHead2(byte[] bDataInfo)
        {
            DataHeadInfo dhi = new DataHeadInfo();
            StringBuilder sbDataVersion = new StringBuilder();
            StringBuilder sbTrackCode = new StringBuilder();
            StringBuilder sbTrackName = new StringBuilder();
            StringBuilder sbTrain = new StringBuilder();
            StringBuilder sbDate = new StringBuilder();
            StringBuilder sbTime = new StringBuilder();

            //数据类型
            dhi.iDataType = BitConverter.ToInt32(bDataInfo, 0); //iDataType：1轨检、2动力学、3弓网，

            //1+20个字节，数据版本
            for (int i = 1; i <= (int)bDataInfo[DataHeadOffset.DataVersion]; i++)
            {
                sbDataVersion.Append(UnicodeEncoding.Default.GetString(bDataInfo, DataHeadOffset.DataVersion + i, 1));
            }
            //1+4个字节，线路代码
            for (int i = 1; i <= (int)bDataInfo[DataHeadOffset.TrackCode]; i++)
            {
                sbTrackCode.Append(UnicodeEncoding.Default.GetString(bDataInfo, DataHeadOffset.TrackCode + i, 1));
            }
            //1+20个字节，线路名
            for (int i = 1; i <= (int)bDataInfo[DataHeadOffset.TrackName]; i++, i++)
            {
                sbTrackName.Append(UnicodeEncoding.Default.GetString(bDataInfo, DataHeadOffset.TrackName + i, 2));
            }

            //检测方向
            dhi.iDir = BitConverter.ToInt32(bDataInfo, DataHeadOffset.Dir);

            //1+20个字节，检测车号
            for (int i = 1; i <= (int)bDataInfo[DataHeadOffset.TrainCode]; i++)
            {
                sbTrain.Append(UnicodeEncoding.Default.GetString(bDataInfo, DataHeadOffset.TrainCode + i, 1));
            }
            //1+10个字节，检测日期
            for (int i = 1; i <= (int)bDataInfo[DataHeadOffset.Date]; i++)
            {
                sbDate.Append(UnicodeEncoding.Default.GetString(bDataInfo, DataHeadOffset.Date + i, 1));
            }
            //1+8个字节，检测时间
            for (int i = 1; i <= (int)bDataInfo[DataHeadOffset.Time]; i++)
            {
                sbTime.Append(UnicodeEncoding.Default.GetString(bDataInfo, DataHeadOffset.Time + i, 1));
            }

            dhi.iRunDir = BitConverter.ToInt32(bDataInfo, DataHeadOffset.RunDir);
            dhi.iKmInc = BitConverter.ToInt32(bDataInfo, DataHeadOffset.KmInc);
            dhi.fkmFrom = BitConverter.ToSingle(bDataInfo, DataHeadOffset.KmFrom);
            dhi.fkmTo = BitConverter.ToSingle(bDataInfo, DataHeadOffset.KmTo);
            dhi.iSmaleRate = BitConverter.ToInt32(bDataInfo, DataHeadOffset.SmaleRate);
            //dhi.iSmaleRate = 4;
            dhi.iChannelNumber = BitConverter.ToInt32(bDataInfo, DataHeadOffset.ChannelNumber);
            dhi.sDataVersion = sbDataVersion.ToString();
            dhi.sDate = DateTime.Parse(sbDate.ToString()).ToString("yyyy-MM-dd");
            dhi.sTime = DateTime.Parse(sbTime.ToString()).ToString("HH:mm:ss");
            dhi.sTrackCode = sbTrackCode.ToString();
            dhi.sTrackName = sbTrackName.ToString();
            dhi.sTrain = sbTrain.ToString();

            return dhi;
        }


        /// <summary>
        /// 获取单个通道定义信息
        /// </summary>
        /// <param name="bDataInfo">包含通道定义信息的字节数组</param>
        /// <param name="start">起始下标</param>
        /// <returns>通道定义信息结构体对象</returns>
        private DataChannelInfo GetChannelInfo(byte[] bDataInfo, int start)
        {
            DataChannelInfo dci = new DataChannelInfo();
            StringBuilder sUnit = new StringBuilder();

            dci.sID = BitConverter.ToInt32(bDataInfo, start);//通道起点为0，导致通道id取的都是第一个通道的id，把0改为start，
            //1+20   通道英文名
            dci.sNameEn = UnicodeEncoding.Default.GetString(bDataInfo, DataChannelOffset.NameEn + 1 + start, (int)bDataInfo[DataChannelOffset.NameEn + start]);
            //1+20    通道中文名
            dci.sNameCh = UnicodeEncoding.Default.GetString(bDataInfo, DataChannelOffset.NameCh + 1 + start, (int)bDataInfo[DataChannelOffset.NameCh + start]);
            //通道单位 1+10
            for (int i = 1; i <= (int)bDataInfo[DataChannelOffset.Unit + start]; i++)
            {
                sUnit.Append(UnicodeEncoding.Default.GetString(bDataInfo, DataChannelOffset.Unit + i + start, 1));
            }
            dci.sUnit = sUnit.ToString();

            //4  通道比例
            dci.fScale = BitConverter.ToSingle(bDataInfo, DataChannelOffset.Scale + start);
            //4   通道基线值
            dci.fOffset = BitConverter.ToSingle(bDataInfo, DataChannelOffset.Offset + start);

            return dci;
        }



        private void WriteCitFileTemp(string citFileName, DataHeadInfo dataHeadInfo, List<DataChannelInfo> channelList)
        {
            try
            {
                FileStream fsRead = new FileStream(citFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                FileStream fsWrite = new FileStream(citFileName + ".bak", FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fsRead, Encoding.UTF8);
                BinaryWriter bw1 = new BinaryWriter(fsWrite, Encoding.UTF8);
                byte[] bHead = br.ReadBytes(120);
                byte[] bChannels = br.ReadBytes(65 * dataHeadInfo.iChannelNumber);
                byte[] bData = new byte[dataHeadInfo.iChannelNumber * 2];
                byte[] bDataNew = new byte[dataHeadInfo.iChannelNumber * 2];
                byte[] bTail = br.ReadBytes(BitConverter.ToInt32(br.ReadBytes(4), 0));

                //bw1.Write(bHead);

                bw1.Write(GetBytesFromDataHeadInfo(dataHeadInfo));//文件头
                ////反向--转换为正向
                //if (m_dhi.iRunDir == 0)
                //{
                //    bw1.Write(bChannels);
                //}
                //else
                //{
                //    bw1.Write(GetBytesFromChannelDataInfoList(channelList));
                //}
                bw1.Write(GetBytesFromChannelDataInfoList(channelList));

                bw1.Write(bTail.Length);
                bw1.Write(bTail);

                long startPos = br.BaseStream.Position;//记录数据开始位置的文件指针

                while (br.BaseStream.Position < br.BaseStream.Length)
                {
                    bw1.Write(br.ReadBytes(dataHeadInfo.iChannelNumber * 2));
                    //br.BaseStream.Position += m_dhi.iChannelNumber * 2;
                }

                //
                bw1.Close();
                br.Close();
                fsWrite.Close();
                fsRead.Close();
                //删除bak
                Application.DoEvents();
                File.Delete(citFileName);
                Application.DoEvents();
                File.Move(citFileName + ".bak", citFileName);
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// 判断通道数据是否被加密
        /// </summary>
        /// <param name="dhi">文件头结构体</param>
        /// <returns>true：加密；false:不加密</returns>
        public bool IsEncrypt(DataHeadInfo dhi)
        {
            if (dhi.sDataVersion.StartsWith("3"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 从里程序列中，判断增减里程
        /// </summary>
        /// <param name="citFileName"></param>
        /// <param name="isKmInc">true：增里程；false：减里程</param>
        /// <returns>true：函数执行成功；false：函数执行失败</returns>
        private bool IsCitKmInc(string citFileName, ref bool isKmInc)
        {
            double[] kMeter = GetMilesData(citFileName);

            int len = kMeter.Length;
            int sum = 0;
            if (len < 10)
            {
                //MessageBox.Show("点数太少");

                throw new Exception("点数太少");
            }

            for (int i = 0; i < 10; i++)
            {
                if (kMeter[(i + 0) * len / 10] < kMeter[(i + 1) * len / 10 - 1])
                {
                    sum += 1;
                }
            }

            if (sum >= 5)
            {
                isKmInc = true;
            }
            else
            {
                isKmInc = false;
            }

            return true;
        }


        /// <summary>
        /// 通道调换
        /// </summary>
        /// <param name="m_dciL"></param>
        /// <param name="channel_L"></param>
        /// <param name="channel_R"></param>
        /// <param name="isInverted"></param>
        private void ChannelExchange(List<DataChannelInfo> m_dciL, String channel_L, String channel_R, Boolean isInverted)
        {
            int index_a = 0;
            int index_b = 0;
            DataChannelInfo m_dci_a = new DataChannelInfo();
            DataChannelInfo m_dci_b = new DataChannelInfo();
            foreach (DataChannelInfo m_dci in m_dciL)
            {
                if (m_dci.sNameEn == channel_L)
                {
                    index_a = m_dciL.IndexOf(m_dci);
                    m_dci_a = m_dci;
                    if (isInverted)
                    {
                        m_dci_a.fScale = m_dci_a.fScale * (-1);
                    }
                }

                if (m_dci.sNameEn == channel_R)
                {
                    index_b = m_dciL.IndexOf(m_dci);
                    m_dci_b = m_dci;
                    if (isInverted)
                    {
                        m_dci_b.fScale = m_dci_b.fScale * (-1);
                    }
                }
            }
            if (index_a < index_b)
            {
                m_dciL.RemoveAt(index_a);
                m_dciL.Insert(index_a, m_dci_b);

                m_dciL.RemoveAt(index_b);
                m_dciL.Insert(index_b, m_dci_a);
            }
            else if (index_a > index_b)
            {
                m_dciL.RemoveAt(index_b);
                m_dciL.Insert(index_b, m_dci_a);

                m_dciL.RemoveAt(index_a);
                m_dciL.Insert(index_a, m_dci_b);
            }
        }

        #endregion

    }
}
