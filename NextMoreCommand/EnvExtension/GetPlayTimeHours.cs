using System;
using BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;
using UnityEngine;

namespace SkySwordKill.NextMoreCommand.EnvExtension
{
    [DialogEnvQuery("GetPlayTimeHours")]
    public class GetPlayTimeHours : IDialogEnvQuery
    {
        public object Execute(DialogEnvQueryContext context)
        {
            return PlayTimeUtils.GetHours();
        }
    }
}