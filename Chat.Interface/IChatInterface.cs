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
        /// <summary>
        /// 获取用户信息
        /// </summary>
        ResponseContext<GetUserInfoResponse> GetUserInfo(RequestContext<GetUserInfoRequest> request);

        /// <summary>
        /// 存入用户信息
        /// </summary>
        ResponseContext<SetUserInfoResponse> SetUserInfo(RequestContext<SetUserInfoRequest> request);

        /// <summary>
        /// 更新用户信息
        /// </summary>
        ResponseContext<UpdateUserInfoResponse> UpdateUserInfo(RequestContext<UpdateUserInfoRequest> request);

        /// <summary>
        /// 获取用户偏好设置
        /// </summary>
        ResponseContext<GetUserPreferenceResponse> GetUserPreference(RequestContext<GetUserPreferenceRequest> request);

        /// <summary>
        /// 修改用户偏好设置
        /// </summary>
        ResponseContext<UpdateUserPreferenceResponse> UpdateUserPreference(RequestContext<UpdateUserPreferenceRequest> request);

        /// <summary>
        /// 根据用户Id获取金币
        /// </summary>
        ResponseContext<GetGoldCoinNumberResponse> GetGoldCoinNumber(RequestContext<GetGoldCoinNumberRequest> request);

        /// <summary>
        /// 获取聊天列表
        /// </summary>
        ResponseContext<GetChatListResponse> GetChatList(RequestContext<GetChatListRequest> request);

        /// <summary>
        /// 更新金币信息
        /// </summary>
        ResponseContext<UpdateGoldCoinResponse> UpdateGoldCoin(RequestContext<UpdateGoldCoinRequest> request);

        /// <summary>
        /// 获取金币信息历史记录
        /// </summary>
        ResponseContext<GetGoldCoinDetailsResponse> GetGoldCoinDetails(RequestContext<GetGoldCoinDetailsRequest> request);

        /// <summary>
        /// 获取聊天对话详情列表
        /// </summary>
        ResponseContext<GetChatContentListReponse> GetChatContentList(RequestContext<GetChatContentListRequest> request);

        /// <summary>
        /// 删除会话
        /// </summary>
        ResponseContext<DeleteChatResponse> DeleteChat(RequestContext<DeleteChatRequest> request);

        /// <summary>
        /// 清除未读消息
        /// </summary>
        ResponseContext<ClearUnReadCountResponse> ClearUnReadCount(RequestContext<ClearUnReadCountRequest> request);

        /// <summary>
        /// 发送消息
        /// </summary>
        ResponseContext<SendMessageResponse> SendMessage(RequestContext<SendMessageRequest> request);

        /// <summary>
        /// 获取动态
        /// </summary>
        ResponseContext<GetMomentsResponse> GetMoments(RequestContext<GetMomentsRequest> request);

        /// <summary>
        /// 发布动态
        /// </summary>
        ResponseContext<PublishMomentResponse> PublishMoment(RequestContext<PublishMomentRequest> request);
    }
}
