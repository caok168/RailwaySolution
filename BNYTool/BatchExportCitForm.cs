using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CitFileProcess;
using System.Xml.Serialization;
using CommonFileSDK;
using BNYTool;
using System.Threading.Tasks;
using System.Threading;
using BNYTool.Model;


namespace BNYTool
{
    public partial class BatchExportCitForm : Form
    {
        private MainForm _mainForm = null;

        private CitCreater _citCreater = null;

        private bool _isSingle = false;

        private static object lockOjbect = new object();

        private FileInfo bnyFileInfo = null;

        public class AggregateExceptionArgs : EventArgs
        {
            public AggregateException AggregateException { get; set; }
        }  

        public BatchExportCitForm(MainForm main,bool isSingle,FileInfo bnyFile)
        {
            InitializeComponent();
            _mainForm = main;
            _isSingle = isSingle;
            bnyFileInfo = bnyFile;
        }

        private event EventHandler<AggregateExceptionArgs> AggregateExceptionCatched;

        private Common common = new Common();

        private List<DataChannelInfo> citChannelInfo = new List<DataChannelInfo>();

        private Dictionary<FileInfo, DataHeadInfo> headInfoList = new Dictionary<FileInfo, DataHeadInfo>();


        private Dictionary<string, Queue<List<float[]>>> channelDataList = new Dictionary<string, Queue<List<float[]>>>();

        private CancellationTokenSource tokenSource = new CancellationTokenSource();
        private CancellationToken token = new CancellationToken();
        private bool isCancel = false;
        

        private string fileExportPath = string.Empty;

        //cit文件名称
        private string exportCitPath = String.Empty;

        private void btnSelectImportPath_Click(object sender, EventArgs e)
        {
            if(folderBrowserImportDialog.ShowDialog()== DialogResult.OK)
            {
                txtExportPath.Text = folderBrowserImportDialog.SelectedPath;
                txtImportPath.Text = folderBrowserImportDialog.SelectedPath;
                fileExportPath = txtExportPath.Text;
                DirectoryInfo folder = new DirectoryInfo(txtImportPath.Text);
                FileInfo[] files = folder.GetFiles("*.bny", SearchOption.TopDirectoryOnly);
                if (files.Count() > 0)
                {
                    headInfoList.Clear();
                    errorProvider.Clear();
                    isCancel = false;
                    string error = "";
                    foreach (FileInfo file in files)
                    {
                        DataHeadInfo headInfo =_citCreater.InitDataHead(file, ref error);
                        if (headInfo != null)
                        {
                            headInfoList.Add(file, headInfo);
                        }
                        else
                        {
                            error += Environment.NewLine;
                        }
                    }
                    if(!string.IsNullOrEmpty(error))
                    {
                        MessageBox.Show(error);
                    }
                    DisplayFileInfo();
                }
                else
                {
                    errorProvider.SetError(txtImportPath, "在目录中找不到BNY文件");
                }
            }
        }

        


