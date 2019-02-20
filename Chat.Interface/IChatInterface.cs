using Chat.Model.Utils;

namespace Chat.Interface
{
    /// <summary>
    /// 前端接口集合，每个控制器请求的接口都在这里汇集
    /// 目的：使得接口更加清晰明了，规范前端交互所需类和服务端代码所需类
    /// 强制要求：前端请求类：XXXXRequest，前端响应类：XXXXResponse,都放在Chat.Model.Utils.ChatApi下面
    /// </summary>
    public interface IChatInterface
    {
        ResponseContext<GetUserInfoResponse> GetUserInfo(RequestContext<GetUserInfoRequest> request);

        ResponseContext<SetUserInfoResponse> SetUserInfo(RequestContext<SetUserInfoRequest> request);

        ResponseContext<UpdateUserInfoResponse> UpdateUserInfo(RequestContext<UpdateUserInfoRequest> request);

        ResponseContext<GetUserPreferenceResponse> GetUserPreference(RequestContext<GetUserPreferenceRequest> request);

        ResponseContext<UpdateUserPreferenceResponse> UpdateUserPreference(RequestContext<UpdateUserPreferenceRequest> request);

        ResponseContext<GetGoldCoinNumberResponse> GetGoldCoinNumber(RequestContext<GetGoldCoinNumberRequest> request);

        ResponseContext<GetChatListResponse> GetChatList(RequestContext<GetChatListRequest> request);

        ResponseContext<UpdateGoldCoinResponse> UpdateGoldCoin(ResponseContext<UpdateGoldCoinRequest> request);
    }
    
}
