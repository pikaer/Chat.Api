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
        public JsonResult GetUserInfo(RequestContext<GetUserInfoRequest> request)
        {
            try
            {
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
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return ErrorJsonResult(ErrCodeEnum.InnerError, "GetUserInfo", ex);
            }
        }

        /// <summary>
        /// 存入用户信息
        /// </summary>
        [HttpPost]
        public JsonResult SetUserInfo(RequestContext<SetUserInfoRequest> request)
        {
            try
            {
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
                var response = api.SetUserInfo(request);
                return new JsonResult(response);
            }
            catch(Exception ex)
            {
                return ErrorJsonResult(ErrCodeEnum.InnerError, "SetUserInfo", ex);
            }
        }

        /// <summary>
        /// 存入用户信息
        /// </summary>
        [HttpPost]
        public JsonResult UpdateUserInfo(RequestContext<UpdateUserInfoRequest> request)
        {
            try
            {
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
                var response = api.UpdateUserInfo(request);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return ErrorJsonResult(ErrCodeEnum.InnerError, "UpdateUserInfo", ex);
            }
        }

        /// <summary>
        /// 获取用户偏好设置
        /// </summary>
        [HttpPost]
        public JsonResult GetUserPreference(RequestContext<GetUserPreferenceRequest> request)
        {
            try
            {
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
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return ErrorJsonResult(ErrCodeEnum.InnerError, "GetUserPreference", ex);
            }
        }

        /// <summary>
        /// 存入用户偏好设置
        /// </summary>
        [HttpPost]
        public JsonResult UpdateUserPreference(RequestContext<UpdateUserPreferenceRequest> request)
        {
            try
            {
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
                var response = api.UpdateUserPreference(request);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return ErrorJsonResult(ErrCodeEnum.InnerError, "UpdateUserPreference", ex);
            }
        }
    }
}