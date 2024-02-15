using System;
using System.Linq;
using JSONClass;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils
{
    [DialogEvent("ChangeSelectTianFu")]
    [DialogEvent("改变选中天赋")]
    public class ChangeSelectTianFu : IDialogEvent
    {

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var tianfuID = command.GetInt(0);
            var mode     = command.GetInt(1);
            if (tianfuID > 0 && CreateAvatarJsonData.DataDict.Keys.Contains(tianfuID))
            {
                var list = env.player.SelectTianFuID.ToList();
                switch (mode)
                {
                    case 1:

                        var newList = new JSONObject(JSONObject.Type.ARRAY);
                        foreach (var id in list.Where(id => id != tianfuID))
                        {
                            newList.Add(id);
                        }
                        env.player.SelectTianFuID = newList;
                        break;
                    default:
                        if (!list.Contains(tianfuID))
                        {
                            env.player.SelectTianFuID.Add(tianfuID);
                        }
                        break;
                }

            }
            callback?.Invoke();

        }
    }
}