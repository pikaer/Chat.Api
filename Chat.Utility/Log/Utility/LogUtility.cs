using System.Text;
using System.Diagnostics;
using System.Web;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using System;
using Infrastructure.Log.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Log.Utility
{
    public class LogUtility
    {
        //监控日志开关
        private static string MonitorSwitch { get { return ConfigManager.AppSettings["MonitorSwitch"]; } }

        /// <summary>
        /// 获取跟踪信息
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static CallerTraceInfo GetTraceInfo(int index = 1)
        {
            var cti = new CallerTraceInfo();

            var sf = new StackTrace(true).GetFrame(index);
            if (sf == null) return cti;
            var m = sf.GetMethod();

            cti.FileName = sf.GetFileName();
            cti.LineNumber = sf.GetFileLineNumber();
            cti.ClassName = m == null || m.ReflectedType == null ? "" : m.ReflectedType.Name;
            cti.MethodName = m == null ? "" : m.Name;
            cti.Parameters = m?.GetParameters();

            return cti;
        }
        
        /// <summary>
        /// 读流信息
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string ReadStream(HttpContent content)
        {
            if (content == null) return "";

            var stream = content.ReadAsStreamAsync().Result;
            return ReadStream(stream);
        }

        /// <summary>
        /// 读流信息
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string ReadStream(Stream stream)
        {
            if (stream == null) return "";

            var encoding = Encoding.UTF8;
            stream.Seek(0, SeekOrigin.Begin);
            StreamReader reader = new StreamReader(stream, encoding);
            var result = reader.ReadToEnd().ToString();
            stream.Seek(0, SeekOrigin.Begin);

            if (!string.IsNullOrEmpty(result)) result = result.Replace("\r\n", "");
            return result;
        }

        /// <summary>
        /// Json序列化
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string JsonSerialize(ActionResult result)
        {
            var set = new JsonSerializerSettings();
            set.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            return JsonConvert.SerializeObject(result, set);
        }

        /// <summary>
        /// 替换关键字
        /// </summary>
        /// <param name="orgStr"></param>
        /// <returns></returns>
        public static string ReplaceKeyword(string orgStr)
        {
            //if (orgStr.Contains(Const.SEPARATOR_LEFT)) orgStr = orgStr.Replace(Const.SEPARATOR_LEFT, Const.REPLACE_LEFT);
            //if (orgStr.Contains(Const.SEPARATOR_RIGHT)) orgStr = orgStr.Replace(Const.SEPARATOR_RIGHT, Const.REPLACE_RIGHT);
            return orgStr;
        }

        /// <summary>
        /// 是否可以输出Monitor日志，共HttpClient调用
        /// </summary>
        /// <returns></returns>
        public static bool CanOutputMonitorLog { get { return MonitorSwitch.Equals("On", StringComparison.OrdinalIgnoreCase); } }
    }
}
