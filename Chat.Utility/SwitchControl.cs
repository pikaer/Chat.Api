using Infrastructure;
using System;

namespace Chat.Utility
{
    /// <summary>
    /// 开关公共方法
    /// </summary>
    public static class SwitchControl
    {
        /// <summary>
        /// 是否开启测试数据
        /// </summary>
        /// <returns></returns>
        public static bool TestIsOpen()
        {
            string testOpenstr =JsonSettingHelper.AppSettings["TestIsOpen"];
            return Convert.ToBoolean(testOpenstr);
        }
    }
}
