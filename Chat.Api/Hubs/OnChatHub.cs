using Chat.Model.Entity.Hubs;
using Chat.Repository;
using Chat.Service;
using Infrastructure;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Chat.Api.Hubs
{
    public class OnChatHub: Hub
    {
        private UserInfoRepository _userInfoDal = SingletonProvider<UserInfoRepository>.Instance;
        private HubService hubService = SingletonProvider<HubService>.Instance;
        private static readonly object SyncObj = new object();

        /// <summary>
        ///  在线用户连接Id =>UId 映射
        /// </summary>
        public static ConcurrentDictionary<string, OnChat> OnlineChats { get; set; }

        /// <summary>
        /// 成功连接
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            long uId = Convert.ToInt64(Context.GetHttpContext().Request.Query["UId"]);
            long partnerUId = Convert.ToInt64(Context.GetHttpContext().Request.Query["PartnerUId"]);
            var user = _userInfoDal.GetUserInfoByUId(uId);
            var partner = _userInfoDal.GetUserInfoByUId(partnerUId);
            if (user != null&&partner!=null)
            {
                lock (SyncObj)
                {
                    var onChat=hubService.OnChatConnected(uId, partnerUId,Context.ConnectionId);
                    if (onChat != null)
                    {
                        OnlineChats[Context.ConnectionId] = onChat;
                    }
                }
            }
            await base.OnConnectedAsync();
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
            lock (SyncObj)
            {
                OnlineChats.TryRemove(Context.ConnectionId, out OnChat onChat);
                hubService.OnChatDisconnected(onChat.UId);
            }
        }
    }
}
