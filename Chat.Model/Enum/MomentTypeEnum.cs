using System.ComponentModel;

namespace Chat.Model.Enum
{
    /// <summary>
    /// 动态类别
    /// </summary>
    public enum MomentTypeEnum
    {
        [Description("推荐")]
        Recommend = 0,

        [Description("最新")]
        Newest = 1,

        [Description("关注")]
        Attention = 2
    }
}
