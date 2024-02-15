using System.Collections.Generic;
using SkySwordKill.Next.DialogSystem;
using UnityEngine;

namespace SkySwordKill.NextMoreCommand.Slave;

public static class SlaveUtils
{
    public static Dictionary<string, int> Slave => DialogAnalysis.AvatarNextData.IntGroup.GetGroup("SLAVE_ID");

    public static int CreateNpc(int liupai, int level, int sex)
    {
        var npcid = FactoryManager.inst.npcFactory.CreateNpcByLiuPaiAndLevel(liupai, level, sex);
        new UINPCData(npcid).RefreshData();
        DialogAnalysis.StartTestDialogEvent($"SetNpcFollow*{npcid}");
        DialogAnalysis.SetInt("SLAVE_ID", npcid.ToString(), 1);
        var npcData = NpcJieSuanManager.inst.GetNpcData(npcid);
        return npcid;
    }

    public static GameObject SayDialog;
    public static PlayerSetRandomFace CreateFace(string json, Transform parent)
    {
        SayDialog ??= Resources.Load<GameObject>("Prefabs/SayDialog")
            .GetComponentInChildren<PlayerSetRandomFace>().transform.parent.gameObject;
        var setRandomFace = Object.Instantiate(SayDialog, parent).GetComponentInChildren<PlayerSetRandomFace>();
        setRandomFace.setFaceByJson(JSONObject.CreateStringObject(json));
        return setRandomFace;
    }
}