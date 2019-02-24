using Chat.Model.Enum;
using System;

namespace Chat.Model.Entity.Chat
{
    public class ChatContent
    {
        ///<summary>
        /// 会话Id,唯一标识符
        /// </summary>
        public Guid ChatId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public long UId { get; set; }

        /// <summary>
        /// 对方Id
        /// </summary>
        public long PartnerUId { get; set; }
        
        /// <summary>
        /// 本条聊天内容
        /// </summary>
        public string ContentDetail { get; set; }

        /// <summary>
        /// 聊天内容类别
        /// </summary>
        public ChatContentTypeEnum Type { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        public bool HasRead { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }
       
    }
}
