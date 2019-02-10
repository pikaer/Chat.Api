using Infrastructure.Utility;

namespace Chat.Service
{
    public static class BaseService
    {
        private static readonly string HeadPhoto = ConfigurationHelper.AppSettings["HeadPhoto"];
        private static readonly string HeadshotFormat = ConfigurationHelper.AppSettings["HeadshotFormat"];
        private static readonly string DefaultHead = ConfigurationHelper.AppSettings["DefaultHead"];

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
