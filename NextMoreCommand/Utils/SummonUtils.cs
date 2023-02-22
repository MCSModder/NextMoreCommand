using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using KBEngine;
using PaiMai;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Patchs;
using Spine.Unity;
using UltimateSurvival;
using UnityEngine;
using YSGame;
using GameObject = UnityEngine.GameObject;
using Object = UnityEngine.Object;

namespace SkySwordKill.NextMoreCommand.Utils;

public static class SummonUtils
{
    private static GameObject _sayDialog;

    public static GameObject SayDialog =>
        _sayDialog ? _sayDialog : _sayDialog = Resources.Load<GameObject>($"Prefabs/SayDialog");

    public static Avatar Player => PlayerEx.Player;
    public static GameObject RenderObj => (GameObject)Player.renderObj;

    public static Transform CreateInstantiate(int npcId)
    {
        var id = npcId.ToNpcNewId();
        var json = id.NPCJson();
        var sex = json["SexType"].I;
        var asset = sex == 1 ? "Fight_new_sanxiu_2_SkeletonData" : "Fight_womensanxiu_0_SkeletonData";
        var avater = sex == 1 ? 50 : 51;
        var ab = ResManager.inst.LoadABSkeletonDataAsset(sex, asset);
        var go = Resources.Load<GameObject>($"Effect/Prefab/gameEntity/Avater/Avater{avater}/Avater{avater}_1");
        var spine = Object.Instantiate(go.transform.Find("Spine GameObject (hero-pro)"));


        spine.position = RenderObj.transform.position;
        var skeletonAnimation = spine.gameObject.GetComponentInChildren<SkeletonAnimation>();
        skeletonAnimation.skeletonDataAsset = ab;
        spine.gameObject.SetActive(true);
        var playerSetRandomFace = go.GetComponentInChildren<PlayerSetRandomFace>();
        playerSetRandomFace.randomAvatar(npcId);
        return spine; 
    }


    public static Transform CreateFaceBase(int npcId)
    {
        var face = SayDialog.transform.Find("Panel/FaceBase");

        var spine = Object.Instantiate(face, PaiMaiUiMag.Instance.transform);
        var playerSetRandomFace = spine.gameObject.GetComponentInChildren<PlayerSetRandomFace>();
        playerSetRandomFace.randomAvatar(npcId.ToNpcNewId());
     
        return spine;
    }
}