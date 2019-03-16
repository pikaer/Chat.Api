using System;

namespace Infrastructure
{
    /// <summary>
    /// 服务日志
    /// </summary>
    public static class ServiceLog
    {
        public static void WriteServiceLog(ServiceLogEntity serviceLogEntity)
        {

        }
    }

    public class ServiceLogEntity
    {
        /// <summary>
        /// 唯一标示
        /// </summary>
        public Guid ServiceLogId { get; set; }

        /// <summary>
        /// 服务名
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 模块
        /// </summary>
        public string Module{ get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// 请求体
        /// </summary>
        public string Request { get; set; }

        /// <summary>
        /// 响应体
        /// </summary>
        public string Response { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public long UId { get; set; }

        /// <summary>
        /// 错误码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 平台（小程序miniApp，android,ios,浏览器browser,h5)
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// 事务号
        /// </summary>
        public Guid TransactionId { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
