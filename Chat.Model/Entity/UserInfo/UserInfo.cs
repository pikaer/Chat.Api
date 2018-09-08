using Chat.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Model.Entity.UserInfo
{
    /// <summary>
    /// 用户详情实体类，结合小程序提供接口：wx.getUserInfo
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
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        public GenderEnum Gender { get; set; }

        /// <summary>
        /// 用户所在城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 用户所在省份
        /// </summary>
        public string Province { get; set; }
        
        /// <summary>
        /// 用户所在国家
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// 用户的语言，简体中文为zh_CN
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 头像路径
        /// </summary>
        public string HeadshotPath { get; set; }

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
