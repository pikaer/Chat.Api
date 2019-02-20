using Chat.Model.Entity.UserInfo;
using Chat.Utility;
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

        private readonly string SELECT_USERINFO = "SELECT UId,UNo,OpenId,Gender,NickName,BirthDate,Province,City,Area,HomeProvince,HomeCity,HomeArea,SchoolName,EntranceDate,SchoolType,LiveState ,Mobile,WeChatNo,HeadPhotoPath,Signature,CreateTime,UpdateTime FROM dbo.user_UserInfo ";

        private readonly string SELECT_USERPREFERENCE = "SELECT PreferId,UId ,PreferGender,PreferPlace,PreferHome,PreferAge,PreferSchoolType,PreferLiveState,CreateTime,UpdateTime FROM dbo.user_UserPreference";

        public UserInfo GetUserInfoByUId(long uid)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = string.Format("{0} Where UId={1}", SELECT_USERINFO, uid);
                    return Db.QueryFirst<UserInfo>(sql);
                }
                catch(Exception ex)
                {
                    Log.Error("GetUserInfoByUId", "通过UId获取用户信息异常，Uid=" + uid, ex);
                    return null;
                }
            }
        }

        public UserInfo GetUserInfoByOpenId(string openId)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = string.Format("{0} Where OpenId='{1}'", SELECT_USERINFO, openId);
                    return Db.QueryFirst<UserInfo>(sql);
                }
                catch (Exception ex)
                {
                    Log.Error("GetUserInfoByOpenId", "获取用户信息异常，OpenId=" + openId, ex);
                    return null;
                }
            }
        }

        public bool UpdateUserInfo(UserInfo req)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = @"UPDATE dbo.user_UserInfo
                                   SET Gender = @Gender
                                      ,NickName =@NickName
                                      ,BirthDate = @BirthDate
                                      ,Province =@Province
                                      ,City = @City
                                      ,Area = @Area
                                      ,HomeProvince =@HomeProvince
                                      ,HomeCity = @HomeCity
                                      ,HomeArea =@HomeArea
                                      ,SchoolName = @SchoolName
                                      ,EntranceDate =@EntranceDate
                                      ,SchoolType = @SchoolType
                                      ,LiveState = @LiveState
                                      ,Mobile = @Mobile
                                      ,WeChatNo = @WeChatNo
                                      ,UpdateTime= @UpdateTime
                                 WHERE UId=@UId";
                    return Db.Execute(sql, req) >0;
                }
                catch (Exception ex)
                {
                    Log.Error("UpdateUserInfo", "更新用户信息异常，UId=" + req.UId, ex);
                    return false;
                }
            }
        }

        public bool InsertUserInfo(UserInfo req)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = @"INSERT INTO dbo.user_UserInfo (OpenId,Gender,UNo,NickName,CreateTime,UpdateTime)
                                        VALUES(@OpenId,@Gender,@UNo,@NickName,@CreateTime,@UpdateTime)";
                    return Db.Execute(sql, req) > 0;
                }
                catch (Exception ex)
                {
                    Log.Error("InsertUserInfo", "存入用户信息异常，OpenId=" + req.OpenId, ex);
                    return false;
                }
            }
        }
        public UserPreference GetUserPreference(long uid)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = string.Format("{0} Where UId={1}", SELECT_USERPREFERENCE, uid);
                    return Db.QueryFirst<UserPreference>(sql);
                }
                catch (Exception ex)
                {
                    Log.Error("GetUserInfo", "获取用户偏好设置信息异常，Uid=" + uid, ex);
                    return null;
                }
            }
        }

        public bool InsertUserPreference(UserPreference req)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = @"INSERT INTO dbo.user_UserPreference (UId,PreferGender,PreferPlace,PreferHome,PreferAge ,PreferSchoolType,PreferLiveState,CreateTime,UpdateTime)
                                  VALUES (@UId,@PreferGender,@PreferPlace,@PreferHome,@PreferAge,@PreferSchoolType,@PreferLiveState,@CreateTime,@UpdateTime)";
                    return Db.Execute(sql,req) > 0;
                }
                catch (Exception ex)
                {
                    Log.Error("InsertUserPreference", "存入用户偏好设置异常，UId=" + req.UId, ex);
                    return false;
                }
            }
        }

        public bool UpdateUserPreference(UserPreference req)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = @"UPDATE dbo.user_UserPreference
                                   SET PreferGender = @PreferGender
                                      ,PreferPlace = @PreferPlace
                                      ,PreferHome = @PreferHome
                                      ,PreferAge = @PreferAge
                                      ,PreferSchoolType = @PreferSchoolType
                                      ,PreferLiveState = @PreferLiveState
                                      ,CreateTime = @CreateTime
                                      ,UpdateTime = @UpdateTime
                                 WHERE PreferId=@PreferId";
                    return Db.Execute(sql, req) > 0;
                }
                catch (Exception ex)
                {
                    Log.Error("UpdateUserPreference", "更新用户偏好设置异常，UId=" + req.UId, ex);
                    return false;
                }
            }
        }

        /// <summary>
        /// 获取当前最大用户编号
        /// </summary>
        /// <returns></returns>
        public long GetMaxUNo()
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = "Select Max(UNo) from user_UserInfo";
                    return Db.QueryFirst<long>(sql);
                }
                catch (Exception ex)
                {
                    Log.Error("GetMaxUNo", "获取当前最大用户编号异常", ex);
                    return 0;
                }
            }
        }

    }
}
