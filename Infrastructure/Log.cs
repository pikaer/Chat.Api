using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace Infrastructure
{
    public static class Log
    {
        public static void WriteLog(LogLevelEnum logLevel, Guid tid, int uid,string platform,string title,string content,Dictionary<string,string> keyValuePairs)
        {
            var logEntity = new LogEntity()
            {
                LogId = Guid.NewGuid(),
                LogLevel = logLevel,
                TransactionID = tid,
                UId = uid,
                Platform = platform,
                LogTitle = title,
                LogContent = content
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
                        Key = item.Key,
                        Value = item.Value
                    };
                    InsertTags(tagEntity);
                }
            }
        }



        private static void InsertLogs(LogEntity entity)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = @"INSERT INTO dbo.sys_LogEntity
                                                 (LogId
                                                 ,LogLevel
                                                 ,TransactionID
                                                 ,UId
                                                 ,Platform
                                                 ,LogTitle
                                                 ,LogContent)
                                           VALUES
                                                 (@LogId
                                                 ,@LogLevel
                                                 ,@TransactionID
                                                 ,@UId
                                                 ,@Platform
                                                 ,@LogTitle
                                                 ,@LogContent)";
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
                                                 ,Key
                                                 ,Value)
                                           VALUES
                                                 (@TagId
                                                 ,@LogId
                                                 ,@Key
                                                 ,@Value)";
                    Db.Execute(sql);
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
        public int? UId { get; set; }

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
        public string Key { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
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
