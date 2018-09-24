using Chat.Model.Enum;
using System;

namespace Chat.Model.DTO.Chat
{
    /// <summary>
    /// 聊天消息列表
    /// </summary>
    public class ChatContentListDTO
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
        /// 聊天内容
        /// </summary>
        public string ChatContent { get; set; }

        /// <summary>
        /// 聊天时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 聊天时间
        /// </summary>
        public string CreateTimeDesc { get; set; }
    }
}
