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
        public JsonResult GetGoldCoinNumber()
        {
            try
            {
                string json = GetInputString();
                if (string.IsNullOrEmpty(json))
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotAllowedEmpty_Code);
                }
                var request = json.JsonToObject<RequestContext<GetGoldCoinNumberRequest>>();

                //ToDo => 优化
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
                //

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
            string json = GetInputString();
            if (string.IsNullOrEmpty(json))
            {
                return ErrorJsonResult(ErrCodeEnum.ParametersIsNotAllowedEmpty_Code);
            }
            var request = json.JsonToObject<RequestContext<UpdateGoldCoinRequest>>();

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

            var response = api.UpdateGoldCoin(request);
            return new JsonResult(response);
        }

        /// <summary>
        /// 获取金币信息历史记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetGoldCoinDetails()
        {
            string json = GetInputString();

            if (string.IsNullOrEmpty(json))
            {
                return ErrorJsonResult(ErrCodeEnum.ParametersIsNotAllowedEmpty_Code);
            }
            var request = json.JsonToObject<RequestContext<GetGoldCoinDetailsRequest>>();

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

            var response = api.GetGoldCoinDetails(request);
            return new JsonResult(response);
        }
    }
}
