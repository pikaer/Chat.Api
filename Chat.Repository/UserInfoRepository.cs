using Chat.Model.Entity.UserInfo;
using Chat.Model.Utils;
using Chat.Utility;
using Dapper;
using Dapper.Contrib.Extensions;
using Infrastructure;
using System;

namespace Chat.Repository
{
    public class UserInfoRepository: BaseRepository
    {
        protected override DbEnum GetDbEnum()
        {
            return DbEnum.ChatConnect;
        }

        private readonly string SELECT_USERINFO = "SELECT UId,OpenId,Gender,NickName,BirthDate,Province,City,Area,HomeProvince,HomeCity,HomeArea,SchoolName,EntranceDate,SchoolType,LiveState ,Mobile,WeChatNo,HeadPhotoPath,Signature,CreateTime,UpdateTime FROM dbo.user_UserInfo ";

        private readonly string SELECT_USERPREFERENCE = "SELECT PreferId,UId ,PreferGender,PreferPlace,PreferHome,PreferAge,PreferSchoolType,PreferLiveState,CreateTime,UpdateTime FROM dbo.user_UserPreference";

        public UserInfo GetUserInfo(long uid)
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
                    Log.Error("GetUserInfo", "获取用户信息异常，Uid=" + uid, ex);
                    return null;
                }
            }
        }
        public bool InsertUserInfo(UserInfo req)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    return Db.Insert(req) > 0;
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
                    return Db.Insert(req) > 0;
                }
                catch (Exception ex)
                {
                    Log.Error("InsertUserPreference", "存入用户偏好设置异常，UId=" + req.UId, ex);
                    return false;
                }
            }
        }
    }
}
