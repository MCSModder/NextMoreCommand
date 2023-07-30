using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
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
            var @event = command.GetStr(1);
            // var animation = command.GetStr(2);
            // var skin = command.GetStr(3, "default");
            var cgSpineManager = CGSpineManager.Instance;
            // MyLog.Log($"spine:{spine} animation:{animation} skin:{skin}");
            if (string.IsNullOrWhiteSpace(@event))
            {
                cgSpineManager.SetSpine(spine, callback);
            }
            else
            {
                
                if (DialogAnalysis.IsRunningEvent)
                {
                    DialogAnalysis.CancelEvent();
                }
                UniTask.ToCoroutine(async () =>
                {
                    cgSpineManager.SetSpine(spine);
                    await UniTask.Delay(TimeSpan.FromSeconds(1), DelayType.Realtime);
                    if (DialogAnalysis.IsRunningEvent)
                    {
                        DialogAnalysis.SwitchDialogEvent(@event);
                    }
                    else
                    {
                        DialogAnalysis.StartDialogEvent(@event);
                    }
                  
                });
            }
        
        }
    }

    [DialogEvent("PrepareCGSpine")]
    public class PrepareCGSpine : IDialogEvent
    {

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var spineKey = command.GetStr(0);
            var spine = command.GetStr(1);
            var animation = command.GetStr(2);
            var skin = command.GetStr(3, "default").Trim();
            var cgSpineManager = CGSpineManager.Instance;
            MyLog.Log($"spine:{spine} animation:{animation} skin:{skin}");
            // DialogAnalysis.SetStr("CG_SPINE",spineKey,new PrepareCGSpineInfo(spine,$"{spineKey}:{animation}:{skin}").ToString());
            cgSpineManager.PrepareSpine(spineKey, spine, animation, skin,callback);
        }
    }

    public class PrepareCGSpineInfo
    {
        public string SpineName { get; private set; }
        public string SpineAnimation { get; private set; } = "";
        public string SpineSkin { get; private set; } = "default";
        public string SpineKey{ get; private set; }
        public void Prepare()
        {
            var cgSpineManager = CGSpineManager.Instance;
            cgSpineManager.PrepareSpine(SpineKey, SpineName, SpineAnimation, SpineSkin);
        }
        public PrepareCGSpineInfo()
        {
            
        }
        public PrepareCGSpineInfo(string spine, string text)
        {
            SpineName = spine;
            if (text.Contains(":"))
            {
                var split = text.Split(':');
                MyLog.Log(JArray.FromObject(split));
                SpineKey = split[0];
                if (split.Length >= 2)
                {
                    SpineAnimation = split[1];
                }
                if (split.Length>= 3)
                {
                    SpineSkin = split[2].Trim();
                }
               
           
            }
            else
            {
                SpineKey = text;
            }
        }
        public override string ToString()
        {
            return JObject.FromObject(this).ToString();
        }
    }
    [DialogEvent("PrepareMultiCGSpine")]
    public class PrepareMultiCGSpine : IDialogEvent
    {

        public void Execute(DialogCommand command, DialogEnvironment env, Action callback)
        {
            var spine = command.GetStr(0);
            var spineInfo = command.ToListString(1).Select(text=>new PrepareCGSpineInfo(spine,text)).ToList();
            var cgSpineManager = CGSpineManager.Instance;
            foreach (var info in spineInfo)
            {
                info.Prepare();
                // DialogAnalysis.SetStr("CG_SPINE",info.SpineKey,info.ToString());
            }
            UniTask.ToCoroutine(async () =>
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(1));
                    callback?.Invoke();
                }
            );
           
        }
    }
    public class SpineCreateOption
    {
        public SpineCreateOption(string spineName, string spineAnimation, string spineSkin, GameObject parent)
        {
            SpineName = spineName;
            SpineAnimation = spineAnimation;
            SpineSkin = spineSkin;
            Parent = parent;
        }
        public string SpineName { get; private set; }
        public string SpineAnimation { get; private set; }
        public string SpineSkin { get; private set; }
        public GameObject Parent { get; private set; }
        public Transform ParentTransform => Parent.transform;
        public GameObject SetSpine(GameObject prefab)
        {
            if (prefab.activeSelf)
            {
                prefab.SetActive(false);
            }
            prefab.AddMissingComponent<CustomSpine>();
            var skeletonGraphic = prefab.GetComponent<SkeletonGraphic>();
            var originalAnimation = skeletonGraphic.startingAnimation;
            // skeletonGraphic.initialSkinName = SpineSkin;
            skeletonGraphic.startingAnimation = SpineAnimation;
            prefab.layer = 5;
            var go = Object.Instantiate(prefab, ParentTransform);
            skeletonGraphic.startingAnimation = originalAnimation;
            var skin = skeletonGraphic.skeletonDataAsset.GetSkeletonData(true).Skins.FirstOrDefault(skin1 => skin1.Name == SpineSkin);
            skeletonGraphic = go.GetComponent<SkeletonGraphic>();

            if (skin is null) return go;
            Parent.SetActive(true);
            go.SetActive(true);
            skeletonGraphic.Skeleton.SetSkin(skin);
            skeletonGraphic.Skeleton.SetSlotsToSetupPose();
            skeletonGraphic.LateUpdate();
            go.SetActive(false);
            Parent.SetActive(false);


            return go;
        }

    }

    public class SpineObject
    {
        public GameObject GameObject { get; private set; }
        public SkeletonGraphic SkeletonGraphic { get; private set; }
        public CustomSpine CustomSpine { get; private set; }
        public string SpineName { get; private set; }
        public bool Enable
        {
            get => GameObject && GameObject.activeSelf;
            set
            {
                if (!GameObject) return;
                GameObject.SetActive(value);
            }
        }
        public Dictionary<string, Skin> SkinDict { get; private set; } = new Dictionary<string, Skin>();
        public SpineObject(string spineName, GameObject gameObject)
        {
            SpineName = spineName;
            GameObject = gameObject;
            SkeletonGraphic = GameObject.GetComponent<SkeletonGraphic>();
            CustomSpine = GameObject.AddMissingComponent<CustomSpine>();
            foreach (var skin in SkeletonGraphic.skeletonDataAsset.GetSkeletonData(true).Skins)
            {
                SkinDict.Add(skin.Name, skin);
            }
        }
        public void SetSkin(string skinName, bool isInit = true)
        {
            var skeleton = SkeletonGraphic.Skeleton;
            if (!SkinDict.TryGetValue(skinName, out var skin))
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
        public void SetAnimation(string animation, bool isLoop = true, bool isInit = true)
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

        public static readonly Dictionary<string, GameObject> SpinePrefab = new Dictionary<string, GameObject>();
        public static void Clear()
        {
            SpinePrefab.Clear();
        }
        public static bool TryCreate(SpineCreateOption spineCreateOption, out SpineObject spineObject)
        {
            var spineName = spineCreateOption.SpineName;
            if (!SpinePrefab.TryGetValue(spineName, out var prefab))
            {
                if (!AssetsUtils.GetSkeletonGraphic(spineName, out prefab, ESpineAssetType.Cg))
                {
                    spineObject = null;
                    return false;
                }
                SpinePrefab.Add(spineName, prefab);
            }

            var go = spineCreateOption.SetSpine(prefab);
            var obj = new SpineObject(spineName, go);
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
            
            CgSpine = new GameObject("CGSpine");
            Canvas = CgSpine.AddMissingComponent<Canvas>();
            Canvas.sortingOrder = 20;
            Canvas.planeDistance = 100f;
            Canvas.renderMode = RenderMode.ScreenSpaceOverlay;
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
            NowSpineObject?.SetSkin(skin, _isInit);
        }
        public void SetAnimation(string animation, bool isLoop = true)
        {
            NowSpineObject?.SetAnimation(animation, isLoop, _isInit);
        }
        public async void PrepareSpine(string spineKey, string spine, string animation, string skin,Action callback)
        {
            PrepareSpine(spineKey, spine, animation, skin);
            await  UniTask.Delay(TimeSpan.FromSeconds(1), DelayType.Realtime);
            callback?.Invoke();
        }
        public bool PrepareSpine(string spineKey, string spine, string animation, string skin)
        {
            if (SpineObjects.TryGetValue(spineKey, out var spineObject))
            {
                return true;
            }
            var spineCreateOption = new SpineCreateOption(spine, animation, skin, CgSpine);
            if (!SpineObject.TryCreate(spineCreateOption, out spineObject))
            {
                return false;
            }
            spineObject.GameObject.name = spineKey;
            SpineObjects.Add(spineKey, spineObject);
            return true;
        }
        public async void SetSpine(string spine, Action callback)
        {
            if (SetSpine(spine))
            {
                MyLog.Log("执行成功");
            }
            else
            {
                MyLog.Log("执行失败");
            }
            await UniTask.Delay(TimeSpan.FromSeconds(2), DelayType.Realtime);
            callback?.Invoke();
        }

        public bool SetSpine(string spine)
        {
            if (string.IsNullOrWhiteSpace(spine))
            {
                return false;
            }
            // if (!PrepareSpine(spine,animation,skin))
            // {
            //     return;
            // }
            if (!SpineObjects.TryGetValue(spine, out var spineObject))
            {
                CgSpine.SetActive(false);
                _isInit = false;
                NowSpineObject = null;
                return false;
            }
            foreach (var spineObj in SpineObjects.Select(obj => obj.Value).Where(obj => obj.Enable))
            {
                spineObj.GameObject.SetActive(false); 
            }
          
            MyLog.Log("开始初始化");
            MyLog.Log($"spineObject:{spineObject}");
            NowSpineObject = spineObject;
            _isInit = true;
            UniTask.ToCoroutine(async () =>
            {
                await UniTask.Yield();
                CgSpine.SetActive(true);
                NowSpineObject.GameObject.SetActive(true);
                NowSpineObject.SetAvatar();
            });

            return true;
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