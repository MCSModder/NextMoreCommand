using System.ComponentModel;

namespace SkySwordKill.NextMoreCommand.Custom.NPC;

public enum SexType
{
    [Description("男")]
    男 = 1,
    [Description("女")]
    女,
    [Description("不男不女")]
    不男不女
}