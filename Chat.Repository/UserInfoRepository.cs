using Chat.Model.Entity.UserInfo;
using Dapper;
using System;

namespace Chat.Repository
{
    public class UserInfoRepository: BaseRepository
    {
        protected override DbEnum GetDbEnum()
        {
            return DbEnum.ChatConnect;
        }

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
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}
