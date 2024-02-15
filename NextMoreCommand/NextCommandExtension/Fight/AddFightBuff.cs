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
using YSGame.Fight;
using Event = KBEngine.Event;
using GameObject = UnityEngine.GameObject;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Fight
{
    class BuffInfo
    {
        public int Id    { get; } = 0;
        public int Value { get; }
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
        public void Deconstruct(out int id, out int value)
        {
            id = Id;
            value = Value;
        }
    }

    [RegisterCommand]
    [DialogEvent("AddFightBuff")]
    [DialogEvent("增加战斗Buff")]
    public class AddFightBuff : IDialogEvent
    {
        private MonstarMag     MonstarMag => Tools.instance.monstarMag;
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

                foreach (var (id, value) in _buffList)
                {
                    command.LogInfos($"战斗BUFF添加 ID:{id} 数量:{value}");
                    switch (value)
                    {
                        case < 0 when avatar.buffmag.HasBuff(id):
                        {
                            var reduceBuff = -value;
                            var buffSum    = avatar.buffmag.GetBuffSum(id);

                            var buffByID = avatar.buffmag.getBuffByID(id);
                            if (reduceBuff > buffSum)
                            {
                                foreach (var buff in buffByID)
                                {
                                    buff[1] = 0;
                                }
                            }
                            else
                            {
                                foreach (var buff in buffByID)
                                {
                                    if (reduceBuff <= 0)
                                    {
                                        break;
                                    }
                                    var buffNum = buff[1];

                                    if (reduceBuff > buffNum)
                                    {
                                        reduceBuff -= buffNum;
                                        buff[1] = 0;
                                        continue;
                                    }
                                    buff[1] -= reduceBuff;
                                }
                            }

                            if (UIFightPanel.Inst is not null)
                                UIFightPanel.Inst.RefreshCD();
                            Event.fireOut("UpdataBuff");
                            continue;
                        }
                        case 0:
                            continue;
                        default:
                            avatar.spell.addDBuff(id, value);
                            break;
                    }


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
                    var id    = buff.Id;
                    var value = buff.Value;
                    command.LogInfos($"战斗前BUFF添加 ID:{id} 数量:{value}");
                    if (dict.ContainsKey(id))
                    {

                        dict[id] += value;
                        if (dict[id] < 0)
                        {
                            dict[id] = 0;
                        }
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