        private void DisplayFileInfo()
        {
            if (headInfoList.Count > 0)
            {
                List<int> dataTypeList = (from r in headInfoList.Values select r.iDataType).ToList<int>().Distinct().ToList<int>();
                if (dataTypeList.Count > 1)
                {
                    datatypevalue.Text = "";
                    cbxFileType.SelectedIndex = 0;
                }
                else if (dataTypeList.Count > 0)
                {
                    datatypevalue.Text = dataTypeList[0].ToString();
                    cbxFileType.SelectedValue = dataTypeList[0];
                }

                List<string> dataVersionList = (from r in headInfoList.Values select r.sDataVersion).ToList<string>().Distinct().ToList<string>();
                if (dataVersionList.Count > 1)
                {
                    dataversionvalue.Text = "数据不统一";
                    dataversionnewvalue.Text = "";
                }
                else
                {
                    dataversionvalue.Text = dataVersionList[0];
                    dataversionnewvalue.Text = dataVersionList[0];
                }
                if (!string.IsNullOrEmpty(headInfoList[headInfoList.Keys.First()].sTrackCode))
                {
                    trackcodevalue.Text = headInfoList[headInfoList.Keys.First()].sTrackCode;
                    trackcodenewvalue.Text = headInfoList[headInfoList.Keys.First()].sTrackCode;
                }
                else
                {
                    trackcodevalue.Text = "";
                    trackcodenewvalue.Text = "";
                }

                if (!string.IsNullOrEmpty(headInfoList[headInfoList.Keys.First()].sTrain))
                {
                    traincodevalue.Text = headInfoList[headInfoList.Keys.First()].sTrain;
                    traincodenewvalue.Text = headInfoList[headInfoList.Keys.First()].sTrain;
                }
                else
                {
                    traincodevalue.Text = "";
                    traincodenewvalue.Text = "";
                }

                List<string> trackNameList = (from r in headInfoList.Values select r.sTrackName).ToList<string>().Distinct().ToList<string>();
                if (trackNameList.Count > 1)
                {
                    tracknamevalue.Text = "数据不统一";
                    tracknamenewvalue.Text = "";
                }
                else
                {
                    tracknamevalue.Text = trackNameList[0];
                    tracknamenewvalue.Text = trackNameList[0];
                }

                List<int> dirList = (from r in headInfoList.Values select r.iDir).ToList<int>().Distinct().ToList<int>();
                if (dirList.Count > 1)
                {
                    dirvalue.Text = "数据不统一";
                    cbxUpDown.SelectedIndex = 0;
                }
                else
                {
                    dirvalue.Text = dirList[0].ToString();
                    cbxUpDown.SelectedValue = dirList[0];
                }
                List<int> runDirList = (from r in headInfoList.Values select r.iRunDir).ToList<int>().Distinct().ToList<int>();
                if (runDirList.Count > 1)
                {
                    rundirvalue.Text = "数据不统一";
                    cbxRunDir.SelectedIndex = 0;
                }
                else
                {
                    rundirvalue.Text = runDirList[0].ToString();
                    cbxRunDir.SelectedValue = runDirList[0];
                }
                List<int> kmIncList = (from r in headInfoList.Values select r.iKmInc).ToList<int>().Distinct().ToList<int>();
                if (kmIncList.Count > 1)
                {
                    kmincvalue.Text = "数据不统一";
                    cbxKmInc.SelectedIndex = 0;
                }
                else
                {
                    kmincvalue.Text = kmIncList[0].ToString();
                    cbxKmInc.SelectedValue = kmIncList[0];
                }
            }
                
        }

        private void BatchExportCitForm_Load(object sender, EventArgs e)
        {
            InnerFileOperator.InnerFilePath = Application.StartupPath + "\\Line.accdb";
            InnerFileOperator.InnerConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Jet OLEDB:Engine Type=5";
            AggregateExceptionCatched += BatchExportCitForm_AggregateExceptionCatched;
            Task task = Task.Factory.StartNew(() => LoadLineData());

            task.ContinueWith((t) =>
            {
                if (t.IsFaulted)
                {
                    MessageBox.Show("错误：" + t.Exception.InnerException.Message);
                }
            }, TaskContinuationOptions.OnlyOnFaulted);

            

            InitDropdownList();
            //获得当前运行的Assembly
            string channelXmlPath = Application.StartupPath + "//CitChannel.xml";
            citChannelInfo = common.GetChannelDefion(channelXmlPath);
            if (citChannelInfo == null)
            {
                MessageBox.Show("配置文件丢失，找不到CitChannel.xml");
                return;
            }

            
            if (_isSingle)
            {
                exportCitPath = bnyFileInfo.Name.Replace(".bny", ".cit");


                txtExportPath.Text = bnyFileInfo.FullName.Replace(".bny",".cit");
                fileExportPath = txtExportPath.Text;
                this.Height -= 40;
                for (int i = 0; i < this.Controls.Count; i++)
                {
                    this.Controls[i].Location = new Point(this.Controls[i].Location.X, this.Controls[i].Location.Y - 40);
                }
                groupBox2.Visible = false;
                btnSelectExportPath.Text = "导出路径";
                labExport.Text = "选择路径：";
                this.Text = "导出cit文件";
                headInfoList.Clear();
                errorProvider.Clear();
                isCancel = false;
                string error = "";
                try
                {
                    task.Wait();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("获取线路数据时出错：" + ex.Message);
                    return;
                }
                DataHeadInfo headInfo = _citCreater.InitDataHead(bnyFileInfo, ref error);
                if (headInfo != null)
                {
                    headInfoList.Add(bnyFileInfo, headInfo);
                }
                if (!string.IsNullOrEmpty(error))
                {
                    MessageBox.Show(error);
                }
                DisplayFileInfo();

            }
        }

        

