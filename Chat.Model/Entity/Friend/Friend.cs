using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Model.Entity.Friend
{
    /// <summary>
    /// 好友实体类
    /// </summary>
    public class Friend
    {
        /// <summary>
        /// 主键,唯一标识
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 对方Id
        /// </summary>
        public long PartnerId { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }
        

        /// <summary>
        /// 最新阅读聊天内容时间
        /// </summary>
        public DateTime? ReadTime { get; set; }
    }
}
