using System;

namespace Chat.Model.Entity.Moment
{
    /// <summary>
    /// 动态评论
    /// </summary>
    public class MomentDiscuss
    {
        /// <summary>
        /// 唯一标示
        /// </summary>
        public Guid DiscussId { get; set; }

        /// <summary>
        /// 动态Id
        /// </summary>
        public Guid MomentId { get; set; }

        /// <summary>
        /// 发表Moment的用户Id(记载一次Uid,减少联合查询)
        /// </summary>
        public long UId { get; set; }

        /// <summary>
        /// 评论人的Id
        /// </summary>
        public long PartnerUId { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string DiscussContent { get; set; }

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
