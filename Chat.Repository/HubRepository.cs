using Chat.Model.Entity.Hubs;
using Chat.Utility;
using Dapper;
using System;

namespace Chat.Repository
{
    /// <summary>
    /// 集线器数据库
    /// </summary>
    public class HubRepository:BaseRepository
    {
        protected override DbEnum GetDbEnum()
        {
            return DbEnum.ChatConnect;
        }

        private readonly string SELECT_Online = "SELECT Id ,ConnectionId,UId,IsOnline,FirstConnectTime,LastConnectTime FROM dbo.hub_Online ";

        private readonly string SELECT_OnChat = "SELECT Id ,ConnectionId,UId,IsOnline,FirstConnectTime,LastConnectTime FROM dbo.hub_Online ";

        public Online GetOnlineUserByUId(long uid)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = string.Format("{0} Where UId={1}", SELECT_Online, uid);
                    return Db.QueryFirst<Online>(sql);
                }
                catch (Exception ex)
                {
                    Log.Error("GetOnlineUserByUId", "通过UId获取在线用户异常，Uid=" + uid, ex);
                    return null;
                }
            }
        }

        public OnChat GetOnChatByUId(long uid)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = string.Format("{0} Where UId={1}", SELECT_OnChat, uid);
                    return Db.QueryFirst<OnChat>(sql);
                }
                catch (Exception ex)
                {
                    Log.Error("GetOnlineUserByUId", "通过UId获取用户聊天状态信息异常，Uid=" + uid, ex);
                    return null;
                }
            }
        }

        public bool InsertOnlineUser(Online entity)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = @"INSERT INTO dbo.hub_Online (Id,ConnectionId,UId,IsOnline ,FirstConnectTime)
                                VALUES (@Id,@ConnectionId ,@UId ,@IsOnline,@FirstConnectTime)";
                    return Db.Execute(sql, entity)>0;
                }
                catch (Exception ex)
                {
                    Log.Error("InsertOnlineUser", "插入在线用户数据异常", ex);
                    return false;
                }
            }
        }

        public bool OnDisconnected(long uId)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = @"UPDATE dbo.hub_Online
                                   SET IsOnline =false
                                      ,LastConnectTime= @LastConnectTime
                                 WHERE UId=@UId";
                    return Db.Execute(sql, new { UId= uId, LastConnectTime =DateTime.Now}) > 0;
                }
                catch (Exception ex)
                {
                    Log.Error("OnDisconnected", "更新用户下线数据异常", ex);
                    return false;
                }
            }
        }

        public bool OnChatDisconnected(long uId)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = @"UPDATE dbo.hub_OnChat
                                   SET IsOnline =false
                                      ,LastConnectTime= @LastConnectTime
                                 WHERE UId=@UId";
                    return Db.Execute(sql, new { UId = uId, LastConnectTime = DateTime.Now }) > 0;
                }
                catch (Exception ex)
                {
                    Log.Error("OnChatDisconnected", "更新用户离开聊天页面数据异常", ex);
                    return false;
                }
            }
        }

        public bool UpdateOnChat(OnChat entity)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = @"UPDATE dbo.hub_OnChat
                                   SET PartnerUId = @PartnerUId
                                      ,ConnectionId= @ConnectionId
                                      ,IsOnline = @IsOnline
                                      ,LastConnectTime = @LastConnectTime
                                 WHERE UId=@UId";
                    return Db.Execute(sql, entity) > 0;
                }
                catch (Exception ex)
                {
                    Log.Error("UpdateOnChat", "更新用户聊天状态数据异常", ex);
                    return false;
                }
            }
        }

        public bool InsertOnChat(OnChat entity)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = @"INSERT INTO dbo.hub_OnChat (Id,UId ,PartnerUId,ConnectionId,IsOnline ,FirstConnectTime)
                                VALUES (@Id ,@UId,@PartnerUId,@ConnectionId ,@IsOnline,@FirstConnectTime)";
                    return Db.Execute(sql, entity) > 0;
                }
                catch (Exception ex)
                {
                    Log.Error("InsertOnlineUser", "插入在线用户数据异常", ex);
                    return false;
                }
            }
        }

        public bool UpdateOnlineUser(Online entity)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = @"UPDATE dbo.hub_Online
                                   SET ConnectionId = @ConnectionId
                                      ,IsOnline = @IsOnline
                                      ,LastConnectTime= @LastConnectTime
                                 WHERE Id=@Id";
                    return Db.Execute(sql, entity) > 0;
                }
                catch (Exception ex)
                {
                    Log.Error("UpdateOnlineUser", "更新在线用户数据异常", ex);
                    return false;
                }
            }
        }
    }
}
