﻿using System;

namespace Chat.Model.Entity.Moment
{
    /// <summary>
    /// 动态点赞
    /// </summary>
    public class MomentSupport
    {
        /// <summary>
        /// 唯一标示
        /// </summary>
        public Guid SupportId { get; set; }

        /// <summary>
        /// 动态Id
        /// </summary>
        public Guid MomentId { get; set; }
        
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
