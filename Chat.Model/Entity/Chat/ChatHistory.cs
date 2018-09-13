using Chat.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Model.Entity.Chat
{
    public class ChatHistory
    {
        ///<summary>
        /// 唯一标识符
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 对方Id
        /// </summary>
        public long PartnerId { get; set; }

        /// <summary>
        /// 本条聊天内容
        /// </summary>
        public string ChatContent { get; set; }

        /// <summary>
        /// 聊天内容类别
        /// </summary>
        public ChatContentEnum Type { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
