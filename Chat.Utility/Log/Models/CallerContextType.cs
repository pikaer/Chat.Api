using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Log.Models
{
    public enum CallerContextType
    {
        Default, //默认
        Request, //请求
        Response, //响应
        None, //空
    }
}
