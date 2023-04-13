using System;
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
            var skin = command.GetStr(3);
            if (!string.IsNullOrWhiteSpace(cg))
            {
                CGSpineManager.Instance.SetCG(cg);
            }
            CGSpineManager.Instance.SetSpine(spine, animation, skin);
            callback?.Invoke();
        }
    }

    public class CGSpineManager : MonoBehaviour
    {
        public static CGSpineManager Instance { get; private set; }
        public static CGManager CgManager => CGManager.Instance;
        public GameObject skeletonAnimationGameObject;
        public SkeletonAnimation skeletonAnimation;
        private CustomSpine _customSpine;
        private void Awake()
        {
            if (Instance != null)
            {
                DestroyImmediate(this);
                return;
            }
            Instance = this;
        }
        public void SetSpine(string spine, string animation, string skin)
        {
            if (!AssetsUtils.GetSkeletonAnimation(spine, out var prefab)) return;
            prefab.AddComponent<CustomSpine>();
            skeletonAnimationGameObject = Instantiate(prefab, CgManager.Image.transform);
         
            skeletonAnimation = skeletonAnimationGameObject.GetComponent<SkeletonAnimation>();
            _customSpine = skeletonAnimationGameObject.GetComponent<CustomSpine>();
            _customSpine.SetAvatar(spine);
           
            skeletonAnimation.initialSkinName =AssetsUtils.CheckSkin(spine,skin) ? skin : "default";
            if (AssetsUtils.CheckAnimation(spine,animation))
            {
                skeletonAnimation.AnimationName = animation;
            }
        
            skeletonAnimation.Initialize(true);
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
        static CGSpineManager()
        {
            var go = new GameObject("NextCGSpineManager", typeof(CGSpineManager));
            DontDestroyOnLoad(go);
        }

    }
}