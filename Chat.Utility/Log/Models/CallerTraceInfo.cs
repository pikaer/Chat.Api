using System.Reflection;
using System.Text;

namespace Infrastructure.Log.Models
{
    /// <summary>
    /// 调用方跟踪信息
    /// </summary>
    public class CallerTraceInfo
    {
        /// <summary>
        /// 代码文件
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 代码行数
        /// </summary>
        public int LineNumber { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// 方法参数
        /// </summary>
        public ParameterInfo[] Parameters { get; set; }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        override
        public string ToString()
        {
            string parametersString = "";
            if (Parameters != null && Parameters.Length > 0)
            {
                foreach (var p in Parameters)
                {
                    parametersString += (p.ParameterType.Name ?? "") + " " + (p.Name ?? "") + ",";
                }
                parametersString = parametersString.Remove(parametersString.Length - 1);
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("在 {0}.{1}({2}) 位置 {3}:行号 {4}", ClassName, MethodName, parametersString, FileName, LineNumber);

            return sb.ToString();
        }
    }
}
