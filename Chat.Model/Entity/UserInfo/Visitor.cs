using System;

namespace Chat.Model.Entity.UserInfo
{
    public class Visitor
    {
        /// <summary>
        /// 主键,唯一标识
        /// </summary>
        public long VisitorId { get; set; }

        /// <summary>
        /// 用户Id(主动访问者）
        /// </summary>
        public long UId { get; set; }

        /// <summary>
        /// 对方Id（被访问者）
        /// </summary>
        public long PartnerUId { get; set; }
        
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
