using Chat.Model.Enum;
using Chat.Model.Utils;
using Chat.Repository;
using Chat.Utility;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Service
{
    public class GoldCoinService
    {
        private GoldCoinRespository goldCoinDal = SingletonProvider<GoldCoinRespository>.Instance;
        private UserInfoRepository userInfoDal = SingletonProvider<UserInfoRepository>.Instance;

        /// <summary>
        /// 根据用户Id获取金币数[用户不存在时返回金币数为-1]
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResponseContext<long?> GetGoldCoinNumberByUid(RequestContext<long> request)
        {
            var response = new ResponseContext<long?>() {Content = null};

            try
            {
                var userIsExist = userInfoDal.CheckUserExist(request.Content);
                if(!userIsExist)
                {
                    return response;
                }
                response.Content = goldCoinDal.GetGoldCoinNumberByUid(request.Content);
            }
            catch (Exception ex)
            {
                response.Head = new ResponseHead(false, ErrCodeEnum.QueryError);
                Log.Error("GetGoldCoinNumberByUid", "获取用户信息异常", ex, request.Head);
            }
            return response;
        }
    }
}
