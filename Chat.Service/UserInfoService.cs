using Chat.Model.DTO.UserInfo;
using Chat.Model.Entity.UserInfo;
using Chat.Model.Enum;
using Chat.Model.Utils;
using Chat.Repository;
using Infrastructure.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Service
{
    public class UserInfoService
    {
        private ChatRepository _chatRepository = new ChatRepository();
        private UserInfoRepository _userInfoRepository = new UserInfoRepository();
        public UserInfo SetUserInfo(WXUserInfoRequest req)
        {
            var userInfo = _userInfoRepository.GetUserInfoByOpenIdOrUserId(req.openId);
            if (userInfo == null)
            {
                var dto = new UserInfo()
                {
                    OpenId = req.openId,
                    NickName = req.nickName,
                    Gender = (GenderEnum)req.gender,
                    City = req.city,
                    Province = req.province,
                    Country = req.country,
                    Language = req.language,
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                };
                string headshotPath = Guid.NewGuid().ToString();
                string path = ConfigurationHelper.AppSettings["HeadPhoto"] + headshotPath + ConfigurationHelper.AppSettings["HeadshotFormat"];
                bool save = ImgHelper.Saveimages(req.avatarUrl, path);
                if (save)
                {
                    dto.HeadshotPath = headshotPath;
                }
                var ret = _userInfoRepository.SetUserInfo(dto);
                if (ret)
                {
                    userInfo = _userInfoRepository.GetUserInfoByOpenIdOrUserId(req.openId);
                }
            }
            return userInfo;
        }

        /// <summary>
        /// 删除好友以及消息
        /// </summary>
        public bool DeleteFriend(CommonRequest request)
        {
            _chatRepository.DeleteChatHistory(request.UserId, request.PartnerId);
            return _userInfoRepository.DeleteFriend(request.UserId, request.PartnerId);
        }
    }
}
