using Chat.Model.DTO.Chat;
using Chat.Repository;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Utility;
using System;

namespace Chat.Service
{
    public class ChatService
    {
        private UserInfoRepository _userInfoRepository = new UserInfoRepository();
        private ChatRepository _chatRepository = new ChatRepository();

        /// <summary>
        /// 获取消息列表
        /// </summary>
        public List<ChatListDTO> GetChatList(long userId)
        {
            try
            {
                var rtn = new List<ChatListDTO>();
                var userList = _userInfoRepository.GetFriendListByUserId(userId);
                var unreadList = GetUnreadList(userId);
                foreach(var dto in userList)
                {
                    var temp = new ChatListDTO();
                    var unread = unreadList.FirstOrDefault(a => a.UserId == dto.UserId);

                    temp.Gender = dto.Gender;
                    temp.NickName = dto.NickName;
                    temp.UserId = dto.UserId;
                    temp.HeadshotPathDesc = dto.HeadshotPath.ToPathDesc();
                    temp.RecentChatContent = unread == null ? "" : unread.RecentChatContent;
                    temp.UnreadCount = unread == null ? 0 : unread.UnreadCount;
                    temp.RecentChatTimeDesc ="";
                    temp.RecentChatTime = new DateTime();
                    if (unread!=null)
                    {
                        temp.RecentChatTime = unread.RecentChatTime;
                        temp.RecentChatTimeDesc = unread.RecentChatTime.GetDateDesc();
                    }
                    rtn.Add(temp);
                }
                
                return rtn.OrderByDescending(a=>a.RecentChatTime.Value).ToList();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取未读消息列表
        /// </summary>
        public List<UnReadListDTO> GetUnreadList(long userId)
        {
            try
            {
                var list = _chatRepository.GetUnreadList(userId);
                foreach (var dto in list)
                {
                    dto.RecentChatTimeDesc = dto.RecentChatTime.GetDateDesc();
                    dto.UnreadCount = _chatRepository.GetUnReadCount(userId,dto.UserId);
                }
                return list.OrderByDescending(a => a.RecentChatTime.Value).ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}
