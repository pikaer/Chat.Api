using Chat.Model.Entity.Hubs;
using Chat.Repository;
using Infrastructure;
using System;

namespace Chat.Service
{
    /// <summary>
    /// webSocket服务
    /// </summary>
    public class HubService
    {
        private readonly HubRepository hubDal = SingletonProvider<HubRepository>.Instance;

        public Online GetOnlineUserByUId(long uId)
        {
            return hubDal.GetOnlineUserByUId(uId);
        }

        public OnChat GetOnChatByUId(long uId)
        {
            return hubDal.GetOnChatByUId(uId);
        }

        /// <summary>
        /// 登录在线
        /// </summary>
        public bool OnlineConnected(long uId,string connectionId)
        {
            var onlineUser= GetOnlineUserByUId(uId);
            if (onlineUser == null)
            {
                var entity = new Online()
                {
                    Id = Guid.NewGuid(),
                    ConnectionId = connectionId,
                    UId = uId,
                    IsOnline = true,
                    FirstConnectTime = DateTime.Now
                };
                return hubDal.InsertOnlineUser(entity);
            }
            else
            {
                onlineUser.ConnectionId = connectionId;
                onlineUser.IsOnline = true;
                onlineUser.LastConnectTime = DateTime.Now;
                return hubDal.UpdateOnlineUser(onlineUser);
            }
        }

        /// <summary>
        /// 聊天页面在线信息
        /// </summary>
        /// <returns></returns>
        public OnChat OnChatConnected(long uId, long partnerUId, string connectionId)
        {
            var onChat= GetOnChatByUId(uId);
            if (onChat == null)
            {
                var entity = new OnChat()
                {
                    Id = Guid.NewGuid(),
                    ConnectionId = connectionId,
                    UId = uId,
                    PartnerUId= partnerUId,
                    IsOnline = true,
                    FirstConnectTime = DateTime.Now
                };
                hubDal.InsertOnChat(entity);
                return entity;
            }
            else
            {
                onChat.ConnectionId = connectionId;
                onChat.PartnerUId = partnerUId;
                onChat.IsOnline = true;
                onChat.LastConnectTime = DateTime.Now;
                hubDal.UpdateOnChat(onChat);
                return onChat;
            }
        }

        public bool OnChatDisconnected(long uId)
        {
            return hubDal.OnChatDisconnected(uId);
        }

        /// <summary>
        /// 离线
        /// </summary>
        public bool OnlineDisconnected(long uId)
        {
            return hubDal.OnDisconnected(uId);
        }
    }
}
