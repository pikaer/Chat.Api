using Chat.Model.DTO.UserInfo;
using Chat.Model.Entity.UserInfo;
using Chat.Model.Enum;
using Chat.Model.Utils;
using Chat.Service;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class UserInfoController : BaseController
    {
        private UserInfoService _userInfoService = new UserInfoService();

        /// <summary>
        /// 存入用户信息
        /// </summary>
        [HttpPost]
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
            var response = new ResponseContext<UserInfo>(res);
            return new JsonResult(response);
        }
    }
}