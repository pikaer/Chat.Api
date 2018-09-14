using Chat.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Model.DTO.Chat
{
    /// <summary>
    /// 消息列表
    /// </summary>
    public class ChatListDTO
    {
        /// <summary>
        /// 用户唯一标示，自增
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        public GenderEnum Gender { get; set; }

        /// <summary>
        /// 头像路径
        /// </summary>
        public string HeadshotPathDesc { get; set; }

        /// <summary>
        /// 最近聊天内容
        /// </summary>
        public string RecentChatContent { get; set; }
        
        /// <summary>
        /// 未读消息条数
        /// </summary>
        public string UnreadCount { get; set; }

        /// <summary>
        /// 最近聊天时间
        /// </summary>
        public DateTime? RecentChatTime { get; set; }

        /// <summary>
        /// 最近聊天时间
        /// </summary>
        public string RecentChatTimeDesc { get; set; }
    }
}
