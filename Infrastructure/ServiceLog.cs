using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure
{
    /// <summary>
    /// 服务日志
    /// </summary>
    public static class ServiceLog
    {
        private static IDbConnection GetDbConnection()
        {
            var connString = ConfigHelper.AppSettings("LogConnection");
            return new SqlConnection(connString);
        }

        public static void WriteServiceLog(ServiceLogEntity serviceLogEntity)
        {
            //日志开关
            if (!ConfigHelper.GetBool("ServiceLogIsOpen"))
            {
                return;
            }
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = @"INSERT INTO dbo.sys_ServiceLog(ServiceLogId,ServiceName,Module,Method,Request,Response,UId,Code,Msg,Platform,TransactionId,CreateTime)
                                     VALUES (@ServiceLogId,@ServiceName,@Module,@Method,@Request,@Response,@UId,@Code,@Msg,@Platform,@TransactionId,@CreateTime)";
                    Db.Execute(sql, serviceLogEntity);
                }
                catch
                {
                    return;
                }
            }
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
