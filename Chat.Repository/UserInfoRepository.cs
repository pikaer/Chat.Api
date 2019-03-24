using Chat.Model.Entity.UserInfo;
using Chat.Utility;
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

        private readonly string SELECT_USERINFO = "SELECT UId,UNo,OpenId,Gender,NickName,BirthDate,Province,City,Area,HomeProvince,HomeCity,HomeArea,SchoolName,EntranceDate,SchoolType,LiveState ,Mobile,WeChatNo,HeadPhotoPath,BackgroundImg,Signature,CreateTime,UpdateTime FROM dbo.user_UserInfo ";

        private readonly string SELECT_USERPREFERENCE = "SELECT PreferId,UId ,PreferGender,PreferPlace,PreferHome,PreferAge,PreferSchoolType,PreferLiveState,CreateTime,UpdateTime FROM dbo.user_UserPreference ";

        private readonly string SELECT_FRIEND = "SELECT FriendId, UId, PartnerUId,RemarkName,IsDelete, CreateTime, UpdateTime FROM Chat.dbo.user_Friend ";

        private readonly string SELECT_VISITOR = "SELECT VisitorId,UId,PartnerUId,CreateTime  FROM dbo.user_Visitor ";

        public UserInfo GetUserInfoByUId(long uid)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = string.Format("{0} Where UId={1}", SELECT_USERINFO, uid);
                    return Db.QueryFirstOrDefault<UserInfo>(sql);
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

        public List<Friend>GetFriendsByUid(long uid,bool isUId=true)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = string.Format("{0} Where IsDelete=0 and UId={1}", SELECT_FRIEND, uid);
                    if (!isUId)
                    {
                        sql = string.Format("{0} Where IsDelete=0 and PartnerUId={1}", SELECT_FRIEND, uid);
                    }
                    return Db.Query<Friend>(sql).AsList();
                }
                catch (Exception ex)
                {
                    Log.Error("GetFriendsByUid", "获取好友列表信息异常，UId=" + uid, ex);
                    return null;
                }
            }
        }

        public Friend GetFriend(long uId, long partnerUId)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = string.Format("{0} Where UId={1} and PartnerUId={2}", SELECT_FRIEND, uId, partnerUId);
                    
                    return Db.QueryFirstOrDefault<Friend>(sql);
                }
                catch (Exception ex)
                {
                    Log.Error("GetFriendsByUid", "获取好友信息异常", ex);
                    return null;
                }
            }
        }

        public List<Visitor>GetVisitors(long partnerUId)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = string.Format("{0} Where PartnerUId={1}", SELECT_VISITOR,partnerUId);
                    return Db.Query<Visitor>(sql).AsList();
                }
                catch (Exception ex)
                {
                    Log.Error("GetVisitors", "获取访客信息异常。PartnerUId="+ partnerUId, ex);
                    return null;
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
        
        public bool UpdateFriend(Friend entity)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = @"UPDATE dbo.user_Friend SET IsDelete =@IsDelete, UpdateTime = @UpdateTime WHERE FriendId = @FriendId";
                    return Db.Execute(sql, entity) > 0;
                }
                catch (Exception ex)
                {
                    Log.Error("UpdateVisitor", "更新好友信息异常", ex);
                    return false;
                }
            }
        }

        public bool InsertVisitor(Visitor entity)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = @"INSERT INTO dbo.user_Visitor(UId,PartnerUId,CreateTime)
                                VALUES(@UId,@PartnerUId,@CreateTime)";
                    return Db.Execute(sql, entity) > 0;
                }
                catch (Exception ex)
                {
                    Log.Error("InsertVisitor", "存入访客信息异常", ex);
                    return false;
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
                    Log.Error("InsertUserInfo", "存入用户信息异常", ex);
                    return false;
                }
            }
        }

        public bool InsertFriend(Friend entity)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = @"INSERT INTO dbo.user_Friend(UId,PartnerUId ,RemarkName,CreateTime)
                                VALUES(@UId,@PartnerUId,@RemarkName,@CreateTime)";
                    return Db.Execute(sql, entity) > 0;
                }
                catch (Exception ex)
                {
                    Log.Error("InsertFriend", "存入好友信息异常", ex);
                    return false;
                }
            }
        }
    }
}
