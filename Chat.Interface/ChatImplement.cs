using Chat.Model.Utils;
using Chat.Service;
using Infrastructure;

namespace Chat.Interface
{
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
