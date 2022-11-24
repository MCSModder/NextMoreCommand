using System;
using System.Collections.Generic;
using HarmonyLib;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.CustomMap;
using SkySwordKill.NextMoreCommand.CustomMap.CustomMapType;
using UnityEngine;

namespace SkySwordKill.NextMoreCommand.HarmonyPatchs;

public class MapControl : MonoBehaviour
{
    private RandomFuBen _randomFuBen;
    private CustomMap.CustomMap _customMap;
    private Dictionary<int, NextDialog> _dialogs = new Dictionary<int, NextDialog>();
    private Dictionary<int, NextTrigger> _triggers = new Dictionary<int, NextTrigger>();

    private void Awake()
    {
        _randomFuBen = GetComponent<RandomFuBen>();
        var fuben = Tools.instance.getPlayer().RandomFuBenList[_randomFuBen.FuBenID.ToString()];
        _customMap = fuben?.ToObject<CustomMap.CustomMap>();
        _customMap?.NextDialog.ForEach(events =>
        {
            if (CustomMapManager.TryGetCustomMapType(events.ID, out ModCustomMapType mapType))
            {
                NextDialog dialog = mapType as NextDialog;

                if (mapType != null)
                {
                    _dialogs.Add(events.Index, dialog);
                }
            }
        });
        _customMap?.NextTrigger.ForEach(events =>
        {
            if (CustomMapManager.TryGetCustomMapType(events.ID, out ModCustomMapType mapType))
            {
                if (mapType is NextTrigger trigger)
                {
                    _triggers.Add(events.Index, trigger);
                }
            }
        });
        _customMap?.Blocks.ForEach(block =>
        {
            var mapCompont = AllMapManage.instance.mapIndex[block.Index] as MapInstComport;

            if (mapCompont != null)
            {
                mapCompont.IsStatic = true;
                mapCompont.NameStr = block.Name;
            }
        });
    }

    private void Update()
    {
        if (WASDMove.Inst != null && WASDMove.Inst.IsMoved)
        {
            var index = WASDMove.Inst.GetNowIndex();
            if (_triggers.TryGetValue(index, out NextTrigger nextTrigger))
            {
                if (!DialogAnalysis.IsRunningEvent)
                {
                    nextTrigger.Execute();
                }
            }
            else if (_dialogs.TryGetValue(index, out NextDialog nextDialog))
            {
                if (!DialogAnalysis.IsRunningEvent)
                {
                    nextDialog.Execute();
                }
            }
        }
    }
}

[HarmonyPatch(typeof(RandomFuBen), "Awake")]
public static class RandomFuBenAwake
{
    public static void Postfix(RandomFuBen __instance)
    {
        Main.LogInfo("RandomFuBen");
        if (__instance.GetComponent<MapControl>() == null &&
            CustomMapManager.CustomMapDatas.ContainsKey(__instance.FuBenID))
        {
            __instance.gameObject.AddComponent<MapControl>();
        }
    }
}