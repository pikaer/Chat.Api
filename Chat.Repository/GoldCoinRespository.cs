using Chat.Utility;
using Dapper;
using System;

namespace Chat.Repository
{
    public class GoldCoinRespository : BaseRepository
    {
        protected override DbEnum GetDbEnum()
        {
            return DbEnum.ChatConnect;
        }

        /// <summary>
        /// 获取用户金币数
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public int GetGoldCoinNumber(long uid)
        {
            using (var Db = GetDbConnection())
            {
                try
                {
                    var sql = $"select CoinTotal from coin_GoldCoin where UId = {uid}";
                    return Db.QueryFirst<int>(sql);
                }
                catch(Exception ex)
                {
                    Log.Error("GetGoldCoinNumberByUid", "获取用户金币数异常，UId=" + uid, ex);
                    return 0;
                }
            }
        }

        /// <summary>
        /// 更新金币表
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="coinNum"></param>
        /// <returns></returns>
        public bool UpdateGoldCoin(long uid,int coinNum)
        {
            using (var Db = GetDbConnection())
            {
                var coinTotal = GetGoldCoinNumber(uid) + coinNum;
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
