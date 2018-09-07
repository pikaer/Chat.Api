using Infrastructure.Utility;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Infrastructure.Redis
{
    public static class ConfigManager
    {
        public static NameValueCollection AppSettings { get; set; }
        private const string FILE_PATH = @"Configs\redis.config";
        static ConfigManager()
        {
            AppSettings = new ConfigHelper().Config(FILE_PATH);
        }
    }
}
