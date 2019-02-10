using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Redis
{
    public class RedisCache
    {
        //所属模块
        private const string MOUDLE_NAME = "Infrastructure.Redis";
        private static string Connection { get { return ConfigManager.AppSettings["Connection"]; } }

        static ConnectionMultiplexer _redis = null;

        static RedisCache()
        {
            _redis = ConnectionMultiplexer.Connect(Connection);
        }

        #region Key相关

        /// <summary>
        /// 刷新过期时间
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="expiry">过期时间</param>
        /// <param name="dbNo">Redis DB号</param>
        /// <returns></returns>
        public static bool KeyExpire(string key, TimeSpan expiry, int? dbNo = null)
        {
            return GetDb(dbNo).KeyExpire(key, expiry);
        }

        /// <summary>
        /// 刷新过期时间
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="expiry">过期时间</param>
        /// <param name="dbNo">Redis DB号</param>
        /// <returns></returns>
        public static bool KeyExpire(string key, DateTime expiry, int? dbNo = null)
        {
            return GetDb(dbNo).KeyExpire(key, expiry);
        }

        /// <summary>
        /// Key是否存在
        /// （对String、Hush、List、Set等都适用）
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="dbNo">Redis DB号</param>
        /// <returns></returns>
        public static bool KeyExists(string key, int? dbNo = null)
        {
            return GetDb(dbNo).KeyExists(key);
        }

        /// <summary>
        /// 删除Key
        /// （对String、Hush、List、Set等都适用）
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="dbNo">Redis DB号</param>
        /// <returns></returns>
        public static bool KeyDelete(string key, int? dbNo = null)
        {
            return GetDb(dbNo).KeyDelete(key);
        }

        /// <summary>
        /// 按模式查找Keys
        /// </summary>
        /// <param name="dbNo">库号</param>
        /// <param name="pattern">模式。例："UserToken:*"</param>
        /// <returns></returns>
        public static List<string> ScanKeys(int dbNo, string pattern)
        {
            var keys = new List<string>();
            foreach (var ep in _redis.GetEndPoints())
            {
                var server = _redis.GetServer(ep);
                var serverKeys = server.Keys(database: dbNo, pattern: pattern).ToList();
                keys.AddRange(serverKeys.Select(x => x.ToString()));
            }
            return keys;
        }

        /// <summary>
        /// 在默认库里按模式查找Keys
        /// </summary>
        /// <param name="dbNo">库号</param>
        /// <param name="pattern">模式。例："UserToken:*"</param>
        /// <returns></returns>
        public static List<string> ScanKeys(string pattern)
        {
            var dbNo = _redis.GetDatabase().Database;
            return ScanKeys(dbNo, pattern);
        }

        #endregion

        #region 计数器

        /// <summary>
        /// String方式数字递增
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name=" increments ">递增量</param>
        /// <returns>递增后的值</returns>
        public static double Increment(string key, double increments = 1, int? dbNo = null)
        {
            return GetDb(dbNo).StringIncrement(key, increments);
        }

        /// <summary>
        /// String方式数字递增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="increments"></param>
        /// <param name="expiry">过期时间</param>
        /// <param name="dbNo"></param>
        /// <returns></returns>
        public static double IncrementWithExpiry(string key, double increments = 1, TimeSpan? expiry = null, int? dbNo = null)
        {
            if (expiry == null)
            {
                return GetDb(dbNo).StringIncrement(key, increments);
            }
            else
            {
                var db = GetDb(dbNo);
                var res = db.StringIncrement(key, increments);
                db.KeyExpire(key, expiry);
                return res;
            }
        }

        /// <summary>
        /// String方式数字递减
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name=" increments ">递减量</param>
        /// <returns>递减后的值</returns>
        public static double Decrement(string key, double decrements = 1, int? dbNo = null)
        {
            return GetDb(dbNo).StringDecrement(key, decrements);
        }

        #endregion

        #region String操作

        /// <summary>
        /// 保存单个字符串
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="expiry">过期时间</param>
        /// <param name="dbNo">Redis DB号</param>
        /// <returns></returns>
        public static bool StringSet(string key, string value, TimeSpan? expiry = default(TimeSpan?), int? dbNo = null)
        {
            return GetDb(dbNo).StringSet(key, value, expiry);
        }

        /// <summary>
        /// 保存一组字符串
        /// </summary>
        /// <param name="values"></param>
        /// <param name="dbNo"></param>
        /// <returns></returns>
        [Obsolete]
        public static bool Set(List<KeyValuePair<string, string>> keyValues, int? dbNo = null)
        {
            if (keyValues == null || keyValues.Count == 0) return false;

            var values = keyValues.Select(p => new KeyValuePair<RedisKey, RedisValue>(p.Key, p.Value)).ToArray();
            return GetDb(dbNo).StringSet(values);
        }

        /// <summary>
        /// 保存单个对象
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="expiry">过期时间</param>
        /// <param name="dbNo">Redis DB号</param>
        /// <returns></returns>
        public static bool Set<T>(string key, T value, TimeSpan? expiry = default(TimeSpan?), int? dbNo = null)
        {
            if (string.IsNullOrEmpty(key)) return false;

            var setting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var v = JsonConvert.SerializeObject(value, setting);
            return GetDb(dbNo).StringSet(key, v, expiry);
        }

        /// <summary>
        /// 保存一组字符串
        /// </summary>
        /// <param name="values"></param>
        /// <param name="dbNo"></param>
        /// <returns></returns>
        [Obsolete]
        public static bool Set<T>(List<KeyValuePair<string, T>> keyValues, int? dbNo = null)
        {
            var values = keyValues.Select(p => new KeyValuePair<RedisKey, RedisValue>(p.Key, JsonConvert.SerializeObject(p.Value))).ToArray();
            return GetDb(dbNo).StringSet(values);
        }

        /// <summary>
        /// 获取单个字符串(String格式)
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="dbNo">Redis DB号</param>
        /// <returns></returns>
        public static string StringGet(string key, int? dbNo = null)
        {
            return GetDb(dbNo).StringGet(key);
        }

        /// <summary>
        /// 获取一组字符串(String格式)
        /// </summary>
        /// <param name="keys">Key</param>
        /// <param name="dbNo">Redis DB号</param>
        /// <returns></returns>
        public static List<string> StringGet(List<string> keys, int? dbNo = null)
        {
            var redisKeys = keys.Select(p => (RedisKey)p).ToArray();
            var redisValues = GetDb(dbNo).StringGet(redisKeys);
            return redisValues.Select(p => p.ToString()).ToList();
        }

        /// <summary>
        /// 获取单个字符串(Json格式)
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="dbNo">Redis DB号</param>
        /// <returns></returns>
        public static T Get<T>(string key, int? dbNo = null)
        {
            var v = GetDb(dbNo).StringGet(key);
            return string.IsNullOrEmpty(v) ? default(T) : JsonConvert.DeserializeObject<T>(v);
        }

        /// <summary>
        /// 获取一组字符串(Json格式)
        /// </summary>
        /// <param name="keys">Key</param>
        /// <param name="dbNo">Redis DB号</param>
        /// <returns></returns>
        public static List<T> Get<T>(List<string> keys, int? dbNo = null)
        {
            var redisKeys = keys.Select(p => (RedisKey)p).ToArray();
            var redisValues = GetDb(dbNo).StringGet(redisKeys);
            return redisValues.Select(p => {
                return string.IsNullOrEmpty(p) ? default(T) : JsonConvert.DeserializeObject<T>(p);
            }).ToList();
        }

        #endregion

        #region Hush操作

        /// <summary>
        /// 判断Hash域是否存在
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="hashField">Hash域</param>
        /// <param name="dbNo">Redis DB号</param>
        /// <returns></returns>
        public static bool HashExists(string key, string hashField, int? dbNo = null)
        {
            return GetDb(dbNo).HashExists(key, hashField);
        }

        /// <summary>
        /// 删除Hash域
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="hashField">Hash域</param>
        /// <param name="dbNo">Redis DB号</param>
        /// <returns></returns>
        public static bool HashDelete(string key, string hashField, int? dbNo = null)
        {
            return GetDb(dbNo).HashDelete(key, hashField);
        }

        /// <summary>
        /// Hash长度
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="dbNo">Redis DB号</param>
        /// <returns></returns>
        public static long HashLength(string key, int? dbNo = null)
        {
            return GetDb(dbNo).HashLength(key);
        }

        /// <summary>
        /// Hash Field列表
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="dbNo">Redis DB号</param>
        /// <returns></returns>
        public static List<string> HashFields(string key, int? dbNo = null)
        {
            var fields = GetDb(dbNo).HashKeys(key);
            return fields.Select(p => p.ToString()).ToList();
        }

        /// <summary>
        /// 保存一项Hash值
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="hashField">Hash域</param>
        /// <param name="hashValue">Hash值</param>
        /// <param name="dbNo">Redis DB号</param>
        /// <returns></returns>
        public static bool HashSet(string key, string hashField, string hashValue, TimeSpan? expiry = default(TimeSpan?), int? dbNo = null)
        {
            var db = GetDb(dbNo);
            var ret = db.HashSet(key, hashField, hashValue);
            if (ret && expiry != null)
            {
                db.KeyExpire(key, expiry);
            }
            return ret;
        }

        /// <summary>
        /// 保存一组Hash值
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="hashFieldValues">Hash域-值</param>
        /// <param name="dbNo">Redis DB号</param>
        /// <returns></returns>
        public static void HashSet(string key, List<KeyValuePair<string, string>> hashFieldValues, TimeSpan? expiry = default(TimeSpan?), int? dbNo = null)
        {
            var db = GetDb(dbNo);
            var hashFields = hashFieldValues.Select(p => new HashEntry(p.Key, p.Value)).ToArray();
            db.HashSet(key, hashFields);
            if (expiry != null)
            {
                db.KeyExpire(key, expiry);
            }
        }

        /// <summary>
        /// 获取一项Hash值
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="hashField">Hash域</param>
        /// <param name="dbNo">Redis DB号</param>
        /// <returns></returns>
        public static string HashGet(string key, string hashField, int? dbNo = null)
        {
            return GetDb(dbNo).HashGet(key, hashField);
        }

        /// <summary>
        /// 获取一组Hash值
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="hashFields">一组Hash域</param>
        /// <param name="dbNo">Redis DB号</param>
        /// <returns></returns>
        public static List<string> HashGet(string key, List<string> hashFields, int? dbNo = null)
        {
            var hashFieldArrary = hashFields.Select(p => (RedisValue)p).ToArray();
            var hashValues = GetDb(dbNo).HashGet(key, hashFieldArrary);
            return hashValues.Select(p => p.ToString()).ToList();
        }

        /// <summary>
        /// 获取全部Hash值
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="dbNo">Redis DB号</param>
        /// <returns></returns>
        public static List<string> HashGet(string key, int? dbNo = null)
        {
            var hashValues = GetDb(dbNo).HashGetAll(key);
            return hashValues.Select(p => p.ToString()).ToList();
        }

        /// <summary>
        /// Hash方式递增
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="hashField">Hash域</param>
        /// <param name="dbNo">Redis DB号</param>
        /// <returns></returns>
        public static double HashIncrement(string key, string hashField, double increments = 1, int? dbNo = null)
        {
            return GetDb(dbNo).HashIncrement(key, hashField, increments);
        }

        /// <summary>
        /// Hash方式递减
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="hashField">Hash域</param>
        /// <param name="dbNo">Redis DB号</param>
        /// <returns></returns>
        public static double HashDecrement(string key, string hashField, double decrements = 1, int? dbNo = null)
        {
            return GetDb(dbNo).HashDecrement(key, hashField, decrements);
        }

        #endregion

        #region List操作

        /// <summary>
        /// 左入栈
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">值</param>
        /// <param name="dbNo">Redis DB号</param>
        /// <returns></returns>
        public static long ListLeftPush(string key, string value, int? dbNo = null)
        {
            return GetDb(dbNo).ListLeftPush(key, value);
        }

        /// <summary>
        /// 左出栈
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="dbNo">Redis DB号</param>
        /// <returns></returns>
        public static string ListLeftPop(string key, int? dbNo = null)
        {
            return GetDb(dbNo).ListLeftPop(key);
        }

        /// <summary>
        /// 右入栈
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">值</param>
        /// <param name="dbNo">Redis DB号</param>
        /// <returns></returns>
        public static long ListRightPush(string key, string value, int? dbNo = null)
        {
            return GetDb(dbNo).ListRightPush(key, value);
        }

        /// <summary>
        /// 右出栈
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="dbNo">Redis DB号</param>
        /// <returns></returns>
        public static string ListRightPop(string key, int? dbNo = null)
        {
            return GetDb(dbNo).ListRightPop(key);
        }

        /// <summary>
        /// 删除链表的值
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">值</param>
        /// <param name="dbNo">Redis DB号</param>
        /// <returns></returns>
        public static long ListRemove(string key, string value, int? dbNo = null)
        {
            return GetDb(dbNo).ListRemove(key, value);
        }

        /// <summary>
        /// 获取整个链表
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="dbNo">Redis DB号</param>
        /// <returns></returns>
        public static List<string> ListRange(string key, int? dbNo = null)
        {
            var list = GetDb(dbNo).ListRange(key);
            return list.Select(p => p.ToString()).ToList();
        }

        /// <summary>
        /// 获取链表长度
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="dbNo">Redis DB号</param>
        /// <returns></returns>
        public static long ListLength(string key, int? dbNo = null)
        {
            return GetDb(dbNo).ListLength(key);
        }

        #endregion

        #region SortedSet操作

        /// <summary>
        /// 增加有向集合项
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">值</param>
        /// <param name="score">分数</param>
        /// <param name="dbNo">Redis DB号</param>
        /// <returns></returns>
        public static bool SortedSetAdd(string key, string value, double score, int? dbNo = null)
        {
            return GetDb(dbNo).SortedSetAdd(key, value, score);
        }

        /// <summary>
        /// 删除有向集合项
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">值</param>
        /// <param name="dbNo">Redis DB号</param>
        /// <returns></returns>
        public static bool SortedSetRemove(string key, string value, int? dbNo = null)
        {
            return GetDb(dbNo).SortedSetRemove(key, value);
        }

        /// <summary>
        /// 获得按分数一段范围内的数量
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="minScore">最小分数</param>
        /// <param name="maxScore">最大分数</param>
        /// <param name="dbNo">Redis DB号</param>
        /// <returns></returns>
        public static long SortedSetLength(string key, double minScore = double.NegativeInfinity, double maxScore = double.PositiveInfinity, int? dbNo = null)
        {
            return GetDb(dbNo).SortedSetLength(key, minScore, maxScore);
        }

        /// <summary>
        /// 获取一段按分数一段范围内的内容
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="minScore">最小分数</param>
        /// <param name="maxScore">最大分数</param>
        /// <param name="dbNo">Redis DB号</param>
        /// <returns></returns>
        public static List<KeyValuePair<string, double>> SortedSetRange(string key, double minScore = double.NegativeInfinity, double maxScore = double.PositiveInfinity, int? dbNo = null)
        {
            var list = GetDb(dbNo).SortedSetRangeByScoreWithScores(key, minScore, maxScore);
            return list.Select(p => new KeyValuePair<string, double>(p.Element, p.Score)).ToList();
        }

        #endregion

        #region 发布订阅

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="channel">通道</param>
        /// <param name="msg">消息</param>
        public static void Publish(string channel, string msg)
        {
            var subscriber = _redis.GetSubscriber();
            subscriber.Publish(channel, msg);
        }

        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="channel">通道</param>
        /// <param name="action">处理动作，参数：string channel, string value</param>
        public static void Subscribe(string channel, Action<string, string> action = null)
        {
            var subscriber = _redis.GetSubscriber();
            Action<RedisChannel, RedisValue> handler = action == null
                ? new Action<RedisChannel, RedisValue>((RedisChannel c, RedisValue v) => { })
                : new Action<RedisChannel, RedisValue>((RedisChannel c, RedisValue v) => action(c.ToString(), v.ToString()));
            subscriber.Subscribe(channel, handler);
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="channel">通道</param>
        public static void Unsubscribe(string channel)
        {
            var subscriber = _redis.GetSubscriber();
            subscriber.Unsubscribe(channel);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获取Redis DB
        /// </summary>
        /// <param name="dbNo"></param>
        /// <returns></returns>
        private static IDatabase GetDb(int? dbNo)
        {
            return dbNo == null ? _redis.GetDatabase() : _redis.GetDatabase((int)dbNo);
        }

        #endregion
    }
}
