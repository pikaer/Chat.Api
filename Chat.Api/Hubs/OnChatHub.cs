using Chat.Model.Entity.Hubs;
using Chat.Repository;
using Chat.Service;
using Chat.Utility;
using Infrastructure;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chat.Api.Hubs
{
    public class OnChatHub: Hub
    {
        private UserInfoRepository _userInfoDal = SingletonProvider<UserInfoRepository>.Instance;
        private HubService hubService = SingletonProvider<HubService>.Instance;

        //操作OnlineChats的时候，要加锁
        private static readonly object SyncObj = new object();

        /// <summary>
        ///  在线用户连接Id =>UId 映射
        /// </summary>
        public static ConcurrentDictionary<string, OnChat> OnlineChats { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        static OnChatHub()
        {
            OnlineChats = new ConcurrentDictionary<string, OnChat>();
        }

        /// <summary>
        /// 成功连接
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            try
            {
                //用户连接信息同步到数据库目的：不同页面之间共享用户行为信息
                long uId = Convert.ToInt64(Context.GetHttpContext().Request.Query["UId"]);
                long partnerUId = Convert.ToInt64(Context.GetHttpContext().Request.Query["PartnerUId"]);
                var user = _userInfoDal.GetUserInfoByUId(uId);
                var partner = _userInfoDal.GetUserInfoByUId(partnerUId);
                if (user != null && partner != null)
                {
                    lock (SyncObj)
                    {
                        var onChat = hubService.OnChatConnected(uId, partnerUId, Context.ConnectionId);
                        if (onChat != null)
                        {
                            OnlineChats[Context.ConnectionId] = onChat;
                        }
                    }
                }
                await base.OnConnectedAsync();
            }
            catch (Exception ex)
            {
                Log.Error("OnChatHub-OnConnectedAsync", "用户连接异常", ex,null,
                    new Dictionary<string, string>()
                    {
                        { "UId",Context.GetHttpContext().Request.Query["UId"] },
                        { "PartnerUId",Context.GetHttpContext().Request.Query["PartnerUId"] }
                    });
            }
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception exception=null)
        {
            try
            {
                await base.OnDisconnectedAsync(exception);

                lock (SyncObj)
                {
                    long uId = Convert.ToInt64(Context.GetHttpContext().Request.Query["UId"]);
                    OnlineChats.TryRemove(Context.ConnectionId, out OnChat onChat);
                    hubService.OnChatDisconnected(onChat.UId);
                }
            }
            catch (Exception ex)
            {
                Log.Error("OnChatHub-OnDisconnectedAsync", "用户断开连接异常", ex, null,
                    new Dictionary<string, string>()
                    {
                        { "ConnectionId",Context.GetHttpContext().Request.Query["ConnectionId"] }
                    });
            }
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        public async Task SubScribeMessage(long uId, long partnerUId)
        {
            try
            {
                var onChat = hubService.GetOnChatByUId(partnerUId);

                //当对方正在和自己聊天,通知对方刷新页面
                if (onChat!=null&&onChat.IsOnline&& onChat.PartnerUId== uId)
                {
                    await Clients.Client(onChat.ConnectionId).SendAsync("receive", new { partnerUId= uId });
                }

            }
            catch (Exception ex)
            {
                Log.Error("OnChatHub-SubScribeMessage", "用户订阅消息异常", ex, null,
                    new Dictionary<string, string>()
                    {
                        { "UId",Context.GetHttpContext().Request.Query["UId"] },
                        { "PartnerUId",Context.GetHttpContext().Request.Query["PartnerUId"] }
                    });
            }
        }
    }
}
