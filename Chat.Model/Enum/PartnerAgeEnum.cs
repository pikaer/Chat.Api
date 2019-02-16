using System.ComponentModel;

namespace Chat.Model.Enum
{
    public enum PartnerAgeEnum
    {
        [Description("都可以")]
        Default = 0,

        [Description("12-18岁")]
        Age12_18 = 1,

        [Description("18-24岁")]
        Age18_24 = 2,

        [Description("24-30岁")]
        Age24_30 = 1,

        [Description("30-35岁")]
        Age30_35 = 1,

        [Description("35-45岁")]
        Age35_45 = 1,

        [Description("大于45岁")]
        AgeOver45 = 2
    }
}
