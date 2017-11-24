using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using CitFileProcess;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using BNYTool.Model;
using System.Threading.Tasks;

namespace BNYTool
{
    public partial class TxtToCitForm : Form
    {
        public TxtToCitForm()
        {
            InitializeComponent();
        }

        public Common common = new Common();


        public void ConvertTxtToCit(string txtPath,string citPath)
        {
            string channelXmlPath = Application.StartupPath + "//CitChannel.xml";
            List<DataChannelInfo> citChannelInfo = common.GetChannelDefion(channelXmlPath);
            //由于值过大，比例变为1
            //citChannelInfo.Find(p => p.sNameEn == "M").fScale = 1;
            DataHeadInfo headerInfo = new DataHeadInfo();
            headerInfo.iDataType = 2;
            headerInfo.sDataVersion = "0.0.1";
            headerInfo.sTrackName = "shuohuang";
            headerInfo.sTrain = "abcd";
            headerInfo.sTrackCode = "2302";
            headerInfo.iRunDir = 0;
            headerInfo.iSmaleRate = 100;
            string txtFileName = Path.GetFileNameWithoutExtension(txtPath);
            DateTime dateString = DateTime.ParseExact(txtFileName.Substring(0, 12), "yyMMddHHmmss",CultureInfo.InvariantCulture);
            headerInfo.sDate = dateString.ToString("yyyy-MM-dd");
            headerInfo.sTime = dateString.ToString("HH:mm:ss");
            headerInfo.iChannelNumber = citChannelInfo.Count;
            string xb = txtFileName.Substring(txtFileName.Length - 1).ToLower();
            if(xb=="x")
            {
                headerInfo.iDir = 2;
                headerInfo.iKmInc = 1;
            }
            else if(xb=="s")
            {
                headerInfo.iDir = 1;
                headerInfo.iKmInc = 0;
            }
            long lineCount = common.GetTextLineCount(txtTxtPath.Text);
            double startMileage= common.GetAppointMileInLine(txtTxtPath.Text, 1);
            double endMileage = common.GetAppointMileInLine(txtTxtPath.Text, lineCount);
            headerInfo.fkmFrom = (float)startMileage;
            headerInfo.fkmTo = (float)endMileage;
            List<ChannelNew> channes = new List<ChannelNew>();
            channes = GetChannelNewInfo(citChannelInfo);
            
            common.WriteCitHeadAndChannelInfo(citPath, headerInfo, citChannelInfo);
            GetTxtDataAndWriteCit(txtTxtPath.Text, citPath, channes);

        }

