using System.ComponentModel;

namespace Chat.Model.Enum
{
    public enum MomentStateEnum
    {
        [Description("未知/默认")]
        Default = 0,

        [Description("用户主动删除")]
        UserDelete = 1,

        [Description("系统删除")]
        SystemDelete = 2,

        [Description("动态审核中")]
        IsChecking= 3,

        [Description("动态通过")]
        IsChecked = 4,
    }
}