        private void InitDropdownList()
        {
            Dictionary<string, int> fileType = new Dictionary<string, int>();
            fileType.Add("", 0);
            fileType.Add("轨检", 1);
            fileType.Add("动力学", 2);
            fileType.Add("弓网", 3);
            BindingSource typeSource = new BindingSource();
            typeSource.DataSource = fileType;
            cbxFileType.DataSource = typeSource;
            cbxFileType.DisplayMember = "Key";
            cbxFileType.ValueMember = "Value";
            Dictionary<string,int> upDown = new Dictionary<string,int>();
            upDown.Add("",0);
            upDown.Add("上行",1);
            upDown.Add("下行",2);
            upDown.Add("单线",3);
            BindingSource upDownSource = new BindingSource();
            upDownSource.DataSource = upDown;
            cbxUpDown.DataSource = upDownSource;
            cbxUpDown.DisplayMember = "Key";
            cbxUpDown.ValueMember = "Value";

            Dictionary<string, int> runDir = new Dictionary<string, int>();
            runDir.Add("", -1);
            runDir.Add("正", 0);
            runDir.Add("反", 1);
            BindingSource runDirSource = new BindingSource();
            runDirSource.DataSource = runDir;
            cbxRunDir.DataSource = runDirSource;
            cbxRunDir.DisplayMember = "Key";
            cbxRunDir.ValueMember = "Value";

            Dictionary<string, int> kmInc = new Dictionary<string, int>();
            kmInc.Add("", -1);
            kmInc.Add("增里程", 0);
            kmInc.Add("减里程", 1);
            BindingSource kmIncSource = new BindingSource();
            kmIncSource.DataSource = kmInc;
            cbxKmInc.DataSource = kmIncSource;
            cbxKmInc.DisplayMember = "Key";
            cbxKmInc.ValueMember = "Value";
            
        }

        void BatchExportCitForm_AggregateExceptionCatched(object sender, AggregateExceptionArgs e)
        {
            string ex = "";
            foreach (var item in e.AggregateException.InnerExceptions)
            {
                ex += string.Format("异常类型：{0}{1}来自：{2}{3}异常内容：{4}",
            item.GetType(), Environment.NewLine, item.Source,
            Environment.NewLine, item.Message);
            }
            MessageBox.Show(ex);
        }

        private void LoadLineData()
        {
            DataTable dt = InnerFileOperator.Query("select LineName,LineCode from line");
            _citCreater = new CitCreater(dt);
        }

