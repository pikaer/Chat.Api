using System;
using System.Collections.Generic;
using System.Reflection;

namespace Infrastructure.Utility
{
    /// <summary>
    ///     枚举扩展方法类
    /// </summary>
    public static class EnumExtensions
    {
        public static Dictionary<int, string> ToDictionary(Type enumType)
        {
            Dictionary<int, string> listitem = new Dictionary<int, string>();
            Array vals = Enum.GetValues(enumType);
            foreach (Enum enu in vals)
            {
                listitem.Add(Convert.ToInt32(enu), enu.ToDescription());
            }
            return listitem;
        }

        /// <summary>
        /// 获取枚举描述值
        /// </summary>
        /// <returns></returns>
        public static string ToDescription(this Enum enumValue)
        {
            string str = enumValue.ToString();
            FieldInfo field = enumValue.GetType().GetField(str);
            object[] objs = field.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            if (objs == null || objs.Length == 0) return str;
            System.ComponentModel.DescriptionAttribute da = (System.ComponentModel.DescriptionAttribute)objs[0];
            return da.Description;
        }
    }
}
