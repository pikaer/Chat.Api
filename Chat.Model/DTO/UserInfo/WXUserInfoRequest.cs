using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Model.DTO.UserInfo
{
    /// <summary>
    /// 小程序存入用户信息请求
    /// </summary>
    public class WXUserInfoRequest
    {
        /// <summary>
        /// 小程序端-用户唯一标示
        /// </summary>
        public string openId { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string nickName { get; set; }

        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        public int gender { get; set; }

        /// <summary>
        /// 用户所在城市
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// 用户所在省份
        /// </summary>
        public string province { get; set; }

        /// <summary>
        /// 用户所在国家
        /// </summary>
        public string country { get; set; }

        /// <summary>
        /// 头像路径
        /// </summary>
        public string avatarUrl { get; set; }

        /// <summary>
        /// 用户的语言，简体中文为zh_CN
        /// </summary>
        public string language { get; set; }
    }
}
