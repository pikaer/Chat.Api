using Chat.Model.Enum;
using Chat.Model.Utils;
using Chat.Repository;
using Chat.Utility;
using Infrastructure;
using System;

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
        public ResponseContext<GetGoldCoinNumberResponse> GetGoldCoinNumberByUid(RequestContext<GetGoldCoinNumberRequest> request)
        {
            var response = new ResponseContext<GetGoldCoinNumberResponse>()
            {
                Content = new GetGoldCoinNumberResponse()
            };

            try
            {
                var userInfo = userInfoDal.GetUserInfoByUId(request.Content.UId);
                if(userInfo==null)
                {
                    response.Head = new ResponseHead(true, ErrCodeEnum.Success,"该用户不存在");
                    response.Content.TotalCoin = 0;
                    return response;
                }

                response.Content.TotalCoin = goldCoinDal.GetGoldCoinNumberByUid(request.Content.UId);
            }
            catch (Exception ex)
            {
                response.Head = new ResponseHead(false, ErrCodeEnum.QueryError);
                Log.Error("GetGoldCoinNumberByUid", "根据用户Id获取金币数异常", ex, request.Head);
            }
            return response;
        }
    }
}
