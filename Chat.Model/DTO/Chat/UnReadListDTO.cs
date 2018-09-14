using Chat.Model.Enum;
using System;

namespace Chat.Model.DTO.Chat
{
    public class UnReadListDTO
    {
        /// <summary>
        /// 用户唯一标示，自增
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 最近聊天内容
        /// </summary>
        public string RecentChatContent { get; set; }

        /// <summary>
        /// 聊天内容类别
        /// </summary>
        public ChatContentEnum Type { get; set; }
        
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
