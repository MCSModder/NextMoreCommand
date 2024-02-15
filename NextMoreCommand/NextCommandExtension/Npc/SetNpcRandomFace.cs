using System;
using System.Collections.Generic;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;
using Random = UnityEngine.Random;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Npc
{
    public class NpcFaceInfo
    {
        public string Face;
        public string LiHui = string.Empty;
        public static NpcFaceInfo Create(string face)
        {
            return new(face);
        }
        public NpcFaceInfo(string face)
        {
            Face = face;
            if (!face.Contains(":")) return;
            var split = face.Split(':');
            Face = split[0];
            LiHui = split[1];
        }
        public override string ToString()
        {
            return string.IsNullOrWhiteSpace(LiHui) ? Face : $"{Face}#{LiHui}";
        }
    }

    [RegisterCommand]
    [DialogEvent("SetNpcRandomFace")]
    [DialogEvent("设置角色随机立绘")]
    public class SetNpcRandomFace : IDialogEvent
    {
        private int               npc;
        private List<NpcFaceInfo> value;

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            MyLog.LogCommand(command);
            npc = command.ToNpcId();
            value = command.ToList(NpcFaceInfo.Create, 1);
            var count = value.Count;
            switch (npc)
            {
                case > 0 when count > 0:
                {
                    var index = count == 1 ? 0 : Random.Range(0, count - 1);
                    DialogAnalysis.RunDialogEventCommand(new DialogCommand($"SetFace*{npc.ToString()}#{value[index].ToString()}", command.BindEventData, env, command.IsEnd), env, callback);
                    break;
                }
                case <= 0:
                    MyLog.Log(command, $"角色ID:{npc.ToString()} 不能为小于等于 0", true);
                    break;
            }
            if (count == 0)
            {
                MyLog.Log(command, $"立绘列表不能为空", true);
            }

            MyLog.LogCommand(command, false);
            callback?.Invoke();
        }
    }
}