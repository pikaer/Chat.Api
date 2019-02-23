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
                    CurrentTotalUnReadCount="67"
                }
            };
            return response;
        }


        public GetChatContentListReponse ChatContentListTestData()
        {
            var rtn = new GetChatContentListReponse();
            var item = new List<ChatContentDetail>();
            var today = DateTime.Now;
            for(int i = 1; i <= 40; i++)
            {
                var dto = new ChatContentDetail()
                {
                    IsOwner = i % 3 == 0, //取余数
                    HeadImgPath= i % 3==0? "../../content/images/pikaer.jpg" : "../../content/images/partner.jpg",
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

        public ResponseContext<SendMessageResponse> SendMessage(RequestContext<SendMessageRequest> request)
        {
            throw new NotImplementedException();
        }
    }
}
