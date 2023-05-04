using System;
using SkySwordKill.Next.DialogSystem;
using UnityEngine;

namespace SkySwordKill.NextMoreCommand.Utils.Fight
{
    public class FightManager : MonoBehaviour
    {

        public static FightManager Inst { get; private set; }
        public static FightInfo NowFightInfo { get; private set; }
        public static void ResetEventFight()
        {
            FightInfo.ResetEventFight();
        }
        public void Awake()
        {
            if (Inst != null)
            {
                DestroyImmediate(gameObject);
                return;
            }
            Inst = this;
            NowFightInfo = new FightInfo();
        }
        public static void SetVictory()
        {
            FightInfo.SetVictory();
        }
        public static void RunEvent()
        {
            FightInfo.RunEvent();
        }
        public static void SetFightInfo(DialogCommand command)
        {
            NowFightInfo ??= new FightInfo();
            NowFightInfo?.SetFightInfo(command);
            NowFightInfo?.Init();
        }
    }
}