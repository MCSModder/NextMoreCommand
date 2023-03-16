using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using HarmonyLib;
using KBEngine;
using Live2D.Cubism.Framework.Json;
using Live2D.Cubism.Framework.Motion;
using Live2D.Cubism.Framework.MotionFade;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProGif.GifManagers;
using ProGif.Lib;
using SkySwordKill.Next;
using SkySwordKill.NextMoreCommand.Utils;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Avatar = KBEngine.Avatar;
using GameObject = UnityEngine.GameObject;
using Image = UnityEngine.UI.Image;

namespace SkySwordKill.NextMoreCommand.Patchs
{

    enum EStateType
    {
        None,
        Image,
        SpriteRenderer
    }

    class CustomImage : MonoBehaviour
    {
        public Transform spine;
        private SpriteRenderer _spriteRenderer;
        public SkeletonAnimation _skeletonAnimation;
        private int _avatarId;
        private PlayerSetRandomFace playerSetRandomFace;
        private Image _image;
        private DImageDisplayHandler _displayHandler;
        private EStateType stateType;

        public void SetSpine(PlayerSetRandomFace playerSetRandom)
        {
            spine = playerSetRandom.transform;
            playerSetRandomFace = playerSetRandom;

            if (!NpcUtils.IsFightScene || playerSetRandom.GetComponent<SkeletonGraphic>() != null)
            {
                if (playerSetRandom.BaseImage == null) return;
                _image = playerSetRandom.BaseImage.GetComponentInChildren<Image>();
                if (_image == null) return;
                _displayHandler = gameObject.AddMissingComponent<DImageDisplayHandler>();
                stateType = EStateType.Image;

            }
            else
            {
                _skeletonAnimation = playerSetRandom.GetComponent<SkeletonAnimation>();
                _spriteRenderer = gameObject.AddMissingComponent<SpriteRenderer>();
                var transform1 = transform;
                transform1.SetParent(spine.parent);
                // _animatedTextures = transform.parent.gameObject.AddMissingComponent<AnimatedTextures>();
                stateType = EStateType.SpriteRenderer;
                transform1.localRotation = spine.localRotation;
                transform1.localPosition = new Vector3(0, 3, 0);
                transform1.localScale = new Vector3(0.5f, 0.5f, 0);
            }

        }
        private void OnEnable()
        {
            if (_avatarId > 0)
            {
                SetAvatarId(_avatarId);
            }
        }
        private static Avatar Player => PlayerEx.Player;
        public void SetAvatarId(int monstarID)
        {
            _avatarId = NPCEx.NPCIDToOld(monstarID);
            MyLog.Log($"当前角色ID:{monstarID.ToString()} 角色ID:{_avatarId.ToString()}");
            StartCoroutine(SetAvatar(NPCEx.NPCIDToOld(monstarID)));
        }
        public string GifPlayerName
        {
            get
            {
                if (_avatarId <= 1 || stateType == EStateType.None)
                {
                    return string.Empty;
                }
                var npcName = _avatarId.GetNpcName();
                return $"{_avatarId.ToString()}_{npcName}_{stateType.GetName()}_Idle";
            }
        }
        public IEnumerator SetAvatar(int avatar)
        {
            MyLog.Log($"角色ID:{avatar.ToString()}");

            string faceId;
            if (avatar == 1)
            {
                var player = Player;
                // lihui = player.FaceWorkshop;
                faceId = player.Face.ToString();
            }
            else
            {
                var npcJson = avatar.ToNpcNewId().NPCJson();
                //lihui = npcJson.HasField("workshoplihui") ? npcJson["workshoplihui"].str : string.Empty;
                faceId = npcJson["face"].I.ToString();
            }
            // MyLog.Log($"角色id:{faceId} 立绘id:{faceId}");
            //var imagePath = string.IsNullOrWhiteSpace(lihui) ? $"Effect/Prefab/gameEntity/Avater/Avater{faceId}/{faceId}" : $"workshop_{lihui}_{faceId}";

            var sprite = ModResources.LoadSprite($"Effect/Prefab/gameEntity/Avater/Avater{faceId}/{faceId}");
            var hasSprite = sprite != null;
            if (_spriteRenderer != null)
            {
                _spriteRenderer.sprite = sprite;
            }

            if (_skeletonAnimation != null)
            {
                _skeletonAnimation.maskInteraction = hasSprite ? SpriteMaskInteraction.VisibleInsideMask : SpriteMaskInteraction.None;

            }

            if (!GifUtils.GetGifPath(avatar, out var path)) yield break;
            {
                if (stateType == EStateType.None)
                {
                    yield break;
                }

                var playerName = GifPlayerName;
                var player = PGif.iGetPlayer(playerName);
                if (player == null)
                {
                    var tranform2 = NextMoreCommand.Instance.transform;
                    var playerTranform = tranform2.Find(playerName);
                    GameObject go;
                    if (playerTranform == null)
                    {
                        go = new GameObject(playerName);
                        go.transform.SetParent(tranform2);
                        go.SetActive(false);
                    }
                    else
                    {
                        go = playerTranform.gameObject;
                        go.SetActive(false);
                    }
                    switch (stateType)
                    {
                        case EStateType.SpriteRenderer:
                            var spriteRenderer = go.AddMissingComponent<SpriteRenderer>();
                            PGif.iPlayGif(path, spriteRenderer, playerName);
                            player = PGif.iGetPlayer(playerName);
                            player.ChangeDestination(_spriteRenderer);
                            Destroy(spriteRenderer);
                            break;
                        case EStateType.Image:
                            var image = go.AddMissingComponent<Image>();
                            PGif.iPlayGif(path, image, playerName);
                            player = PGif.iGetPlayer(playerName);
                            player.ChangeDestination(_image);
                            Destroy(image);
                            break;
                        case EStateType.None:
                            break;
                    }
                    go.SetActive(true);
                    player = PGif.iGetPlayer(playerName);
                    switch (stateType)
                    {
                        case EStateType.SpriteRenderer:
                            player.AddExtraDestination(_spriteRenderer);
                            break;
                        case EStateType.Image:
                            player.AddExtraDestination(_image);
                            break;
                        case EStateType.None:
                            break;
                    }
                }
                else
                {
                    var hasPlayerComponent = player.playerComponent != null;
                    if (!hasPlayerComponent)
                    {
                        var tranform2 = NextMoreCommand.Instance.transform;
                        var playerTranform = NextMoreCommand.SearchTransform(playerName);
                        GameObject go;
                        if (playerTranform == null)
                        {
                            go = new GameObject(playerName);
                            go.transform.SetParent(tranform2);
                            go.SetActive(false);
                        }
                        else
                        {
                            go = playerTranform.gameObject;
                            go.SetActive(false);
                        }
                        switch (stateType)
                        {
                            case EStateType.SpriteRenderer:
                                var spriteRenderer = go.AddMissingComponent<SpriteRenderer>();
                                PGif.iPlayGif(path, spriteRenderer, playerName);
                                player = PGif.iGetPlayer(playerName);
                                player.ChangeDestination(_spriteRenderer);
                                Destroy(spriteRenderer);
                                break;
                            case EStateType.Image:
                                var image = go.AddMissingComponent<Image>();
                                PGif.iPlayGif(path, image, playerName);
                                player = PGif.iGetPlayer(playerName);
                                player.ChangeDestination(_image);
                                Destroy(image);
                                break;
                            case EStateType.None:
                                break;
                        }

                        player = PGif.iGetPlayer(playerName);
                        switch (stateType)
                        {
                            case EStateType.SpriteRenderer:
                                go.SetActive(true);
                                player.AddExtraDestination(_spriteRenderer);
                                break;
                            case EStateType.Image:
                                go.SetActive(true);
                                player.AddExtraDestination(_image);
                                break;
                            case EStateType.None:
                                break;
                        }

                    }
                    else
                    {
                        var component = player.playerComponent;
                        var @byte = component.GetBytes();
                        if (@byte == null)
                        {
                            var filePathName = FilePathName.Instance;
                            component.SetBytes(filePathName.ReadFileToBytes(path), true);
                        }
                        else
                        {
                            if (player.State == ProGifPlayerComponent.PlayerState.None)
                            {
                                component.PlayWithLoadedBytes();
                            }
                        }
                        var go = component.gameObject;
                        switch (component)
                        {
                            case ProGifPlayerSpriteRenderer playerSpriteRenderer when playerSpriteRenderer.destinationRenderer != null:
                                go.SetActive(true);
                                player.AddExtraDestination(_spriteRenderer);
                                break;
                            case ProGifPlayerSpriteRenderer playerSpriteRenderer:
                                go.SetActive(true);
                                player.ChangeDestination(_spriteRenderer);
                                break;
                            case ProGifPlayerImage proGifPlayerImage when proGifPlayerImage.destinationImage != null:
                                go.SetActive(true);
                                player.AddExtraDestination(_image);
                                break;
                            case ProGifPlayerImage proGifPlayerImage:
                                go.SetActive(true);
                                player.ChangeDestination(_image);
                                break;
                            case not null:

                                switch (stateType)
                                {
                                    case EStateType.SpriteRenderer:
                                        go.SetActive(true);
                                        player.AddExtraDestination(_spriteRenderer);
                                        break;
                                    case EStateType.Image:
                                        go.SetActive(true);
                                        player.AddExtraDestination(_image);
                                        break;
                                    case EStateType.None:
                                        break;
                                }
                                break;
                        }
                    }


                }
            }

        }
        private void OnDisable()
        {
            var playerName = GifPlayerName;
            var player = PGif.iGetPlayer(playerName);
            if (player == null || player.playerComponent == null) return;
            var go = NextMoreCommand.SearchGameObject(playerName);
            switch (player.playerComponent)
            {
                case ProGifPlayerSpriteRenderer playerSpriteRenderer:
                {
                    var list = playerSpriteRenderer.GetExtraDestinationList();
                    if (playerSpriteRenderer.destinationRenderer == _spriteRenderer && list.Count == 0)
                    {

                        player.Clear(false, true);
                        if (go != null)
                        {
                            go.SetActive(false);
                        }

                    }
                    else if (playerSpriteRenderer.destinationRenderer == _spriteRenderer && list.Count > 0)
                    {
                        var first = list.First();
                        playerSpriteRenderer.RemoveFromExtraDestination(first);
                        playerSpriteRenderer.ChangeDestination(first);
                    }
                    else
                    {
                        playerSpriteRenderer.RemoveFromExtraDestination(_spriteRenderer);
                    }
                    break;
                }
                case ProGifPlayerImage proGifPlayerImage:
                {
                    var list = proGifPlayerImage.GetExtraDestinationList();
                    if (proGifPlayerImage.destinationImage == _image && list.Count == 0)
                    {
                        player.Clear(false, true);
                        if (go != null)
                        {
                            go.SetActive(false);
                        }

                    }
                    else if (proGifPlayerImage.destinationImage == _image && list.Count > 0)
                    {
                        var first = list.First();
                        proGifPlayerImage.RemoveFromExtraDestination(first);
                        proGifPlayerImage.ChangeDestination(first);
                    }
                    else
                    {
                        proGifPlayerImage.RemoveFromExtraDestination(_image);
                    }
                    break;
                }
            }
        }
        private void OnDestroy()
        {
            var playerName = GifPlayerName;
            var player = PGif.iGetPlayer(playerName);
            var go = NextMoreCommand.SearchGameObject(playerName);
            if (player != null)
            {
                if (player.playerComponent != null)
                {
                    switch (player.playerComponent)
                    {
                        case ProGifPlayerSpriteRenderer playerSpriteRenderer:
                        {
                            var list = playerSpriteRenderer.GetExtraDestinationList();
                            if (playerSpriteRenderer.destinationRenderer == _spriteRenderer && list.Count == 0)
                            {

                                player.Clear(true, true);
                                if (go != null)
                                {
                                    go.SetActive(false);
                                }


                            }
                            else if (playerSpriteRenderer.destinationRenderer == _spriteRenderer && list.Count > 0)
                            {
                                var first = list.First();
                                playerSpriteRenderer.RemoveFromExtraDestination(first);
                                playerSpriteRenderer.ChangeDestination(first);
                            }
                            else
                            {
                                playerSpriteRenderer.RemoveFromExtraDestination(_spriteRenderer);
                            }
                            break;
                        }
                        case ProGifPlayerImage proGifPlayerImage:
                        {
                            var list = proGifPlayerImage.GetExtraDestinationList();
                            if (proGifPlayerImage.destinationImage == _image && list.Count == 0)
                            {
                                player.Clear(false, true);
                                if (go != null)
                                {
                                    go.SetActive(false);
                                }

                            }
                            else if (proGifPlayerImage.destinationImage == _image && list.Count > 0)
                            {
                                var first = list.First();
                                proGifPlayerImage.RemoveFromExtraDestination(first);
                                proGifPlayerImage.ChangeDestination(first);
                            }
                            else
                            {
                                proGifPlayerImage.RemoveFromExtraDestination(_image);
                            }
                            break;
                        }
                    }
                }


            }
            spine = null;

            if (_spriteRenderer != null)
            {
                Destroy(_spriteRenderer);
            }
            _skeletonAnimation = null;
            _avatarId = 0;
            playerSetRandomFace = null;

            if (_image != null && _displayHandler != null)
            {
                Destroy(_displayHandler);
            }
        }
        public void SetLive2D()
        {
            if (!Main.Res.TryGetFileAsset($"Assets/Live2D/{_avatarId.ToString()}/{_avatarId.ToString()}.model3.json", out var fileAsset)) return;
            var model3Json = CubismModel3Json.LoadAtPath(fileAsset.FileRawPath, BuiltinLoadAssetAtPath);
            if (!Main.Res.TryGetFileAsset($"Assets/Live2D/{_avatarId.ToString()}/Idle.motion3.json", out var motion3Asset)) return;
            var model = model3Json.ToModel();
            var motion = CubismMotion3Json.LoadFrom(File.ReadAllText(motion3Asset.FileRawPath));
            Transform transform1;
            (transform1 = model.transform).SetParent(transform.parent);
            transform1.localScale = new Vector3(7, 7, 1);
            var animationClip = motion.ToAnimationClip(isCallFormModelJson: true);
            animationClip.legacy = false;
            var fadeController = model.gameObject.AddMissingComponent<CubismFadeController>();
            fadeController.CubismFadeMotionList = ScriptableObject.CreateInstance<CubismFadeMotionList>();
            fadeController.CubismFadeMotionList.CubismFadeMotionObjects = new CubismFadeMotionData[]
            {
                CubismFadeMotionData.CreateInstance(motion,"Idle",animationClip.length)
            };
            fadeController.CubismFadeMotionList.MotionInstanceIds = new[]
            {
                animationClip.GetInstanceID()
            };
            var motionController = model.gameObject.AddMissingComponent<CubismMotionController>();
 
            motionController.PlayAnimation(animationClip, isLoop: true);
        }
        private static object BuiltinLoadAssetAtPath(Type assetType, string assetPath)
        {
            MyLog.Log($"assetType:{assetType.Name} 路径:{assetPath}");
            // Explicitly deal with byte arrays.
            if (assetType == typeof(byte[]))
            {
                return File.ReadAllBytes(assetPath);

            }
            if (assetType == typeof(string))
            {

                return File.ReadAllText(assetPath);

            }
            if (assetType == typeof(Texture2D))
            {

                var texture = new Texture2D(1, 1);
                texture.hideFlags = HideFlags.HideAndDontSave;
                texture.LoadImage(File.ReadAllBytes(assetPath));
                return texture;
            }
            return null;
        }
    }

