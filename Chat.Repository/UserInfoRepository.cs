using Chat.Model.Entity.UserInfo;
using Dapper;
using System;
using System.Collections.Generic;

namespace Chat.Repository
{
    public class UserInfoRepository: BaseRepository
    {
        protected override DbEnum GetDbEnum()
        {
            return DbEnum.ChatConnect;
        }

        /// <summary>
        /// 保存用户信息
        /// </summary>
        public bool SetUserInfo(UserInfo dto)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = @"INSERT INTO dbo.user_UserInfo
                                            (OpenId
                                            ,NickName
                                            ,Gender
                                            ,City
                                            ,Province
                                            ,Country
                                            ,Language
                                            ,Mobile
                                            ,HeadshotPath
                                            ,Signature
                                            ,CreateTime
                                            ,UpdateTime)
                                      VALUES
                                            (@OpenId
                                            ,@NickName
                                            ,@Gender
                                            ,@City
                                            ,@Province
                                            ,@Country
                                            ,@Language
                                            ,@Mobile
                                            ,@HeadshotPath
                                            ,@Signature
                                            ,@CreateTime
                                            ,@UpdateTime)";
                    return Db.Execute(sql,dto) > 0;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 获取好友列表
        /// </summary>
        public List<UserInfo> GetFriendListByUserId(long userId)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = @"SELECT users.UserId
                                      ,users.OpenId
                                      ,users.NickName
                                      ,users.Gender
                                      ,users.City
                                      ,users.Province
                                      ,users.Country
                                      ,users.Language
                                      ,users.Mobile
                                      ,users.HeadshotPath
                                      ,users.Signature
                                      ,users.CreateTime
                                      ,users.UpdateTime 
                                 FROM dbo.friend_Friend friend
                                 INNER JOIN dbo.user_UserInfo users
                                 ON users.UserId=friend.PartnerId 
                                 WHERE friend.UserId=@UserId";
                    return Db.Query<UserInfo>(sql, new { UserId = userId }).AsList();
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
