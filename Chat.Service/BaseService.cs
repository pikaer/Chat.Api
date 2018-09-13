using Infrastructure.Utility;

namespace Chat.Service
{
    public static class BaseService
    {
        private static string HeadPhoto = ConfigurationHelper.AppSettings["HeadPhoto"];
        private static string HeadshotFormat = ConfigurationHelper.AppSettings["HeadshotFormat"];

        public static string ToPathDesc(this string path)
        {
            return HeadPhoto + path + HeadshotFormat;
        }


    }
}
