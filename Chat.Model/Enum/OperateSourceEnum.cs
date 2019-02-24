using System.ComponentModel;

namespace Chat.Model.Enum
{
    /// <summary>
    /// 金币操作来源
    /// </summary>
    public enum OperateSourceEnum
    {
        [Description("充值")]
        Recharge = 0,
        [Description("消费")]
        Consume = 0,
        [Description("奖励")]
        Reward = 0,
        [Description("活动")]
        Activity = 0,
    }
}
