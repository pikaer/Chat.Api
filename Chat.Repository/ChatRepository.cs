using Chat.Model.DTO.Chat;
using Chat.Model.Entity.Chat;
using Chat.Model.Utils;
using Dapper;
using System;
using System.Collections.Generic;

namespace Chat.Repository
{
    public class ChatRepository : BaseRepository
    {
        protected override DbEnum GetDbEnum()
        {
            return DbEnum.ChatConnect;
        }

        /// <summary>
        /// 获取未读消息条数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<UnReadListDTO> GetUnreadList(long userId)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = @"SELECT  temp.UserId, 
                                        temp.ChatContent AS RecentChatContent,
                                        temp.Type,
                                        temp.CreateTime AS RecentChatTime
                                FROM (
                                	  SELECT row_number() over(partition by history.UserId order by history.CreateTime desc) as rownum  --对每个分组添加编号
                                             ,UserId,ChatContent,Type,CreateTime
                                      FROM dbo.chat_ChatHistory history
                                	  WHERE history.PartnerId=@UserId
                                	  ) temp
                                Where temp.rownum=1 --取出编号为1的数据";
                    return Db.Query<UnReadListDTO>(sql, new { UserId = userId }).AsList();
                }
                catch 
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 获取未读消息数量
        /// </summary>
        /// <param name="userId">当前登录人</param>
        /// <param name="partnerId">好友</param>
        /// <returns></returns>
        public int GetUnReadCount(long userId,long partnerId)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = @"SELECT COUNT(1)
                                FROM dbo.chat_ChatHistory history
                                INNER JOIN dbo.friend_Friend friend
                                ON friend.UserId=history.PartnerId AND friend.PartnerId=history.UserId
                                WHERE history.PartnerId=@UserId AND history.UserId=@PartnerId AND history.CreateTime>ISNULL(friend.ReadTime,'1990.1.1')";
                    return Db.QueryFirstOrDefault<int>(sql, new { UserId = userId, PartnerId = partnerId });
                }
                catch
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 清理未读提示
        /// </summary>
        public bool ClearUnRead(CommonRequest request)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = @"UPDATE dbo.friend_Friend
                                   SET UpdateTime =@now
                                      ,ReadTime =@now
                                 WHERE UserId=@UserId AND PartnerId=@PartnerId";
                    return Db.QueryFirstOrDefault<int>(sql, new { request.UserId, request.PartnerId, now =DateTime.Now})>0;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 删除聊天记录
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="partnerId">好友Id</param>
        public bool DeleteChatHistory(long userId,long partnerId)
        {
            using (var Db = GetDbConnection())
            {
                var sql = @"DELETE FROM dbo.chat_ChatHistory WHERE (UserId=@UserId and PartnerId=@PartnerId) or (UserId=@PartnerId and PartnerId=@UserId)";
                return Db.Execute(sql, new {UserId= userId, PartnerId= partnerId }) > 0;
            }
        }

        public List<ChatHistory> GetChatHistories(long userId, long partnerId)
        {
            using (var Db = GetDbConnection())
            {
                var sql = @"SELECT Id
                                  ,UserId
                                  ,PartnerId
                                  ,ChatContent
                                  ,Type
                                  ,CreateTime
                              FROM dbo.chat_ChatHistory
                              Where (UserId=@UserId and PartnerId=@PartnerId) or (UserId=@PartnerId and PartnerId=@UserId)";
                return Db.Query<ChatHistory>(sql, new { UserId = userId, PartnerId = partnerId }).AsList();
            }
        }
    }
}
