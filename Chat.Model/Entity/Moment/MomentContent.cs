using Chat.Model.Enum;
using System;

namespace Chat.Model.Entity.Moment
{
    public class MomentContent
    {
        /// <summary>
        /// 动态Id
        /// </summary>
        public Guid MomentId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public long UId { get; set; }

        /// <summary>
        /// 文本内容
        /// </summary>
        public string TextContent { get; set; }

        /// <summary>
        /// 动态目前状态
        /// </summary>
        public MomentStateEnum MomentState { get; set; }
        
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
