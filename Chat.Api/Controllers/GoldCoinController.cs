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
        private readonly string MODULE = "GoldCoinController";
        private readonly IChatInterface api = SingletonProvider<ChatImplement>.Instance;

        /// <summary>
        /// 根据用户Id获取金币
        /// </summary>
        [HttpPost]
        public JsonResult GetGoldCoinNumber()
        {
            RequestContext<GetGoldCoinNumberRequest> request = null;
            ResponseContext<GetGoldCoinNumberResponse> response = null;
            try
            {
                string json = GetInputString();
                if (string.IsNullOrEmpty(json))
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotAllowedEmpty_Code);
                }
                request = json.JsonToObject<RequestContext<GetGoldCoinNumberRequest>>();

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
                response = api.GetGoldCoinNumber(request);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return ErrorJsonResult(ErrCodeEnum.InnerError, "GetGoldCoinNumber", ex);
            }
            finally
            {
                WriteServiceLog(MODULE, "GetGoldCoinNumber", request?.Head, response?.Head, request, response);
            }
        }

        /// <summary>
        /// 更新金币信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateGoldCoinNumber()
        {
            RequestContext<UpdateGoldCoinRequest> request = null;
            ResponseContext<UpdateGoldCoinResponse> response = null;
            try
            {
                string json = GetInputString();
                if (string.IsNullOrEmpty(json))
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotAllowedEmpty_Code);
                }
                request = json.JsonToObject<RequestContext<UpdateGoldCoinRequest>>();

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

                response = api.UpdateGoldCoin(request);
                return new JsonResult(response);
            }
            catch(Exception ex)
            {
                return ErrorJsonResult(ErrCodeEnum.InnerError, "UpdateGoldCoinNumber", ex);
            }
            finally
            {
                WriteServiceLog(MODULE, "UpdateGoldCoinNumber", request?.Head, response?.Head, request, response);
            }
        }

        /// <summary>
        /// 获取金币信息历史记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetGoldCoinDetails()
        {
            RequestContext<GetGoldCoinDetailsRequest> request = null;
            ResponseContext<GetGoldCoinDetailsResponse> response = null;
            try
            {
                string json = GetInputString();

                if (string.IsNullOrEmpty(json))
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotAllowedEmpty_Code);
                }
                request = json.JsonToObject<RequestContext<GetGoldCoinDetailsRequest>>();

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

                response = api.GetGoldCoinDetails(request);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return ErrorJsonResult(ErrCodeEnum.InnerError, "GetGoldCoinDetails", ex);
            }
            finally
            {
                WriteServiceLog(MODULE, "GetGoldCoinDetails", request?.Head, response?.Head, request, response);
            }
        }
    }
}
