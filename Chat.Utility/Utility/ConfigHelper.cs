using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Xml;

namespace Infrastructure.Utility
{
    public class ConfigHelper
    {

        /// <summary>
        /// 加载配置
        /// </summary>
        public NameValueCollection Config(string filePath)
        {
            var rtn = new NameValueCollection();
            try
            {
                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                baseDirectory = baseDirectory.TrimEnd('\\') + "\\";
                var file = baseDirectory + filePath;
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
                    if(node.Name.Equals("appSettings"))
                    {
                        var nodes = node.ChildNodes;
                        for (int j = 0; j < nodes.Count; j++)
                        {
                            var child = (XmlElement)nodes[j];
                            var key = child.GetAttribute("key").ToString();
                            var value = child.GetAttribute("value").ToString();
                            if (rtn.AllKeys.Contains(key))
                            {
                                rtn[key] = value;
                            }
                            else
                            {
                                rtn.Add(key, value);
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
            return rtn;
        }

       
        /// <summary>
        /// 从程序根目录复制
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private void CopyConfigFile(string targetFile, string filePath)
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
