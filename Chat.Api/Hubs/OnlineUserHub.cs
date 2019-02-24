using Chat.Repository;
using Chat.Service;
using Infrastructure;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
namespace Chat.Api.Hubs
{
    /// <summary>
    /// 在线用户
    /// </summary>
    public class OnlineUserHub: Hub
    {
        private UserInfoRepository _userInfoDal = SingletonProvider<UserInfoRepository>.Instance;
        private HubService hubService = SingletonProvider<HubService>.Instance;
        private static readonly object SyncObj = new object();

        /// <summary>
        ///  在线用户连接Id =>UId 映射
        /// </summary>
        public static ConcurrentDictionary<string, long> OnlineUsers { get; set; }

        /// <summary>
        /// 成功连接
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            long uId = Convert.ToInt64(Context.GetHttpContext().Request.Query["UId"]);
            var user = _userInfoDal.GetUserInfoByUId(uId);
            if (user != null)
            {
                lock (SyncObj)
                {
                    OnlineUsers[Context.ConnectionId] = uId;
                    hubService.OnlineConnected(uId, Context.ConnectionId);
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
                OnlineUsers.TryRemove(Context.ConnectionId, out long uId);
                hubService.OnlineDisconnected(uId);
            }
        }
    }
}
