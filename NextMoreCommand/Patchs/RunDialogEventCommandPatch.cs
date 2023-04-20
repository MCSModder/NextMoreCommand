using System.Collections.Generic;
using Fungus;
using HarmonyLib;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.DialogTrigger;
using UnityEngine;

namespace SkySwordKill.NextMoreCommand.Patchs
{
    [HarmonyPatch(typeof(DialogAnalysis), nameof(DialogAnalysis.RunDialogEventCommand))]
    public static class RunDialogEventCommandPatch
    {
        public static void Postfix(DialogCommand command)
        {
            if (command.Command != "Dungeon_Exit")
            {
                return;
            }
            if (OnEnterThreeScene.ThreeSceneMag != null)
            {
              var go =  OnEnterThreeScene.ThreeSceneMag.gameObject;
              Object.DestroyImmediate(OnEnterThreeScene.ThreeSceneMag);
              go.AddMissingComponent<ThreeSceneMag>();
            }
            var normalShow = MapPlayerController.Inst.NormalShow;
            var nowSpineName = Traverse.Create(normalShow).Field<string>("nowSpineName");
            var old = nowSpineName.Value;
            if (_original.Contains(old) || string.IsNullOrWhiteSpace(old))
            {
                return;
            }
            nowSpineName.Value = "";
            normalShow.Refresh();
        }
        private static List<string> _original = new List<string>()
        {
            "MapPlayerYuJian",
            "MapPlayerHeDianZu",
            "MapPlayerWalk"
        };
    }
}