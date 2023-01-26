using System;
using KBEngine;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.Next.StaticFace;
using SkySwordKill.NextMoreCommand.Attribute;
using Spine;
using Spine.Unity;
using UnityEngine;
using GameObject = UnityEngine.GameObject;
using Object = UnityEngine.Object;

namespace SkySwordKill.NextMoreCommand.Command;

[RegisterCommand]
[DialogEvent("SetFightCustomFace")]
public class SetFightCustomFace : IDialogEvent
{
    public Avatar Player => (Avatar)KBEngineApp.app.entities[10];
    public JSONObject AvatarRandomJsonData => jsonData.instance.AvatarRandomJsonData;
    public JSONObject PlayerFace => AvatarRandomJsonData[NPCEx.NPCIDToNew(0)];

    public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
    {
        var faceId = command.GetInt(0, -1);
        Main.LogInfo($"触发指令{faceId}");
        var go = Player.renderObj as GameObject;
        if (faceId == 0)
        {
            if (go != null)
            {
                Main.LogInfo("触发换装");
                var setFace = go.GetComponentInChildren<PlayerSetRandomFace>();
                Main.LogInfo(setFace);
                setFace.setFaceByJson(PlayerFace);
        
            }
            callback?.Invoke();
            return;
        }
        
        if (StaticFaceUtils.HasFace(faceId))
        {
            var face = StaticFaceUtils.GetFace(faceId);
            var clone = new JSONObject(PlayerFace.ToString());
            foreach (var info in face.RandomInfos)
            {
                if (clone.HasField(info.Key))
                {
                    if (info.Key == "Sex")
                    {
                        continue;
                    }

                    Main.LogInfo($"KEY: {info.Key} VALUE: {info.Value}");
                    clone.SetField(info.Key, info.Value);
                }
            }

            Main.LogInfo(clone.ToString());
          
            if (go != null)
            {
                Main.LogInfo("触发换装");
                var setFace = go.GetComponentInChildren<PlayerSetRandomFace>();
                Main.LogInfo(setFace);
                setFace.setFaceByJson(clone);
            }
        }

        callback?.Invoke();
    }
}