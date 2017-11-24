using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BNYTool
{
    public partial class MainForm : Form
    {
        Common common = new Common();
        /// <summary>
        /// BNY文件路径
        /// </summary>
        string bnyPath = "";
        /// <summary>
        /// 是否为增里程
        /// </summary>
        bool isInc = false;

        /// <summary>
        /// 文件开始位置
        /// </summary>
        long startPosition = 0;
        /// <summary>
        /// 文件结束位置
        /// </summary>
        long endPosition = 0;

        /// <summary>
        /// 当前页
        /// </summary>
        int pageIndex = 1;
        /// <summary>
        /// 一页显示的行数
        /// </summary>
        int rowCount = 500;
        /// <summary>
        /// 总页数
        /// </summary>
        int pageCount;

        /// <summary>
        /// 提示消息的个数
        /// </summary>
        public int tipsCount = 0;

        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.btnPrev.Enabled = false;
        }

        /// <summary>
        /// 调整窗体大小事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Resize(object sender, EventArgs e)
        {
            dataGridView1.Height = this.ClientSize.Height - 150;
            dataGridView1.Width = this.ClientSize.Width - 15;
        }

        /// <summary>
        /// 选择BNY文件按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "bny 文件|*.bny";
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                txtBNYFilePath.Text = openFileDialog1.FileName;

                btnSearch_Click(sender, e);
            }
        }

        /// <summary>
        /// 查询按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            bnyPath = txtBNYFilePath.Text.Trim();

            if (bnyPath == "")
            {
                MessageBox.Show("请先选择BNY文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //bnyPath = @"H:\工作文件汇总\铁科院\程序\工具\BNY\data\20170409京广高速上行（广州南~武汉）_001.bny";

            //bnyPath = @"F:\个人文件\铁路\工程代码\文件工具\BNY工具\data\20170409京广高速上行（广州南~武汉）_001.bny";

            long bnyFileLength = common.GetBNYLastPosition(bnyPath);
            pageCount=0;
            pageCount = Convert.ToInt32(bnyFileLength / (rowCount * BNYFile.GetChannelSize()));
            if(bnyFileLength % (rowCount * BNYFile.GetChannelSize())>0)
            {
                pageCount += 1;
            }

            var listAll = common.GetBNYData(bnyPath, rowCount, startPosition, ref endPosition);
            startPosition = endPosition;


            this.txtCurrentPage.Text = pageIndex.ToString();
            this.lblTotalPage.Text = pageCount.ToString();

            float startmiled= common.GetBNYStartMile(bnyPath);
            float endmiled= common.GetBNYEndMile(bnyPath);
            if (startmiled < endmiled)
            {
                isInc = true;
            }
            this.lbl_StartMile.Text = startmiled.ToString();
            this.lbl_EndMile.Text = endmiled.ToString();

            DisplayData(listAll);
        }

        
        /// <summary>
        /// 上一页按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrev_Click(object sender, EventArgs e)
        {
            //凡是点击上一页下一页总会可用    20170927
            this.btnNext.Enabled = true;

            startPosition = (pageIndex - 1) * rowCount * BNYFile.GetChannelSize();
            var listAll = common.GetBNYData(bnyPath, rowCount, startPosition, ref endPosition);

            pageIndex--;
            DisplayData(listAll);
            this.txtCurrentPage.Text = pageIndex.ToString();
            if (pageIndex == 1)
            {
                this.btnPrev.Enabled = false;
            }
        }

        /// <summary>
        /// 下一页按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            this.btnPrev.Enabled = true;
            if (pageIndex >= pageCount)
            {
                return;
            }
            else
            {
                startPosition = (pageIndex - 1) * rowCount * BNYFile.GetChannelSize();
                var listAll = common.GetBNYData(bnyPath, rowCount, startPosition, ref endPosition);

                pageIndex++;
                DisplayData(listAll);
                this.txtCurrentPage.Text = pageIndex.ToString();
                if (pageIndex == pageCount)
                {
                    this.btnNext.Enabled = false;
                }
            }
        }

        /// <summary>
        /// 跳转按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGo_Click(object sender, EventArgs e)
        {
            this.btnPrev.Enabled = true;

            if (Convert.ToInt32(this.txtGoPage.Text.Trim()) > pageCount)
            {
                MessageBox.Show("您输入的页数大于总页数");
                return;
            }
            else
            {
                startPosition = (pageIndex - 1) * rowCount * BNYFile.GetChannelSize();
                var listAll = common.GetBNYData(bnyPath, rowCount, startPosition, ref endPosition);

                pageIndex = Convert.ToInt32(this.txtGoPage.Text.Trim());
                DisplayData(listAll);
                this.txtCurrentPage.Text = pageIndex.ToString();

                if (pageIndex == pageCount)
                {
                    this.btnNext.Enabled = false;
                }
                if (pageIndex == 1)
                {
                    this.btnPrev.Enabled = false;
                }
            }
        }

        /// <summary>
        /// 将数据展示到窗体上
        /// </summary>
        /// <param name="dataList"></param>
        private void DisplayData(List<float[]> dataList)
        {
            this.dataGridView1.Rows.Clear();
            if (dataList.Count > 0)
            {
                int rowCount = dataList[0].Length;
                if (rowCount > 0)
                {
                    for (int i = 0; i < rowCount; i++)
                    {
                        DateTime date = common.ConvertLongDateTime(Convert.ToInt64(dataList[0][i]));

                        this.dataGridView1.Rows.Add(
                            date, 
                            dataList[1][i], dataList[2][i], dataList[3][i], dataList[4][i], dataList[5][i],
                            dataList[6][i], dataList[7][i], dataList[8][i], dataList[9][i], dataList[10][i], dataList[11][i],
                            dataList[12][i], dataList[13][i], dataList[14][i], dataList[15][i], dataList[16][i], dataList[17][i]);

                        dataGridView1.RowHeadersWidth = 60;
                        int index = (pageIndex - 1) * rowCount + (i + 1);
                        dataGridView1.Rows[i].HeaderCell.Value = index.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// 导出txt文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportTxt_Click(object sender, EventArgs e)
        {
            ExportForm export = new ExportForm(bnyPath, isInc, this);
            export.ShowDialog();
            
        }

        /// <summary>
        /// 生成cit文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateCitFile_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(bnyPath))
            {
                //CITForm citform = new CITForm(bnyPath, this);
                //citform.ShowDialog();
                FileInfo file = new FileInfo(bnyPath);
                BatchExportCitForm exportForm = new BatchExportCitForm(this, true, file);
                exportForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("请先打开一个bny文件！");
            }
        }

        private void btnBatchExportCit_Click(object sender, EventArgs e)
        {
            BatchExportCitForm citForm = new BatchExportCitForm(this, false, null);
            citForm.ShowDialog();
        }

        public void ShowTips(string text)
        {
            notifyIcon.Visible = true;
            tipsCount++;
            Thread.Sleep((tipsCount - 1) * 1000);
            notifyIcon.ShowBalloonTip(3000, "通知", text, ToolTipIcon.Info);
            Task.Factory.StartNew(() => HideTips());
        }

        private void HideTips()
        {
            Thread.Sleep(3000);
            tipsCount--;
            if (tipsCount == 0)
            {
                Thread.Sleep(3000);
                notifyIcon.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TxtToCitForm form = new TxtToCitForm();
            form.ShowDialog();
        }
    }
}
