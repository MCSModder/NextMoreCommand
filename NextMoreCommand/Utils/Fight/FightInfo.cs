using System;
using System.Collections.Generic;
using System.Linq;
using Fungus;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;
using SkySwordKill.NextMoreCommand.Utils.Npc;
using YSGame;

namespace SkySwordKill.NextMoreCommand.Utils.Fight
{
    public class FightInfo
    {
        public int Id { get; private set; }
        public int CanRun { get; private set; }
        public StartFight.FightEnumType FightType { get; private set; }
        public int Background { get; private set; }
        public string Music { get; private set; }
        public List<BuffInfo> PlayerBuff { get; private set; }
        public List<BuffInfo> EnemyBuff { get; private set; }
        public string VictoryEvent { get; private set; }
        public string DefeatEvent { get; private set; }
        public string[] Tags { get; private set; }
        public void SetFightInfo(DialogCommand command)
        {
            Id = command.ToNpcId();
            // 0无法逃跑 1可以逃跑
            CanRun = command.GetInt(1);
            FightType = (StartFight.FightEnumType)command.GetInt(2);

            Background = command.GetInt(3);
            Music = command.GetStr(4);

            PlayerBuff = command.ParseBuffInfo(5);
            EnemyBuff = command.ParseBuffInfo(6);

            VictoryEvent = command.GetStr(7);
            DefeatEvent = command.GetStr(8);
            Tags = command.GetStr(9).Split(',');
        }
        public void InitBuff()
        {
            foreach (var buff in PlayerBuff)
            {
                buff.SetPlayer();
            }
            foreach (var buff in EnemyBuff)
            {
                buff.SetEnemy();
            }
        }
        public void InitFight()
        {
            MusicMag.instance.playMusic(Music);
            Tools.instance.monstarMag.FightImageID = Background;
            Tools.instance.CanFpRun = CanRun;
            Tools.instance.monstarMag.FightType = FightType;
            Tools.instance.startFight(Id);
        }
        public void SetEventFight()
        {
            Next.DialogEvent.StartFight.FightTags = Tags;
            SetStr("VictoryEvent", VictoryEvent);
            SetStr("DefeatEvent", DefeatEvent);
            SetBool("IsVictory", false);
            SetBool("IsEventFight", true);
        }
        public void Init()
        {
            try
            {
                SetEventFight();
                InitBuff();
                InitFight();
            }
            catch (Exception e)
            {
                ResetEventFight();
                MyPluginMain.LogError(e);
                throw;
            }
            DialogAnalysis.CancelEvent();
        }
        public static void ResetEventFight()
        {
            if (PlayerEx.Player == null)
            {
                return;
            }
            Next.DialogEvent.StartFight.FightTags = null;
            SetStr("VictoryEvent", string.Empty);
            SetStr("DefeatEvent", string.Empty);
            SetBool("IsVictory", false);
            SetBool("IsEventFight", false);
        }
        public const string FightNext = "Fight_NEXT";
        public static int GetInt(string key) => DialogAnalysis.GetInt(FightNext, key);
        public static void SetInt(string key, int value) => DialogAnalysis.SetInt(FightNext, key, value);
        public static bool GetBool(string key) => GetInt(key) != 0;
        public static void SetBool(string key, bool value) => SetInt(key, value ? 1 : 0);
        public static string GetStr(string key) => DialogAnalysis.GetStr(FightNext, key);
        public static void SetStr(string key, string value) => DialogAnalysis.SetStr(FightNext, key, value);
        public static bool IsEventFight => GetBool("IsEventFight");
        public static void SetVictory() => SetBool("IsVictory", true);
        public static void RunEvent()
        {
            if (!IsEventFight)
            {
                return;
            }
            var key = GetBool("IsVictory") ? "VictoryEvent" : "DefeatEvent";
            var eventId = GetStr(key);
            if (string.IsNullOrWhiteSpace(eventId))
            {
                return;
            }
            ResetEventFight();
            Action<string, DialogEnvironment> action = DialogAnalysis.IsRunningEvent ? DialogAnalysis.SwitchDialogEvent : DialogAnalysis.StartDialogEvent;
            action?.Invoke(eventId, null);
        }
    }
}