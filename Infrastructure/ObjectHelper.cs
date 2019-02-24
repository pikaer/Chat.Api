using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure
{
    public static class ObjectHelper
    {
        /// <summary>
        /// 把对象类型转化为指定类型，转化失败时返回该类型默认值
        /// </summary>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <param name="value"> 要转化的源对象 </param>
        /// <returns> 转化后的指定类型的对象，转化失败返回类型的默认值 </returns>
        public static T JsonToObject<T>(this object value)
        {
            T rtn = default(T);
            try
            {
                rtn = JsonConvert.DeserializeObject<T>(value.ToString());
            }
            catch
            {
                string txt = ("Json值:" + value.ToString() + "转换失败");
            }
            return rtn;
        }

        /// <summary>
        /// 判断集合是否为空
        /// </summary>
        public static bool IsNullOrEmpty<T>(this List<T> list)
        {
            if(list==null|| list.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断集合是否为空
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            if (list == null)
            {
                return true;
            }
            return !list.Any();
        }
    }
}
