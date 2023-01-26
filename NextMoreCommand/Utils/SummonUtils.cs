// using System.Collections.Generic;
// using KBEngine;
// using Spine.Unity;
// using UltimateSurvival;
// using UnityEngine;
// using YSGame;
// using GameObject = UnityEngine.GameObject;
//
// namespace SkySwordKill.NextMoreCommand.Utils;
//
// public static class SummonUtils
// {
//     public static KBEngineApp App => KBEngineApp.app;
//     public static Dictionary<int, Entity> Entities => App.entities;
//     public static List<int> Ids = new List<int>();
//     public static Avatar Player => (Avatar)Entities[10];
//
//     public static void CreateAvatar(int avatarID, Vector3 position, Vector3 direction)
//     {
//         var id = NPCEx.NPCIDToNew(avatarID);
//         Ids.Add(id);
//         App.Client_onCreatedProxies((ulong)id, id, "Avatar");
//         var avatar = (Avatar)Entities[id];
//         avatar.position = position;
//         avatar.direction = direction;
//         SetAvatar(avatarID);
//         // World.instance.onEnterWorld(avatar);
//         createInstantiate("Effect/Prefab/gameEntity/Avater/Avater", avatar, "Avater");
//     }
//
//     public static void createInstantiate(string patch, Entity entity, string entitytype)
//     {
//         var y = entity.position.y;
//         var avatar = (Avatar)entity;
//         var roleSurfaceCall = avatar.roleSurfaceCall;
//         entity.renderObj = (object)new GameObject(entitytype + "_" + (object)entity.id);
//         var position = new Vector3(entity.position.x, y, entity.position.z);
//         var rotation = Quaternion.Euler(new Vector3(entity.direction.y, entity.direction.z, entity.direction.x));
//         var goAvatar = new GameObject(entitytype + "_" + (object)(object)(49 + avatar.Sex) +
//                                       "_" + (object)roleSurfaceCall);
//         goAvatar.transform.parent = ((GameObject)entity.renderObj).transform;
//         goAvatar.transform.position = position;
//         goAvatar.transform.rotation = rotation;
//         var spine = new GameObject("Spine");
//         spine.transform.parent = goAvatar.transform;
//         var asset = avatar.Sex - 1 == 0 ? "Fight_new_sanxiu_2_SkeletonData" : "Fight_womensanxiu_0_SkeletonData";
//
//         var abSkeletonDataAsset = ResManager.inst.LoadABSkeletonDataAsset(avatar.Sex + 1, asset);
//         var skeletonAnimation = SkeletonAnimation.AddToGameObject(spine, abSkeletonDataAsset, true);
//         skeletonAnimation.Initialize(true,true);
//         var face = spine.AddComponent<PlayerSetRandomFace>();
//         face.faceID = entity.id;
//         face.SetNPCFace(entity.id);
//     }
//
//     public static void CreateAvatar(int avatarID)
//     {
//         var position = Player.position - new Vector3(2, 0, 0);
//         CreateAvatar(avatarID, position, Player.direction);
//     }
//
//     public static Avatar GetAvatar(int avatarID)
//     {
//         var id = NPCEx.NPCIDToNew(avatarID);
//         return (Avatar)Entities[id];
//     }
//
//     public static JSONObject AvatarJsonData => jsonData.instance.AvatarJsonData;
//
//     public static bool SetAvatar(int avatarId)
//     {
//         var id = NPCEx.NPCIDToNew(avatarId);
//         if (!AvatarJsonData.HasField(id.ToString()))
//         {
//             return false;
//         }
//
//         var avatar = GetAvatar(avatarId);
//         var data = AvatarJsonData[id.ToString()];
//         foreach (JSONObject skill in data["skills"].list)
//         {
//             SkillItem item = new SkillItem();
//             item.itemId = (int)skill.n;
//             avatar.equipSkillList.Add(item);
//         }
//
//         foreach (JSONObject staticSkill in data["staticSkills"].list)
//         {
//             SkillItem item = new SkillItem();
//             item.itemId = (int)staticSkill.n;
//             avatar.equipStaticSkillList.Add(item);
//         }
//
//         if (data.HasField("yuanying"))
//         {
//             int i = data["yuanying"].I;
//             if (i != 0)
//             {
//                 SkillItem skillItem3 = new SkillItem();
//                 skillItem3.itemId = i;
//                 skillItem3.itemIndex = 6;
//                 avatar.equipStaticSkillList.Add(skillItem3);
//             }
//         }
//
//         if (data.HasField("HuaShenLingYu") && data["HuaShenLingYu"].I > 0)
//         {
//             SkillItem skillItem4 = new SkillItem();
//             skillItem4.itemId = data["HuaShenLingYu"].I;
//             avatar.equipSkillList.Add(skillItem4);
//             avatar.HuaShenLingYuSkill = new JSONObject(data["HuaShenLingYu"].I);
//         }
//
//         for (int j = 0; j < data["LingGen"].Count; j++)
//         {
//             int item = (int)data["LingGen"][j].n;
//             avatar.LingGeng.Add(item);
//         }
//
//         if (data["id"].I < 20000)
//         {
//             if ((int)data["equipWeapon"].n > 0)
//             {
//                 avatar.YSequipItem((int)data["equipWeapon"].n, 0);
//             }
//
//             if ((int)data["equipClothing"].n > 0)
//             {
//                 avatar.YSequipItem((int)data["equipClothing"].n, 1);
//             }
//
//             if ((int)data["equipRing"].n > 0)
//             {
//                 avatar.YSequipItem((int)data["equipRing"].n, 2);
//             }
//         }
//
//         avatar.ZiZhi = (int)data["ziZhi"].n;
//         avatar.dunSu = (int)data["dunSu"].n;
//         avatar.wuXin = (uint)data["wuXin"].n;
//         avatar.shengShi = (int)data["shengShi"].n;
//         avatar.shaQi = (uint)data["shaQi"].n;
//         avatar.shouYuan = (uint)data["shouYuan"].n;
//         avatar.age = (uint)data["age"].n;
//         avatar.HP_Max = (int)data["HP"].n;
//         avatar.HP = (int)data["HP"].n;
//         avatar.level = (ushort)data["Level"].n;
//         avatar.AvatarType = (uint)((ushort)data["AvatarType"].n);
//         avatar.name =
//             Tools.instance.Code64ToString(jsonData.instance.AvatarRandomJsonData[id.ToString()]["Name"].str);
//         avatar.roleTypeCell = (uint)data["fightFace"].n;
//         avatar.roleType = (uint)data["face"].n;
//         avatar.fightTemp.MonstarID = id;
//         avatar.fightTemp.useAI = true;
//         avatar.Sex = (int)data["SexType"].n;
//         return true;
//     }
// }