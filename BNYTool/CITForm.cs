using BNYTool.Model;
using CitFileProcess;
using CommonFileSDK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace BNYTool
{
    public partial class CITForm : Form
    {
        /// <summary>
        /// 加载cit文件的路径
        /// </summary>
        string citFilePath = "";
        /// <summary>
        /// 创建cit文件的路径
        /// </summary>
        string createCitFilePath = "";
        /// <summary>
        /// BNY文件的路径
        /// </summary>
        string bnyFilePath = "";

        List<ChannelNew> channelListNew = new List<ChannelNew>();

        List<DataChannelInfo> channelList = new List<DataChannelInfo>();

        CitFileHelper citHelper = new CitFileHelper();
        Common common = new Common();
        private MainForm _mainForm = null;
        private CancellationTokenSource tokenSource = new CancellationTokenSource();
        private CancellationToken token = new CancellationToken();

        private Dictionary<string, string> Lines = new Dictionary<string, string>();

        DataHeadInfo dataHead = new DataHeadInfo();

        public CITForm()
        {
            InitializeComponent();
        }

        public CITForm(string bnyFile,MainForm mainForm)
        {
            InitializeComponent();

            bnyFilePath = bnyFile;
            _mainForm = mainForm;
        }

        

        /// <summary>
        /// 选择文件夹路径 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Select_Click(object sender, EventArgs e)
        {
            saveFileDialog.InitialDirectory = bnyFilePath;
            if(saveFileDialog.ShowDialog()== System.Windows.Forms.DialogResult.OK)
            {
                this.txt_FolderPath.Text = saveFileDialog.FileName;
            }
            //DialogResult dr = folderBrowserDialog1.ShowDialog();
            //if (dr == DialogResult.OK)
            //{
            //    this.txt_FolderPath.Text = folderBrowserDialog1.SelectedPath;
            //}
        }

        /// <summary>
        /// 生成新的cit文件事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_CreateCit_Click(object sender, EventArgs e)
        {
            if (bnyFilePath == "")
            {
                MessageBox.Show("请回到主界面先加载BNY文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(trackcodevalue.Text.Trim()) || string.IsNullOrEmpty(traincodevalue.Text.Trim()))
            {
                if (string.IsNullOrEmpty(trackcodevalue.Text.Trim()))
                {
                    MessageBox.Show("线路代码不能为空！");
                    return;
                }
                if (string.IsNullOrEmpty(traincodevalue.Text.Trim()))
                {
                    MessageBox.Show("检测车号不能为空！");
                    return;
                }
            }
            tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;
            string filePath = this.txt_FolderPath.Text.Trim();

            if (String.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("文件路径或者文件名不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            createCitFilePath = filePath;
            Task task = Task.Factory.StartNew(() => CreateCitFile(), token);
            task.ContinueWith((t) =>
            {
                if (t.Exception != null && t.Exception.InnerException is OperationCanceledException||t.IsCanceled)
                {
                    _mainForm.ShowTips("导出cit操作已取消！");
                    MessageBox.Show("导出cit操作已取消！");
                   
                }
                else if (t.Exception != null||t.IsFaulted)
                {
                    _mainForm.ShowTips("导出文件出现错误：" + t.Exception.InnerException.Message);
                    MessageBox.Show("导出文件出现错误：" + t.Exception.InnerException.Message);
                }
                else
                {
                    _mainForm.ShowTips("导出cit文件成功！");
                    MessageBox.Show("导出cit文件成功");
                }
                if (!IsDisposed && this.IsHandleCreated && this.btn_CreateCit.IsHandleCreated)
                {
                    this.Invoke(new Action(() =>
                    {
                        btn_CreateCit.Enabled = true;
                    }));
                }
            });
            btn_CreateCit.Enabled = false;
            _mainForm.ShowTips("开始导出，系统将在后台进行,完成后会通知您！");
        }

        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Close_Click(object sender, EventArgs e)
        {
            tokenSource.Cancel();
            btn_CreateCit.Enabled = true;
        }


        #region 私有方法

        /// <summary>
        /// 显示文件头信息
        /// </summary>
        /// <param name="headInfo"></param>
        private void ShowHeadInfo(DataHeadInfo headInfo)
        {

            datatypevalue.Text = headInfo.iDataType.ToString();
            cbxFileType.Text = headInfo.iDataType.ToString();
            if (!string.IsNullOrEmpty(headInfo.sDataVersion))
            {
                dataversionvalue.Text = headInfo.sDataVersion;
                dataversionnewvalue.Text = headInfo.sDataVersion;
            }
            else
            {
                dataversionvalue.Text = "";
                dataversionnewvalue.Text = "";
            }
            if (!string.IsNullOrEmpty(headInfo.sTrackCode))
            {
                trackcodevalue.Text = headInfo.sTrackCode;
                trackcodenewvalue.Text = headInfo.sTrackCode;
            }
            else
            {
                trackcodevalue.Text = "";
                trackcodenewvalue.Text = "";
            }

            if (!string.IsNullOrEmpty(headInfo.sTrain))
            {
                traincodevalue.Text = headInfo.sTrain;
                traincodenewvalue.Text = headInfo.sTrain;
            }
            else
            {
                traincodevalue.Text = "";
                traincodenewvalue.Text = "";
            }

            if (!string.IsNullOrEmpty(headInfo.sTrackName))
            {
                tracknamevalue.Text = headInfo.sTrackName;
                tracknamenewvalue.Text = headInfo.sTrackName;
            }
            else
            {
                tracknamevalue.Text = "";
                tracknamenewvalue.Text = "";
            }

            dirvalue.Text = headInfo.iDir.ToString();
            cbxUpDown.Text = headInfo.iDir.ToString();


            rundirvalue.Text = headInfo.iRunDir.ToString();
            cbxRunDir.Text = headInfo.iRunDir.ToString();


            kmincvalue.Text = headInfo.iKmInc.ToString();
            cbxKmInc.Text = headInfo.iKmInc.ToString();
        }

        

        /// <summary>
        /// 显示通道信息
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
                    case "里程":
                        channel.BNYChannelId = 3;
                        channel.BNYChannelName = "综合里程";
                                                break;
                    //case "KM":
                    //    channel.BNYChannelId = 3;
                    //    channel.BNYChannelName = "综合里程";
                    //    break;
                    case "SPEED":
                        channel.BNYChannelId = 4;
                        channel.BNYChannelName = "V（速度）";
                        break;
                    case "CB_Lt_R_11":
                        channel.BNYChannelId = 9;
                        channel.BNYChannelName = "车体横加2";
                        break;
                    case "CB_Vt_R_11":
                        channel.BNYChannelId = 10;
                        channel.BNYChannelName = "车体垂加2";
                        break;
                    case "CB_Lg_R_11":
                        channel.BNYChannelId = 11;
                        channel.BNYChannelName = "车体纵加2";
                        break;
                    case "Fr_Vt_L_11":
                        channel.BNYChannelId = 13;
                        channel.BNYChannelName = "构架垂加2";
                        break;
                    case "Fr_Lt_L_11":
                        channel.BNYChannelId = 14;
                        channel.BNYChannelName = "构架横加2";
                        break;
                    case "AB_Vt_L_11":
                        channel.BNYChannelId = 15;
                        channel.BNYChannelName = "左轴箱垂加2";
                        break;
                    case "AB_Vt_R_11":
                        channel.BNYChannelId = 16;
                        channel.BNYChannelName = "右轴箱垂加2";
                        break;
                    default:
                        break;
                }

                channelListNew.Add(channel);
            }

            return channelListNew;
        }

        /// <summary>
        /// 生成cit文件
        /// </summary>
        private void CreateCitFile()
        {

            CreateCitFileHeader(createCitFilePath, dataHead, channelList);

            GetBnyDataAndWriteCit(channelListNew);
        }

        /// <summary>
        /// 获取新的文件头信息
        /// </summary>
        /// <returns></returns>
        private DataHeadInfo GetNewDataHead()
        {
            DataHeadInfo head = new DataHeadInfo();
            head.iDataType = Convert.ToInt32(cbxFileType.SelectedItem != null ? cbxFileType.SelectedItem.ToString() : "2");
            head.sDataVersion = dataversionnewvalue.Text;
            head.sTrackCode = trackcodenewvalue.Text;
            head.sTrackName = tracknamenewvalue.Text;
            head.iDir = Convert.ToInt32(cbxUpDown.SelectedItem != null ? cbxUpDown.SelectedItem.ToString() : "1");
            head.sTrain = traincodenewvalue.Text;
            head.iRunDir = Convert.ToInt32(cbxRunDir.SelectedItem != null ? cbxRunDir.SelectedItem.ToString() : "1");
            head.iKmInc = Convert.ToInt32(cbxKmInc.SelectedItem != null ? cbxKmInc.SelectedItem.ToString() : "1");

            return head;
        }

        /// <summary>
        /// 生成cit文件头信息
        /// </summary>
        /// <param name="citFilePath"></param>
        /// <param name="head"></param>
        /// <param name="channelList"></param>
        /// <param name="dataList"></param>
        private void CreateCitFileHeader(string citFilePath, DataHeadInfo head, List<DataChannelInfo> channelList)
        {
            citHelper.WriteDataInfoHead(citFilePath, head);
            citHelper.WriteDataChannelInfoHead(citFilePath, channelList);
            citHelper.WriteDataExtraInfo(citFilePath, "");
        }

        /// <summary>
        /// 获取BNY数据并向cit文件中写入数据
        /// </summary>
        /// <param name="channelList"></param>
        private void GetBnyDataAndWriteCit(List<ChannelNew> channelList)
        {
            int[] bnyChannelArr = channelList.Where(s => s.BNYChannelId >= 0).Select(s => s.BNYChannelId).ToArray();

            int sampleNum = 1000;
            long startPosition = 0;
            long endPosition = 0;

            long bnyFileLength = common.GetBNYLastPosition(bnyFilePath);

            int pageCount = Convert.ToInt32(bnyFileLength / (sampleNum * BNYFile.GetChannelSize()));

            List<float[]> bnyList = new List<float[]>();
            try
            {
                using (FileStream fs = new FileStream(createCitFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs, Encoding.Default))
                    {
                        DataHeadInfo dhi = citHelper.GetDataInfoHead(createCitFilePath);
                        List<DataChannelInfo> channelInfoList = citHelper.GetDataChannelInfoHead(createCitFilePath);
                        int iChannelNumberSize = dhi.iChannelNumber * 2;
                        byte[] dataArray = new byte[iChannelNumberSize];

                        List<Byte> dataList = new List<Byte>();
                        short tmpRmsData = 0; ;
                        Byte[] tmpBytes = new Byte[2];
                        for (int i = 0; i < pageCount; i++)
                        {
                            bnyList = common.GetBNYData(bnyFilePath, sampleNum, startPosition, ref endPosition);
                            startPosition = endPosition;

                            List<float[]> resultList = new List<float[]>();
                            //Action<float> action = new Action<float>({ });
                            for (int j = 0; j < channelList.Count; j++)
                            {
                                float[] fvalue = new float[sampleNum];

                                if (channelList[j].BNYChannelId >= 0)
                                {
                                    fvalue = bnyList[channelList[j].BNYChannelId];
                                }
                                resultList.Add(fvalue);
                            }

                            long iArrayLen = resultList[0].Length;
                            for (int k = 0; k < iArrayLen; k++)
                            {
                                if (citHelper.IsEncrypt(dhi))
                                {
                                    for (int iTmp = 0; iTmp < channelList.Count; iTmp++)
                                    {
                                        if (channelList[iTmp].BNYChannelId > 6)
                                        {
                                            tmpRmsData = (short)((resultList[iTmp][k] / 10 - channelInfoList[iTmp].fOffset) * channelInfoList[iTmp].fScale);
                                        }
                                        else
                                        {
                                            tmpRmsData = (short)((resultList[iTmp][k] - channelInfoList[iTmp].fOffset) * channelInfoList[iTmp].fScale);
                                        }
                                        tmpBytes = CitFileHelper.ByteXORByte(BitConverter.GetBytes(tmpRmsData));
                                        dataList.AddRange(tmpBytes);
                                    }
                                }
                                else
                                {
                                    for (int iTmp = 0; iTmp < channelList.Count; iTmp++)
                                    {
                                        try
                                        {
                                            if (channelList[iTmp].BNYChannelId > 6)
                                            {
                                                tmpRmsData = (short)((resultList[iTmp][k] / 10 - channelInfoList[iTmp].fOffset) * channelInfoList[iTmp].fScale);
                                            }
                                            else
                                            {
                                                tmpRmsData = (short)((resultList[iTmp][k] - channelInfoList[iTmp].fOffset) * channelInfoList[iTmp].fScale);
                                            }
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
                            if (token.IsCancellationRequested)
                            {
                                token.ThrowIfCancellationRequested();
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }


        private void WriteCitData(string citFilePath, List<float[]> dataList)
        {
            citHelper.WriteChannelDataFloat(citFilePath, dataList);
        }

        private void LoadLineData()
        {
            DataTable dt = InnerFileOperator.Query("select LineName,LineCode from line");
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!Lines.ContainsKey(dt.Rows[i]["LineName"].ToString()))
                    {
                        Lines.Add(dt.Rows[i]["LineName"].ToString(), dt.Rows[i]["LineCode"].ToString());
                    }
                }
            }
        }

        #endregion

        private void CITForm_Load(object sender, EventArgs e)
        {
            InnerFileOperator.InnerFilePath = Application.StartupPath + "\\Line.accdb";
            InnerFileOperator.InnerConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Jet OLEDB:Engine Type=5";
            LoadLineData();

            channelList.Clear();
            string channelXmlPath = Application.StartupPath + "//CitChannel.xml";

            //获得当前运行的Assembly
            Assembly _assembly = Assembly.GetExecutingAssembly();
            Stream stream = _assembly.GetManifestResourceStream("BNYTool.CitChannel.xml");
            if (stream != null)
            {
                XmlSerializer _serializer = new XmlSerializer(typeof(List<DataChannelInfo>));
                using (StreamReader reader = new StreamReader(stream))
                {
                    channelList = (List<DataChannelInfo>)_serializer.Deserialize(reader);
                }
                
            }
            else if (File.Exists(channelXmlPath))
            {
                XmlSerializer _serializer = new XmlSerializer(typeof(List<DataChannelInfo>));
                using (StreamReader reader = new StreamReader(channelXmlPath))
                {
                    channelList = (List<DataChannelInfo>)_serializer.Deserialize(reader);
                }
            }
            else
            {
                MessageBox.Show("配置文件丢失，找不到CitChannel.xml");
            }
            if (!string.IsNullOrEmpty(bnyFilePath))
            {
                FileInfo file = new FileInfo(bnyFilePath);
                dataHead = InitDataHead(file);
                if (dataHead != null)
                {
                    ShowHeadInfo(dataHead);
                }
            }
            Task.Factory.StartNew(() =>
            {
                if (channelList.Count > 0)
                {
                    channelListNew = GetChannelNewInfo(channelList);
                }
            });
            

        }

        private DataHeadInfo InitDataHead(FileInfo file)
        {
            DataHeadInfo headInfo = new DataHeadInfo();
            headInfo.iDataType = 2;
            headInfo.sDataVersion = "0.0.0";
            DateTime dateTime = DateTime.ParseExact(file.Name.Substring(0, 8), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            headInfo.sDate = dateTime.ToString("yyyy-MM-dd");
            headInfo.sTime = dateTime.ToString("HH:mm:ss");
            if (file.Name.Contains("上"))
            {
                headInfo.iDir = 1;
                string lineName = file.Name.Substring(8, file.Name.IndexOf("上") - 8);
                if (Lines.ContainsKey(lineName))
                {
                    headInfo.sTrackName = Lines[lineName];
                }
                headInfo.iRunDir = 1;
                headInfo.iKmInc = 1;
            }
            else if (file.Name.Contains("下"))
            {
                headInfo.iDir = 2;
                string lineName = file.Name.Substring(8, file.Name.IndexOf("下") - 1);
                if (Lines.ContainsKey(lineName))
                {
                    headInfo.sTrackName = Lines[lineName];
                }
                headInfo.iRunDir = 0;
                headInfo.iKmInc = 0;
            }
            else if (file.Name.Contains("单线"))
            {
                headInfo.iDir = 3;
                string lineName = file.Name.Substring(8, file.Name.IndexOf("单线") - 1);
                if (Lines.ContainsKey(lineName))
                {
                    headInfo.sTrackName = Lines[lineName];
                }
                headInfo.iRunDir = 0;
                headInfo.iKmInc = 0;
            }
            headInfo.iSmaleRate = -2000;
            headInfo.fkmFrom = common.GetBNYStartMile(file.FullName);
            headInfo.fkmTo = common.GetBNYEndMile(file.FullName);
            headInfo.iChannelNumber = 15;
            return headInfo;
        }

        private void cbxFileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxFileType.SelectedIndex >= 0)
            {
                if (cbxFileType.SelectedItem != null && !string.IsNullOrEmpty(cbxFileType.SelectedItem.ToString()))
                {
                    dataHead.iDataType = int.Parse(cbxFileType.SelectedItem.ToString());
                    ShowHeadInfo(dataHead);
                }
            }
        }

        private void dataversionnewvalue_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(dataversionnewvalue.Text.Trim()))
            {
                dataHead.sDataVersion = dataversionnewvalue.Text;
                ShowHeadInfo(dataHead);
            }
        }

        private void trackcodenewvalue_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(trackcodenewvalue.Text.Trim()))
            {
                dataHead.sTrackCode = trackcodenewvalue.Text;
                ShowHeadInfo(dataHead);
            }
        }

        private void tracknamenewvalue_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tracknamenewvalue.Text.Trim()))
            {
                dataHead.sTrackName = tracknamenewvalue.Text.Trim();
                ShowHeadInfo(dataHead);
            }
        }

        private void cbxUpDown_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbxUpDown.SelectedItem != null && !string.IsNullOrEmpty(cbxUpDown.SelectedItem.ToString()))
            {
                dataHead.iDir = int.Parse(cbxUpDown.SelectedItem.ToString());
                ShowHeadInfo(dataHead);
            }

        }

        private void traincodenewvalue_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(traincodenewvalue.Text.Trim()))
            {
                dataHead.sTrain = traincodenewvalue.Text.Trim();
                ShowHeadInfo(dataHead);
            }
        }

        private void cbxRunDir_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRunDir.SelectedItem != null && !string.IsNullOrEmpty(cbxRunDir.SelectedItem.ToString()))
            {
                dataHead.iRunDir = int.Parse(cbxRunDir.SelectedItem.ToString());
                ShowHeadInfo(dataHead);
            }
        }

        private void cbxKmInc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxKmInc.SelectedItem != null && !string.IsNullOrEmpty(cbxKmInc.SelectedItem.ToString()))
            {
                dataHead.iKmInc = int.Parse(cbxKmInc.SelectedItem.ToString());
                ShowHeadInfo(dataHead);
            }
        }




        /***
1	时间		
2	Km（轮轨力系统自身的里程）		
3	里程2（综合里程）	里程	里程
4	V（速度）	速度	SPEED
5	1左垂力		
6	1左横力		
7	1右垂力		
8	1右横力		
9	车体横加2	CB_Lt_R_11	CB_Lt_R_11
10	车体垂加2	CB_Vt_R_11	CB_Vt_R_11
11	车体纵加2	CB_Lg_R_11	CB_Lg_R_11
12	陀螺仪2		
13	构架垂加2	Fr_Vt_L_11	Fr_Vt_L_11
14	构架横加2	Fr_Lt_L_11	Fr_Lt_L_11
15	左轴箱垂加2	AB_Vt_L_11	AB_Vt_L_11
16	右轴箱垂加2	AB_Vt_R_11	AB_Vt_R_11
17	曲率2		
18	ALD		
		AB_Lt_L_11	AB_Lt_L_11
         * */
    }
}
