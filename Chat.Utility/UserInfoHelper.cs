using Infrastructure;

namespace Chat.Utility
{
    public static class UserInfoHelper
    {
        public static string ToHeadImagePath(this string path)
        {
            string rtn= string.Empty;
            if (string.IsNullOrEmpty(path))
            {
                return rtn;
            }
            //默认前缀
            string defaultPath= JsonSettingHelper.AppSettings["HeadPhoto"];

            //头像图片格式
            string shotFormat= JsonSettingHelper.AppSettings["HeadshotFormat"];

            if(!string.IsNullOrEmpty(defaultPath)&& !string.IsNullOrEmpty(shotFormat))
            {
                rtn = string.Format("{0}{1}{2}", defaultPath, path, shotFormat);
            }
            return rtn;
        }
    }
}
