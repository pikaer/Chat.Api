using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Logs
{
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
        Debug=0,
        Info=1,
        Warn=2,
        Error=3,
        Fatal=4
    }
}
