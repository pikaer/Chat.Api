namespace Chat.Model.Utils
{
    public class CommonRequest
    {
        /// <summary>
        /// 用户唯一标示，自增
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 对方Id
        /// </summary>
        public long PartnerId { get; set; }
    }
}
