using System.ComponentModel;

namespace Chat.Model.Enum
{
    /// <summary>
    /// 状态
    /// </summary>
    public enum LiveStateEnum
    {
        [Description("未设置/默认/都可以")]
        Default = 0,

        [Description("学生党")]
        Student = 1,

        [Description("工作党")]
        Working = 2
    }
}
