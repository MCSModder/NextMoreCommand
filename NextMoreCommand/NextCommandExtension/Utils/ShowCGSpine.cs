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
            var skin = command.GetStr(3, "default");
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
        public string nowSpine = string.Empty;
        private bool _isInit;
        public bool Enable
        {
            get => skeletonAnimationGameObject && skeletonAnimationGameObject.activeSelf;
            set
            {
                if (skeletonAnimationGameObject)
                {
                    skeletonAnimationGameObject.SetActive(value);
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
        }
        public void SetShow(bool show)
        {
            CgManager.Enable = show;
            Enable = show;
        }
        public void SetSkin(string skin)
        {
            var skinName = AssetsUtils.CheckSkin(nowSpine, skin) ? skin : "default";
            if (_isInit)
            {
                skeletonAnimation.Skeleton.SetSkin(skinName);
            }
            else
            {
                skeletonAnimation.initialSkinName = skinName;
            }
        }
        public void SetAnimation(string animation, bool isLoop = true)
        {
            if (!AssetsUtils.CheckAnimation(nowSpine, animation)) return;
            if (_isInit)
            {
                skeletonAnimation.AnimationState.SetAnimation(0, animation, isLoop);
            }
            else
            {
                skeletonAnimation.AnimationName = animation;
            }
        }
        public void SetSpine(string spine, string animation, string skin)
        {
            if (string.IsNullOrWhiteSpace(spine))
            {
                return;
            }
            if (skeletonAnimationGameObject != null)
            {
                if (nowSpine == spine)
                {
                    SetSkin(skin);
                    SetAnimation(animation);
                    return;
                }
                DestroyImmediate(skeletonAnimationGameObject);
            }
            if (!AssetsUtils.GetSkeletonAnimation(spine, out var prefab, ESpineAssetType.Cg))
            {
                nowSpine = string.Empty;
                _isInit = false;
                return;
            }

            nowSpine = spine;
            prefab.AddComponent<CustomSpine>();
            skeletonAnimationGameObject = Instantiate(prefab, CgManager.Image.transform);
            SetShow(true);

            skeletonAnimation = skeletonAnimationGameObject.GetComponent<SkeletonAnimation>();
            _customSpine = skeletonAnimationGameObject.GetComponent<CustomSpine>();
            _customSpine.SetAvatar(spine);
            SetSkin(skin);
            SetAnimation(animation);
            _isInit = true;
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