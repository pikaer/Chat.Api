using Chat.Model.Utils;
using Chat.Service;
using Infrastructure;

namespace Chat.Interface
{
    /// <summary>
    /// 接口实现
    /// 目的：规范代码，让所有接口的实现一目了然
    /// 强制要求：实例化都采用单利模式，不提倡单独实例化
    /// </summary>
    public class ChatImplement : IChatInterface
    {
        private UserInfoService userInfoService = SingletonProvider<UserInfoService>.Instance;

        public ResponseContext<GetUserInfoResponse> GetUserInfo(RequestContext<GetUserInfoRequest> request)
        {
            return userInfoService.GetUserInfo(request);
        }

        public ResponseContext<GetUserPreferenceResponse> GetUserPreference(RequestContext<GetUserPreferenceRequest> request)
        {
            return userInfoService.GetUserPreference(request);
        }

        public ResponseContext<SetUserInfoResponse> SetUserInfo(RequestContext<SetUserInfoRequest> request)
        {
            return userInfoService.SetUserInfo(request);
        }

        public ResponseContext<SetUserPreferenceResponse> SetUserPreference(RequestContext<SetUserPreferenceRequest> request)
        {
            return userInfoService.SetUserPreference(request);
        }
    }
}
