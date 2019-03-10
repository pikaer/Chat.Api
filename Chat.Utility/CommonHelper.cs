using Infrastructure;

namespace Chat.Utility
{
    public static class CommonHelper
    {
        /// <summary>
        /// 获取头像路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ToHeadImagePath(this string shortPath)
        {
            string rtn = string.Empty;
            if (string.IsNullOrEmpty(shortPath))
            {
                return rtn;
            }
            //默认前缀
            string defaultPath = JsonSettingHelper.AppSettings["HeadPhoto"];

            
            return GetPath(defaultPath, shortPath);
        }

        /// <summary>
        /// 获取动态路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ToMomentImagePath(this string shortPath)
        {
            string rtn = string.Empty;
            if (string.IsNullOrEmpty(shortPath))
            {
                return rtn;
            }
            //默认前缀
            string defaultPath = JsonSettingHelper.AppSettings["MomentImg"];


            return GetPath(defaultPath, shortPath);
        }

        //获取绝对路径
        private static string GetPath(string defaultPath,string shortPath)
        {
            string rtn = string.Empty;

            //头像图片格式
            string shotFormat = JsonSettingHelper.AppSettings["ImgFormat"];

            if (!string.IsNullOrEmpty(defaultPath) && !string.IsNullOrEmpty(shotFormat))
            {
                rtn = string.Format("{0}{1}{2}", defaultPath, shortPath, shotFormat);
            }
            return rtn;
        }
    }
}
