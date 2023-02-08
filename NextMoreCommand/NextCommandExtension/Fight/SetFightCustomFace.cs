using System;
using KBEngine;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.Next.StaticFace;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Utils;
using GameObject = UnityEngine.GameObject;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Fight;

[RegisterCommand]
[DialogEvent("SetFightCustomFace")]
[DialogEvent("自定义战斗立绘")]
public class SetFightCustomFace : IDialogEvent
{
    public Avatar Player => (Avatar)KBEngineApp.app.entities[10];
    public JSONObject AvatarRandomJsonData => jsonData.instance.AvatarRandomJsonData;
    public JSONObject PlayerFace => AvatarRandomJsonData[NPCEx.NPCIDToNew(0)];
    private int _faceId;
    public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
    {
        MyLog.LogCommand(command);
         _faceId = command.GetInt(0, -1);
        var go = Player.renderObj as GameObject;
        if (_faceId == 0)
        {
            MyLog.Log(command,$"开始重置换装 立绘ID:{_faceId}");
            if (go != null)
            {
                var setFace = go.GetComponentInChildren<PlayerSetRandomFace>();
                setFace.setFaceByJson(PlayerFace);
                MyLog.Log(command,$"成功重置换装 CustomFaceId:{_faceId}");
            }
        }
        else
        {
            MyLog.Log(command,$"开始替换换装 CustomFaceId:{_faceId}");
            if (StaticFaceUtils.HasFace(_faceId))
            {
                var face = StaticFaceUtils.GetFace(_faceId);
                var clone = new JSONObject(PlayerFace.ToString());
                foreach (var info in face.RandomInfos)
                {
                    if (clone.HasField(info.Key))
                    {
                        if (info.Key == "Sex")
                        {
                            continue;
                        }
                        clone.SetField(info.Key, info.Value);
                    }
                }


                if (go != null)
                {
                    var setFace = go.GetComponentInChildren<PlayerSetRandomFace>();
                    setFace.setFaceByJson(clone);
                    MyLog.Log(command,$"成功替换换装 CustomFaceId:{_faceId}");
                }
                else
                {
                    MyLog.Log(command,$"失败替换换装 CustomFaceId:{_faceId} 未找到PlayerSetRandomFace组件",true);
                }
            }
            else
            {
                MyLog.Log(command,$"失败替换换装 不存在该 CustomFaceId:{_faceId}",true);
            }
        }

        MyLog.LogCommand(command,false);
        callback?.Invoke();
    }
}