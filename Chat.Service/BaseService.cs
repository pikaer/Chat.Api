using Infrastructure;

namespace Chat.Service
{
    public static class BaseService
    {
        private static readonly string HeadPhoto = JsonSettingHelper.AppSettings["HeadPhoto"];
        private static readonly string HeadshotFormat = JsonSettingHelper.AppSettings["HeadshotFormat"];
        private static readonly string DefaultHead = JsonSettingHelper.AppSettings["DefaultHead"];

        public static string ToPathDesc(this string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = DefaultHead;
            }
            return HeadPhoto + path + HeadshotFormat;
        }


    }
}
