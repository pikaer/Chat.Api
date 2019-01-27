using Chat.Service;
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

    }
}