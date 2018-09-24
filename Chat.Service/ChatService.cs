using Chat.Model.DTO.Chat;
using Chat.Repository;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Utility;
using System;
using Chat.Model.Enum;
using Chat.Model.Utils;

namespace Chat.Service
{
    public class ChatService
    {
        private UserInfoRepository _userInfoRepository = new UserInfoRepository();
        private ChatRepository _chatRepository = new ChatRepository();

        /// <summary>
        /// 获取消息列表
        /// </summary>
        public List<ChatListDTO> GetChatList(ChatListRequest request)
        {
            var rtn = new List<ChatListDTO>();
            var userList = _userInfoRepository.GetFriendListByUserId(request.UserId);
            var unreadList = GetUnreadList(request.UserId);
            foreach (var dto in userList)
            {
                var temp = new ChatListDTO();
                var unread = unreadList.FirstOrDefault(a => a.UserId == dto.UserId);

                temp.Gender = dto.Gender;
                temp.NickName = dto.NickName.Trim();
                temp.UserId = dto.UserId;
                temp.HeadshotPathDesc = dto.HeadshotPath.ToPathDesc();
                temp.RecentChatContent = unread == null ? "" : unread.RecentChatContent.Trim();
                temp.UnreadCount = unread == null ? "" : unread.UnreadCount;
                temp.RecentChatTimeDesc = "";
                temp.RecentChatTime = new DateTime();
                if (unread != null)
                {
                    temp.RecentChatTime = unread.RecentChatTime;
                    temp.RecentChatTimeDesc = unread.RecentChatTime.HasValue? unread.RecentChatTime.Value.GetDateDesc():"";
                }
                rtn.Add(temp);
            }
            return rtn.OrderByDescending(a => a.RecentChatTime.Value).Skip(request.PageCount * (request.PageIndex - 1)).Take(request.PageCount).ToList();
        }

        /// <summary>
        /// 获取未读消息列表
        /// </summary>
        public List<UnReadListDTO> GetUnreadList(long userId)
        {
            var list = _chatRepository.GetUnreadList(userId);
            foreach (var dto in list)
            {
                dto.RecentChatContent = dto.Type == ChatContentEnum.Text ? dto.RecentChatContent : dto.Type.ToDescription();
                dto.RecentChatTimeDesc = dto.RecentChatTime.HasValue?dto.RecentChatTime.Value.GetDateDesc():"";
                var count = _chatRepository.GetUnReadCount(userId, dto.UserId);
                dto.UnreadCount = count > 0 ? count.ToString() : "";
            }
            return list.OrderByDescending(a => a.RecentChatTime.Value).ToList();
        }

        /// <summary>
        /// 清理未读提示
        /// </summary>
        public bool ClearUnRead(CommonRequest request)
        {
            return _chatRepository.ClearUnRead(request);
        }

        /// <summary>
        /// 获取聊天列表
        /// </summary>
        public List<ChatContentListDTO> GetChatContentList(CommonRequest request)
        {
            var rtn = new List<ChatContentListDTO>();
            var list = _chatRepository.GetChatHistories(request.UserId, request.PartnerId);
            foreach (var dto in list)
            {
                var user = _userInfoRepository.GetUserInfoByOpenId("", dto.UserId);
                var temp = new ChatContentListDTO()
                {
                    UserId = dto.UserId,
                    ChatContent = dto.ChatContent,
                    CreateTime = dto.CreateTime,
                    CreateTimeDesc = dto.CreateTime.GetDateDesc(),
                    Gender = user.Gender,
                    NickName = user.NickName,
                    HeadshotPathDesc = user.HeadshotPath.ToPathDesc()
                };
                rtn.Add(temp);
            }
            return rtn.OrderByDescending(a => a.CreateTime).ToList();
        }
    }
}
