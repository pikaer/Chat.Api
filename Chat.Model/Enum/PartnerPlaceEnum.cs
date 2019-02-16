using System.ComponentModel;

namespace Chat.Model.Enum
{
    public enum PartnerPlaceEnum
    {
        [Description("都可以")]
        Default = 0,

        [Description("同城优先")]
        SameCity =1,

        [Description("同学校/母校优先")]
        SameSchool =2
    }
}
