using Chat.Model.Enum;
using System;

namespace Chat.Model.Entity.UserInfo
{
    /// <summary>
    /// 用户偏好设置实体类(数据库一一对应)
    /// </summary>
    public class UserPreference
    {
        /// <summary>
        /// 用户唯一标示，自增
        /// </summary>
        public long PreferId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public long UId { get; set; }

        /// <summary>
        /// 用户性别偏好
        /// </summary>
        public GenderEnum PreferGender { get; set; }

        /// <summary>
        /// 用户希望对方所在地偏好
        /// </summary>
        public PartnerPlaceEnum PreferPlace { get; set; }

        /// <summary>
        /// 用户希望对方家乡偏好
        /// </summary>
        public PartnerHomeEnum PreferHome { get; set; }

        /// <summary>
        /// 用户希望对方年龄偏好
        /// </summary>
        public PartnerAgeEnum PreferAge { get; set; }

        /// <summary>
        /// 用户希望对方学校类型偏好
        /// </summary>
        public SchoolTypeEnum PreferSchoolType { get; set; }

        /// <summary>
        /// 用户希望对方状态偏好
        /// </summary>
        public LiveStateEnum PreferLiveState { get; set; }

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
