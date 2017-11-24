using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BNYTool
{
    public partial class Form1 : Form
    {
        Common common = new Common();

        public Form1()
        {
            InitializeComponent();


            long timeStart = DateTime.Now.Ticks;
            
            DateTime date = new DateTime(timeStart);

            //636325992725045276

            long num = 1176185216;

            DateTime aaa = ConvertLongDateTime(num);

            //1176185216
            DateTime datetime = new DateTime(1176185216/1000);
            
        }

        public DateTime ConvertLongDateTime(long d)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(d + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime dtResult = dtStart.Add(toNow);
            return dtResult;
        }



        private List<double[]> ReadBNY(string bnyFilePath,int sumpleNum,ref long endFilePos)
        {
            try
            {
                FileStream fs = new FileStream(bnyFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fs, Encoding.Default);
                br.BaseStream.Position = 0;

                int iChannelNumberSize = 18 * 4;
                byte[] b = new byte[iChannelNumberSize];

                endFilePos = sumpleNum * iChannelNumberSize;
                
                List<double[]> allList = new List<double[]>();
                for (int i = 0; i < 18; i++)
                {
                    double[] array = new double[sumpleNum];
                    allList.Add(array);
                }

                for (int i = 0; i < sumpleNum; i++)
                {
                    b = br.ReadBytes(iChannelNumberSize);

                    for (int channelNum = 1; channelNum <= 18; channelNum++)
                    {
                        double fGL = (BitConverter.ToSingle(b, (channelNum - 1) * 4));

                        allList[channelNum - 1][i] = fGL;
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

        private void button1_Click(object sender, EventArgs e)
        {
            string bnyPath = @"H:\工作文件汇总\铁科院\程序\工具\BNY\data\20170409京广高速上行（广州南~武汉）_001.bny";
            long endPosition = 0;
            long startPosition = 0;

            long bnyFileLength = common.GetBNYLastPosition(bnyPath);

            int sampleCount = 1000;

            int count = Convert.ToInt32(bnyFileLength / sampleCount);

            string[] channelNames = common.GetChannelNames();

            for (int i = 0; i <= count + 1; i++)
            {
                var listAll = common.GetBNYData(bnyPath, sampleCount, startPosition, ref endPosition);
                startPosition = endPosition;

                //common.ExportDataTxt("123", listAll, channelNames);
            }

            //var listAll = common.GetBNYData(bnyPath,1000,0,ref endPosition);
            
        }


        private void btnRead_Click(object sender, EventArgs e)
        {
            string txtPath = @"H:\工作文件汇总\铁科院\程序\工具\BNY\data\20170409京广高速上行（广州南~武汉）.txt";

            common.GetTxtData(txtPath);
        }
    }
}
