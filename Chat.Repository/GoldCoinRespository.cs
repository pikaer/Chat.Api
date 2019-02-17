using Chat.Utility;
using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Repository
{
    public class GoldCoinRespository : BaseRepository
    {
        protected override DbEnum GetDbEnum()
        {
            return DbEnum.ChatConnect;
        }

        public long? GetGoldCoinNumberByUid(long uid)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = $"select CoinTotal from coin_GoldCoin where UId = {uid}";
                    return Db.QueryFirst<long>(sql);
                }
                catch(Exception ex)
                {
                    Log.Error("GetGoldCoinNumberByUid", "获取用户金币数异常，UId=" + uid, ex);
                    return null;
                }
            }
        }

        public bool UpdateGoldCoinNumber(long uid,long coinNum)
        {
            using (var Db = GetDbConnection())
            {
                var coinTotal = GetGoldCoinNumberByUid(uid) + coinNum;
                try
                {
                    var sql = $"update coin_GoldCoin set CoinTotal = {coinTotal} where UId = 1";
                    return Db.Execute(sql) > 0;
                }
                catch (Exception ex)
                {
                    Log.Error("UpdateGoldCoinNumber", "更新用户金币数异常，UId=" + uid, ex);
                    return false;
                }
            }
        }
    }
}
