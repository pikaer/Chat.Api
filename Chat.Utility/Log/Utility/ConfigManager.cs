using Infrastructure.Utility;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Xml;

namespace Infrastructure.Log.Utility
{
    public static class ConfigManager
    {
        public static NameValueCollection AppSettings { get; set; }
        private const string FILE_PATH = @"Configs\log.config";
        static ConfigManager()
        {
            AppSettings=new ConfigHelper().Config(FILE_PATH);
        }
    }
}
