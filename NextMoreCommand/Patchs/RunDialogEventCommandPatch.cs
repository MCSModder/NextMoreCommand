﻿using System.Collections.Generic;
using HarmonyLib;
using SkySwordKill.Next.DialogSystem;

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
            var normalShow = MapPlayerController.Inst.NormalShow;
            var nowSpineName = Traverse.Create(normalShow).Field<string>("nowSpineName");
            if (_original.Contains(nowSpineName.Value))
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