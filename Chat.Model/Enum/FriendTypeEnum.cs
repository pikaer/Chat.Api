using System.ComponentModel;

namespace Chat.Model.Enum
{
    public enum FriendTypeEnum
    {
        [Description("默认/互相关注的好友")]
        Default = 0,

        [Description("关注的好友")]
        Attentin = 1,

        [Description("我的粉丝")]
        Fans = 2,

        [Description("访客")]
        Visitor = 3,
    }
}