        private void cbxFileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxFileType.SelectedIndex >= 0)
            {
                if (cbxFileType.SelectedItem != null && !string.IsNullOrEmpty(cbxFileType.SelectedItem.ToString()))
                {
                    foreach (var header in headInfoList.Values)
                    {
                        header.iDataType = (int)cbxFileType.SelectedValue;
                    }
                    DisplayFileInfo();
                }
            }
        }

        private void SetDataVersion(string verstion)
        {
            if (!string.IsNullOrEmpty(verstion))
            {
                foreach (var header in headInfoList.Values)
                {
                    header.sDataVersion = verstion;
                }
                DisplayFileInfo();
            }
        }

        private void SetTrackCode(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                foreach (var header in headInfoList.Values)
                {
                    header.sTrackCode = code;
                }
                errorProvider.Clear();
                DisplayFileInfo();
            }
        }

        private void SetTrackName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                foreach (var header in headInfoList.Values)
                {
                    header.sTrackName = name;
                }
                DisplayFileInfo();
            }
        }

        private void SetTrain(string TrainCode)
        {
            if (!string.IsNullOrEmpty(TrainCode))
            {
                foreach (var header in headInfoList.Values)
                {
                    header.sTrain = TrainCode;
                }
                errorProvider.Clear();
                DisplayFileInfo();
            }
        }

        private void dataversionnewvalue_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetDataVersion(dataversionnewvalue.Text.Trim());
            }
        }

        private void dataversionnewvalue_Leave(object sender, EventArgs e)
        {
            SetDataVersion(dataversionnewvalue.Text.Trim());
        }

        private void trackcodenewvalue_Leave(object sender, EventArgs e)
        {
            SetTrackCode(trackcodenewvalue.Text.Trim());
        }

        private void trackcodenewvalue_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode== Keys.Enter)
            {
                SetTrackCode(trackcodenewvalue.Text.Trim());
            }
        }

        private void tracknamenewvalue_Leave(object sender, EventArgs e)
        {
            SetTrackName(tracknamenewvalue.Text.Trim());
        }

        private void cbxUpDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxUpDown.SelectedIndex > 0)
            {
                if (cbxUpDown.SelectedItem != null && !string.IsNullOrEmpty(cbxUpDown.SelectedItem.ToString()))
                {
                    foreach (var header in headInfoList.Values)
                    {
                        header.iDir = (int)cbxUpDown.SelectedValue;
                    }
                    DisplayFileInfo();
                }
            }
        }

        private void traincodenewvalue_Leave(object sender, EventArgs e)
        {
            SetTrain(traincodenewvalue.Text.Trim());
        }

        private void traincodenewvalue_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode== Keys.Enter)
            {
                SetTrain(traincodenewvalue.Text.Trim());
            }
        }

        private void cbxRunDir_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRunDir.SelectedIndex > 0)
            {
                if (cbxRunDir.SelectedItem != null && !string.IsNullOrEmpty(cbxRunDir.SelectedItem.ToString()))
                {
                    foreach (var header in headInfoList.Values)
                    {
                        header.iRunDir = (int)cbxRunDir.SelectedValue;
                    }
                    DisplayFileInfo();
                }
            }
        }

        private void cbxKmInc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxKmInc.SelectedIndex > 0)
            {
                if (cbxKmInc.SelectedItem != null && !string.IsNullOrEmpty(cbxKmInc.SelectedItem.ToString()))
                {
                    foreach (var header in headInfoList.Values)
                    {
                        header.iKmInc = (int)cbxKmInc.SelectedValue;
                    }
                    DisplayFileInfo();
                }
            }
        }

        private void btnSelectExportPath_Click(object sender, EventArgs e)
        {
            if (_isSingle)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "cit 文件|*.cit";
                if(saveDialog.ShowDialog()== DialogResult.OK)
                {
                    txtExportPath.Text = saveDialog.FileName;
                    fileExportPath = saveDialog.FileName;
                }
            }
            else
            {
                folderBrowserExportDialog.SelectedPath = folderBrowserImportDialog.SelectedPath;
                if (folderBrowserExportDialog.ShowDialog() == DialogResult.OK)
                {
                    txtExportPath.Text = folderBrowserExportDialog.SelectedPath;
                    fileExportPath = txtExportPath.Text;
                    errorProvider.Clear();
                }
            }
        }

      
        
        private void btnBgeinExport_Click(object sender, EventArgs e)
        {
            channelDataList.Clear();
            isCancel = false;
            tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;
            _mainForm.tipsCount = 0;
            int cpuCount = Environment.ProcessorCount;
            if (!_isSingle)
            {
                if (string.IsNullOrEmpty(txtImportPath.Text.Trim()) || headInfoList.Count <= 0)
                {
                    errorProvider.SetError(txtImportPath, "请选择bny导出目录！！");
                    return;
                }
            }
            if (string.IsNullOrEmpty(txtExportPath.Text.Trim()) || string.IsNullOrEmpty(fileExportPath))
            {
                errorProvider.SetError(txtExportPath, "请选择cit导出目录！");
                return;
            }
            if (string.IsNullOrEmpty(trackcodevalue.Text.Trim()) || string.IsNullOrEmpty(traincodevalue.Text.Trim()))
            {
                if(string.IsNullOrEmpty(trackcodevalue.Text.Trim()))
                {
                    errorProvider.SetError(trackcodevalue, "值不能为空！");
                }
                if(string.IsNullOrEmpty(traincodevalue.Text.Trim()))
                {
                    errorProvider.SetError(traincodevalue, "值不能为空！");
                }
                return;
            }
            int completeCount = 0;
            int errorCount = 0;
            int cancelCount = 0;
            if (headInfoList.Count > cpuCount)
            {
                int pageCount = 0;
                if( headInfoList.Count % cpuCount==0)
                {
                    pageCount = headInfoList.Count / cpuCount;
                }
                else
                {
                    pageCount = (headInfoList.Count / cpuCount) + 1;
                }
                List<List<FileInfo>> groupList = new List<List<FileInfo>>();
                int index = 0;
                List<FileInfo> files = new List<FileInfo>();
                foreach (var item in headInfoList.Keys.ToList())
                {
                    files.Add(item);
                    index++;
                    if (index == pageCount)
                    {
                        groupList.Add(new List<FileInfo>(files));
                        files.Clear();
                        index = 0;
                    }
                }
                if (files.Count > 0)
                {
                    groupList.Add(new List<FileInfo>(files));
                }
               
                foreach(var list in groupList)
                {
                    Task t1 = Task.Factory.StartNew(() => CreateCit(list), token);
                    t1.ContinueWith((t) =>
                    {
                        if (t.Exception != null && t.Exception.InnerException is OperationCanceledException)
                        {
                            _mainForm.ShowTips("任务组：" + t1.Id + "已经被取消");
                            cancelCount++;
                        }
                        else if (t.Exception != null)
                        {
                            _mainForm.ShowTips("任务组：" + t1.Id + "出现错误：" + t.Exception.InnerException.Message);
                            errorCount++;
                        }
                        else
                        {
                            completeCount++;
                            _mainForm.ShowTips("任务组：" + t1.Id + "导出完成");
                        }
                        if (((completeCount + errorCount + cancelCount) == groupList.Count))
                        {
                            _mainForm.ShowTips("批量导出cit文件已全部完成， " + completeCount + "个完成，" + cancelCount + "个取消， " + errorCount + "个失败！");
                            MessageBox.Show("批量导出cit文件已全部完成， " + completeCount + "个完成，" + cancelCount + "个取消， " + errorCount + "个失败！");
                            if (!IsDisposed && this.IsHandleCreated && this.btnBgeinExport.IsHandleCreated)
                            {
                                this.Invoke(new Action(() =>
                                {
                                    btnBgeinExport.Enabled = true;
                                    progressBar1.Visible = false;
                                }));
                            }
                        }
                        
                    });
                }
                _mainForm.ShowTips("开始导出，一共需要处理：" + groupList.Count + "组数据,每组处理完成后会单独通知您！");
                btnBgeinExport.Enabled = false;
                progressBar1.Visible = true;
            }
            else
            {
                foreach (var file in headInfoList.Keys.ToList())
                {

                    channelDataList.Add(file.FullName, new Queue<List<float[]>>());
                    Task t1 = Task.Factory.StartNew(() => CreateCit(file,0), token);
                    t1.ContinueWith((t) =>
                    {
                        //嵌套几次task，InnerException也会相应嵌套
                        if (t.Exception != null && t.Exception.InnerException.InnerException is OperationCanceledException)
                        {
                            _mainForm.ShowTips("文件任务：" + t1.Id + "已经被取消");
                            cancelCount++;
                        }
                        else if (t.Exception != null)
                        {
                            _mainForm.ShowTips("文件任务：" + t1.Id + "出现错误：" + t.Exception.InnerException.InnerException.Message);
                            errorCount++;
                        }
                        else
                        {
                            _mainForm.ShowTips("文件任务：" + t1.Id + "导出完成");
                            completeCount++;
                        }
                        if (((completeCount + errorCount + cancelCount) == headInfoList.Count))
                        {
                            _mainForm.ShowTips("批量导出cit文件已全部完成， " + completeCount + "个完成，" + cancelCount + "个取消， " + errorCount + "个失败！");
                            MessageBox.Show("批量导出cit文件已全部完成， " + completeCount + "个完成，" + cancelCount + "个取消， " + errorCount + "个失败！");
                            if (!IsDisposed && this.IsHandleCreated && this.btnBgeinExport.IsHandleCreated)
                            {
                                this.Invoke(new Action(() =>
                                {
                                    btnBgeinExport.Enabled = true;
                                    progressBar1.Visible = false;
                                }));
                            }
                        }
                        
                    });
                }
                _mainForm.ShowTips("开始导出，一共需要处理：" + headInfoList.Count + "个文件，每个文件完成后会通知您！");
                btnBgeinExport.Enabled = false;
                progressBar1.Visible = true;
            }
            
            
        }

        private void CreateCit(List<FileInfo> fileList)
        {
            
            foreach (var file in fileList)
            {
                lock (lockOjbect)
                {
                    channelDataList.Add(file.FullName, new Queue<List<float[]>>());
                }
                Task task = Task.Factory.StartNew(() => CreateCit(file,0), token, TaskCreationOptions.AttachedToParent, TaskScheduler.Default);

                try
                {
                    task.Wait();
                }
                catch (AggregateException ex)
                {
                    if (ex != null && ex.InnerException.InnerException is OperationCanceledException)
                    {
                        _mainForm.ShowTips("任务：" + task.Id + "已经被取消");
                    }
                    else if (ex != null)
                    {
                        _mainForm.ShowTips("任务：" + task.Id + "出现错误：" + ex.InnerException.InnerException.Message);
                    }
                }
            }
        }

        private void CreateCit(FileInfo file)
        {
            if (token.IsCancellationRequested || isCancel)
            {
                token.ThrowIfCancellationRequested();
                throw new OperationCanceledException();
            }
            else
            {
                try
                {
                    string citPath = fileExportPath + "\\" + file.Name.Substring(0, file.Name.IndexOf('.')) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".cit";
                    WriteCitHeadAndChannelInfo(citPath, headInfoList[file], citChannelInfo);
                    List<ChannelNew> channelNewList = GetChannelNewInfo(citChannelInfo);
                    GetBnyDataAndWriteCit(citPath, file.FullName, channelNewList);
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void CreateCit(FileInfo file,int taskId)
        {
            if (token.IsCancellationRequested || isCancel)
            {
                token.ThrowIfCancellationRequested();
                throw new OperationCanceledException();
            }
            else
            {
                try
                {
                    string citPath = "";
                    if (_isSingle)
                    {
                        citPath = Path.GetDirectoryName(fileExportPath);
                        if(File.Exists(citPath))
                        {
                            File.Delete(citPath);
                        }
                        citPath = fileExportPath;
                    }
                    else
                    {
                        citPath = fileExportPath + "\\" + file.Name.Substring(0, file.Name.LastIndexOf('.')) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".cit";

                        citPath = fileExportPath + "\\" + file.Name.Replace(".bny", ".cit");
                    }
                    WriteCitHeadAndChannelInfo(citPath, headInfoList[file], citChannelInfo);
                    List<ChannelNew> channelNewList = GetChannelNewInfo(citChannelInfo);
                    Task tWrite = Task.Factory.StartNew(() => WriteBnyData(file.FullName, citPath, channelNewList));
                    Task tRead = Task.Factory.StartNew(() => GetBnyData(file.FullName, channelNewList));
                    
                    Task.WaitAll(tRead, tWrite);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
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
                        channel.BNYChannelId = 2;
                        channel.BNYChannelName = "综合里程";
                        break;
                    case "M":
                        channel.BNYChannelId = 2;
                        channel.BNYChannelName = "综合里程";
                        break;
                    case "里程":
                        channel.BNYChannelId = 2;
                        channel.BNYChannelName = "综合里程";
                        break;
                    case "SPEED":
                        channel.BNYChannelId = 3;
                        channel.BNYChannelName = "V（速度）";
                        break;
                    case "CB_Lt_R_11":
                        channel.BNYChannelId = 8;
                        channel.BNYChannelName = "车体横加2";
                        break;
                    case "CB_Vt_R_11":
                        channel.BNYChannelId = 9;
                        channel.BNYChannelName = "车体垂加2";
                        break;
                    case "CB_Lg_R_11":
                        channel.BNYChannelId = 10;
                        channel.BNYChannelName = "车体纵加2";
                        break;
                    case "Fr_Vt_L_11":
                        channel.BNYChannelId = 12;
                        channel.BNYChannelName = "构架垂加2";
                        break;
                    case "Fr_Lt_L_11":
                        channel.BNYChannelId = 13;
                        channel.BNYChannelName = "构架横加2";
                        break;
                    case "AB_Vt_L_11":
                        channel.BNYChannelId = 4;
                        channel.BNYChannelName = "1左垂力";
                        break;
                    case "AB_Vt_R_11":
                        channel.BNYChannelId = 6;
                        channel.BNYChannelName = "1右垂力";
                        break;
                    case "AB_Lt_L_11":
                        channel.BNYChannelId = 5;
                        channel.BNYChannelName = "1左横力";
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


        /// <summary>
        /// 获取BNY数据并向cit文件中写入数据
        /// </summary>
        /// <param name="channelList"></param>
        private void GetBnyDataAndWriteCit(string citPath,string bnyPath,List<ChannelNew> channelList)
        {
            CitFileHelper citHelper = new CitFileHelper();
            int[] bnyChannelArr = channelList.Where(s => s.BNYChannelId >= 0).Select(s => s.BNYChannelId).ToArray();
            int sampleNum = 50000;
            long startPosition = 0;
            long endPosition = 0;

            long bnyFileLength = common.GetBNYLastPosition(bnyPath);

            int pageCount =0;
            double realPageCount = bnyFileLength * 1.0 / (sampleNum * BNYFile.GetChannelSize());
            if (realPageCount % ((int)realPageCount) > 0)
            {
                pageCount = ((int)realPageCount) + 1;
            }
            else
            {
                pageCount = (int)realPageCount;
            }

            List<float[]> bnyList = new List<float[]>();
            List<float[]> resultList = new List<float[]>();
            float[] fvalue = new float[sampleNum];
            for (int i = 0; i < pageCount; i++)
            {
                bnyList = common.GetBNYData(bnyPath, sampleNum, startPosition, ref endPosition);
                startPosition = endPosition;
                resultList.Clear();
                for (int j = 0; j < channelList.Count; j++)
                {
                    if (channelList[j].BNYChannelId >= 0)
                    {
                        if (bnyList[channelList[j].BNYChannelId].Length < sampleNum)
                        {
                            fvalue = new float[bnyList[channelList[j].BNYChannelId].Length];
                            if (channelList[j].BNYChannelId == 2)
                            {
                                if (channelList[j].dataChannelInfo.sNameEn == "KM")
                                {
                                    fvalue = bnyList[channelList[j].BNYChannelId].Select(p => ((float)(int)p)).ToArray();
                                }
                                else if (channelList[j].dataChannelInfo.sNameEn == "M")
                                {
                                    fvalue = bnyList[channelList[j].BNYChannelId].Select(p => (p - (int)p)*1000).ToArray();
                                }
                                else
                                {
                                    fvalue = bnyList[channelList[j].BNYChannelId];
                                }
                            }
                            else
                            {
                                fvalue = bnyList[channelList[j].BNYChannelId];
                            }
                            
                           
                        }
                        else
                        {
                            if (channelList[j].BNYChannelId == 2)
                            {
                                if (channelList[j].dataChannelInfo.sNameEn == "KM")
                                {
                                    fvalue = bnyList[channelList[j].BNYChannelId].Select(p => ((float)(int)p)).ToArray();
                                }
                                else if (channelList[j].dataChannelInfo.sNameEn == "M")
                                {
                                    fvalue = bnyList[channelList[j].BNYChannelId].Select(p => (p - (int)p)*1000).ToArray();
                                }
                                else
                                {
                                    fvalue = bnyList[channelList[j].BNYChannelId];
                                }
                            }
                            else
                            {
                                fvalue = bnyList[channelList[j].BNYChannelId];
                            }
                        }
                       
                    }
                    else
                    {
                        fvalue = new float[bnyList[0].Length];
                    }
                    resultList.Add(fvalue);
                }
                citHelper.WriteChannelDataFloat(citPath, resultList);
                if (tokenSource.IsCancellationRequested)
                {
                    throw new OperationCanceledException();
                }
            }
        }

        private void GetBnyData(string bnyPath, List<ChannelNew> channelList)
        {
            int sampleNum = 5000;
            long startPosition = 0;
            long endPosition = 0;

            long bnyFileLength = common.GetBNYLastPosition(bnyPath);

            int pageCount = 0;
            double realPageCount = bnyFileLength * 1.0 / (sampleNum * BNYFile.GetChannelSize());
            if (realPageCount % ((int)realPageCount) > 0)
            {
                pageCount = ((int)realPageCount) + 1;
            }
            else
            {
                pageCount = (int)realPageCount;
            }

            List<float[]> bnyList = new List<float[]>();
            List<float[]> resultList = new List<float[]>();
            float[] fvalue = new float[sampleNum];
            for (int i = 0; i < pageCount; i++)
            {
                bnyList = common.GetBNYData(bnyPath, sampleNum, startPosition, ref endPosition);
                startPosition = endPosition;
                resultList.Clear();
                for (int j = 0; j < channelList.Count; j++)
                {
                    if (channelList[j].BNYChannelId >= 0)
                    {
                        if (bnyList[channelList[j].BNYChannelId].Length < sampleNum)
                        {
                            fvalue = new float[bnyList[channelList[j].BNYChannelId].Length];
                            if (channelList[j].BNYChannelId == 2)
                            {
                                if (channelList[j].dataChannelInfo.sNameEn == "KM")
                                {
                                    fvalue = bnyList[channelList[j].BNYChannelId].Select(p => ((float)(int)p)).ToArray();
                                }
                                else if (channelList[j].dataChannelInfo.sNameEn == "M")
                                {
                                    fvalue = bnyList[channelList[j].BNYChannelId].Select(p => (p - (int)p)*1000).ToArray();
                                }
                                else
                                {
                                    fvalue = bnyList[channelList[j].BNYChannelId];
                                }
                            }
                            else
                            {
                                fvalue = bnyList[channelList[j].BNYChannelId];
                            }


                        }
                        else
                        {
                            if (channelList[j].BNYChannelId == 2)
                            {
                                if (channelList[j].dataChannelInfo.sNameEn == "KM")
                                {
                                    fvalue = bnyList[channelList[j].BNYChannelId].Select(p => ((float)(int)p)).ToArray();
                                }
                                else if (channelList[j].dataChannelInfo.sNameEn == "M")
                                {
                                    fvalue = bnyList[channelList[j].BNYChannelId].Select(p => (p - (int)p)*1000).ToArray();
                                }
                                else
                                {
                                    fvalue = bnyList[channelList[j].BNYChannelId];
                                }
                            }
                            else
                            {
                                fvalue = bnyList[channelList[j].BNYChannelId];
                            }
                        }

                    }
                    else
                    {
                        fvalue = new float[bnyList[0].Length];
                    }
                    resultList.Add(fvalue);
                   
                }
                if (channelDataList.ContainsKey(bnyPath))
                {
                    channelDataList[bnyPath].Enqueue(new List<float[]>(resultList));
                }
                if (tokenSource.IsCancellationRequested)
                {
                    token.ThrowIfCancellationRequested();
                    break;
                }
                int count = channelDataList[bnyPath].Count / 10;
                if (count > 1)
                {
                    Thread.Sleep(300 * count);
                }
            }
            channelDataList[bnyPath].Enqueue(null);
        }

        private void WriteBnyData(string fileName, string citPath, List<ChannelNew> channelNewList)
        {
            CitFileHelper citHelper = new CitFileHelper();
            using (FileStream fs = new FileStream(citPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            {
                using (BinaryWriter bw = new BinaryWriter(fs, Encoding.Default))
                {
                    DataHeadInfo dhi = citHelper.GetDataInfoHead(citPath);
                    long pos = bw.BaseStream.Length;
                    List<DataChannelInfo> channelList = citHelper.GetDataChannelInfoHead(citPath);
                    int iChannelNumberSize = dhi.iChannelNumber * 2;
                    byte[] dataArray = new byte[iChannelNumberSize];

                    List<Byte> dataList = new List<Byte>();
                    short tmpRmsData = 0; 

                    Byte[] tmpBytes = new Byte[2];
                    while (true)
                    {
                        if (channelDataList.ContainsKey(fileName) && channelDataList[fileName].Count > 0)
                        {
                            List<float[]> channelData = channelDataList[fileName].Dequeue();
                            DateTime dt2 = DateTime.Now;
                            if (channelData == null || isCancel)
                            {
                                break;
                            }
                            long iArrayLen = channelData[0].Length;
                            for (int k = 0; k < iArrayLen; k++)
                            {
                                if (citHelper.IsEncrypt(dhi))
                                {
                                    for (int iTmp = 0; iTmp < channelNewList.Count; iTmp++)
                                    {
                                        if (channelNewList[iTmp].BNYChannelId > 6)
                                        {
                                            tmpRmsData = (short)((channelData[iTmp][k] / 10 - channelList[iTmp].fOffset) * channelList[iTmp].fScale);
                                        }
                                        else
                                        {
                                            tmpRmsData = (short)((channelData[iTmp][k] - channelList[iTmp].fOffset) * channelList[iTmp].fScale);
                                        }
                                        tmpBytes = CitFileHelper.ByteXORByte(BitConverter.GetBytes(tmpRmsData));
                                        dataList.AddRange(tmpBytes);
                                    }
                                }
                                else
                                {
                                    for (int iTmp = 0; iTmp < channelNewList.Count; iTmp++)
                                    {
                                        try
                                        {
                                            if (channelNewList[iTmp].BNYChannelId > 6)
                                            {
                                                tmpRmsData = (short)((channelData[iTmp][k] / 10 - channelList[iTmp].fOffset) * channelList[iTmp].fScale);
                                            }
                                            else
                                            {
                                                tmpRmsData = (short)((channelData[iTmp][k] - channelList[iTmp].fOffset) * channelList[iTmp].fScale);
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
                            
                        }
                       
                    }
                    //bw.Close();
                    //DateTime dt3 = DateTime.Now;
                    //Console.WriteLine("two :" + (dt3 - dt2).TotalSeconds.ToString());
                }
               
                //fs.Close();
            }

        }
    

        private void btnCancelExport_Click(object sender, EventArgs e)
        {
            tokenSource.Cancel();
            isCancel = true;
            btnBgeinExport.Enabled = true;
            progressBar1.Visible = false;

        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }
    }
}
