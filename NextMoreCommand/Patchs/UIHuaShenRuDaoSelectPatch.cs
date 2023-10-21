using System;
using System.Collections.Generic;
using System.Linq;
using GUIPackage;
using HarmonyLib;
using JSONClass;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogSystem;
using SuperScrollView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace SkySwordKill.NextMoreCommand.Patchs
{
    public enum ButtonState
    {
        Dark = -1,
        Button = 0,
        Light
    }

    public class CustomHuaShenRuDao : MonoBehaviour
    {
        private GameObject _btn;
        private GameObject _light;
        private GameObject _dark;
        private Image btnImage;
        private Image lightImage;
        private Image darkImage;
        private FpBtn FpBtn;
        public string Name => HuaShenData.DataDict.ContainsKey(Id) ? HuaShenData.DataDict[Id].Name : string.Empty;
        public int Id = 0;
        public ButtonState State = ButtonState.Dark;
        private void Awake()
        {
            _btn = transform.Find("Btn").gameObject;
            _light = transform.Find("Light").gameObject;
            _dark = transform.Find("Dark").gameObject;
            btnImage = _btn.GetComponent<Image>();
            lightImage = _light.GetComponent<Image>();
            darkImage = _dark.GetComponent<Image>();
            FpBtn = _btn.GetComponent<FpBtn>();
            FpBtn.mouseUpEvent.RemoveAllListeners();
        }
        private void OnEnable()
        {

            Refresh();
            var mouseUp = FpBtn.mouseUpEvent;
            mouseUp.RemoveAllListeners();
            mouseUp.AddListener(OnDadao);

        }
        public void Refresh()
        {
            var gameObjectName = Name;
            if (!string.IsNullOrWhiteSpace(gameObjectName))
            {

                gameObject.name = gameObjectName;
            }
            SetImage();
            SetState();
        }
        public void SetState()
        {
            var wudaoMag = PlayerEx.Player.wuDaoMag;
            var nowSelectDaDao = UIHuaShenRuDao.Field<int>("nowSelectDaDao");
            State = (ButtonState)(wudaoMag.getWuDaoLevelByType(Id) >= 5 ? nowSelectDaDao.Value != Id ? 0 : 1 : -1);
            switch (State)
            {
                case ButtonState.Dark:
                    _btn.SetActive(false);
                    _dark.SetActive(true);
                    _light.SetActive(false);
                    break;
                case ButtonState.Button:
                    _btn.SetActive(true);
                    _dark.SetActive(false);
                    _light.SetActive(false);
                    break;
                case ButtonState.Light:
                    _btn.SetActive(false);
                    _dark.SetActive(false);
                    _light.SetActive(true);
                    break;
            }
        }
        private Traverse UIHuaShenRuDao => Traverse.Create(UIHuaShenRuDaoSelect.Inst);
        public void OnDadao()
        {
            var inst = UIHuaShenRuDaoSelect.Inst;
            var nowSelectDaDao = Traverse.Create(inst).Field<int>("nowSelectDaDao");
            nowSelectDaDao.Value = Id;
            inst.RefreshBtnState();
            var huaShenData = HuaShenData.DataDict[Id];
            inst.Title.text = huaShenData.Name;
            var buffJsonData = _BuffJsonData.DataDict[huaShenData.Buff];
            var skill = SkillDatebase.instence.Dict[huaShenData.Skill][1];
            inst.Desc1.text = "突破化神时，" + buffJsonData.descr;
            inst.Desc2.text = skill.skill_Desc ?? "";
            inst.HideObj.SetActive(true);
        }
        public void SetImage()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return;
            }
            var btnSprite = HuaShenRuDaoUtils.GetSprite($"{Name}/btn");
            var lightSprite = HuaShenRuDaoUtils.GetSprite($"{Name}/light");
            var darkSprite = HuaShenRuDaoUtils.GetSprite($"{Name}/dark");
            if (btnSprite != null)
            {

                FpBtn.nomalSprite = btnSprite;
                btnImage.sprite = btnSprite;
            }
            if (lightSprite != null)
            {
                FpBtn.stopClickSprite = lightSprite;
                FpBtn.mouseEnterSprite = lightSprite;
                FpBtn.mouseUpSprite = lightSprite;
                lightImage.sprite = lightSprite;
            }
            if (darkSprite != null)
            {
                FpBtn.mouseDownSprite = darkSprite;
                darkImage.sprite = darkSprite;
            }
        }
    }

    public static class HuaShenRuDaoUtils
    {
        public static Traverse UIHuaShenRuDao => Traverse.Create(UIHuaShenRuDaoSelect.Inst);
        public const string Symbolkey = "HuaShenRuDao";
        public static readonly List<CustomHuaShenRuDao> CustomHuaShenRuDaos = new List<CustomHuaShenRuDao>();
        private static DataGroup<string> StrGroup => DialogAnalysis.AvatarNextData.StrGroup;
        public static Dictionary<string, string> HuaShenRuDaoGroup => StrGroup.GetGroup(Symbolkey);
        public static Sprite GetSprite(string path)
        {
          var texture2D=  Main.Res.LoadAsset<Texture2D>($"Assets/HuaShenRuDao/{path}.png");
          return texture2D is null ? null : Main.Res.GetSpriteCache(texture2D);
        }
    }

    [HarmonyPatch(typeof(UIHuaShenRuDaoSelect))]
    public static class UIHuaShenRuDaoSelectPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(UIHuaShenRuDaoSelect), "Awake")]
        public static void Awake_Postfix(UIHuaShenRuDaoSelect __instance)
        {
            if (HuaShenData.DataList.Count == 9)
            {
                return;
            }
            var dict = HuaShenData.DataDict;

            var transform = __instance.ButtomListTransform;
            var go = transform.gameObject;
            go.AddMissingComponent<GridLayoutGroup>();
            transform.localPosition = new Vector3(0, 220, 0);
            var rectTransform = go.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(100 * 6, 100 * 2);
            var wudaoObj = transform.GetChild(0).gameObject;
            wudaoObj.SetActive(false);
            var key = dict.Keys.Where(i => i > 22).ToList();
            foreach (var i in key)
            {
                var clone = Object.Instantiate(wudaoObj, transform);
                var custom = clone.AddMissingComponent<CustomHuaShenRuDao>();
                custom.Id = i;
                HuaShenRuDaoUtils.CustomHuaShenRuDaos.Add(custom);
                clone.SetActive(true);
            }
            wudaoObj.SetActive(true);


        }
        [HarmonyPostfix]
        [HarmonyPatch(typeof(UIHuaShenRuDaoSelect), nameof(UIHuaShenRuDaoSelect.RefreshBtnState))]
        public static void RefreshBtnState_Postfix(UIHuaShenRuDaoSelect __instance)
        {
            if (HuaShenRuDaoUtils.CustomHuaShenRuDaos.Count == 0)
            {
                return;
            }
            foreach (var custom in HuaShenRuDaoUtils.CustomHuaShenRuDaos)
            {
                custom.Refresh();
            }

        }
    }
}