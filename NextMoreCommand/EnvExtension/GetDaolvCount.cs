﻿using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.EnvExtension
{
    [DialogEnvQuery("GetDaolvCount")]
    public class GetDaolvCount : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            return DaolvUtils.DaolvId.Count;
        }
    }
}