    [HarmonyPatch(typeof(PlayerSetRandomFace), nameof(PlayerSetRandomFace.randomAvatar))]
    public static class PlayerSetRandomFaceRandomAvatarPatch
    {
        public static int avartarID;
        public static void Prefix(PlayerSetRandomFace __instance, int monstarID)
        {
            avartarID = monstarID;
            var isAvatar = avartarID == 1 || avartarID >= 20000;
            if (SceneEx.NowSceneName == "MainMenu" || !isAvatar)
            {
                return;
            }


            MyLog.Log($"Prefix avartarID:{avartarID.ToString()} monstarID:{monstarID.ToString()}");
            if (__instance.BaseImage == null)
            {
                var gameObject = new GameObject("Image", typeof(CustomImage));
                __instance.BaseImage = gameObject;
            }
            var customImage = __instance.BaseImage.AddMissingComponent<CustomImage>();
            customImage.SetSpine(__instance);


        }
        public static void Postfix(PlayerSetRandomFace __instance, int monstarID)
        {
            var isAvatar = avartarID == 1 || avartarID >= 20000;
            if (SceneEx.NowSceneName == "MainMenu" || !isAvatar)
            {
                return;
            }

            var img = __instance.BaseImage;
            if (img == null) return;

            MyLog.Log($"Postfix avartarID:{avartarID.ToString()} monstarID:{monstarID.ToString()}");
            var customImage = img.AddMissingComponent<CustomImage>();

            img.SetActive(true);
            customImage.SetAvatarId(avartarID);
            if (!NpcUtils.IsFightScene && __instance.GetComponent<SkeletonGraphic>() != null)
            {

                if (__instance.BaseSpine.activeSelf)
                {
                    img.SetActive(false);
                }
            }
            else
            {
                img.SetActive(true);
            }

        }
    }

}