        private void GetTxtDataAndWriteCit(string txtPath, string citPath, List<ChannelNew> channelList)
        {
            CitFileHelper citHelper = new CitFileHelper();
            int[] bnyChannelArr = channelList.Where(s => s.BNYChannelId >= 0).Select(s => s.BNYChannelId).ToArray();
            int sampleNum = 50000;
            long startPosition = 1;
            long endPosition = sampleNum;

            long lineCount = common.GetTextLineCount(txtTxtPath.Text);
            int pageCount = 0;
            double realPageCount = lineCount * 1.0 / (sampleNum);
            pageCount = (int)realPageCount;
            List<float[]> txtDataList = new List<float[]>();
            List<float[]> resultList = new List<float[]>();
            float[] fvalue = new float[sampleNum];
            for (int i = 0; i < pageCount; i++)
            {
                txtDataList = common.GetTxtData(txtPath, startPosition, endPosition);
                long temp = endPosition;
                endPosition = endPosition + sampleNum;
                startPosition = temp + 1;
                resultList.Clear();
                for (int j = 0; j < channelList.Count; j++)
                {
                    if (channelList[j].BNYChannelId >= 0)
                    {
                        if (channelList[j].BNYChannelId == 0)
                        {
                            if (channelList[j].dataChannelInfo.sNameEn == "KM")
                            {
                                fvalue = txtDataList[channelList[j].BNYChannelId].Select(p => ((float)(int)p)).ToArray();
                            }
                            else if (channelList[j].dataChannelInfo.sNameEn == "M")
                            {
                                var kmValues = txtDataList[channelList[j].BNYChannelId];

                                float[] fvalueNew = new float[sampleNum];

                                for (int k = 0; k < kmValues.Length; k++)
                                {
                                    if (k == 20861)
                                    {

                                    }

                                    string[] strValues = kmValues[k].ToString("F3").Split('.');
                                    if (strValues.Length == 1)
                                    {
                                        fvalueNew[k] = 0;
                                    }
                                    else
                                    {
                                        fvalueNew[k] = Convert.ToSingle(strValues[1]);
                                    }
                                }
                                fvalue = fvalueNew;

                            }
                            else
                            {
                                float aa1 = txtDataList[channelList[j].BNYChannelId][20860];
                                float aa2 = txtDataList[channelList[j].BNYChannelId][20861];
                                float aa3 = txtDataList[channelList[j].BNYChannelId][20862];

                                fvalue = txtDataList[channelList[j].BNYChannelId];
                            }
                        }
                        else
                        {
                            float aa1 = txtDataList[channelList[j].BNYChannelId][20860];
                            float aa2 = txtDataList[channelList[j].BNYChannelId][20861];
                            float aa3 = txtDataList[channelList[j].BNYChannelId][20862];

                            fvalue = txtDataList[channelList[j].BNYChannelId];
                        }
                    }
                    else
                    {
                        fvalue = new float[txtDataList[0].Length];
                    }
                    resultList.Add(fvalue);
                }
                citHelper.WriteChannelDataFloat(citPath, resultList);

                //if (tokenSource.IsCancellationRequested)
                //{
                //    throw new OperationCanceledException();
                //}
            }
            if (realPageCount % ((int)realPageCount) > 0)
            {
                startPosition = pageCount * sampleNum + 1;
                endPosition = lineCount;
                txtDataList = common.GetTxtData(txtPath, startPosition, endPosition);
                resultList.Clear();
                for (int j = 0; j < channelList.Count; j++)
                {
                    if (channelList[j].BNYChannelId >= 0)
                    {
                        if (channelList[j].BNYChannelId == 0)
                        {
                            if (channelList[j].dataChannelInfo.sNameEn == "KM")
                            {
                                fvalue = txtDataList[channelList[j].BNYChannelId].Select(p => ((float)(int)p)).ToArray();
                            }
                            else if (channelList[j].dataChannelInfo.sNameEn == "M")
                            {
                                //fvalue = txtDataList[channelList[j].BNYChannelId].Select(p => (p - (int)p) * 1000).ToArray();
                                var kmValues = txtDataList[channelList[j].BNYChannelId];

                                float[] fvalueNew = new float[sampleNum];

                                for (int k = 0; k < kmValues.Length; k++)
                                {
                                    string[] strValues = kmValues[k].ToString("F3").Split('.');
                                    if (strValues.Length == 1)
                                    {
                                        fvalueNew[k] = 0;
                                    }
                                    else
                                    {
                                        fvalueNew[k] = Convert.ToSingle(strValues[1]);
                                    }
                                }
                                fvalue = fvalueNew;
                            }
                            else
                            {
                                fvalue = txtDataList[channelList[j].BNYChannelId];
                            }
                        }
                        else
                        {
                            fvalue = txtDataList[channelList[j].BNYChannelId];
                        }
                    }
                    else
                    {
                        fvalue = new float[txtDataList[0].Length];
                    }
                    resultList.Add(fvalue);
                }
                citHelper.WriteChannelDataFloat(citPath, resultList);
            }
        }

