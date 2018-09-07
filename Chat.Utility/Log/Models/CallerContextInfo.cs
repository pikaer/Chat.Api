using Infrastructure.Log.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Log.Models
{
    /// <summary>
    /// 调用方当前上下文信息
    /// </summary>
    public class CallerContextInfo
    {
        public CallerContextInfo()
        {
            ContextType = CallerContextType.Default;
        }

        public CallerContextType ContextType { get; set; }

        /// <summary>
        /// 请求类型
        /// </summary>
        public string HttpMethod { get; set; }

        /// <summary>
        /// 请求地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 请求内容
        /// </summary>
        public string RequestContent { get; set; }

        /// <summary>
        /// 响应内容
        /// </summary>
        public string ResponseContent { get; set; }

        /// <summary>
        /// 总计耗时
        /// </summary>
        public long? Interval { get; set; }

        override
        public string ToString()
        {
            var httpMethod = string.IsNullOrEmpty(HttpMethod) ? Const.PLACEHOLDER : HttpMethod;
            var url = string.IsNullOrEmpty(Url) ? Const.PLACEHOLDER : Url;
            var requestContent = string.IsNullOrEmpty(RequestContent) ? Const.PLACEHOLDER : RequestContent;
            requestContent = LogUtility.ReplaceKeyword(requestContent);
            var responseContent = string.IsNullOrEmpty(ResponseContent) ? Const.PLACEHOLDER : ResponseContent;
            responseContent = LogUtility.ReplaceKeyword(responseContent);
            var interval = Interval == null ? Const.PLACEHOLDER : Interval.ToString();

            var sb = new StringBuilder();
            if (ContextType == CallerContextType.Default)
            {
                sb.AppendFormat("{0} " + Const.SEPARATOR_LEFT + "{1}" + Const.SEPARATOR_RIGHT + " " + Const.SEPARATOR_LEFT + "{2}" + Const.SEPARATOR_RIGHT,
                    httpMethod, url, requestContent);
            }
            else if (ContextType == CallerContextType.None)
            {
                sb.AppendFormat("{0} " + Const.SEPARATOR_LEFT + "{0}" + Const.SEPARATOR_RIGHT + " " + Const.SEPARATOR_LEFT + "{0}" + Const.SEPARATOR_RIGHT,
                    Const.PLACEHOLDER);
            }
            else if (ContextType == CallerContextType.Request)
            {
                sb.AppendFormat("Request {0} " + Const.SEPARATOR_LEFT + "{1}" + Const.SEPARATOR_RIGHT + " " + Const.SEPARATOR_LEFT + "{2}" + Const.SEPARATOR_RIGHT + " "
                    + Const.SEPARATOR_LEFT + Const.PLACEHOLDER + Const.SEPARATOR_RIGHT + " " + Const.SEPARATOR_LEFT + Const.PLACEHOLDER + Const.SEPARATOR_RIGHT,
                    httpMethod, url, requestContent);
            }
            else if (ContextType == CallerContextType.Response)
            {

                sb.AppendFormat("Response {0} " + Const.SEPARATOR_LEFT + "{1}" + Const.SEPARATOR_RIGHT + " " + Const.SEPARATOR_LEFT + "{2}" + Const.SEPARATOR_RIGHT + " "
                    + Const.SEPARATOR_LEFT + "{3}" + Const.SEPARATOR_RIGHT + " " + Const.SEPARATOR_LEFT + "{4}" + Const.SEPARATOR_RIGHT,
                    httpMethod, url, requestContent, responseContent, interval);
            }
            return sb.ToString();
        }
    }
}
