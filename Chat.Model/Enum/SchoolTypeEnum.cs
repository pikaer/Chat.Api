using System.ComponentModel;

namespace Chat.Model.Enum
{
    /// <summary>
    /// 学校类型
    /// </summary>
    public enum SchoolTypeEnum
    {
        [Description("其他")]
        Default = 0,

        [Description("学院/大学")]
        College = 1,

        [Description("一本")]
        GoodCollege = 2,

        [Description("211/985/海外院校")]
        GreatCollege = 3
    }
}
