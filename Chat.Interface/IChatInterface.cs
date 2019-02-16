using Chat.Model.Utils;

namespace Chat.Interface
{
    /// <summary>
    /// 前端接口集合，每个控制器请求的接口都在这里汇集
    /// </summary>
    public interface IChatInterface
    {
        ResponseContext<GetUserInfoResponse> GetUserInfo(RequestContext<GetUserInfoRequest> request);

        ResponseContext<SetUserInfoResponse> SetUserInfo(RequestContext<SetUserInfoRequest> request);

        ResponseContext<GetUserPreferenceResponse> GetUserPreference(RequestContext<GetUserPreferenceRequest> request);

        ResponseContext<SetUserPreferenceResponse> SetUserPreference(RequestContext<SetUserPreferenceRequest> request);
    }
    
}
