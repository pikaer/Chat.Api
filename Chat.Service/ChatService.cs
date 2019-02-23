using Chat.Model.Enum;
using Chat.Model.Utils;
using Chat.Repository;
using Infrastructure;
using System;
using System.Collections.Generic;

namespace Chat.Service
{
    public class ChatService
    {
        private readonly ChatRepository chatDal = SingletonProvider<ChatRepository>.Instance;

        public ResponseContext<GetChatListResponse> GetChatList(RequestContext<GetChatListRequest> request)
        {
            var response = new ResponseContext<GetChatListResponse>()
            {
                Content = new GetChatListResponse()
            };

            //暂时返回模拟数据
            var chatList = new List<ChatListType>();
            for(int i = 1; i <= 10; i++)
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
            response.Content.ChatList = chatList;
            response.Content.TotalUnReadCount = "50";
            return response;
        }

        public ResponseContext<GetChatContentListReponse> GetChatContentList(RequestContext<GetChatContentListRequest> request)
        {
            throw new NotImplementedException();
        }
    }
}
