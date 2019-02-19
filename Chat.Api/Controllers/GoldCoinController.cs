using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.Model.Enum;
using Chat.Model.Utils;
using Chat.Service;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers
{
    /// <summary>
    /// 金币信息
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class GoldCoinController : BaseController
    {
        GoldCoinService goldCoinService = SingletonProvider<GoldCoinService>.Instance;

        /// <summary>
        /// 根据用户Id获取金币
        /// </summary>
        [HttpPost]
        public JsonResult GetGoldCoinNumberByUid(RequestContext<long> request)
        {
            if(request == null)
            {
                return ErrorJsonResult(ErrCodeEnum.ParametersIsNotValid_Code);
            }
            if (request.Head == null)
            {
                return ErrorJsonResult(ErrCodeEnum.InvalidRequestHead);
            }
            var response = goldCoinService.GetGoldCoinNumberByUid(request);
            return new JsonResult(response);

        }

        /// <summary>
        /// 更新金币信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateGoldCoinNumber(RequestContext<GoldCoinRequest> request)
        {
            return new JsonResult(null);
        }

        /// <summary>
        /// 获取金币信息历史记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetGoldCoinHistory(RequestContext<GoldCoinHistoryRequest> request)
        {
            return new JsonResult(null);
        }
    }
}
