using Infrastructure.Utility;

namespace Chat.Service
{
    public static class BaseService
    {
        private static string HeadPhoto = ConfigurationHelper.AppSettings["HeadPhoto"];
        private static string HeadshotFormat = ConfigurationHelper.AppSettings["HeadshotFormat"];
        private static string DefaultHead = ConfigurationHelper.AppSettings["DefaultHead"];

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
