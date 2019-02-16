using System.ComponentModel;

namespace Chat.Model.Enum
{
    public enum GenderEnum
    {
        [Description("未知/默认/都可以")]
        Default = 0,

        [Description("小哥哥")]
        Man = 1,

        [Description("小姐姐")]
        Woman = 2
    }
}
