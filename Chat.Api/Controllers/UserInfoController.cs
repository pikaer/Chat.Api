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
        private readonly string MODULE = "UserInfoController";
        private readonly IChatInterface api = SingletonProvider<ChatImplement>.Instance;

        /// <summary>
        /// 获取用户信息
        /// </summary>
        [HttpPost]
        public JsonResult GetUserInfo()
        {
            RequestContext<GetUserInfoRequest> request = null;
            ResponseContext<GetUserInfoResponse> response = null;
            try
            {
                string json = GetInputString();
                if (string.IsNullOrEmpty(json))
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotAllowedEmpty_Code);
                }
                request = json.JsonToObject<RequestContext<GetUserInfoRequest>>();
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
                response = api.GetUserInfo(request);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return ErrorJsonResult(ErrCodeEnum.InnerError, "GetUserInfo", ex);
            }
            finally
            {
                WriteServiceLog(MODULE, "GetUserInfo", request?.Head, response?.Head, request, response);
            }
        }

        /// <summary>
        /// 存入用户信息
        /// </summary>
        [HttpPost]
        public JsonResult SetUserInfo()
        {
            RequestContext<SetUserInfoRequest> request = null;
            ResponseContext<SetUserInfoResponse> response = null;
            try
            {
                string json = GetInputString();
                if (string.IsNullOrEmpty(json))
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotAllowedEmpty_Code);
                }
                request = json.JsonToObject<RequestContext<SetUserInfoRequest>>();
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
                 response = api.SetUserInfo(request);
                return new JsonResult(response);
            }
            catch(Exception ex)
            {
                return ErrorJsonResult(ErrCodeEnum.InnerError, "SetUserInfo", ex);
            }
            finally
            {
                WriteServiceLog(MODULE, "SetUserInfo", request?.Head, response?.Head, request, response);
            }
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        [HttpPost]
        public JsonResult UpdateUserInfo()
        {
            RequestContext<UpdateUserInfoRequest> request = null;
            ResponseContext<UpdateUserInfoResponse> response = null;
            try
            {
                string json = GetInputString();
                if (string.IsNullOrEmpty(json))
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotAllowedEmpty_Code);
                }
                request = json.JsonToObject<RequestContext<UpdateUserInfoRequest>>();
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
                response = api.UpdateUserInfo(request);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return ErrorJsonResult(ErrCodeEnum.InnerError, "UpdateUserInfo", ex);
            }
            finally
            {
                WriteServiceLog(MODULE, "UpdateUserInfo", request?.Head, response?.Head, request, response);
            }
        }

        /// <summary>
        /// 获取用户偏好设置
        /// </summary>
        [HttpPost]
        public JsonResult GetUserPreference()
        {
            RequestContext<GetUserPreferenceRequest> request = null;
            ResponseContext<GetUserPreferenceResponse> response = null;
            try
            {
                string json = GetInputString();
                if (string.IsNullOrEmpty(json))
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotAllowedEmpty_Code);
                }
                request = json.JsonToObject<RequestContext<GetUserPreferenceRequest>>();
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
                response = api.GetUserPreference(request);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return ErrorJsonResult(ErrCodeEnum.InnerError, "GetUserPreference", ex);
            }
            finally
            {
                WriteServiceLog(MODULE, "GetUserPreference", request?.Head, response?.Head, request, response);
            }
        }

        /// <summary>
        /// 修改用户偏好设置
        /// </summary>
        [HttpPost]
        public JsonResult UpdateUserPreference()
        {
            RequestContext<UpdateUserPreferenceRequest> request = null;
            ResponseContext<UpdateUserPreferenceResponse> response = null;
            try
            {
                string json = GetInputString();
                if (string.IsNullOrEmpty(json))
                {
                    return ErrorJsonResult(ErrCodeEnum.ParametersIsNotAllowedEmpty_Code);
                }
                request = json.JsonToObject<RequestContext<UpdateUserPreferenceRequest>>();
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
                response = api.UpdateUserPreference(request);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return ErrorJsonResult(ErrCodeEnum.InnerError, "UpdateUserPreference", ex);
            }
            finally
            {
                WriteServiceLog(MODULE, "UpdateUserPreference", request?.Head, response?.Head, request, response);
            }
        }
    }
}