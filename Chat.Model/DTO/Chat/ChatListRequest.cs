namespace Chat.Model.DTO.Chat
{
    public class ChatListRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页多少个
        /// </summary>
        public int PageCount { get; set; }
    }
}
