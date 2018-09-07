using Infrastructure.Utility;
using System.Collections.Specialized;

namespace Infrastructure.Mail
{
    public static class ConfigManager
    {
        public static NameValueCollection AppSettings { get; set; }
        private const string FILE_PATH = @"Configs\mail.config";
        static ConfigManager()
        {
            AppSettings = new ConfigHelper().Config(FILE_PATH);
        }
    }
}
