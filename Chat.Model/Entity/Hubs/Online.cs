using System;

namespace Chat.Model.Entity.Hubs
{
    /// <summary>
    /// 登录在线用户
    /// </summary>
    public class Online
    {
        /// <summary>
        /// 唯一标示
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// webSocket连接Id
        /// </summary>
        public string ConnectionId { get; set; }

        /// <summary>
        /// 在线用户
        /// </summary>
        public long UId { get; set; }

        /// <summary>
        /// 是否在线
        /// </summary>
        public bool IsOnline { get; set; }

        /// <summary>
        /// 首次登录时间
        /// </summary>
        public DateTime FirstConnectTime { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LastConnectTime { get; set; }
    }
}
