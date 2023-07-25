using System;
using System.Collections;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Patchs;
using SkySwordKill.NextMoreCommand.Utils;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Serialization;

namespace SkySwordKill.NextMoreCommand.NextCommandExtension.Utils
{
    [DialogEvent("ShowCGSpine")]
    public class ShowCGSpine : IDialogEvent
    {

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var spine = command.GetStr(0);
            var cg = command.GetStr(1);
            var animation = command.GetStr(2);
            var skin = command.GetStr(3, "default");
            var cgSpineManager = CGSpineManager.Instance;
            if (!string.IsNullOrWhiteSpace(cg))
            {
                cgSpineManager.SetCG(cg);
            }
            MyLog.Log($"spine:{spine} animation:{animation} skin:{skin}");
            var nowSpine = cgSpineManager.nowSpine;
            cgSpineManager.SetSpine(spine, animation, skin);
            if (nowSpine != spine)
            {
                cgSpineManager.StartCoroutine(cgSpineManager.WaitForSeconds(0.5f,callback));
            }
            else
            {
                callback?.Invoke();
            }
           
        }
    }

    public class CGSpineManager : MonoBehaviour
    {
        public static CGSpineManager Instance { get; private set; }
        public static CGManager CgManager => CGManager.Instance;
        public Canvas Canvas { get; set; }
        public GameObject CgSpine { get; set; }

        public GameObject skeletonGraphicGameObject;
        public SkeletonGraphic skeletonGraphic;
        private CustomSpine _customSpine;
        public string nowSpine = string.Empty;
        private bool _isInit;
        public bool Enable
        {
            get => skeletonGraphicGameObject && skeletonGraphicGameObject.activeSelf;
            set
            {
                if (skeletonGraphicGameObject)
                {
                    skeletonGraphicGameObject.SetActive(value);
                }
            }
        }
        private void Awake()
        {
            if (Instance != null)
            {
                DestroyImmediate(this);
                return;
            }
            Instance = this;
            Canvas = gameObject.AddMissingComponent<Canvas>();
            Canvas.sortingOrder = 20;
            Canvas.planeDistance = 100f;
            Canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            CgSpine = new GameObject("CGSpine");
            CgSpine.transform.SetParent(transform);
            CgSpine.layer = 5;
            Enable = false;
        }
        public void SetShow(bool show)
        {
            Enable = show;
        }
        public void SetSkin(string skin)
        {
            var checkSkin= AssetsUtils.CheckSkin(nowSpine, skin, ESpineAssetType.Cg);
            var skinName = checkSkin ? skin : "default";
            skeletonGraphic.initialSkinName = skinName;
            MyLog.Log($"nowSpine:{nowSpine}skin:{skin} checkSkin:{checkSkin} skinName:{skinName}");
            if (_isInit)
            {
                skeletonGraphic.Initialize(true);
            }
        }
        public void SetAnimation(string animation, bool isLoop = true)
        {
            if (!AssetsUtils.CheckAnimation(nowSpine, animation, ESpineAssetType.Cg)) return;
            if (_isInit)
            {
                skeletonGraphic.AnimationState.SetAnimation(0, animation, isLoop);
            }
            else
            {
                skeletonGraphic.startingAnimation = animation;
            }
        }
   
        public void SetSpine(string spine, string animation, string skin)
        {
            if (string.IsNullOrWhiteSpace(spine))
            {
                return;
            }
            if (skeletonGraphicGameObject != null)
            {
                if (nowSpine == spine)
                {
                    SetSkin(skin);
                    SetAnimation(animation);
                    return;
                }
                DestroyImmediate(skeletonGraphicGameObject);
            }
            if (!AssetsUtils.GetSkeletonGraphic(spine, out var prefab, ESpineAssetType.Cg))
            {
                nowSpine = string.Empty;
                _isInit = false;
                return;
            }

            nowSpine = spine;
            prefab.AddComponent<CustomSpine>();
            // var skeletonGraphicPrefab = prefab.GetComponent<SkeletonGraphic>();
            // if (AssetsUtils.CheckSkin(nowSpine, skin, ESpineAssetType.Cg)) skeletonGraphicPrefab.initialSkinName = skin;
            // if (AssetsUtils.CheckAnimation(nowSpine, animation, ESpineAssetType.Cg)) skeletonGraphicPrefab.startingAnimation = animation;
            skeletonGraphicGameObject = Instantiate(prefab, CgSpine.transform);
            skeletonGraphicGameObject.layer = 5;
            skeletonGraphic = skeletonGraphicGameObject.GetComponent<SkeletonGraphic>();
            _customSpine = skeletonGraphicGameObject.GetComponent<CustomSpine>();
            SetSkin(skin);
            SetAnimation(animation);
            _customSpine.SetAvatar(spine);
            Enable = true;
            _isInit = true; 
            
            skeletonGraphic.Initialize(true);
            
        }
        public void SetCG(string cgName)
        {
            var path = $"Assets/CG/{cgName}.png";
            if (Main.Res.TryGetAsset(path, out var asset)
                && asset is Texture2D texture)
            {
                var sprite = Main.Res.GetSpriteCache(texture);
                CgManager.CGSprite = sprite;
                CgManager.Enable = true;
            }
            else
            {
                Main.LogWarning($"背景图片 {path} 不存在。");
            }
        }
        public void Reset()
        {
            Enable = false;
            var go = skeletonGraphicGameObject;
            skeletonGraphicGameObject = null;
            DestroyImmediate(go);
            _customSpine = null;
            skeletonGraphic = null;
            nowSpine = "";
            _isInit = false;
            CgManager.Enable = false;
        }
        public void LoadSave()
        {
            StartCoroutine(WaitForSeconds(1,AutoLoad));
        }
        public IEnumerator WaitForSeconds(float seconds,Action callback)
        {
            yield return new WaitForSeconds(seconds);
            callback?.Invoke();
        }
        private void AutoLoad()
        {
            YSNewSaveSystem.LoadSave(PlayerPrefs.GetInt("NowPlayerFileAvatar"), 1);
        }
        static CGSpineManager()
        {
            var go = new GameObject("NextCGSpineManager", typeof(CGSpineManager));
            DontDestroyOnLoad(go);
            go.layer = 5;

        }

    }
}
