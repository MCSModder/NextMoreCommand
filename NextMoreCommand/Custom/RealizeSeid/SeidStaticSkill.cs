using System;

namespace SkySwordKill.NextMoreCommand.Custom.RealizeSeid;

public class SeidStaticSkill : ISeid<SeidStaticSkill>
{
    public Action<SeidStaticSkill> Prefix  { get; set; }
    public Action<SeidStaticSkill> Postfix { get; set; }
}