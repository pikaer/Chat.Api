using Chat.Model.Enum;
using System;

namespace Chat.Model.Entity.UserInfo
{
    /// <summary>
    /// 用户详情实体类(数据库一一对应)
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 用户唯一标示，自增
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 小程序端-用户唯一标示
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        public GenderEnum Gender { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// 用户所在省份
        /// </summary>
        public int? ProvinceId { get; set; }

        /// <summary>
        /// 用户所在城市
        /// </summary>
        public int? CityId { get; set; }
        
        /// <summary>
        /// 用户家乡所在省份
        /// </summary>
        public int? HomeProvinceId { get; set; }

        /// <summary>
        /// 用户家乡所在城市
        /// </summary>
        public int? HomeCityId { get; set; }
        
        /// <summary>
        /// 学校名称
        /// </summary>
        public string SchoolName { get; set; }

        /// <summary>
        /// 入学日期
        /// </summary>
        public DateTime EntranceDate { get; set; }

        /// <summary>
        /// 学校类型
        /// </summary>
        public SchoolTypeEnum SchoolType { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public LiveStateEnum LiveState { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 微信号
        /// </summary>
        public string WeChatNo { get; set; }

        /// <summary>
        /// 头像路径
        /// </summary>
        public string HeadPhotoPath { get; set; }

        /// <summary>
        /// 个性签名
        /// </summary>
        public string Signature { get; set; }

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
