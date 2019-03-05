using Chat.Model.Entity.UserInfo;
using Chat.Repository;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Api.Hubs
{
    /// <summary>
    /// 聊天集线器
    /// </summary>
    public class ChatHub: Hub
    {
        /// <summary>
        /// 在线用户连接Id => UserInfo 映射
        /// </summary>
        public static ConcurrentDictionary<string, UserInfo> OnlineClients { get; set; }
        private UserInfoRepository _userInfoRepository = new UserInfoRepository();
        private static readonly object SyncObj = new object();

        static ChatHub()
        {
            OnlineClients = new ConcurrentDictionary<string, UserInfo>();
        }

        /// <summary>
        /// 成功连接
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            long uId = Convert.ToInt64(Context.GetHttpContext().Request.Query["UId"]);
            var user = _userInfoRepository.GetUserInfoByUId(uId);
            if (user != null)
            {
                lock (SyncObj)
                {
                    OnlineClients[Context.ConnectionId] = user;
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
                OnlineClients.TryRemove(Context.ConnectionId, out UserInfo user);
            }
        }
    }
}
