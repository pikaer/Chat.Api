namespace Chat.Model.Utils
{
    public class RequestContext<T>
    {
        /// <summary>
        /// 请求头
        /// </summary>

        public RequestHead Head { get; set; }

        /// <summary>
        /// 请求体
        /// </summary>

        public T Content { get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        public RequestContext()
        {
            Head = new RequestHead();
            Content = default(T);
        }
    }

    public class RequestHead
    {
        /// <summary>
        /// Token信息
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// App类型 默认为0: 微信小程序 1:Web端
        /// </summary>
        public int AppType { get; set; }
    }

}
