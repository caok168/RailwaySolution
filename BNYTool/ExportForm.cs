using BNYTool.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BNYTool
{
    public partial class ExportForm : Form
    {
        Common common = new Common();

        List<BNYChannel> channelList = new List<BNYChannel>();

        List<BNYChannel> channelListSelect = new List<BNYChannel>();

        MainForm _mainForm = null;

        private CancellationTokenSource tokenSource = new CancellationTokenSource();
        private CancellationToken token = new CancellationToken();

        string bnyFilePath = "";
        bool isInc = false;

        public ExportForm()
        {
            InitializeComponent();
        }

        public ExportForm(string bnyFile,bool isIncMile,MainForm mainFrom)
        {
            InitializeComponent();

            bnyFilePath = bnyFile;
            isInc = isIncMile;
            _mainForm = mainFrom;
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        

        /// <summary>
        /// 点击全选事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_all_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_all.Checked)
            {
                listView1.Focus();
                foreach (ListViewItem item in listView1.Items)
                {
                    item.Checked = true;
                }
            }
            else
            {
                listView1.Focus();
                foreach (ListViewItem item in listView1.Items)
                {
                    item.Checked = false;
                }
            }
        }

        /// <summary>
        /// 导出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            btnExport.Enabled = false;
            string startMile = this.txt_StartMile.Text.Trim();
            string endMile = this.txt_EndMile.Text.Trim();
            tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;
            GetChannelList();

            if (String.IsNullOrEmpty(this.txt_ExportPath.Text.Trim()))
            {
                MessageBox.Show("文件路径不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (String.IsNullOrWhiteSpace(startMile) && String.IsNullOrWhiteSpace(startMile))
            {
                MessageBox.Show("开始里程与结束里程不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                string txtFileName = this.txt_ExportPath.Text;
                if(File.Exists(txtFileName))
                {
                    File.Delete(txtFileName);
                }
                float startM = 0;
                float endM = 0;
                if (float.TryParse(startMile, out startM) && float.TryParse(endMile, out endM))
                {
                    if (startM > 0 && endM > 0)
                    {
                        
                        if(!txtFileName.EndsWith(".txt"))
                        {
                            txtFileName += ".txt";
                        }
                        Task task = Task.Factory.StartNew(() => ExportBnyToTxt(txtFileName, startM, endM), token);
                        task.ContinueWith((t) =>
                        {
                            if (t.Exception != null && t.Exception.InnerException is OperationCanceledException || t.IsCanceled)
                            {
                                _mainForm.ShowTips("导出txt任务已取消！");
                            }
                            else if (t.Exception != null||t.IsFaulted)
                            {
                                _mainForm.ShowTips("导出txt时出现错误:" + t.Exception.InnerException.Message);
                            }
                            else
                            {
                                MessageBox.Show("导出完成！");
                                _mainForm.ShowTips("导出txt文件完成！");
                                if (!IsDisposed && this.IsHandleCreated && this.txt_ExportPath.IsHandleCreated)
                                {
                                    this.Invoke(new Action(() => { txt_ExportPath.Text = ""; }));
                                }
                            }
                            if (!IsDisposed && this.IsHandleCreated && this.btnExport.IsHandleCreated)
                            {
                                this.Invoke(new Action(() =>
                                {
                                    btnExport.Enabled = true;
                                }));
                            }

                        });
                    }
                    else
                    {
                        MessageBox.Show("里程不能为负数！");
                    }
                    _mainForm.ShowTips("开始导出：里程范围" + startM + "->" + endM + "，系统将会在完成后通知您！");
                }
                else
                {
                    MessageBox.Show("开始里程或结束里程为非数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            
        }

        private void ExportBnyToTxt(string filePath,float startMil,float endMil)
        {
            long[] positions = common.GetMilePostions(bnyFilePath, isInc, startMil, endMil);
            if (positions[0] == 0 && positions[1] == 0)
            {
                throw new ArgumentException("开始里程或结束里程错误");
            }
            long endPostion = 0;
            int sampleCount = 10000;
            var dataList = common.GetBNYData(bnyFilePath, sampleCount, positions[0], ref endPostion);
            if (endPostion < positions[1])
            {
                long startPostion = 0;
                List<float[]> dataListSelect = new List<float[]>();
                string[] channelNames = new string[channelListSelect.Count];
                for (int i = 0; i < channelListSelect.Count; i++)
                {
                    dataListSelect.Add(dataList[channelListSelect[i].ID - 1]);
                    channelNames[i] = channelListSelect[i].ChannelName;
                }
                common.ExportChannelDataText(filePath, channelNames);
                common.ExportDataTxt(filePath, dataListSelect);
                while (true)
                {
                    startPostion = endPostion;
                    dataListSelect.Clear();
                    dataList = common.GetBNYData(bnyFilePath, sampleCount, startPostion, ref endPostion);
                    for (int i = 0; i < channelListSelect.Count; i++)
                    {
                        dataListSelect.Add(dataList[channelListSelect[i].ID - 1]);
                        if (tokenSource.IsCancellationRequested)
                        {
                            token.ThrowIfCancellationRequested();
                        }
                    }
                    if (endPostion < positions[1])
                    {
                        common.ExportDataTxt(filePath, dataListSelect);
                    }
                    else
                    {
                        dataListSelect.Clear();
                        dataList = common.GetBNYData(bnyFilePath, startPostion, positions[1]);
                        for (int i = 0; i < channelListSelect.Count; i++)
                        {
                            dataListSelect.Add(dataList[channelListSelect[i].ID - 1]);
                            if (tokenSource.IsCancellationRequested)
                            {
                                token.ThrowIfCancellationRequested();
                            }
                        }
                        common.ExportDataTxt(filePath, dataListSelect);
                        break;
                    }
                    if(tokenSource.IsCancellationRequested)
                    {
                        token.ThrowIfCancellationRequested();
                    }
                }
            }
            else
            {
                dataList = common.GetBNYData(bnyFilePath, positions[0], positions[1]);
                List<float[]> dataListSelect = new List<float[]>();
                string[] channelNames = new string[channelListSelect.Count];

                for (int i = 0; i < channelListSelect.Count; i++)
                {
                    dataListSelect.Add(dataList[channelListSelect[i].ID - 1]);
                    channelNames[i] = channelListSelect[i].ChannelName;
                }
                common.ExportChannelDataText(filePath, channelNames);
                common.ExportDataTxt(filePath, dataListSelect);
            }
            
        }

        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            btnExport.Enabled = true;
            tokenSource.Cancel();
        }

        /// <summary>
        /// 选择文件夹路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Select_Click(object sender, EventArgs e)
        {
            saveFileDialog.InitialDirectory = bnyFilePath;
            if(saveFileDialog.ShowDialog()== System.Windows.Forms.DialogResult.OK)
            {
                this.txt_ExportPath.Text = saveFileDialog.FileName;
            }
           
        }


        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            string[] channelNames = common.GetChannelNames();

            channelList = common.GetChannelNameList();

            listView1.BeginUpdate();

            for (int i = 0; i < channelList.Count; i++)
            {
                listView1.Items.Add(channelList[i].ID.ToString());
                listView1.Items[listView1.Items.Count - 1].SubItems.Add(channelList[i].ChannelName);
                Application.DoEvents();
            }

            listView1.EndUpdate();
            if (!string.IsNullOrEmpty(bnyFilePath.Trim()))
            {
                
                
                float startMileage = common.GetBNYStartMile(bnyFilePath);
                float endMileage = common.GetBNYEndMile(bnyFilePath);
                txt_StartMile.Text = ((double)startMileage).ToString();
                txt_EndMile.Text = ((double)endMileage).ToString();
            }
        }

        /// <summary>
        /// 获取选中的通道集合
        /// </summary>
        private void GetChannelList()
        {
            channelListSelect.Clear();

            for (int i = 0; i < listView1.CheckedItems.Count; i++)
            {
                channelListSelect.Add(new Model.BNYChannel { ID = Convert.ToInt32(listView1.CheckedItems[i].SubItems[0].Text), ChannelName = listView1.CheckedItems[i].SubItems[0].Text });
            }

        }
    }
}
