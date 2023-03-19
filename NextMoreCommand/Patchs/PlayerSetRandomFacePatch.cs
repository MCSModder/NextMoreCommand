using System;
using System.Collections;
using System.Linq;
using HarmonyLib;
using ProGif.GifManagers;
using ProGif.Lib;
using SkySwordKill.NextMoreCommand.Utils;
using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Serialization;
using AnimationState = Spine.AnimationState;
using Avatar = KBEngine.Avatar;
using GameObject = UnityEngine.GameObject;
using Image = UnityEngine.UI.Image;
using Object = UnityEngine.Object;

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
        [FormerlySerializedAs("_skeletonAnimation")] public SkeletonAnimation skeletonAnimation;
        private int _avatarId;
        private PlayerSetRandomFace _playerSetRandomFace;
        private Image _image;
        private DImageDisplayHandler _displayHandler;
        private EStateType _stateType;

        public void SetSpine(PlayerSetRandomFace playerSetRandom)
        {
            spine = playerSetRandom.transform;
            _playerSetRandomFace = playerSetRandom;

            if (!NpcUtils.IsFightScene || playerSetRandom.GetComponent<SkeletonGraphic>() != null)
            {
                if (playerSetRandom.BaseImage == null) return;
                _image = playerSetRandom.BaseImage.GetComponentInChildren<Image>();
                if (_image == null) return;
                _displayHandler = gameObject.AddMissingComponent<DImageDisplayHandler>();
                _stateType = EStateType.Image;

            }
            else
            {
                skeletonAnimation = playerSetRandom.GetComponent<SkeletonAnimation>();
                _spriteRenderer = gameObject.AddMissingComponent<SpriteRenderer>();
                var transform1 = transform;
                transform1.SetParent(spine.parent);
                // _animatedTextures = transform.parent.gameObject.AddMissingComponent<AnimatedTextures>();
                _stateType = EStateType.SpriteRenderer;
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
        private string _gifPlayerName = string.Empty;
        public string GifPlayerName
        {
            get
            {
                if (_avatarId <= 1 || _stateType == EStateType.None)
                {
                    return string.Empty;
                }
                if (!string.IsNullOrWhiteSpace(_gifPlayerName)) return _gifPlayerName;
                //  MyPluginMain.LogInfo($"gif:{_gifPlayerName}");
                var npcName = _avatarId.GetNpcName();
                _gifPlayerName = $"{_avatarId.ToString()}_{npcName}_{_stateType.GetName()}_Idle";
                return _gifPlayerName;

            }
        }
        // ReSharper disable Unity.PerformanceAnalysis
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

            if (skeletonAnimation != null)
            {
                skeletonAnimation.maskInteraction = hasSprite ? SpriteMaskInteraction.VisibleInsideMask : SpriteMaskInteraction.None;

            }

            if (!AssetsUtils.GetGifPath(avatar, out var path))
            {
                //    Destroy(this);
                yield break;
            }

            {
                if (_stateType == EStateType.None)
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
                    switch (_stateType)
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
                    switch (_stateType)
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
                        switch (_stateType)
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
                        switch (_stateType)
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
                            default:
                                //throw new ArgumentOutOfRangeException();
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
                            case ProGifPlayerSpriteRenderer:
                                go.SetActive(true);
                                player.ChangeDestination(_spriteRenderer);
                                break;
                            case ProGifPlayerImage proGifPlayerImage when proGifPlayerImage.destinationImage != null:
                                go.SetActive(true);
                                player.AddExtraDestination(_image);
                                break;
                            case ProGifPlayerImage:
                                go.SetActive(true);
                                player.ChangeDestination(_image);
                                break;
                            case not null:

                                switch (_stateType)
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
            if (string.IsNullOrWhiteSpace(_gifPlayerName))
            {
                return;
            }
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
            _gifPlayerName = string.Empty;
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
            skeletonAnimation = null;
            _avatarId = 0;
            _playerSetRandomFace = null;
            _gifPlayerName = string.Empty;
            if (_image != null && _displayHandler != null)
            {
                Destroy(_displayHandler);
            }
        }
    }

    [HarmonyPatch(typeof(PlayerSetRandomFace), nameof(PlayerSetRandomFace.randomAvatar))]
    public static class PlayerSetRandomFaceRandomAvatarPatch
    {
        public static int m_avartarID;
        public static bool customSpine;
        public static bool Prefix(PlayerSetRandomFace __instance, int monstarID)
        {
            customSpine = false;
            m_avartarID = monstarID;
            var isAvatar = m_avartarID == 1 || m_avartarID >= 20000;
            if (SceneEx.NowSceneName == "MainMenu" || !isAvatar)
            {
                return true;
            }
            var avartarID = NPCEx.NPCIDToOld(m_avartarID);
            if (AssetsUtils.GetSkeletonData(avartarID, out var skeletonData))
            {

                var skeletonGraphic = __instance.GetComponent<SkeletonGraphic>();
                var skeletonAnimation = __instance.GetComponent<SkeletonAnimation>();
                if (skeletonGraphic != null)
                {
                    skeletonGraphic.skeletonDataAsset = skeletonData;
                    skeletonGraphic.initialSkinName = "default";
                    skeletonGraphic.Skeleton.SetToSetupPose();
                    skeletonGraphic.startingAnimation = "Idle_0";
                    skeletonGraphic.Initialize(true);
                    customSpine = true;
                }
                else if (skeletonAnimation != null)
                {
                    if (AssetsUtils.GetSkeletonAnimation(avartarID, out var skeletonAnimationGo))
                    {
                        skeletonAnimation.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                        var gameObject = Object.Instantiate(skeletonAnimationGo, skeletonAnimation.transform.parent);
                        var skeletonAnimation1 = gameObject.GetComponent<SkeletonAnimation>();
                        skeletonAnimation.AnimationState.Start += entry =>
                        {
                          //  MyPluginMain.LogInfo(entry.Animation.Name);
                            var name = entry.Animation.Name;
                            var hasAnimation = AssetsUtils.CheckAnimation(avartarID, skeletonAnimation1.Skeleton, name);
                            if (!hasAnimation) return;
                            var isIdle = name == "Idle_0";
                            var trackEntry = skeletonAnimation1.AnimationState.GetCurrent(0);
                            if (trackEntry != null)
                            {
                                skeletonAnimation1.AnimationState.ClearTrack(0);
                            }
                            skeletonAnimation1.AnimationState.SetAnimation(0, name, isIdle);

                        };
                        customSpine = true;
                    }
                }
                var baseSpine = __instance.BaseSpine;
                if (baseSpine != null)
                {
                    baseSpine.SetActive(customSpine);
                }
                var baseImage = __instance.BaseImage;
                if (baseImage != null)
                {
                    baseImage.SetActive(!customSpine);
                }
                if (customSpine)
                {
                    return false;
                }


            }

            MyLog.Log($"Prefix avartarID:{m_avartarID.ToString()} monstarID:{monstarID.ToString()}");
            if (__instance.BaseImage == null)
            {
                var gameObject = new GameObject("Image", typeof(CustomImage));
                __instance.BaseImage = gameObject;
            }
            var customImage = __instance.BaseImage.AddMissingComponent<CustomImage>();
            customImage.SetSpine(__instance);

            return true;
        }
        public static void Postfix(PlayerSetRandomFace __instance, int monstarID)
        {
            var isAvatar = m_avartarID == 1 || m_avartarID >= 20000;
            if (SceneEx.NowSceneName == "MainMenu" || !isAvatar)
            {
                return;
            }
            if (customSpine)
            {
                return;
            }
            var img = __instance.BaseImage;
            if (img == null) return;

            MyLog.Log($"Postfix avartarID:{m_avartarID.ToString()} monstarID:{monstarID.ToString()}");
            var customImage = img.AddMissingComponent<CustomImage>();

            img.SetActive(true);
            customImage.SetAvatarId(m_avartarID);
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