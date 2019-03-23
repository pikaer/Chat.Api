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

        /// <summary>
        /// 空间背景图片加载路径
        /// </summary>
        public static string ToBackgroundImgPath(this string shortPath)
        {
            string rtn = string.Empty;
            if (string.IsNullOrEmpty(shortPath))
            {
                return rtn;
            }

            //默认前缀
            string defaultPath = JsonSettingHelper.AppSettings["BackgroundImg"];


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
        /// 拼接所在地
        /// </summary>
        /// <param name="province">省</param>
        /// <param name="city">市</param>
        /// <param name="area">区</param>
        /// <returns></returns>
        public static string GetLocation(string province,string city,string area)
        {
            var rtn = string.Empty;
            if(string.IsNullOrEmpty(province)|| province == "全部")
            {
                return "全国";
            }
            
            if(string.IsNullOrEmpty(city) || city == "全部")
            {
                return province;
            }

            if (string.IsNullOrEmpty(area) || area == "全部")
            {
                return string.Format("{0}{1}", province, city);
            }

            return string.Format("{0}{1}{2}", province, city, area);
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
