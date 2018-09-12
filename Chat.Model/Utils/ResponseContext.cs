using Chat.Model.Enum;

namespace Chat.Model.Utils
{
    public class ResponseContext<T>
    {
        /// <summary>
        /// 响应头
        /// </summary>
        public ResponseHead Head { get; set; }

        /// <summary>
        /// 响应体
        /// </summary>
        public T Content { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ResponseContext(T content)
        {
            Head = new ResponseHead();
            Content = content;
        }
        
    }

    public class ResponseHead
    {
        /// <summary>
        /// 默认成功
        /// </summary>
        public ResponseHead()
        {
            Success = false;
            Code = ErrCodeEnum.Sucess;
            Msg = "调用接口成功";
        }

        public ResponseHead(bool success, ErrCodeEnum code, string msg)
        {
            Success = success;
            Code = code;
            Msg = msg;
        }

        /// <summary>
        /// 返回值
        /// true:成功
        /// false:失败
        /// </summary>

        public bool Success { get; set; }

        /// <summary>
        /// 错误码
        /// </summary>

        public ErrCodeEnum Code { get; set; }
        
        /// <summary>
        /// 错误信息
        /// </summary>

        public string Msg { get; set; }
       
    }
}
