using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure
{
    public static class Logs
    {
        /// <summary>
        /// 将日志写入数据库
        /// </summary>
        /// <param name="logLevel">日志级别</param>
        /// <param name="tid">事物号</param>
        /// <param name="uid">用户Id</param>
        /// <param name="platform">平台</param>
        /// <param name="title">日志标题</param>
        /// <param name="content">日志内容</param>
        /// <param name="serverName">服务标示（服务名称）</param>
        /// <param name="keyValuePairs">附加信息</param>
        public static void WriteLog(LogLevelEnum logLevel, Guid tid, long uid,string platform,string title,string content,string serverName,Dictionary<string,string> keyValuePairs=null)
        {
            //日志开关
            if (!ConfigHelper.GetBool("LogIsOpen"))
            {
                return;
            }

            //日志级别开关
            var level = ConfigHelper.GetInt("DefaultLogLevel", 0);
            if ((int)logLevel< level)
            {
                return;
            }

            var logEntity = new LogEntity()
            {
                LogId = Guid.NewGuid(),
                LogLevel = logLevel,
                TransactionID = tid,
                UId = uid,
                Platform = platform,
                LogTitle = title,
                LogContent = content,
                ServiceName=serverName,
                CreateTime = DateTime.Now
            };
            InsertLogs(logEntity);

            if (keyValuePairs!=null&& keyValuePairs.Count > 0)
            {
                foreach(var item in keyValuePairs)
                {
                    var tagEntity = new LogTag()
                    {
                        TagId = Guid.NewGuid(),
                        LogId = logEntity.LogId,
                        LogKey = item.Key,
                        LogValue = item.Value,
                        CreateTime=DateTime.Now
                    };
                    InsertTags(tagEntity);
                }
            }

            //发送报警邮件
            if(ConfigHelper.GetBool("ErrorLogSendEmail")&&(logLevel==LogLevelEnum.Error|| logLevel==LogLevelEnum.Fatal))
            {
                string body = string.Format("UId={0},TransactionID={1},Platform={2},LogId={3},Content={4}",
                    uid.ToString(), tid.ToString(), platform, logEntity.LogId.ToString(), content);

                Mail.Send(title, body);
            }
        }
        
        private static void InsertLogs(LogEntity entity)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = @"INSERT INTO dbo.sys_Log
                                                 (LogId
                                                 ,LogLevel
                                                 ,TransactionID
                                                 ,UId
                                                 ,Platform
                                                 ,LogTitle
                                                 ,LogContent
                                                 ,ServiceName
                                                 ,CreateTime)
                                           VALUES
                                                 (@LogId
                                                 ,@LogLevel
                                                 ,@TransactionID
                                                 ,@UId
                                                 ,@Platform
                                                 ,@LogTitle
                                                 ,@LogContent
                                                 ,@ServiceName
                                                 ,@CreateTime)";
                    Db.Execute(sql, entity);
                }
                catch
                {
                    return;
                }
            }
        }

        private static void InsertTags(LogTag logTag)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = @"INSERT INTO dbo.sys_LogTag
                                                 (TagId
                                                 ,LogId
                                                 ,LogKey
                                                 ,LogValue
                                                 ,CreateTime)
                                           VALUES
                                                 (@TagId
                                                 ,@LogId
                                                 ,@LogKey
                                                 ,@LogValue
                                                 ,CreateTime)";
                    Db.Execute(sql, logTag);
                }
                catch
                {
                    return;
                }
            }
        }

        private static IDbConnection GetDbConnection()
        {
            var connString = ConfigHelper.AppSettings("LogConnection");
            return new SqlConnection(connString);
        }
    }
    

    /// <summary>
    /// 日志实体类
    /// </summary>
    public class LogEntity
    {
        /// <summary>
        /// 唯一主键
        /// </summary>
        public Guid LogId { get; set; }

        /// <summary>
        /// 日志级别
        /// </summary>
        public LogLevelEnum LogLevel { get; set; }

        /// <summary>
        /// 事务号
        /// </summary>
        public Guid TransactionID { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public long UId { get; set; }

        /// <summary>
        /// 平台（小程序miniApp，android,ios,浏览器browser,h5)
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// 日志标题
        /// </summary>
        public string LogTitle { get; set; }

        /// <summary>
        /// 日志内容
        /// </summary>
        public string LogContent { get; set; }

        /// <summary>
        /// 服务名称（标示）
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 日志标签（供日志精准筛查）
    /// </summary>
    public class LogTag
    {
        /// <summary>
        /// 唯一主键
        /// </summary>
        public Guid TagId { get; set; }

        /// <summary>
        /// 日志主表Id
        /// </summary>
        public Guid LogId { get; set; }

        /// <summary>
        /// 键
        /// </summary>
        public string LogKey { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string LogValue { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 日志级别
    /// </summary>
    public enum LogLevelEnum
    {
        Debug = 0,
        Info = 1,
        Warn = 2,
        Error = 3,
        Fatal = 4
    }
}
