using Chat.Model.Entity.Chat;
using Chat.Model.Enum;
using Chat.Model.Utils;
using Chat.Repository;
using Chat.Utility;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chat.Service
{
    public class ChatService
    {
        private readonly ChatRepository chatDal = SingletonProvider<ChatRepository>.Instance;
        private readonly UserInfoRepository userInfoDal = SingletonProvider<UserInfoRepository>.Instance;

        public ResponseContext<GetChatListResponse> GetChatList(RequestContext<GetChatListRequest> request)
        {
            var response = new ResponseContext<GetChatListResponse>()
            {
                Content = new GetChatListResponse()
            };

            response.Content.ChatList = ChatListTestData();
            response.Content.TotalUnReadCount = "50";
            return response;
        }

        public ResponseContext<GetChatContentListReponse> GetChatContentList(RequestContext<GetChatContentListRequest> request)
        {
            var response = new ResponseContext<GetChatContentListReponse>()
            {
                Content = ChatContentListTestData()
            };

            var rtn = new List<ChatContentDetail>();

            //我发给对方消息
            var myMessahes = GetChatContentList(request.Content.UId, request.Content.PartnerUId, true);
            if(myMessahes.NotEmpty())
            {
                rtn.AddRange(myMessahes);
            }

            //对方发给我的消息
            var partnerMessages = GetChatContentList(request.Content.PartnerUId, request.Content.UId, false);
            if(partnerMessages.NotEmpty())
            {
                rtn.AddRange(partnerMessages);
            }
            

            //response.Content.ChatContentList = rtn.OrderBy(a => a.CreateTime).ToList();
            return response;
        }

        /// <summary>
        /// 删除会话
        /// </summary>
        public ResponseContext<DeleteChatResponse> DeleteChat(RequestContext<DeleteChatRequest> request)
        {
            var response = new ResponseContext<DeleteChatResponse>()
            {
                Content = new DeleteChatResponse()
                {
                    IsExecuteSuccess = true,
                    CurrentTotalUnReadCount = "50"
                }
            };
            return response;
        }

        /// <summary>
        /// 清除未读条数
        /// </summary>
        public ResponseContext<ClearUnReadCountResponse> ClearUnReadCount(RequestContext<ClearUnReadCountRequest> request)
        {
            var response = new ResponseContext<ClearUnReadCountResponse>()
            {
                Content = new ClearUnReadCountResponse()
                {
                    IsExecuteSuccess = true,
                    CurrentTotalUnReadCount = "67"
                }
            };
            return response;
        }

        /// <summary>
        /// 发送聊天内容
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResponseContext<SendMessageResponse> SendMessage(RequestContext<SendMessageRequest> request)
        {
            var response = new ResponseContext<SendMessageResponse>()
            {
                Content = new SendMessageResponse()
            };

            var message = new ChatContent()
            {
                ChatId = Guid.NewGuid(),
                UId = request.Content.UId,
                PartnerUId = request.Content.PartnerUId,
                ContentDetail = request.Content.ChatContent,
                Type = request.Content.ChatContentType,
                CreateTime = DateTime.Now,
                HasRead = false
            };

            response.Content.IsExecuteSuccess = chatDal.InsertChatContent(message);
            return response;
        }

        public GetChatContentListReponse ChatContentListTestData()
        {
            var rtn = new GetChatContentListReponse();
            var item = new List<ChatContentDetail>();
            var today = DateTime.Now;
            for (int i = 1; i <= 40; i++)
            {
                var dto = new ChatContentDetail()
                {
                    IsOwner = i % 3 == 0, //取余数
                    HeadImgPath = i % 3 == 0 ? "../../content/images/pikaer.jpg" : "../../content/images/partner.jpg",
                    ChatContent = string.Format("我给你发送了第{0}条消息对话", i),
                    ChatContentType = ChatContentTypeEnum.Text,
                    ChatTime = today.AddDays(i).GetDateDesc(),
                    IsDisplayChatTime = i % 5 == 0
                };
                item.Add(dto);
            }

            rtn.ChatContentList = item;
            return rtn;
        }

        /// <summary>
        /// 用户列表模拟数据
        /// </summary>
        private List<ChatListType> ChatListTestData()
        {
            //暂时返回模拟数据
            var chatList = new List<ChatListType>();
            for (int i = 1; i <= 10; i++)
            {
                var dto = new ChatListType()
                {
                    PartnerUId = i + 1,
                    HeadImgPath = "../../content/images/pikaer.jpg",
                    DispalyName = string.Format("我是第{0}个小星星", i),
                    LatestChatContent = string.Format("我是第{0}个Pikaer", i),
                    ChatContentType = ChatContentTypeEnum.Text,
                    LatestChatTime = "2018-8-9",
                    UnReadCount = "23"
                };
                chatList.Add(dto);
            }
            return chatList;
        }

        private List<ChatContentDetail> GetChatContentList(long uId, long partnerUId, bool isOwner)
        {
            var rtn = new List<ChatContentDetail>();
            var message = chatDal.GetChatContent(uId, partnerUId);
            var userInfo = userInfoDal.GetUserInfoByUId(uId);
            if (userInfo == null || message.IsNullOrEmpty())
            {
                return null;
            }
            foreach (var item in message)
            {
                var dto = new ChatContentDetail()
                {
                    IsOwner = isOwner,
                    HeadImgPath = userInfo.HeadPhotoPath.ToHeadImagePath(),
                    ChatContent = item.ContentDetail,
                    ChatContentType = item.Type,
                    ChatTime = item.CreateTime.GetDateDesc(),
                    CreateTime = item.CreateTime,
                    IsDisplayChatTime = true
                };
                rtn.Add(dto);
            }
            return rtn;
        }
    }
}
