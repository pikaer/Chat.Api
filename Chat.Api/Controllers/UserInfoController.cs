using Chat.Interface;
using Chat.Model.Enum;
using Chat.Model.Utils;
using Chat.Service;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Chat.Api.Controllers
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class UserInfoController : BaseController
    {
        private readonly IChatInterface api = SingletonProvider<ChatImplement>.Instance;

        /// <summary>
        /// 获取用户信息
        /// </summary>
        [HttpPost]
        public JsonResult GetUserInfo()
        {
            try
            {
                string json = GetInputString();
                if (string.IsNullOrEmpty(json))
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotAllowedEmpty_Code);
                }
                var request = json.JsonToObject<RequestContext<GetUserInfoRequest>>();
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
                var response = api.GetUserInfo(request);
                return SuccessJsonResult(response);
            }
            catch (Exception ex)
            {
                return ErrorJsonResult(ErrCodeEnum.InnerError, "SetUserInfo", ex);
            }
        }

        /// <summary>
        /// 存入用户信息
        /// </summary>
        [HttpPost]
        public JsonResult SetUserInfo()
        {
            try
            {
                string json = GetInputString();
                if (string.IsNullOrEmpty(json))
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotAllowedEmpty_Code);
                }
                var request = json.JsonToObject<RequestContext<SetUserInfoRequest>>();
                if (request == null)
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotValid_Code);
                }
                if (request.Content == null)
                {
                    return ErrorJsonResult(ErrCodeEnum.InvalidRequestBody);
                }
                var response = api.SetUserInfo(request);
                return SuccessJsonResult(response);
            }
            catch(Exception ex)
            {
                return ErrorJsonResult(ErrCodeEnum.InnerError, "SetUserInfo", ex);
            }
        }

        /// <summary>
        /// 获取用户偏好设置
        /// </summary>
        [HttpPost]
        public JsonResult GetUserPreference()
        {
            try
            {
                string json = GetInputString();
                if (string.IsNullOrEmpty(json))
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotAllowedEmpty_Code);
                }
                var request = json.JsonToObject<RequestContext<GetUserPreferenceRequest>>();
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
                var response = api.GetUserPreference(request);
                return SuccessJsonResult(response);
            }
            catch (Exception ex)
            {
                return ErrorJsonResult(ErrCodeEnum.InnerError, "SetUserInfo", ex);
            }
        }

        /// <summary>
        /// 存入用户偏好设置
        /// </summary>
        [HttpPost]
        public JsonResult SetUserPreference()
        {
            try
            {
                string json = GetInputString();
                if (string.IsNullOrEmpty(json))
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotAllowedEmpty_Code);
                }
                var request = json.JsonToObject<RequestContext<SetUserPreferenceRequest>>();
                if (request == null)
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotValid_Code);
                }
                if (request.Content == null)
                {
                    return ErrorJsonResult(ErrCodeEnum.InvalidRequestBody);
                }
                var response = api.SetUserPreference(request);
                return SuccessJsonResult(response);
            }
            catch (Exception ex)
            {
                return ErrorJsonResult(ErrCodeEnum.InnerError, "SetUserInfo", ex);
            }
        }
    }
}