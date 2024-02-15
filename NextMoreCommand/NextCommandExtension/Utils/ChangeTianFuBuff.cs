using System;
using System.Linq;
using JSONClass;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils
{
    [DialogEvent("ChangeTianFuBuff")]
    [DialogEvent("改变天赋Buff")]
    public class ChangeTianFuBuff : IDialogEvent
    {

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var tianfuID = command.GetInt(0);
            var mode     = command.GetInt(1);
            var json     = env.player.TianFuID;
            var seid     = CrateAvatarSeidJsonData16.DataDict;
            if (tianfuID > 0 && seid.Keys.Contains(tianfuID) && json.HasField("16"))
            {
                var tianfu   = seid[tianfuID].value1;
                var buffjson = json["16"];
                var bufflist = buffjson.ToList();
                switch (mode)
                {
                    case 1:

                        var newList = new JSONObject(JSONObject.Type.ARRAY);
                        foreach (var id in bufflist.Where(id => id != tianfu))
                        {
                            newList.Add(id);
                        }
                        json.SetField("16", newList);
                        break;
                    default:
                        if (!bufflist.Contains(tianfu))
                        {
                            buffjson.Add(tianfu);
                        }
                        break;
                }

            }
            callback?.Invoke();

        }
    }
}