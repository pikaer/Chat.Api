using Chat.Model.Entity.UserInfo;
using Dapper.Contrib.Extensions;
using System;

namespace Chat.Repository
{
    public class UserInfoRepository: BaseRepository
    {
        protected override DbEnum GetDbEnum()
        {
            return DbEnum.MyChat;
        }

        public bool SetUserInfo(UserInfo dto)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    return Db.Insert(dto) > 0;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}
