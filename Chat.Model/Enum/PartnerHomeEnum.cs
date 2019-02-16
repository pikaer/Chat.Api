using System.ComponentModel;

namespace Chat.Model.Enum
{
    public enum PartnerHomeEnum
    {
        [Description("都可以")]
        Default = 0,

        [Description("同省优先")]
        SameProvince = 1,

        [Description("同城优先")]
        SameCity = 2
    }
}
