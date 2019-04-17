using Chat.Model.Entity.Chat;
using Chat.Utility;
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

        private readonly string SELECT_ChatContent = "SELECT ChatId,UId ,PartnerUId,ContentDetail,Type,HasRead,CreateTime,UpdateTime FROM dbo.chat_ChatContent ";
        /// <summary>
        /// 将聊天内容插入数据库
        /// </summary>
        public bool InsertChatContent(ChatContent message)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = @"INSERT INTO dbo.chat_ChatContent (ChatId,UId,PartnerUId,ContentDetail ,Type ,HasRead,CreateTime)
                                VALUES  (@ChatId,@UId ,@PartnerUId ,@ContentDetail,@Type,@HasRead,@CreateTime)";
                    return Db.Execute(sql, message) > 0;
                }
                catch (Exception ex)
                {
                    Log.Error("InsertUserPreference", "将聊天内容插入数据库异常",ex);
                    return false;
                }
            }
        }

        /// <summary>
        /// 获取聊天内容
        /// </summary>
        /// <param name="uId"></param>
        /// <param name="partnerUId"></param>
        /// <returns></returns>
        public List<ChatContent> GetChatContent(long uId,long partnerUId)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = string.Format("{0} Where UId={1} and PartnerUId={2}", SELECT_ChatContent, uId, partnerUId);
                    return Db.Query<ChatContent>(sql).AsList();
                }
                catch (Exception ex)
                {
                    Log.Error("GetUserInfoByUId", string.Format("从数据库获取聊天内容异常UId={0},PartnerUId={1}", uId, partnerUId), ex);
                    return null;
                }
            }
        }

        public List<ChatContent> GetChatContent(long uId,bool isOwner)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = string.Empty;
                    if (isOwner)
                    {
                        sql = @"SELECT temp.ChatId,temp.UId ,temp.PartnerUId,temp.ContentDetail,temp.Type,temp.HasRead,temp.CreateTime,temp.UpdateTime 
                                FROM( SELECT row_number() over(partition by PartnerUId order by CreateTime desc) as rownum,  --对每个分组添加编号
                                             ChatId,
                                             UId ,
                                             PartnerUId,
                                             ContentDetail,
                                             Type,HasRead,
                                             CreateTime,
                                             UpdateTime 
                                       FROM dbo.chat_ChatContent
                                       Where UId=@Uid) temp
                                Where rownum=1  --取出编号为1的数据";
                    }
                    else
                    {
                        sql = @"SELECT temp.ChatId,temp.UId ,temp.PartnerUId,temp.ContentDetail,temp.Type,temp.HasRead,temp.CreateTime,temp.UpdateTime 
                                FROM( SELECT row_number() over(partition by UId order by CreateTime desc) as rownum,  --对每个分组添加编号
                                             ChatId,
                                             UId ,
                                             PartnerUId,
                                             ContentDetail,
                                             Type,HasRead,
                                             CreateTime,
                                             UpdateTime 
                                     FROM dbo.chat_ChatContent
                                     Where PartnerUId=@Uid) temp
                                Where rownum=1 --取出编号为1的数据";
                    }
                    return Db.Query<ChatContent>(sql,new { Uid= uId }).AsList();
                }
                catch (Exception ex)
                {
                    Log.Error("GetChatContent", string.Format("从数据库获取聊天内容异常UId={0},isOwner={1}", uId, isOwner.ToString()), ex);
                    return null;
                }
            }
        }

        /// <summary>
        /// 获取未读条数
        /// </summary>
        /// <param name="uId"></param>
        /// <param name="partnerUId"></param>
        /// <returns></returns>
        public int UnReadCount(long uId,long partnerUId)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = @"Select Count(0)
                                FROM dbo.chat_ChatContent
                                Where HasRead=0 and UId=@UId and PartnerUId=@PartnerUId";
                    return Db.QueryFirstOrDefault<int>(sql,new { UId = uId , PartnerUId = partnerUId });
                }
                catch (Exception ex)
                {
                    Log.Error("UnReadCount", string.Format("从数据库获取未读条数UId={0},PartnerUId={1}", uId, partnerUId), ex);
                    return 0;
                }
            }
        }
    }
}
