using Chat.Model.Utils;
using Chat.Utility;
using Dapper;
using Infrastructure;
using System;

namespace Chat.Repository
{
    public class UserInfoRepository: BaseRepository
    {
        public static UserInfoRepository Instance = SingletonProvider<UserInfoRepository>.Instance;

        protected override DbEnum GetDbEnum()
        {
            return DbEnum.ChatConnect;
        }

        private readonly string SELECT_USERINFO = "SELECT UId,OpenId,Gender,NickName,BirthDate,Province,City,Area,HomeProvince,HomeCity,HomeArea,SchoolName,EntranceDate,SchoolType,LiveState ,Mobile,WeChatNo,HeadPhotoPath,Signature,CreateTime,UpdateTime FROM dbo.user_UserInfo ";

        private readonly string SELECT_USERPREFERENCE = "SELECT PreferId,UId ,PreferGender,PreferPlace,PreferHome,PreferAge,PreferSchoolType,PreferLiveState,CreateTime,UpdateTime FROM dbo.user_UserPreference";

        public GetUserInfoResponse GetUserInfo(long uid)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = string.Format("{0} Where UId={1}", SELECT_USERINFO, uid);
                    return Db.QueryFirst<GetUserInfoResponse>(sql);
                }
                catch(Exception ex)
                {
                    Log.Error("GetUserInfo", "获取用户信息异常，Uid=" + uid, ex);
                    return null;
                }
            }
        }

    }
}
