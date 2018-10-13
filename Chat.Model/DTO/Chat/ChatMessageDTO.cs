namespace Chat.Model.DTO.Chat
{
    public class ChatMessageDTO
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 对方Id
        /// </summary>
        public long PartnerId { get; set; }

        /// <summary>
        /// 聊天内容
        /// </summary>
        public string ChatContent { get; set; }
    }
}
