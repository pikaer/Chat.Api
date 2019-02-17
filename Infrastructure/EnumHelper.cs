using System;
using System.ComponentModel;
using System.Reflection;

namespace Infrastructure
{
    public static class EnumHelper
    {
        /// <summary>
        /// 获取枚举描述值
        /// </summary>
        /// <returns></returns>
        public static string ToDescription(this Enum enumValue)
        {
            string str = enumValue.ToString();
            FieldInfo field = enumValue.GetType().GetField(str);
            object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (objs == null || objs.Length == 0) return str;
            DescriptionAttribute da = (DescriptionAttribute)objs[0];
            return da.Description;
        }
    }
}
