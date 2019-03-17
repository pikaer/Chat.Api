using System;

namespace Chat.Model.Entity.Moment
{
    /// <summary>
    /// 评论点赞
    /// </summary>
    public class DiscussSupport
    {
        /// <summary>
        /// 唯一标示
        /// </summary>
        public Guid DiscussSupportId { get; set; }

        /// <summary>
        /// 动态评论Id
        /// </summary>
        public Guid DiscussId { get; set; }

        /// <summary>
        /// 点赞人的Id
        /// </summary>
        public long UId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
