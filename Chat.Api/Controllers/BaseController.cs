using Chat.Model.Enum;
using Chat.Model.Utils;
using Chat.Utility;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Chat.Api.Controllers
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// 处理请求数据流
        /// </summary>
        /// <returns></returns>
        protected string GetInputString()
        {
            Stream req = Request.Body;
            string json = new StreamReader(req).ReadToEnd();

            if (!string.IsNullOrEmpty(json))
            {
                while (json.IndexOf("\\/", StringComparison.Ordinal) != -1) json = json.Replace("\\/", "/");
            }

            return json;
        }


        /// <summary>
        /// 获取错误的返回
        /// </summary>
        protected JsonResult ErrorJsonResult(ErrCodeEnum code, string title = null, Exception ex = null)
        {
            var errResponse = new ResponseContext<object>()
            {
                Head = new ResponseHead(false, code, code.ToDescription())
            };

            if (string.IsNullOrEmpty(title) || ex != null)
            {
                Log.Error(title, code.ToDescription(), ex);
            }

            return new JsonResult(errResponse);
        }

        /// <summary>
        /// 写入服务接口调用日志到数据库,用作接口性能检测
        /// </summary>
        protected void WriteServiceLog(string module, string method, RequestHead reqHead, ResponseHead resHead, object request, object response)
        {
            Task.Factory.StartNew(() =>
            {
                var dto = new ServiceLogEntity()
                {
                    ServiceLogId = Guid.NewGuid(),
                    ServiceName = "Chat.Api",
                    Module = module,
                    Method = method,
                    Request = request.SerializeToString(),
                    Response = response.SerializeToString(),
                    UId = reqHead.UId,
                    Code = (int)resHead.Code,
                    Msg = resHead.Msg,
                    Platform = reqHead.Platform,
                    TransactionId = reqHead.TransactionId,
                    CreateTime = DateTime.Now
                };
                ServiceLog.WriteServiceLog(dto);
            });
        }
    }
}