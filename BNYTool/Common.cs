using BNYTool.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Reflection;
using System.Xml.Serialization;
using CitFileProcess;

namespace BNYTool
{
    /// <summary>
    /// 公共类
    /// </summary>
    public class Common
    {
        /// <summary>
        /// 获取BNY文件的长度
        /// </summary>
        /// <param name="bnyFilePath">bny文件路径</param>
        /// <returns>bny文件的长度</returns>
        public long GetBNYLastPosition(string bnyFilePath)
        {
            try
            {
                FileStream fs = new FileStream(bnyFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fs, Encoding.Default);

                long position = br.BaseStream.Length;

                br.Close();
                fs.Close();

                return position;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取BNY文件的开始里程
        /// </summary>
        /// <param name="bnyFilePath">bny文件路径</param>
        /// <returns>开始里程</returns>
        public float GetBNYStartMile(string bnyFilePath)
        {
            try
            {
                FileStream fs = new FileStream(bnyFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fs, Encoding.Default);
                br.BaseStream.Position = 0;

                int iChannelNumberSize = BNYFile.GetChannelSize();
                
                byte[] b = new byte[iChannelNumberSize];
                b = br.ReadBytes(iChannelNumberSize);

                float fGL = (BitConverter.ToSingle(b, (3 - 1) * BNYFile.oneByteSize));

                br.Close();
                fs.Close();

                return fGL;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取BNY文件的结束里程
        /// </summary>
        /// <param name="bnyFilePath">bny文件路径</param>
        /// <returns>结束里程</returns>
        public float GetBNYEndMile(string bnyFilePath)
        {
            try
            {
                FileStream fs = new FileStream(bnyFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fs, Encoding.Default);
                br.BaseStream.Position = 0;

                int iChannelNumberSize = BNYFile.GetChannelSize();

                br.BaseStream.Position = br.BaseStream.Length - iChannelNumberSize;

                byte[] b = new byte[iChannelNumberSize];
                b = br.ReadBytes(iChannelNumberSize);

                float fGL = (BitConverter.ToSingle(b, (3 - 1) * BNYFile.oneByteSize));

                br.Close();
                fs.Close();

                return fGL;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        
        /// <summary>
        /// 获取BNY文件里程的位置
        /// </summary>
        /// <param name="bnyFilePath">bny文件路径</param>
        /// <param name="isInc">是否为增里程</param>
        /// <param name="mile">里程</param>
        /// <returns>位置</returns>
        public long GetMilePosition(string bnyFilePath, bool isInc, float mile)
        {
            try
            {
                long position = -1;

                FileStream fs = new FileStream(bnyFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fs, Encoding.Default);
                br.BaseStream.Position = 0;

                int iChannelNumberSize = BNYFile.GetChannelSize();
                long endFilePos = br.BaseStream.Length;

                int sampleNum = Convert.ToInt32(endFilePos / iChannelNumberSize);
                
                byte[] b = new byte[iChannelNumberSize];
                

                for (int i = 0; i < sampleNum; i++)
                {
                    b = br.ReadBytes(iChannelNumberSize);

                    float fGL = (BitConverter.ToSingle(b, (3 - 1) * BNYFile.oneByteSize));

                    if (isInc)
                    {
                        if (fGL >= mile)
                        {
                            position = br.BaseStream.Position - iChannelNumberSize;
                            break;
                        }
                    }
                    else
                    {
                        if (fGL <= mile)
                        {
                            position = br.BaseStream.Position - iChannelNumberSize;
                            break;
                        }
                    }
                }
                br.Close();
                fs.Close();

                return position;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取BNY文件里指定里程的位置点信息
        /// </summary>
        /// <param name="bnyFilePath">bny文件路径</param>
        /// <param name="isInc">是否为增里程</param>
        /// <param name="startmile">开始里程</param>
        /// <param name="endmile">结束里程</param>
        /// <returns>开始里程、结束里程对应的位置数组</returns>
        //public long[] GetMilePostions(string bnyFilePath, bool isInc, float startmile, float endmile)
        //{
        //    try
        //    {
        //        long[] positions = new long[2];

        //        bool isSet = false;

        //        long startpos = 0;
        //        long endpos = 0;

        //        FileStream fs = new FileStream(bnyFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        //        BinaryReader br = new BinaryReader(fs, Encoding.Default);
        //        br.BaseStream.Position = 0;

        //        int iChannelNumberSize = BNYFile.GetChannelSize();
        //        long endFilePos = br.BaseStream.Length;

        //        int sampleNum = Convert.ToInt32(endFilePos / iChannelNumberSize);

        //        byte[] b = new byte[iChannelNumberSize];


        //        for (int i = 0; i < sampleNum; i++)
        //        {
        //            b = br.ReadBytes(iChannelNumberSize);

        //            float fGL = (BitConverter.ToSingle(b, (3 - 1) * BNYFile.oneByteSize));

        //            if (isInc)
        //            {
        //                if (fGL >= startmile && !isSet)
        //                {
        //                    startpos = br.BaseStream.Position - iChannelNumberSize;
        //                    isSet = true;
        //                }
        //                if (fGL > endmile)
        //                {
        //                    endpos = br.BaseStream.Position;
        //                    break;
        //                }
        //            }
        //            else
        //            {
        //                if (fGL <= endmile && !isSet)
        //                {
        //                    startpos = br.BaseStream.Position - iChannelNumberSize;
        //                    isSet = true;
        //                }
        //                if (fGL < startmile)
        //                {
        //                    endpos = br.BaseStream.Position;
        //                    break;
        //                }
        //            }
        //        }
        //        br.Close();
        //        fs.Close();

        //        positions[0] = startpos;
        //        positions[1] = endpos;

        //        return positions;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message, ex);
        //    }
        //}

        public long[] GetMilePostions(string bnyFilePath, bool isInc, float startmile, float endmile)
        {
            try
            {
                long[] positions = new long[2];
                long startpos = 0;
                long endpos = 0;

                startpos = GetMilePosition(bnyFilePath, isInc, startmile);
                endpos = GetMilePosition(bnyFilePath, isInc, endmile);
                if (startpos != -1 && endpos != -1)
                {
                    if (startpos < endpos)
                    {
                        positions[0] = startpos;
                        positions[1] = endpos;
                    }
                    else
                    {
                        positions[0] = endpos;
                        positions[1] = startpos;
                    }
                }
                /*
                FileStream fs = new FileStream(bnyFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fs, Encoding.Default);
                br.BaseStream.Position = 0;

                int iChannelNumberSize = BNYFile.GetChannelSize();
                long endFilePos = br.BaseStream.Length;

                int sampleNum = Convert.ToInt32(endFilePos / iChannelNumberSize);

                byte[] b = new byte[iChannelNumberSize];


                for (int i = 0; i < sampleNum; i++)
                {
                    b = br.ReadBytes(iChannelNumberSize);

                    float fGL = (BitConverter.ToSingle(b, (3 - 1) * BNYFile.oneByteSize));
                    if(isInc)
                    {
                        if (fGL >= startmile && !isSetStart)
                        {
                            startpos = br.BaseStream.Position - iChannelNumberSize;
                            isSetStart = true;
                        }
                        if (fGL >= endmile && !isSetEnd)
                        {
                            endpos = br.BaseStream.Position - iChannelNumberSize;
                            isSetEnd = true;
                        }
                    }
                    else
                    {
                        if (fGL <= startmile && !isSetStart)
                        {
                            startpos = br.BaseStream.Position - iChannelNumberSize;
                            isSetStart = true;
                        }
                        if (fGL <= endmile && !isSetEnd)
                        {
                            endpos = br.BaseStream.Position - iChannelNumberSize;
                            isSetEnd = true;
                        }
                    }


                }
                br.Close();
                fs.Close();

                positions[0] = startpos;
                positions[1] = endpos;
                */
                return positions;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 查询指定采样点的BNY数据集合
        /// </summary>
        /// <param name="bnyFilePath">bny文件路径</param>
        /// <param name="sampleNum">采样点个数</param>
        /// <param name="startFilePos">开始位置</param>
        /// <param name="endFilePos">结束位置</param>
        /// <returns>数据集合</returns>
        public List<float[]> GetBNYData(string bnyFilePath, int sampleNum, long startFilePos, ref long endFilePos)
        {
            try
            {
                FileStream fs = new FileStream(bnyFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fs, Encoding.Default);
                br.BaseStream.Position = startFilePos;

                int iChannelNumberSize = BNYFile.GetChannelSize();
                endFilePos = startFilePos + iChannelNumberSize * sampleNum;

                if (endFilePos > br.BaseStream.Length)
                {
                    endFilePos = br.BaseStream.Length;

                    sampleNum = Convert.ToInt32((endFilePos - startFilePos) / iChannelNumberSize);
                }
                
                byte[] b = new byte[iChannelNumberSize];

                List<float[]> allList = new List<float[]>();
                for (int i = 0; i < BNYFile.channelNum; i++)
                {
                    float[] array = new float[sampleNum];
                    allList.Add(array);
                }

                for (int i = 0; i < sampleNum; i++)
                {
                    b = br.ReadBytes(iChannelNumberSize);
                    
                    for (int channelId = 1; channelId <= BNYFile.channelNum; channelId++)
                    {
                        float fGL = (BitConverter.ToSingle(b, (channelId - 1) * BNYFile.oneByteSize));

                        allList[channelId - 1][i] = fGL;
                    }
                }
                br.Close();
                fs.Close();

                return allList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 查询指定采样点的BNY数据集合
        /// </summary>
        /// <param name="bnyFilePath">bny文件路径</param>
        /// <param name="channelNums">通道号数组</param>
        /// <param name="sampleNum">采样点个数</param>
        /// <param name="startFilePos">开始位置</param>
        /// <param name="endFilePos">结束位置</param>
        /// <returns>数据集合</returns>
        public List<float[]> GetBNYData(string bnyFilePath, int[] channelNums, int sampleNum, long startFilePos, ref long endFilePos)
        {
            try
            {
                FileStream fs = new FileStream(bnyFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fs, Encoding.Default);
                br.BaseStream.Position = startFilePos;

                int iChannelNumberSize = BNYFile.GetChannelSize();
                endFilePos = startFilePos + iChannelNumberSize;

                if (endFilePos > br.BaseStream.Length)
                {
                    endFilePos = br.BaseStream.Length;

                    sampleNum = Convert.ToInt32((endFilePos - startFilePos) / iChannelNumberSize);
                }

                byte[] b = new byte[iChannelNumberSize];

                List<float[]> allList = new List<float[]>();

                int channelCount = channelNums.Length;

                for (int i = 0; i < channelCount; i++)
                {
                    float[] array = new float[sampleNum];
                    allList.Add(array);
                }

                for (int i = 0; i < sampleNum; i++)
                {
                    b = br.ReadBytes(iChannelNumberSize);

                    for (int j = 0; j < channelCount; j++)
                    {
                        float fGL = (BitConverter.ToSingle(b, (channelNums[j] - 1) * BNYFile.oneByteSize));

                        allList[j][i] = fGL;
                    }
                }
                br.Close();
                fs.Close();

                return allList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 查询指定区间位置的BNY数据集合
        /// </summary>
        /// <param name="bnyFilePath">bny文件路径</param>
        /// <param name="startFilePos">开始位置</param>
        /// <param name="endFilePos">结束位置</param>
        /// <returns>数据集合</returns>
        public List<float[]> GetBNYData(string bnyFilePath, long startFilePos, long endFilePos)
        {
            try
            {
                FileStream fs = new FileStream(bnyFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fs, Encoding.Default);
                br.BaseStream.Position = startFilePos;

                int iChannelNumberSize = BNYFile.GetChannelSize();

                if (endFilePos > br.BaseStream.Length)
                {
                    endFilePos = br.BaseStream.Length;
                }

                int sampleNum = Convert.ToInt32((endFilePos - startFilePos) / iChannelNumberSize);

                byte[] b = new byte[iChannelNumberSize];

                List<float[]> allList = new List<float[]>();
                for (int i = 0; i < BNYFile.channelNum; i++)
                {
                    float[] array = new float[sampleNum];
                    allList.Add(array);
                }

                for (int i = 0; i < sampleNum; i++)
                {
                    b = br.ReadBytes(iChannelNumberSize);

                    for (int channelId = 1; channelId <= BNYFile.channelNum; channelId++)
                    {
                        float fGL = (BitConverter.ToSingle(b, (channelId - 1) * BNYFile.oneByteSize));

                        allList[channelId - 1][i] = fGL;
                    }
                }
                br.Close();
                fs.Close();

                return allList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


        public void ExportChannelDataText(string filePath,string[] channelNames)
        {
            string path = filePath;

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    for (int i = 0; i < channelNames.Length; i++)
                    {
                        sw.Write(channelNames[i]);
                        if (i != channelNames[i].Length - 1)
                        {
                            sw.Write(",");
                        }
                    }

                    sw.WriteLine();
                    //清空缓冲区
                    sw.Flush();
                    //关闭流
                    sw.Close();
                    fs.Close();
                }
            }
        }

        /// <summary>
        /// 导出数据到txt文件
        /// </summary>
        /// <param name="filePath">txt文件名</param>
        /// <param name="dataList">数据集合</param>
        /// <param name="channelNames">通道名称</param>
        public void ExportDataTxt(string filePath, List<float[]> dataList)
        {
            try
            {
                string path = filePath;

                using (FileStream fs = new FileStream(path, FileMode.Append))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        if (dataList.Count > 0)
                        {
                            int length = dataList[0].Length;
                            for (int i = 0; i < length; i++)
                            {
                                for (int j = 0; j < dataList.Count; j++)
                                {
                                    sw.Write(dataList[j][i]);
                                    if (j != dataList.Count - 1)
                                    {
                                        sw.Write(",");
                                    }
                                }
                                sw.WriteLine();
                            }
                        }

                        //清空缓冲区
                        sw.Flush();
                        //关闭流
                        sw.Close();
                        fs.Close();
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="txtFilePath"></param>
        public void GetTxtData(string txtFilePath)
        {
            int count = 0;
            string line = "";
            StreamReader file = new StreamReader(txtFilePath);
            while ((line = file.ReadLine()) != null)
            {
                line = file.ReadLine();
                count++;
            }
            file.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="txtFilePath"></param>
        /// <returns></returns>
        public long GetTextLineCount(string txtFilePath)
        {
            int count = 0;
            string line = "";
            StreamReader file = new StreamReader(txtFilePath);
            file.ReadLine();
            while ((line = file.ReadLine()) != null)
            {
                line = file.ReadLine();
                if (line != null)
                {
                    count++;
                }
            }
            file.Close();
            return count;
        }

        public double GetAppointMileInLine(string txtFilePath,long lineIndex)
        {
            string line = "";
            StreamReader file = new StreamReader(txtFilePath);

            file.ReadLine();

            //float[] myData = new float[endLine - (startLine - 1)];
            int count = 0;
            while ((line = file.ReadLine()) != null)
            {
                line = file.ReadLine();
                count++;
                if (count ==lineIndex)
                {
                    string[] split = line.Split(',');
                    return double.Parse(split[0]);
                }
            }
            return -1;
        }

        public List<float[]> GetTxtData(string txtPath,long startLine,long endLine)
        {
            string line = "";
            List<float[]> data = null;
            StreamReader file = new StreamReader(txtPath);

            file.ReadLine();

            //float[] myData = new float[endLine - (startLine - 1)];
            long index = startLine;
            int count = 0;
            while ((line = file.ReadLine()) != null)
            {
                line = file.ReadLine();
                count++;
                if (count > endLine)
                {
                    break;
                }
                if (count >= startLine)
                {
                    string[] split = line.Split(',');
                    if (data == null)
                    {
                        data = new List<float[]>();
                        for (int i = 0; i < split.Length; i++)
                        {
                            float[] myData = new float[endLine - (startLine - 1)];
                            data.Add(myData);
                        }
                    }
                    
                    float[] d = new float[split.Length];
                    //d[0] = float.Parse(split[0]);
                    //d[1] = float.Parse(split[1]);
                    //d[2] = float.Parse(split[2]);
                    //d[3] = float.Parse(split[3]);
                    data[0][count-startLine] = float.Parse(split[0]);
                    data[1][count - startLine] = float.Parse(split[1]);
                    data[2][count - startLine] = float.Parse(split[2]);
                    data[3][count - startLine] = float.Parse(split[3]);
                }
                
            }

            file.Close();
            return data;
        }


        public DateTime ConvertLongDateTime(long d)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(d + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime dtResult = dtStart.Add(toNow);
            return dtResult;
        }


        public string[] GetChannelNames()
        {
            string[] channelNames = new string[18];

            channelNames[0] = "时间";
            channelNames[1] = "里程";
            channelNames[2] = "综合里程";
            channelNames[3] = "速度";
            channelNames[4] = "左垂力";
            channelNames[5] = "左横力";
            channelNames[6] = "右垂力";
            channelNames[7] = "右横力";
            channelNames[8] = "车体横加";
            channelNames[9] = "车体垂加";
            channelNames[10] = "车体纵加";
            channelNames[11] = "陀螺仪";
            channelNames[12] = "构架垂";
            channelNames[13] = "构架横";
            channelNames[14] = "左轴箱加";
            channelNames[15] = "右轴箱加";
            channelNames[16] = "曲率";
            channelNames[17] = "ALD";

            //for (int i = 0; i < 18; i++)
            //{
            //    channelNames[i] = "通道" + i;
            //}

            return channelNames;
        }


        public List<BNYChannel> GetChannelNameList()
        {
            List<BNYChannel> list = new List<BNYChannel>();

            string[] channelNames = GetChannelNames();
            for (int i = 0; i < channelNames.Length; i++)
            {
                BNYChannel channel = new BNYChannel();
                channel.ID = i + 1;
                channel.ChannelName = channelNames[i];

                list.Add(channel);
            }

            return list;
        }

        public List<float[]> GetBnyTxtData(string txtFilePath,int[] channelNum,int SampleCount,long rowIndex,ref long rowEndIndex)
        {
            List<float[]> allList = new List<float[]>();

            return allList;
        }

        public List<DataChannelInfo> GetChannelDefion(string channelXmlPath)
        {
            Assembly _assembly = Assembly.GetExecutingAssembly();

            //根据资源名称从Assembly中获取此资源的Stream
            List<DataChannelInfo> citChannelInfo = new List<DataChannelInfo>();
            Stream stream = _assembly.GetManifestResourceStream("BNYTool.CitChannel.xml");
            if (stream != null)
            {
                XmlSerializer _serializer = new XmlSerializer(typeof(List<DataChannelInfo>));
                using (StreamReader reader = new StreamReader(stream))
                {
                    citChannelInfo = (List<DataChannelInfo>)_serializer.Deserialize(reader);
                }
                return citChannelInfo;
            }
            else if (File.Exists(channelXmlPath))
            {
                XmlSerializer _serializer = new XmlSerializer(typeof(List<DataChannelInfo>));
                using (StreamReader reader = new StreamReader(channelXmlPath))
                {
                    citChannelInfo = (List<DataChannelInfo>)_serializer.Deserialize(reader);
                }
                return citChannelInfo;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 创建cit文件并写入头部和通道定义信息
        /// </summary>
        /// <param name="citPath">cit文件路径</param>
        /// <param name="headInfo">文件头部信息</param>
        /// <param name="channelInfo">通道定义信息</param>
        public void WriteCitHeadAndChannelInfo(string citPath, DataHeadInfo headInfo, List<DataChannelInfo> channelInfo)
        {
            CitFileHelper citHelper = new CitFileHelper();
            citHelper.WriteDataInfoHead(citPath, headInfo);
            citHelper.WriteDataChannelInfoHead(citPath, channelInfo);
            citHelper.WriteDataExtraInfo(citPath, "");
        }
    }
}
