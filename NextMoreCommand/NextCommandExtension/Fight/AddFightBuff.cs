using System;
using System.Collections.Generic;
using System.Linq;
using KBEngine;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.Next.StaticFace;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;
using GameObject = UnityEngine.GameObject;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Fight
{
    class BuffInfo
    {
        public int Id { get; } = 0;
        public int Value { get; } = 1;
        public BuffInfo(string str)
        {
            MyLog.Log(str);
            if (str.Contains(":"))
            {
                var split = str.Split(':');
                Id = Convert.ToInt32(split[0]);
                Value = Convert.ToInt32(split[1]);
            }
            else
            {
                Id = Convert.ToInt32(str);
                Value = 1;
            }
        }
    }

    [RegisterCommand]
    [DialogEvent("AddFightBuff")]
    [DialogEvent("增加战斗Buff")]
    public class AddFightBuff : IDialogEvent
    {
        private MonstarMag MonstarMag => Tools.instance.monstarMag;
        private List<BuffInfo> _buffList;
        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {

            MyLog.LogCommand(command);
            var target = command.GetInt(0, 0);
            _buffList = command.ToListString(1).Select(buff => new BuffInfo(buff)).ToList();
            var isPlayer = target == 0;
            if (NpcUtils.IsFightScene)
            {
                var avatar = isPlayer ? env.player : env.player.OtherAvatar;

                foreach (var buff in _buffList)
                {
                    command.LogInfos($"战斗BUFF添加 ID:{buff.Id} 数量:{buff.Value}");
                    avatar.spell.addDBuff(buff.Id, buff.Value);
                }
            }
            else
            {
                var dict = PlayerEx.Player.StreamData.DanYaoBuFFDict;
                var name = isPlayer ? "玩家" : "敌方";
                command.LogInfos($"{name}");
                if (!isPlayer)
                {

                    dict = MonstarMag.monstarAddBuff;
                }

                foreach (var buff in _buffList)
                {
                    var id = buff.Id;
                    var value = buff.Value;
                    command.LogInfos($"战斗前BUFF添加 ID:{id} 数量:{value}");
                    if (dict.ContainsKey(id))
                    {
                        dict[id] += value;

                    }
                    else
                    {
                        dict.Add(id, value);
                    }

                }
            }
            MyLog.LogCommand(command, false);
            callback?.Invoke();
        }
    }
}