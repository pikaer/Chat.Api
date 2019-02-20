using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Xml;

namespace Infrastructure
{
    public static class ConfigHelper
    {
        private readonly static string filePath = "Infrastructure.config";

        /// <summary>
        /// 加载配置
        /// </summary>
        public static string AppSettings(string key)
        {
            var rtn = new NameValueCollection();
            try
            {
                var baseDirectory = Directory.GetCurrentDirectory();
                var file = baseDirectory+"\\" + filePath;
                if (!File.Exists(file)) CopyConfigFile(file, filePath); //如果文件不存在，从程序根目录复制
                var xmlDoc = new XmlDocument();
                var settings = new XmlReaderSettings
                {
                    IgnoreComments = true
                };
                var reader = XmlReader.Create(file, settings);
                xmlDoc.Load(reader);
                var root = xmlDoc.SelectSingleNode("configuration");
                var childNodes = root.ChildNodes;
                for (int i = 0; i < childNodes.Count; i++)
                {
                    var node = childNodes[i];
                    if (node.Name.Equals("appSettings"))
                    {
                        var nodes = node.ChildNodes;
                        for (int j = 0; j < nodes.Count; j++)
                        {
                            var child = (XmlElement)nodes[j];
                            var keys = child.GetAttribute("key").ToString();
                            var value = child.GetAttribute("value").ToString();
                            if (rtn.AllKeys.Contains(keys))
                            {
                                rtn[keys] = value;
                            }
                            else
                            {
                                rtn.Add(keys, value);
                            }
                        }
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return rtn[key];
        }

        public static string GetString(string key, string defaultValue=null)
        {
            return AppSettings(key) ?? defaultValue;
        }

        public static int GetInt(string key, int defaultValue = 0)
        {
            int value = defaultValue;
            if (int.TryParse(AppSettings(key), out value))
            {
                return value;
            }
            return defaultValue;
        }

        public static long GetLong(string key, long defaultValue = 0)
        {
            long value = defaultValue;
            if (long.TryParse(AppSettings(key), out value))
            {
                return value;
            }
            return defaultValue;
        }

        public static bool GetBool(string key, bool defaultValue = false)
        {
            bool value = defaultValue;
            if (bool.TryParse(AppSettings(key), out value))
            {
                return value;
            }
            return defaultValue;
        }

        public static float GetFloat(string key, float defaultValue = 0)
        {
            float value = defaultValue;
            if (float.TryParse(AppSettings(key), out value))
            {
                return value;
            }
            return defaultValue;
        }

        /// <summary>
        /// 从程序根目录复制
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static void CopyConfigFile(string targetFile, string filePath)
        {
            var idx1 = targetFile.IndexOf(@"\bin\");
            if (idx1 <= 0) return;

            var sourceFile = targetFile.Substring(0, idx1) + "\\" + filePath;
            if (!File.Exists(sourceFile)) return;

            var targetDir = Path.GetDirectoryName(targetFile);
            if (!Directory.Exists(targetDir)) Directory.CreateDirectory(targetDir);

            File.Copy(sourceFile, targetFile, false);
        }
    }
}
