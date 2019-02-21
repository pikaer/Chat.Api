using Chat.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Model.Entity.GoldCoin
{
    public class CoinOperateHistory
    {
        /// <summary>
        /// 唯一标识Id，自增
        /// </summary>
        public long CoinConsumeId { get; set; }

        /// <summary>
        ///用户Id 
        /// </summary>
        public long UId { get; set; }

        /// <summary>
        /// 金币收支描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 金币变化量
        /// </summary>
        public int AlertCoinNum { get; set; }

        /// <summary>
        /// 金币操作来源
        /// </summary>
        public OperateSourceEnum OperateSource { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDeleted { get; set; }

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
