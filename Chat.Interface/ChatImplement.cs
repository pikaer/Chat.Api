using Chat.Model.Utils;
using Chat.Service;
using Infrastructure;

namespace Chat.Interface
{
    /// <summary>
    /// 接口实现
    /// 目的：规范代码，让所有接口的实现一目了然
    /// 强制要求：实例化都采用单例模式，不提倡单独实例化
    /// </summary>
    public class ChatImplement : IChatInterface
    {
        private UserInfoService userInfoService = SingletonProvider<UserInfoService>.Instance;
        private GoldCoinService goldCoinService = SingletonProvider<GoldCoinService>.Instance;
        private ChatService chatService = SingletonProvider<ChatService>.Instance;
        private MomentService momentService = SingletonProvider<MomentService>.Instance;

        public ResponseContext<GetUserInfoResponse> GetUserInfo(RequestContext<GetUserInfoRequest> request)
        {
            return userInfoService.GetUserInfo(request);
        }

        public ResponseContext<GetUserPreferenceResponse> GetUserPreference(RequestContext<GetUserPreferenceRequest> request)
        {
            return userInfoService.GetUserPreference(request);
        }

        public ResponseContext<UpdateUserInfoResponse> UpdateUserInfo(RequestContext<UpdateUserInfoRequest> request)
        {
            return userInfoService.UpdateUserInfo(request);
        }

        public ResponseContext<SetUserInfoResponse> SetUserInfo(RequestContext<SetUserInfoRequest> request)
        {
            return userInfoService.SetUserInfo(request);
        }
       
        public ResponseContext<UpdateUserPreferenceResponse> UpdateUserPreference(RequestContext<UpdateUserPreferenceRequest> request)
        {
            return userInfoService.UpdateUserPreference(request);
        }

        public ResponseContext<GetGoldCoinNumberResponse> GetGoldCoinNumber(RequestContext<GetGoldCoinNumberRequest> request)
        {
            return goldCoinService.GetGoldCoinNumber(request);
        }

        public ResponseContext<UpdateGoldCoinResponse> UpdateGoldCoin(RequestContext<UpdateGoldCoinRequest> request)
        {
            return goldCoinService.UpdateGoldCoin(request);
        }

        public ResponseContext<GetChatListResponse> GetChatList(RequestContext<GetChatListRequest> request)
        {
            return chatService.GetChatList(request);
        }

        public ResponseContext<GetGoldCoinDetailsResponse> GetGoldCoinDetails(RequestContext<GetGoldCoinDetailsRequest> request)
        {
            return goldCoinService.GetGoldCoinDetails(request);
        }

        public ResponseContext<GetChatContentListReponse> GetChatContentList(RequestContext<GetChatContentListRequest> request)
        {
            return chatService.GetChatContentList(request);
        }

        public ResponseContext<DeleteChatResponse> DeleteChat(RequestContext<DeleteChatRequest> request)
        {
            return chatService.DeleteChat(request);
        }

        public ResponseContext<ClearUnReadCountResponse> ClearUnReadCount(RequestContext<ClearUnReadCountRequest> request)
        {
            return chatService.ClearUnReadCount(request);
        }

        public ResponseContext<SendMessageResponse> SendMessage(RequestContext<SendMessageRequest> request)
        {
            return chatService.SendMessage(request);
        }

        public ResponseContext<GetMomentsResponse> GetMoments(RequestContext<GetMomentsRequest> request)
        {
           return momentService.GetMoments(request);
        }

        public ResponseContext<PublishMomentResponse> PublishMoment(RequestContext<PublishMomentRequest> request)
        {
            return momentService.PublishMoment(request);
        }

        public ResponseContext<DeleteImgResponse> DeleteImg(RequestContext<DeleteImgRequest> request)
        {
            return momentService.DeleteImg(request);
        }

        public ResponseContext<SupportMomentResponse> SupportMoment(RequestContext<SupportMomentRequest> request)
        {
            return momentService.SupportMoment(request);
        }

        public ResponseContext<MomentDetailResponse> MomentDetail(RequestContext<MomentDetailRequest> request)
        {
            return momentService.MomentDetail(request);
        }

        public ResponseContext<SupportDiscussResponse> SupportDiscuss(RequestContext<SupportDiscussRequest> request)
        {
            return momentService.SupportDiscuss(request);
        }

        public ResponseContext<MySpaceResponse> MySpace(RequestContext<MySpaceRequest> request)
        {
            return momentService.MySpace(request);
        }

        public ResponseContext<MomentDiscussResponse> MomentDiscuss(RequestContext<MomentDiscussRequest> request)
        {
            return momentService.MomentDiscuss(request);
        }

        public ResponseContext<GetUserSimpleInfoResponse> GetUserSimpleInfo(RequestContext<GetUserSimpleInfoRequest> request)
        {
            return userInfoService.GetUserSimpleInfo(request);
        }

        public ResponseContext<SetVisitorResponse> SetVisitor(RequestContext<SetVisitorRequest> request)
        {
            return userInfoService.SetVisitor(request);
        }

        public ResponseContext<UpdateAttentionStateResponse> UpdateAttentionState(RequestContext<UpdateAttentionStateRequest> request)
        {
            return userInfoService.UpdateAttentionState(request);
        }

        public ResponseContext<GetFriendsResponse> GetFriends(RequestContext<GetFriendsRequest> request)
        {
            return userInfoService.GetFriends(request);
        }
    }
}
