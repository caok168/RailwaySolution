using CitFileProcess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;

namespace BNYTool
{
    public class CitCreater
    {


        private Dictionary<string, string> Lines = new Dictionary<string, string>();

        Common comm = new Common();

        public CitCreater(DataTable lineData)
        {
            if (lineData != null && lineData.Rows.Count > 0)
            {
                for (int i = 0; i < lineData.Rows.Count; i++)
                {
                    if (!Lines.ContainsKey(lineData.Rows[i]["LineName"].ToString()))
                    {
                        Lines.Add(lineData.Rows[i]["LineName"].ToString(), lineData.Rows[i]["LineCode"].ToString());
                    }
                }
            }
        }

        public DataHeadInfo InitDataHead(FileInfo file, ref string error)
        {
            DataHeadInfo headInfo = new DataHeadInfo();
            headInfo.iDataType = 2;
            headInfo.sDataVersion = "0.0.0";
            headInfo.sTrackCode = "0000";
            headInfo.sTrain = "1000";
            try
            {
                //DateTime dateTime = DateTime.ParseExact(file.Name.Substring(0, 8), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                DateTime dateTime = DateTime.ParseExact(file.Name.Split('_')[1], "yyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                headInfo.sDate = dateTime.ToString("yyyy-MM-dd");
                headInfo.sTime = dateTime.ToString("HH:mm:ss");
            }
            catch (Exception)
            {
                error += "无法解析文件:" + file.Name + ",原因：时间格式无效";
                return null;
            }
            try
            {
                string strackName = file.Name.Split('_')[2];
                string lineType=strackName.Substring(3,1);
                if(lineType=="S")
                {
                    headInfo.iDir = 1;
                    headInfo.iRunDir = 1;
                    headInfo.iKmInc = 1;
                    headInfo.sTrackName = strackName;
                }
                else if (lineType == "X")
                {
                    headInfo.iDir = 2;
                    headInfo.iRunDir = 0;
                    headInfo.iKmInc = 0;
                    headInfo.sTrackName = strackName;
                }

                //string chName = RemoveNumber(file.Name);
                //if (chName.Contains("上行"))
                //{
                //    headInfo.iDir = 1;
                //    headInfo.iRunDir = 1;
                //    headInfo.iKmInc = 1;
                //    string lineName = getName(chName,"上行");
                //    if (Lines.ContainsKey(lineName))
                //    {
                //        headInfo.sTrackName = Lines[lineName] + "S";
                //    }
                //    else
                //    {
                //        error += "找不到线路名:[" + lineName + "]对应的线路代码,请确认！";
                //        return null;
                //    }

                //}
                //else if (chName.Contains("下行"))
                //{
                //    headInfo.iDir = 2;
                //    headInfo.iRunDir = 0;
                //    headInfo.iKmInc = 0;
                //    string lineName = getName(chName, "下行");
                //    if (Lines.ContainsKey(lineName))
                //    {
                //        headInfo.sTrackName = Lines[lineName] + "X";
                //    }
                //    else
                //    {
                //        error += "找不到线路名:[" + lineName + "]对应的线路代码,请确认！";
                //        return null;
                //    }
                    
                //}
                //else if (chName.Contains("单线"))
                //{
                //    headInfo.iDir = 3;
                //    headInfo.iRunDir = 0;
                //    headInfo.iKmInc = 0;
                //    string lineName = getName(chName,"单线");
                //    if (Lines.ContainsKey(lineName))
                //    {
                //        headInfo.sTrackName = Lines[lineName];
                //    }
                //    else
                //    {
                //        error += "找不到线路名:[" + lineName + "]对应的线路代码,请确认！";
                //        return null;
                //    }

                //}
            }
            catch (Exception)
            {
                error += "无法解析文件:" + file.Name + ",原因：找不到上下行或文件名称不标准！";
                return null;
            }
            headInfo.iSmaleRate = -2000;
            try
            {
                headInfo.fkmFrom = comm.GetBNYStartMile(file.FullName);
                headInfo.fkmTo = comm.GetBNYEndMile(file.FullName);
            }
            catch (Exception)
            {
                error += "无法解析文件:" + file.Name + ",原因：获取不到开始或结束里程，请确认文件是否合法！";
                return null;
            }
            headInfo.iChannelNumber = 15;

            return headInfo;
        }

        public string getName(string name, string key)
        {
            int index = name.LastIndexOf(key);
            return name.Substring(0,index);
        }

        public static string RemoveNumber(string key)
        {
            return Regex.Replace(key, @"\d", "");
        }
    }
}
