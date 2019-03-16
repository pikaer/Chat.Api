using System;

namespace Chat.Model.Entity.Moment
{
    public class MomentImg
    {
        /// <summary>
        /// 图片唯一标示
        /// </summary>
        public Guid ImgId { get; set; }

        /// <summary>
        /// 动态Id
        /// </summary>
        public Guid MomentId { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImgPath { get; set; }

        /// <summary>
        /// 压缩后的路径
        /// </summary>
        public string CompressPath { get; set; }

        /// <summary>
        /// 文件原图大小
        /// </summary>
        public long ImgLength { get; set; }

        /// <summary>
        /// 后缀
        /// </summary>
        public string ImgMime { get; set; }

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
