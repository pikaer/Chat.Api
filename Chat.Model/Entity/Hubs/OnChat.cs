using System;

namespace Chat.Model.Entity.Hubs
{
    /// <summary>
    /// 正在聊天用户
    /// </summary>
    public class OnChat
    {
        /// <summary>
        /// 唯一标示
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public long UId { get; set; }

        /// <summary>
        /// 正在聊天对象
        /// </summary>
        public long PartnerUId { get; set; }

        /// <summary>
        /// webSocket连接Id
        /// </summary>
        public string ConnectionId { get; set; }

        /// <summary>
        /// 是否在线
        /// </summary>
        public bool IsOnline { get; set; }

        /// <summary>
        /// 首次连接时间
        /// </summary>
        public DateTime FirstConnectTime { get; set; }

        /// <summary>
        /// 最后连接时间
        /// </summary>
        public DateTime LastConnectTime { get; set; }
    }
}