        private void TxtToCitForm_Load(object sender, EventArgs e)
        {
            
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtCitPath.Text.Trim()))
            {
                MessageBox.Show("导出路径不能为空！");
                return;
            }
            if(File.Exists(txtCitPath.Text))
            {
                if (MessageBox.Show("已存在该cit，是否覆盖已有文件？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    File.Delete(txtCitPath.Text);
                }
                else
                {
                    return;
                }
            }
            this.btnExport.Enabled = false;
            this.progressBar1.Visible = true;
            Task t = new Task(() => {
                ConvertTxtToCit(txtTxtPath.Text, txtCitPath.Text);
            });
            t.ContinueWith((task) => 
            {
                if (!IsDisposed && this.IsHandleCreated && this.btnExport.IsHandleCreated)
                {
                    this.Invoke(new Action(() =>
                    {
                        btnExport.Enabled = true;
                        this.progressBar1.Visible = false;
                    }));
                }
                if (task.Exception == null || task.IsCompleted)
                {
                    MessageBox.Show("导出完成！");
                }
                else if (task.Exception != null)
                {
                    MessageBox.Show("错误：" + task.Exception.InnerException.Message);
                }
               
            });
            t.Start();
            
        }

        private void btnSelecttxtPath_Click(object sender, EventArgs e)
        {
            DialogResult dr= openFileDialog1.ShowDialog();
            if(dr== DialogResult.OK)
            {
                txtTxtPath.Text = openFileDialog1.FileName;
                txtCitPath.Text = Path.GetFullPath(openFileDialog1.FileName).Replace(".txt", ".cit");
            }
        }

        /// <summary>
        /// 获取要显示的通道信息
        /// </summary>
        /// <param name="channelList"></param>
        private List<ChannelNew> GetChannelNewInfo(List<DataChannelInfo> channelList)
        {
            List<ChannelNew> channelListNew = new List<ChannelNew>();

            List<BNYChannel> bnyChannelList = common.GetChannelNameList();

            for (int i = 0; i < channelList.Count; i++)
            {
                ChannelNew channel = new ChannelNew();
                channel.dataChannelInfo = channelList[i];
                switch (channelList[i].sNameEn)
                {
                    case "KM":
                        channel.BNYChannelId = 0;
                        channel.BNYChannelName = "综合里程";
                        break;
                    case "M":
                        channel.BNYChannelId = 0;
                        channel.BNYChannelName = "综合里程";
                        break;
                    case "SPEED":
                        channel.BNYChannelId = 3;
                        channel.BNYChannelName = "V（速度）";
                        break;
                    case "AB_Vt_L_11":
                        channel.BNYChannelId = 1;
                        channel.BNYChannelName = "1左垂力";
                        break;
                    case "AB_Vt_R_11":
                        channel.BNYChannelId = 2;
                        channel.BNYChannelName = "1右垂力";
                        break;
                    default:
                        break;
                }
                #region 备份
                //switch (channelList[i].sNameEn)
                //{
                //    case "里程":
                //        channel.BNYChannelId = 3;
                //        channel.BNYChannelName = "综合里程";
                //        break;
                //    case "SPEED":
                //        channel.BNYChannelId = 4;
                //        channel.BNYChannelName = "V（速度）";
                //        break;
                //    case "CB_Lt_R_11":
                //        channel.BNYChannelId = 9;
                //        channel.BNYChannelName = "车体横加2";
                //        break;
                //    case "CB_Vt_R_11":
                //        channel.BNYChannelId = 10;
                //        channel.BNYChannelName = "车体垂加2";
                //        break;
                //    case "CB_Lg_R_11":
                //        channel.BNYChannelId = 11;
                //        channel.BNYChannelName = "车体纵加2";
                //        break;
                //    case "Fr_Vt_L_11":
                //        channel.BNYChannelId = 13;
                //        channel.BNYChannelName = "构架垂加2";
                //        break;
                //    case "Fr_Lt_L_11":
                //        channel.BNYChannelId = 14;
                //        channel.BNYChannelName = "构架横加2";
                //        break;
                //    case "AB_Vt_L_11":
                //        channel.BNYChannelId = 15;
                //        channel.BNYChannelName = "左轴箱垂加2";
                //        break;
                //    case "AB_Vt_R_11":
                //        channel.BNYChannelId = 16;
                //        channel.BNYChannelName = "右轴箱垂加2";
                //        break;
                //    default:
                //        break;
                //}
                #endregion
                channelListNew.Add(channel);
            }

            return channelListNew;
        }
    }
}
