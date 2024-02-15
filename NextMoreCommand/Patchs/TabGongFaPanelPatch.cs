// using System;
// using Bag;
// using HarmonyLib;
// using Tab;
// using UnityEngine;
// using UnityEngine.EventSystems;
// using UnityEngine.Serialization;
// using UnityEngine.UI;
// using Object = System.Object;
//
// namespace SkySwordKill.NextMoreCommand.Patchs
// {
//     enum PassiveSkillType
//     {
//         None = 0,
//         道心 = 7,
//         辅修5,
//         辅修6
//     }
//
//     public class CustomPassiveSkillSlot : PasstiveSkillSlot
//     {
//         private PassiveSkillType passiveSkillType = PassiveSkillType.None;
//         private static TabGongFaPanel GongFaPanel => SingletonMono<TabUIMag>.Instance.GongFaPanel;
//         private void Awake()
//         {
//             AcceptType = CanSlotType.功法;
//  
//             _horizontal = GetComponentInParent<HorizontalLayoutGroup>();
//         }
//         private void Update()
//         {
//
//             switch (skillSlotType)
//             {
//                 case 7:
//                     passiveSkillType = PassiveSkillType.道心;
//                     break;
//                 case 8:
//                     passiveSkillType = PassiveSkillType.辅修5;
//                     break;
//                 case 9:
//                     passiveSkillType = PassiveSkillType.辅修6;
//                     break;
//                 default:
//                     return;
//             }
//             SetPassive();
//         }
//         public bool hasName;
//         public bool hasPostion;
//         public bool isYuanYin => PlayerEx.Player.getLevelType() > 4;
//         public void SetPassive()
//         {
//             switch (passiveSkillType)
//             {
//                 case PassiveSkillType.道心:
//                     if (!hasName)
//                     {
//                         hasName = true;
//                         name = "道心";
//                     }
//                     if (isYuanYin)
//                     {
//
//                         _horizontal.spacing = 260;
//                         gameObject.AddMissingComponent<LayoutGroup>();
//                     }
//                     UpdateUI();
//                     break;
//                 case PassiveSkillType.辅修5:
//                     if (!hasPostion)
//                     {
//
//                     }
//                     break;
//                 case PassiveSkillType.辅修6:
//                     if (!hasPostion)
//                     {
//
//                     }
//                     break;
//                 default:
//                     return;
//             }
//         }
//         private void Reset()
//         {
//             if (passiveSkillType == PassiveSkillType.道心 && _horizontal != null)
//             {
//                 _horizontal.spacing = -220;
//             }
//         }
//         private void OnDisable()
//         {
//             Reset();
//
//         }
//         private void OnDestroy()
//         {
//             Reset();
//         }
//         public override void OnEndDrag(PointerEventData eventData)
//         {
//             if (!CanDrag())
//                 return;
//             if (!IsIn && !DragMag.Inst.EndDrag())
//                 GongFaPanel.RemoveSkill(skillSlotType);
//             DragMag.Inst.Clear();
//         }
//
//         public override void OnPointerUp(PointerEventData eventData)
//         {
//             if (eventData.dragging || eventData.button != PointerEventData.InputButton.Right || IsNull())
//                 return;
//             ToolTipsMag.Inst.Close();
//             GongFaPanel.RemoveSkill(skillSlotType);
//         }
//         public int skillSlotType;
//         private HorizontalLayoutGroup _horizontal;
//     }
//
//     [HarmonyPatch(typeof(TabGongFaPanel), "Init")]
//     public static class TabGongFaPanelPatch
//     {
//         public static void RemoveSkill(this TabGongFaPanel panel, int slotType)
//         {
//             if (panel.PasstiveSkillDict.ContainsKey(slotType))
//             {
//                 PlayerEx.Player.UnEquipStaticSkill(panel.PasstiveSkillDict[slotType].Skill.SkillId);
//                 panel.PasstiveSkillDict[slotType].SetNull();
//             }
//             else
//                 Debug.LogError($"不存在当前Key{slotType}");
//         }
//         private static void Postfix(TabGongFaPanel __instance)
//         {
//             if (__instance.PasstiveSkillDict.Count > 7)
//             {
//                 return;
//             }
//             var go = __instance.PasstiveSkillDict[6].gameObject;
//             go.SetActive(false);
//             var parent = go.transform.parent;
//             var custom = UnityEngine.Object.Instantiate(go, parent);
//             go.SetActive(true);
//
//             UnityEngine.Object.Destroy(custom.GetComponent<PasstiveSkillSlot>());
//             var customPassive = custom.AddMissingComponent<CustomPassiveSkillSlot>();
//             customPassive.skillSlotType = 7;
//             __instance.PasstiveSkillDict.Add(7, customPassive);
//
//             go = __instance.PasstiveSkillDict[4].gameObject;
//             go.SetActive(false);
//             custom = UnityEngine.Object.Instantiate(go, parent);
//             UnityEngine.Object.Destroy(custom.GetComponent<PasstiveSkillSlot>());
//             customPassive = custom.AddMissingComponent<CustomPassiveSkillSlot>();
//             customPassive.skillSlotType = 8;
//             __instance.PasstiveSkillDict.Add(8, customPassive);
//             custom = UnityEngine.Object.Instantiate(go, parent);
//             UnityEngine.Object.Destroy(custom.GetComponent<PasstiveSkillSlot>());
//             customPassive = custom.AddMissingComponent<CustomPassiveSkillSlot>();
//             customPassive.skillSlotType = 9;
//             __instance.PasstiveSkillDict.Add(9, customPassive);
//
//             go.SetActive(true);
//         }
//     }
// }

