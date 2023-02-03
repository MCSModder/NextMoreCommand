using System;

namespace SkySwordKill.NextMoreCommand.Custom.RealizeSeid;

public class SeidBuff:ISeid<SeidBuff>
{
    public Action<SeidBuff> Prefix { get; set; }
    public Action<SeidBuff> Postfix { get; set; }
}