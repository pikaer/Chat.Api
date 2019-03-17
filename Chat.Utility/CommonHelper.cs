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

        public static string DefaultHead()
        {
            string path = JsonSettingHelper.AppSettings["DefaultHead"];

            //默认前缀
            string defaultPath = JsonSettingHelper.AppSettings["HeadPhoto"];

            return GetPath(defaultPath, path);
        }


        /// <summary>
        /// 获取绝对路径
        /// </summary>
        private static string GetPath(string defaultPath,string shortPath)
        {
            string rtn = string.Empty;

            string host = JsonSettingHelper.AppSettings["ApiHost"];

            if (!string.IsNullOrEmpty(defaultPath))
            {
                rtn = string.Format("{0}{1}{2}", host,defaultPath, shortPath);
            }
            return rtn;
        }
    }
}
