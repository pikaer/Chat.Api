using Chat.Model.Utils;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chat.Utility
{
    public class Log
    {
        /// <summary>
        /// Debug日志
        /// </summary>
        /// <param name="title">日志标题</param>
        /// <param name="content">日志内容</param>
        /// <param name="head">公共请求头</param>
        /// <param name="keyValuePairs">附件标签</param>
        public static void Debug(string title, string content, RequestHead head = null, Dictionary<string, string> keyValuePairs = null)
        {
            WriteLogAsync(LogLevelEnum.Debug, title, content,head, keyValuePairs);
        }

        /// <summary>
        /// Info日志
        /// </summary>
        /// <param name="title">日志标题</param>
        /// <param name="content">日志内容</param>
        /// <param name="head">公共请求头</param>
        /// <param name="keyValuePairs">附件标签</param>
        public static void Info(string title, string content, RequestHead head = null, Dictionary<string, string> keyValuePairs = null)
        {
            WriteLogAsync(LogLevelEnum.Info, title, content,head, keyValuePairs);
        }

        /// <summary>
        /// Warn日志
        /// </summary>
        /// <param name="title">日志标题</param>
        /// <param name="content">日志内容</param>
        /// <param name="head">公共请求头</param>
        /// <param name="keyValuePairs">附件标签</param>
        public static void Warn(string title, string content, RequestHead head = null, Dictionary<string, string> keyValuePairs = null)
        {
            WriteLogAsync(LogLevelEnum.Warn, title, content,head, keyValuePairs);
        }

        /// <summary>
        /// Error异常日志
        /// </summary>
        /// <param name="title">日志标题</param>
        /// <param name="content">日志内容</param>
        /// <param name="ex">Exception</param>
        /// <param name="head">公共请求头</param>
        /// <param name="keyValuePairs">附件标签</param>
        public static void Error(string title, string content, Exception ex = null, RequestHead head = null, Dictionary<string, string> keyValuePairs = null)
        {
            WriteLogAsync(LogLevelEnum.Error, title, content,head, keyValuePairs, ex);
        }

        /// <summary>
        /// Fatal致命错误日志
        /// </summary>
        /// <param name="title">日志标题</param>
        /// <param name="content">日志内容</param>
        /// <param name="ex">Exception</param>
        /// <param name="head">公共请求头</param>
        /// <param name="keyValuePairs">附件标签</param>
        public static void Fatal(string title, string content, Exception ex = null, RequestHead head = null, Dictionary<string, string> keyValuePairs = null)
        {
            WriteLogAsync(LogLevelEnum.Fatal, title, content, head, keyValuePairs, ex);
        }

        /// <summary>
        /// 异步写入日志到数据库
        /// </summary>
        private static void WriteLogAsync(LogLevelEnum level, string title, string content, RequestHead head = null, Dictionary<string, string> keyValuePairs = null, Exception ex = null)
        {
            try
            {
                string desc = string.Empty;
                string platform = string.Empty;
                Guid tid = Guid.Empty;
                long uid = 0;

                if (ex == null)
                {
                    desc = string.Format("Content:{0}", content);
                }
                else
                {
                    desc = string.Format("Content:{0},Exception:{1}", content, ex.ToString());
                }
                if (head != null)
                {
                    tid = head.TransactionId;
                    uid = head.UId;
                    platform = head.Platform;
                }
                Task.Factory.StartNew(() =>
                {
                    Logs.WriteLog(level, tid, uid, platform, title, desc, keyValuePairs);
                });
            }
            catch
            {
                return;
            }
        }
    }
}
