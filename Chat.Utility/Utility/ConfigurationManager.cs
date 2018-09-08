using Newtonsoft.Json.Linq;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;

namespace Infrastructure.Utility
{
    /// <summary>
    /// 读取Json配置文件
    /// </summary>
    public class ConfigurationManager
    {
        private static NameValueCollection _configurationCollection;
        private static DateTime _lastWriteTime;
        public static bool _configFileExist;
        public static string _defaultPath = "/appsettings.json";

        static ConfigurationManager()
        {
            _lastWriteTime = DateTime.MinValue;
            _configurationCollection = new NameValueCollection();
        }

        private static void ReadJosnConfig(string filePath = null)
        {
            if (string.IsNullOrEmpty(filePath)) filePath = Directory.GetCurrentDirectory() + _defaultPath;

            FileInfo config = new FileInfo(filePath);
            if (!(_configFileExist = config.Exists)) return;
            _configurationCollection.Clear();
            StreamReader sr = new StreamReader(filePath, Encoding.Default);
            JObject config_object = JObject.Parse(sr.ReadToEnd());
            sr.Close();
            if (config_object == null || !(config_object is JObject)) return;

            if (config_object["appSettings.Url"] != null && !string.IsNullOrEmpty(config_object["appSettings.Url"].ToString()))
            {
                ReadJosnConfig(config_object["appSettings.Url"].ToString());
            }
            else
            {
                foreach (JProperty prop in config_object["AppSettings"])
                {
                    _configurationCollection[prop.Name] = prop.Value.ToString();
                }
            }
        }

        public static bool ConfigFileExist { get { return _configFileExist; } }

        public static NameValueCollection AppSettings
        {
            get
            {
                ReadJosnConfig();
                return _configFileExist ? _configurationCollection : new NameValueCollection();
            }
        }

    }
}
