using System;

namespace Chat.Model.Entity.GoldCoin
{
    /// <summary>
    /// 金币表实体
    /// </summary>
    public class GoldCoin
    {
        /// <summary>
        /// 唯一标识Id，自增
        /// </summary>
        public long CoinId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public long UId { get; set; }

        /// <summary>
        /// 金币数量
        /// </summary>
        public long CoinTotal { get; set; }

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
