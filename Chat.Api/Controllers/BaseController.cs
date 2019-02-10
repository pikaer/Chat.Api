using Chat.Model.Enum;
using Chat.Model.Utils;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

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
        protected JsonResult ErrorJsonResult(ErrCodeEnum code)
        {
            var errResponse = new ResponseContext<object>()
            {
                Head = new ResponseHead(false, code, code.ToDescription())
            };
            return new JsonResult(errResponse);
        }

        /// <summary>
        /// 获取成功的返回
        /// </summary>
        protected JsonResult SuccessJsonResult(object obj)
        {
            var response = new ResponseContext<object>()
            {
                Head = new ResponseHead(true, ErrCodeEnum.Success, ErrCodeEnum.Success.ToDescription()),
                Data = obj
            };
            return new JsonResult(response);
        }
    }
}