using Chat.Model.Enum;
using Chat.Model.Utils;
using Chat.Repository;
using Chat.Utility;
using Infrastructure;
using System;
using System.Collections.Generic;

namespace Chat.Service
{
    public class GoldCoinService
    {
        private GoldCoinRespository goldCoinDal = SingletonProvider<GoldCoinRespository>.Instance;
        private UserInfoRepository userInfoDal = SingletonProvider<UserInfoRepository>.Instance;

        /// <summary>
        /// 根据用户Id获取金币数
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResponseContext<GetGoldCoinNumberResponse> GetGoldCoinNumber(RequestContext<GetGoldCoinNumberRequest> request)
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

                response.Content.TotalCoin = goldCoinDal.GetGoldCoinNumber(request.Content.UId);
            }
            catch (Exception ex)
            {
                response.Head = new ResponseHead(false, ErrCodeEnum.QueryError);
                Log.Error("GetGoldCoinNumber", "根据用户Id获取金币数异常", ex, request.Head);
            }
            return response;
        }

        /// <summary>
        /// 更新用户金币
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResponseContext<UpdateGoldCoinResponse> UpdateGoldCoin(RequestContext<UpdateGoldCoinRequest> request)
        {
            var response = new ResponseContext<UpdateGoldCoinResponse>()
            {
                Content = new UpdateGoldCoinResponse()
            };

            try
            {
                response.Content.ExcuteResult = goldCoinDal.UpdateGoldCoin(request.Content.UId,request.Content.AlertCoinNum);
            }
            catch(Exception ex)
            {
                response.Head = new ResponseHead(false, ErrCodeEnum.QueryError);
                Log.Error("UpdateGoldCoin", "更新用户金币异常", ex, request.Head);
            }
            return response;
        }

        /// <summary>
        /// 获取金币变化明细
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResponseContext<GetGoldCoinDetailsResponse> GetGoldCoinDetails(RequestContext<GetGoldCoinDetailsRequest> request)
        {
            var response = new ResponseContext<GetGoldCoinDetailsResponse>()
            {
                Content = new GetGoldCoinDetailsResponse()
            };

            try
            {
                #region Data Model
                var goldCoinDetailsList = new List<GoldCoinDetailsType>();
                for (int i = 0; i < 10; i++)
                {
                    var alertTmp = new Random().Next(0, 200) - new Random().Next(0, 200);
                    var tempDto = new GoldCoinDetailsType()
                    {
                        Description = "asdgsdg",
                        AlertCoinNum = alertTmp > 0 ? "+" + alertTmp.ToString() : alertTmp.ToString(),
                        CreateTime = DateTime.Now.AddDays(new Random().Next(0,20)).ToString("yyyy-MM-dd HH:mm:ss")
                    };
                    goldCoinDetailsList.Add(tempDto);
                }
                response.Content.GoldCoinDetaislList = goldCoinDetailsList;
                response.Content.TotalCount = 10;
                #endregion


                #region From DataBase
                //var entity = goldCoinDal.GetGoldCoinDetails(request.Content.UId);
                //if (entity == null)
                //{
                //    return response;
                //}

                //var goldCoinDetailsList = new List<GoldCoinDetailsType>();
                //foreach (var item in entity)
                //{
                //    var tempDto = new GoldCoinDetailsType()
                //    {
                //        Description = item.Description,
                //        AlertCoinNum = item.AlertCoinNum > 0 ? "+" + item.AlertCoinNum.ToString():item.ToString(),
                //        CreateTime = item.CreateTime.ToString("yyyy-MM-dd HH:mm:ss")
                //    };
                //    goldCoinDetailsList.Add(tempDto);
                //}
                //response.Content.GoldCoinDetaislList = goldCoinDetailsList;
                //response.Content.TotalCount = goldCoinDetailsList.Count;
                #endregion
            }
            catch (Exception ex)
            {
                response.Head = new ResponseHead(false, ErrCodeEnum.QueryError);
                Log.Error("GetGoldCoinDetails", "获取金币详情异常", ex, request.Head);
            }
            return response;
        }
    }
}
