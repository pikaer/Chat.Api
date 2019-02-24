using Chat.Model.Enum;
using System;

namespace Chat.Model.Entity.UserInfo
{
    /// <summary>
    /// 好友实体类
    /// </summary>
    public class Friend
    {
        /// <summary>
        /// 主键,唯一标识
        /// </summary>
        public long FriendId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public long UId { get; set; }

        /// <summary>
        /// 对方Id
        /// </summary>
        public long PartnerUId { get; set; }

        /// <summary>
        /// 是否逻辑删除
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 添加好友方式
        /// </summary>
        public FunctionEnum AddType { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }
    }
}
