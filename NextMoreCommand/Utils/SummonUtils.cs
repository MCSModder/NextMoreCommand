using System.Collections.Generic;
using KBEngine;
using SkySwordKill.Next.DialogSystem;
using Spine.Unity;
using UltimateSurvival;
using UnityEngine;
using YSGame;
using GameObject = UnityEngine.GameObject;

namespace SkySwordKill.NextMoreCommand.Utils;

public static class SummonUtils
{
    public static Avatar Player => PlayerEx.Player;
    public static GameObject RenderObj => (GameObject)Player.renderObj;

    public static void CreateInstantiate(int npcId)
    {
        var id = npcId.ToNpcNewId();
        var json = id.NPCJson();
        var sex = json["SexType"].I;
        var asset = sex == 1 ? "Fight_new_sanxiu_2_SkeletonData" : "Fight_womensanxiu_0_SkeletonData";
        var avater = sex == 1 ? 50 : 51;

        var go = Resources.Load<GameObject>($"Effect/Prefab/gameEntity/Avater/Avater{avater}/Avater{avater}_1");
        var spine = Object.Instantiate(go.transform.Find("Spine GameObject (hero-pro)"));
        spine.position = RenderObj.transform.position;
        var skeletonAnimation = spine.gameObject.GetComponentInChildren<SkeletonAnimation>();
        skeletonAnimation.skeletonDataAsset = ResManager.inst.LoadABSkeletonDataAsset(sex, asset);
        spine.gameObject.SetActive(true);
        var playerSetRandomFace = spine.gameObject.GetComponentInChildren<PlayerSetRandomFace>();
        playerSetRandomFace.randomAvatar(npcId);
    }
}