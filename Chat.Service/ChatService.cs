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
                {
                    ChatList = new List<ChatListType>(),
                    TotalUnReadCount=""
                }
            };
            //我主动发出的消息
            var myMessages = chatDal.GetChatContent(request.Content.UId, true);
            if (myMessages.NotEmpty())
            {
                foreach(var item in myMessages)
                {
                    var userinfo = userInfoDal.GetUserInfoByUId(item.PartnerUId);
                    var dto = new ChatListType()
                    {
                        PartnerUId=item.PartnerUId,
                        HeadImgPath= userinfo.HeadPhotoPath.GetImgPath(),
                        DispalyName= userinfo.NickName,
                        LatestChatTime=item.CreateTime.GetDateDesc(),
                        LatestChatContent=item.ContentDetail,
                        ChatContentType=item.Type,
                        UnReadCount="",
                    };
                    response.Content.ChatList.Add(dto);
                }
            }

            int unReadCount = 0;

            //我被动接受的信息
            var partnerMessages = chatDal.GetChatContent(request.Content.UId, false);
            if (partnerMessages.NotEmpty())
            {
                foreach(var item in partnerMessages)
                {
                    if (response.Content.ChatList.Exists(a => a.PartnerUId == item.UId))
                    {
                        var myMessage = myMessages.FirstOrDefault(a => a.PartnerUId == item.UId);
                        if(item.CreateTime> myMessage.CreateTime)
                        {
                            response.Content.ChatList.RemoveAll(a => a.PartnerUId == item.UId);
                            response.Content.ChatList.Add(BuildChatType(item, ref unReadCount));
                        }
                    }
                    else
                    {
                        response.Content.ChatList.Add(BuildChatType(item, ref unReadCount));
                    }
                }
            }

            //未读总数
            if (unReadCount == 0)
            {
                response.Content.TotalUnReadCount = "";
            }
            else if (unReadCount > 0 && unReadCount < 100)
            {
                response.Content.TotalUnReadCount = unReadCount.ToString();
            }
            else
            {
                response.Content.TotalUnReadCount = "99+";
            }
            return response;
        }

        private ChatListType BuildChatType(ChatContent content,ref int unReadCount)
        {
            var userinfo = userInfoDal.GetUserInfoByUId(content.UId);
            int unread = chatDal.UnReadCount(content.UId, content.PartnerUId);

            string unreadStr =string.Empty;
            if (unread == 0)
            {
                unreadStr = "";
            }
            else if(unread>0&& unread < 100)
            {
                unreadStr = unread.ToString();
            }
            else
            {
                unreadStr = "99+";
            }
            unReadCount += unread;

            return new ChatListType()
            {
                PartnerUId= content.UId,
                HeadImgPath = userinfo.HeadPhotoPath.GetImgPath(),
                DispalyName = userinfo.NickName,
                LatestChatTime = content.CreateTime.GetDateDesc(),
                LatestChatContent = content.ContentDetail,
                ChatContentType = content.Type,
                UnReadCount = unreadStr,
            };
        }

        public ResponseContext<GetChatContentListReponse> GetChatContentList(RequestContext<GetChatContentListRequest> request)
        {
            var response = new ResponseContext<GetChatContentListReponse>()
            {
                Content =new GetChatContentListReponse()
            };

            var rtn = new List<ChatContentDetail>();

            //我发给对方消息
            var myMessages = GetChatContentList(request.Content.UId, request.Content.PartnerUId, true);
            if(myMessages.NotEmpty())
            {
                rtn.AddRange(myMessages);
            }

            //对方发给我的消息
            var partnerMessages = GetChatContentList(request.Content.PartnerUId, request.Content.UId, false);
            if(partnerMessages.NotEmpty())
            {
                rtn.AddRange(partnerMessages);
            }

            rtn = rtn.OrderBy(a => a.CreateTime).ToList();
            if (rtn.NotEmpty()&&rtn.Count > 1)
            {
                //聊天时间相隔小于10分钟就不展示
                for (int i = 0; i < rtn.Count-1; i++)
                {
                    var time1 = rtn[i].CreateTime;
                    var time2 = rtn[i + 1].CreateTime;
                    if (time1.AddMinutes(10) < time2)
                    {
                        rtn[i].ChatTime = "";
                    }
                }

                //每次返回30条
                int pageIndex = request.Content.PageIndex;
                rtn =rtn.Skip(30* (pageIndex - 1)).Take(30).ToList();
            }
            

            response.Content.ChatContentList = rtn;
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
                    HeadImgPath = userInfo.HeadPhotoPath.GetImgPath(),
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
