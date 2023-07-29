using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using SkySwordKill.Next;
using SkySwordKill.Next.DialogEvent;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Patchs;
using SkySwordKill.NextMoreCommand.Utils;
using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

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
            var nowSpine = cgSpineManager.NowSpineObject?.SpineName;

            if (nowSpine != spine)
            {
                cgSpineManager.SetSpine(spine, animation, skin, callback);
            }
            else
            {
                cgSpineManager.SetSpine(spine, animation, skin);
                callback?.Invoke();
            }

        }
    }
    [DialogEvent("PrepareCGSpine")]
    public class PrepareCGSpine : IDialogEvent
    {

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var spine = command.GetStr(0);
            var animation = command.GetStr(1);
            var skin = command.GetStr(2, "default");
            var cgSpineManager = CGSpineManager.Instance;
            MyLog.Log($"spine:{spine} animation:{animation} skin:{skin}");
            cgSpineManager.PrepareSpine(spine, animation, skin);
            callback?.Invoke();
        }
    }

    public class SpineObject
    {
        public GameObject GameObject { get; private set; }
        public SkeletonGraphic SkeletonGraphic{ get; private set; }
        public CustomSpine CustomSpine{ get; private set; }
        public string SpineName{ get; private set; }
        public bool Enable
        {
            get => GameObject && GameObject.activeSelf;
            set
            {
                if (GameObject)
                {
                    GameObject.SetActive(value);
                }
            }
        }
        public Dictionary<string, Skin> SkinDict { get; private set; } = new Dictionary<string, Skin>();
        public SpineObject(string spineName,GameObject gameObject)
        {
            SpineName = spineName;
            GameObject = gameObject;
            SkeletonGraphic = GameObject.GetComponent<SkeletonGraphic>();
            CustomSpine = GameObject.AddMissingComponent<CustomSpine>();
            foreach (var skin in SkeletonGraphic.SkeletonData.Skins)
            {
                SkinDict.Add(skin.Name,skin);
            }
        }
        public void SetSkin(string skinName,bool isInit= true)
        {
            var skeleton = SkeletonGraphic.Skeleton;
            if (!SkinDict.TryGetValue(skinName,out var skin))
            {
                SkeletonGraphic.initialSkinName = "default";
                Init();
               
                return;
            }
         
            if (isInit)
            {
                SkeletonGraphic.Skeleton.SetSkin(skin);
                skeleton.SetSlotsToSetupPose();
                SkeletonGraphic.LateUpdate();
            }
            else
            {
                SkeletonGraphic.initialSkinName = skinName;
            }
    
        }
        public void SetAnimation(string animation, bool isLoop = true,bool isInit= true)
        {
            if (!AssetsUtils.CheckAnimation(SpineName, animation, ESpineAssetType.Cg)) return;
            if (isInit)
            {
                SkeletonGraphic.AnimationState.SetAnimation(0, animation, isLoop);
            }
            else
            {
                SkeletonGraphic.startingAnimation = animation;
            }
        }
        public void Destroy()
        {
            Object.DestroyImmediate(GameObject);
        }
        public void SetAvatar()
        {
            CustomSpine.SetAvatar(SpineName);
        }
        public void Init()
        {
            SkeletonGraphic.Initialize(true);
            
        }
        public static SpineObject Create(string spineName,Transform parent)
        {
            if (!AssetsUtils.GetSkeletonGraphic(spineName, out var prefab, ESpineAssetType.Cg))
            {
                return null;
            }
            prefab.AddComponent<CustomSpine>();
            var go = Object.Instantiate(prefab, parent);
            go.layer = 5;
            var obj = new SpineObject(spineName, go)
            {
                Enable = false
            };
            return obj;
        }
        public static bool TryCreate(string spineName,Transform parent,out SpineObject spineObject)
        {
            if (!AssetsUtils.GetSkeletonGraphic(spineName, out var prefab, ESpineAssetType.Cg))
            {
                spineObject = null;
                return false;
            }
            prefab.AddMissingComponent<CustomSpine>();
            var go = Object.Instantiate(prefab, parent);
            go.layer = 5;
            var obj = new SpineObject(spineName, go)
            {
                Enable = false
            };
            spineObject = obj;
            return true;
        }
    }

    public class CGSpineManager : MonoBehaviour
    {
        public static CGSpineManager Instance { get; private set; }
        public static CGManager CgManager => CGManager.Instance;
        public Canvas Canvas { get; set; }
        public GameObject CgSpine { get; set; }
        public Dictionary<string, SpineObject> SpineObjects = new Dictionary<string, SpineObject>();
        public SpineObject NowSpineObject { get; set; }
        
        private bool _isInit;
        public bool Enable
        {
            get => CgSpine && CgSpine.activeSelf;
            set
            {
                if (CgSpine)
                {
                    CgSpine.SetActive(value);
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
            Canvas.sortingOrder = 10;
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
            NowSpineObject?.SetSkin(skin,_isInit);
        }
        public void SetAnimation(string animation, bool isLoop = true)
        {
            NowSpineObject?.SetAnimation(animation,isLoop,_isInit);
        }
        public bool PrepareSpine(string spine, string animation, string skin)
        {
            if (SpineObjects.TryGetValue(spine, out var spineObject))
            {
                spineObject.SetSkin(skin,false);
                spineObject.SetAnimation(animation,isInit:false);
                return true;
            }
            if (!SpineObject.TryCreate(spine, CgSpine.transform,out  spineObject))
            {
                return false;
            }
            spineObject.SetSkin(skin,false);
            spineObject.SetAnimation(animation,isInit:false);
            SpineObjects.Add(spine,spineObject);
            return true;
        }
        public async void SetSpine(string spine, string animation, string skin, Action callback)
        {
            SetSpine(spine, animation, skin);
            await UniTask.Delay(TimeSpan.FromSeconds(2), DelayType.Realtime);
            callback?.Invoke();
        }

        public void SetSpine(string spine, string animation, string skin)
        {
            if (string.IsNullOrWhiteSpace(spine))
            {
                return;
            }
            // if (!PrepareSpine(spine,animation,skin))
            // {
            //     return;
            // }
            if (SpineObjects.Count > 0)
            {
                if (NowSpineObject?.SpineName == spine)
                {
                    _isInit = true;
                    Enable = true;
                    NowSpineObject.Enable = true;
                    SetAnimation(animation);
                    SetSkin(skin);
                    return;
                }
                foreach (var spineObj in SpineObjects.Select(obj=>obj.Value).Where(obj=>obj.Enable))
                {
                    spineObj.Enable = false;
                 
                }
            }
            if (!SpineObjects.TryGetValue(spine,out var spineObject))
            {
                Enable = false;
                _isInit = false;
                NowSpineObject = null;
                return;
            }
            MyLog.Log("开始初始化");
            MyLog.Log($"spineObject:{spineObject}");
            NowSpineObject = spineObject;
            _isInit = true;
            SetAnimation(animation);
            SetSkin(skin);
            NowSpineObject.Enable = true;
            NowSpineObject.SetAvatar();
            NowSpineObject.Init();
            Enable = true;

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
            NowSpineObject = null;
            foreach (var value in SpineObjects.Select(spineObject => spineObject.Value))
            {
                value.Enable = false;
                value.Destroy();
            }
            Enable = false;
            SpineObjects.Clear();
            _isInit = false;
            CgManager.Enable = false;
        }
        public async void LoadSave()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1), DelayType.Realtime);
            AutoLoad();
        }
        public IEnumerator WaitForSeconds(float seconds, Action callback)
        {
            yield return new WaitForSeconds(seconds);
            callback?.Invoke();
        }
        private static void AutoLoad()
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