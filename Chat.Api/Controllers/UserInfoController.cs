using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.Model.DTO.UserInfo;
using Chat.Model.Enum;
using Chat.Model.Utils;
using Chat.Service;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Chat.Api.Controllers
{
    [Route("api/UserInfo")]
    public class UserInfoController : BaseController
    {
        private UserInfoService _userInfoService = new UserInfoService();

        /// <summary>
        /// 存入用户信息
        /// </summary>
        [HttpPost]
        [Route("SetUserInfo")]
        public JsonResult SetUserInfo()
        {
            string json = GetInputString();
            if (string.IsNullOrEmpty(json))
            {
                return ErrorJsonResult(ErrCodeEnum.ParametersIsNotAllowedEmpty_Code);
            }
            var request = json.JsonToObject<RequestContext<WXUserInfoRequest>>();
            if (request == null)
            {
                return ErrorJsonResult(ErrCodeEnum.ParametersIsNotValid_Code);
            }
            if (request.Content == null)
            {
                return ErrorJsonResult(ErrCodeEnum.InvalidRequestBody);
            }
            var res = _userInfoService.SetUserInfo(request.Content);
            var response = new ResponseContext<bool>(res);
            return new JsonResult(response);
        }
    }
}