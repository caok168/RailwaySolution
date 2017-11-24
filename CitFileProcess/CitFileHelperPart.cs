using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace CitFileProcess
{
    public partial class CitFileHelper
    {
        #region 处理现有cit文件的数据  2017年2月份新加的方法

        /// <summary>
        /// 用于选择cit文件后列表展示
        /// </summary>
        /// <param name="sFile"></param>
        /// <returns></returns>
        public string QueryDataInfoHead(string sFile)
        {
            try
            {
                using (FileStream fs = new FileStream(sFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (BinaryReader br = new BinaryReader(fs, Encoding.Default))
                    {
                        br.BaseStream.Position = 0;
                        dhi = GetDataInfoHead2(br.ReadBytes(DataOffset.DataHeadLength));
                        StringBuilder sbName = new StringBuilder();
                        ///////////////
                        switch (dhi.iDataType) //iDataType：1轨检、2动力学、3弓网，
                        {
                            case 1:
                                sbName.Append("轨检,");
                                break;
                            case 2:
                                sbName.Append("动力学,");
                                break;
                            case 3:
                                sbName.Append("弓网,");
                                break;
                        }

                        ///////////////

                        sbName.Append(dhi.sDataVersion + ",");
                        sbName.Append(dhi.sTrackCode + ",");
                        sbName.Append(dhi.sTrackName + ",");
                        ///////////////
                        switch (dhi.iDir)
                        {
                            case 1:
                                sbName.Append("上,");
                                break;
                            case 2:
                                sbName.Append("下,");
                                break;
                            case 3:
                                sbName.Append("单,");
                                break;
                            default:
                                sbName.Append("上,");
                                break;
                        }

                        ///////////////
                        sbName.Append(dhi.sTrain + ",");
                        sbName.Append(dhi.sDate + ",");
                        sbName.Append(dhi.sTime + ",");
                        //////////////
                        switch (dhi.iRunDir)
                        {
                            case 0:
                                sbName.Append("正,");
                                break;
                            case 1:
                                sbName.Append("反,");
                                break;
                        }

                        ///////////////
                        ///////////////////
                        switch (dhi.iKmInc)
                        {
                            case 0:
                                sbName.Append("增,");
                                break;
                            case 1:
                                sbName.Append("减,");
                                break;
                        }
                        //////////////////
                        sbName.Append(dhi.fkmFrom.ToString() + ",");
                        sbName.Append(dhi.fkmTo.ToString() + ",");
                        sbName.Append(dhi.iSmaleRate.ToString() + ",");
                        sbName.Append(dhi.iChannelNumber.ToString());

                        br.Close();
                        fs.Close();
                        return "0," + sbName.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                return "1," + ex.Message;
            }
        }

        /// <summary>
        /// 把单行线都统一为增里程(包括正方向和反方向)
        /// </summary>
        /// <param name="citFileName"></param>
        public void ModifyCitMergeKmInc(string citFileName)
        {
            dhi = GetDataInfoHead(citFileName);

            bool isKmInc = true;
            bool bVal = IsCitKmInc(citFileName, ref isKmInc);

            if (!bVal)
            {
                return;
            }
            //文件头中指示为增里程，且文件中确实为增里程，则不需要处理，直接返回。
            if (dhi.iKmInc == 0 && isKmInc == true)
            {
                return;
            }

            //以下情况：有可能是文件头指示错误或是实际文件确实为减里程
            //统一为增里程
            if (dhi.iKmInc != 0)
            {
                dhi.iKmInc = 0;
            }

            try
            {
                #region 存取文件
                FileStream fsRead = new FileStream(citFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                FileStream fsWrite = new FileStream(citFileName + ".bak", FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fsRead, Encoding.UTF8);
                BinaryWriter bw1 = new BinaryWriter(fsWrite, Encoding.UTF8);
                byte[] bHead = br.ReadBytes(120);
                byte[] bChannels = br.ReadBytes(65 * dhi.iChannelNumber);
                byte[] bData = new byte[dhi.iChannelNumber * 2];
                byte[] bDataNew = new byte[dhi.iChannelNumber * 2];
                byte[] bTail = br.ReadBytes(BitConverter.ToInt32(br.ReadBytes(4), 0));

                //bw1.Write(bHead);

                bw1.Write(GetBytesFromDataHeadInfo(dhi));//文件头
                DataHeadInfo mmdhi = GetDataInfoHead(GetBytesFromDataHeadInfo(dhi));


                bw1.Write(bChannels);
                bw1.Write(bTail.Length);
                bw1.Write(bTail);

                long startPos = br.BaseStream.Position;//记录数据开始位置的文件指针

                //增里程时，不反转
                if (isKmInc == true)
                {
                    while (br.BaseStream.Position < br.BaseStream.Length)
                    {
                        bw1.Write(br.ReadBytes(dhi.iChannelNumber * 2));
                        //br.BaseStream.Position += m_dhi.iChannelNumber * 2;
                    }
                }
                else
                {
                    br.BaseStream.Position = br.BaseStream.Length - dhi.iChannelNumber * 2;

                    while (br.BaseStream.Position >= startPos)
                    {
                        bw1.Write(br.ReadBytes(dhi.iChannelNumber * 2));
                        br.BaseStream.Position -= dhi.iChannelNumber * 2 * 2; //liyang: 这块怎么乘以4了 ？ 
                    }
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
                #endregion
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        /// <summary>
        /// 把反方向检测转换为正方向检测
        /// </summary>
        /// <param name="citFileName"></param>
        public void ModifyCitReverseToForward(String citFileName)
        {
            DataHeadInfo m_dhi = GetDataInfoHead(citFileName);
            List<DataChannelInfo> m_dciL = GetDataChannelInfoHead(citFileName);


            //左高低与右高低对调
            ChannelExchange(m_dciL, "L_Prof_SC", "R_Prof_SC", false);
            ChannelExchange(m_dciL, "L_Prof_SC_70", "R_Prof_SC_70", false);
            ChannelExchange(m_dciL, "L_Prof_SC_120", "R_Prof_SC_120", false);

            DataChannelInfo m_dci_a = new DataChannelInfo();

            //左轨向与右轨向对调，然后幅值*（-1）
            ChannelExchange(m_dciL, "L_Align_SC", "R_Align_SC", true);
            ChannelExchange(m_dciL, "L_Align_SC_70", "R_Align_SC_70", true);
            ChannelExchange(m_dciL, "L_Align_SC_120", "R_Align_SC_120", true);


            //水平、超高、三角坑、曲率、曲率变化率*（-1）
            for (int i = 0; i < m_dciL.Count; i++)
            {
                if (m_dciL[i].sNameEn.Equals("Crosslevel"))
                {
                    m_dci_a = m_dciL[i];
                    m_dci_a.fScale = m_dci_a.fScale * (-1);
                    m_dciL[i] = m_dci_a;
                }

                if (m_dciL[i].sNameEn.Equals("Superelevation"))
                {
                    m_dci_a = m_dciL[i];
                    m_dci_a.fScale = m_dci_a.fScale * (-1);
                    m_dciL[i] = m_dci_a;
                }

                if (m_dciL[i].sNameEn.Equals("Short_Twist"))
                {
                    m_dci_a = m_dciL[i];
                    m_dci_a.fScale = m_dci_a.fScale * (-1);
                    m_dciL[i] = m_dci_a;
                }

                if (m_dciL[i].sNameEn.Equals("Curvature"))
                {
                    m_dci_a = m_dciL[i];
                    m_dci_a.fScale = m_dci_a.fScale * (-1);
                    m_dciL[i] = m_dci_a;
                }

                if (m_dciL[i].sNameEn.Equals("Curvature_Rate"))
                {
                    m_dci_a = m_dciL[i];
                    m_dci_a.fScale = m_dci_a.fScale * (-1);
                    m_dciL[i] = m_dci_a;
                }
            }

            try
            {
                #region 存取文件
                FileStream fsRead = new FileStream(citFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                FileStream fsWrite = new FileStream(citFileName + ".bak", FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fsRead, Encoding.UTF8);
                BinaryWriter bw1 = new BinaryWriter(fsWrite, Encoding.UTF8);
                byte[] bHead = br.ReadBytes(120);
                byte[] bChannels = br.ReadBytes(65 * m_dhi.iChannelNumber);
                byte[] bData = new byte[m_dhi.iChannelNumber * 2];
                byte[] bDataNew = new byte[m_dhi.iChannelNumber * 2];
                byte[] bTail = br.ReadBytes(BitConverter.ToInt32(br.ReadBytes(4), 0));

                //bw1.Write(bHead);

                bw1.Write(GetBytesFromDataHeadInfo(m_dhi));//文件头
                //反向--转换为正向
                if (m_dhi.iRunDir == 0)
                {
                    bw1.Write(bChannels);
                }
                else
                {
                    bw1.Write(GetBytesFromChannelDataInfoList(m_dciL));
                }

                bw1.Write(bTail.Length);
                bw1.Write(bTail);

                long startPos = br.BaseStream.Position;//记录数据开始位置的文件指针

                while (br.BaseStream.Position < br.BaseStream.Length)
                {
                    bw1.Write(br.ReadBytes(m_dhi.iChannelNumber * 2));
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
                #endregion
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            return;

        }

        #endregion
    }
}
