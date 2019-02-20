using Chat.Interface;
using Chat.Model.Enum;
using Chat.Model.Utils;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Chat.Api.Controllers
{
    /// <summary>
    /// 金币信息
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class GoldCoinController : BaseController
    {
        private readonly IChatInterface api = SingletonProvider<ChatImplement>.Instance;

        /// <summary>
        /// 根据用户Id获取金币
        /// </summary>
        [HttpPost]
        public JsonResult GetGoldCoinNumberByUid()
        {
            try
            {
                string json = GetInputString();
                if (string.IsNullOrEmpty(json))
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotAllowedEmpty_Code);
                }
                var request = json.JsonToObject<RequestContext<GetGoldCoinNumberRequest>>();

                if (request == null)
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotValid_Code);
                }
                if (request.Head == null)
                {
                    return ErrorJsonResult(ErrCodeEnum.InvalidRequestHead);
                }
                if (request.Content == null)
                {
                    return ErrorJsonResult(ErrCodeEnum.InvalidRequestBody);
                }
                if (request.Content.UId <= 0)
                {
                    return ErrorJsonResult(ErrCodeEnum.InvalidRequestBody);
                }
                var response = api.GetGoldCoinNumber(request);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return ErrorJsonResult(ErrCodeEnum.InnerError, "GetGoldCoinNumberByUid", ex);
            }

        }

        /// <summary>
        /// 更新金币信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateGoldCoinNumber()
        {
            return new JsonResult(null);
        }

        /// <summary>
        /// 获取金币信息历史记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetGoldCoinHistory()
        {
            return new JsonResult(null);
        }
    }
}
