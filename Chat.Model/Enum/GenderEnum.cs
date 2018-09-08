using System.ComponentModel;

namespace Chat.Model.Enum
{
    public enum GenderEnum
    {
        [Description("未知")]
        Default = 0,
        [Description("男")]
        Man = 1,
        [Description("女")]
        Woman = 2
    }
}
