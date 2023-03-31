using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Fungus;
using HarmonyLib;
using JiaoYi;
using KBEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

    public class CustomImage : MonoBehaviour
    {
        public Transform spine;
        private SpriteRenderer _spriteRenderer;
        public SkeletonAnimation skeletonAnimation;
       // private Image _image;
        // private EStateType _stateType;

        public void SetSpine(PlayerSetRandomFace playerSetRandom)
        {
            spine = playerSetRandom.transform;

            skeletonAnimation = playerSetRandom.GetComponent<SkeletonAnimation>();
            _spriteRenderer = gameObject.AddMissingComponent<SpriteRenderer>();
            var transform1 = transform;
            transform1.SetParent(spine.parent);
            transform1.localRotation = spine.localRotation;
            transform1.localPosition = new Vector3(0, 3, 0);
            transform1.localScale = new Vector3(0.5f, 0.5f, 0);

        }
        private static Avatar Player => PlayerEx.Player;
        public void SetAvatarId(int monstarID)
        {

            StartCoroutine(SetAvatar(monstarID));
        }

        public IEnumerator SetAvatar(int avatar)
        {
            var npcJson = avatar.NPCJson();
            var faceId = npcJson["face"].I.ToString();
            var lihui = npcJson.HasField("workshoplihui")? npcJson["workshoplihui"].str:string.Empty;
            var path = string.Empty;
            if (faceId == "0" && string.IsNullOrWhiteSpace(lihui))
            {
                DestroyImmediate(this);
            }else if (!string.IsNullOrWhiteSpace(lihui))
            {
                path = $"workshop_{lihui}_{faceId}";
            }
            else
            {
                path = $"Effect/Prefab/gameEntity/Avater/Avater{faceId}/{faceId}";
            }
            var sprite = ModResources.LoadSprite(path);
            var hasSprite = sprite != null;
            if (_spriteRenderer != null)
            {
                _spriteRenderer.sprite = sprite;
            }

            if (skeletonAnimation != null)
            {
                skeletonAnimation.maskInteraction = hasSprite ? SpriteMaskInteraction.VisibleInsideMask : SpriteMaskInteraction.None;

            }
            if (!hasSprite)
            {
                DestroyImmediate(this);
            }
            yield break;
        }

        private void OnDestroy()
        {

            spine = null;

            if (_spriteRenderer != null)
            {
                DestroyImmediate(_spriteRenderer);
            }
            skeletonAnimation = null;
        }
    }

    public enum ESpineType
    {
        None,
        UINpcSvItem,
        SayDialog,
        UINpcJiaoHuPop,
        UINpcInfoPanel,
        FightAvatar,
        JiaoYiUIMag
    }

    [JsonObject]
    public class CustomSpinePos
    {
        public static CustomSpinePos Zero => new CustomSpinePos();
        public static CustomSpinePos One => new CustomSpinePos(1, 1, 1);
        public float X { get; set; } = 0;
        public float Y { get; set; } = 0;
        public float Z { get; set; } = 0;
        public CustomSpinePos() : this(0f, 0f)
        {
        }
        public CustomSpinePos(float x, float y, float z = 0f)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Vector3 ToVector3()
        {
            return new Vector3(X, Y, Z);
        }
        public Vector2 ToVector2()
        {
            return new Vector2(X, Y);
        }
    }

    [JsonObject]
    public class CustomSpineOption
    {
        public static readonly CustomSpineOption UINpcSvItemPos = new CustomSpineOption(new CustomSpinePos(0, -888), new CustomSpinePos(1, 1, 1));
        public static readonly CustomSpineOption SayDialogPos = new CustomSpineOption(new CustomSpinePos(0, -250.6f), new CustomSpinePos(1, 1, 1));
        public static readonly CustomSpineOption UINpcInfoPanelPos = new CustomSpineOption(new CustomSpinePos(0, -800), new CustomSpinePos(1, 1, 1));
        public static readonly CustomSpineOption FightAvatarPos = new CustomSpineOption(new CustomSpinePos(0, 0), new CustomSpinePos(0.4f, 0.4f, 1));
        public static readonly CustomSpineOption UINpcJiaoHuPopPos = new CustomSpineOption(new CustomSpinePos(0, -750), new CustomSpinePos(1, 1, 1));

        public CustomSpineOption()
        {
        }
        public CustomSpineOption(CustomSpinePos position, CustomSpinePos scale, CustomSpinePos rotation = null)
        {
            Position = position;
            Scale = scale;
            Rotation = rotation ?? CustomSpinePos.Zero;
        }
        [JsonProperty("位置", NullValueHandling = NullValueHandling.Ignore)]
        public CustomSpinePos Position { get; set; } = CustomSpinePos.Zero;
        [JsonProperty("缩放", NullValueHandling = NullValueHandling.Ignore)]
        public CustomSpinePos Scale { get; set; } = CustomSpinePos.One;
        [JsonProperty("角度", NullValueHandling = NullValueHandling.Ignore)]
        public CustomSpinePos Rotation { get; set; } = CustomSpinePos.Zero;
        public void SetTransform(Transform transform)
        {
            transform.localPosition = Position.ToVector3();
            transform.localEulerAngles = Rotation.ToVector3();
            transform.localScale = Scale.ToVector3();
        }
        public CustomSpineOption Clone()
        {
            return new CustomSpineOption(Position, Scale, Rotation);
        }
        public override string ToString()
        {
            return JObject.FromObject(this).ToString(Formatting.Indented);
        }
    }

    public class CustomSpine : MonoBehaviour
    {
        private UINPCSVItem _uiNpcSvItem;
        private SayDialog _sayDialog;
        public ESpineType spineType = ESpineType.None;
        // private CustomSpineOption _customSpineOption = null;
        private UINPCJiaoHuPop _uiNpcJiaoHuPop;
        private UINPCInfoPanel _uiNpcInfoPanel;
        private InitAvatar _fightAvatar;
        private JiaoYiUIMag _jiaoYiUIMag;
        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            spineType = ESpineType.None;
            _uiNpcSvItem = GetComponentInParent<UINPCSVItem>();
            _sayDialog = GetComponentInParent<SayDialog>();
            _uiNpcJiaoHuPop = GetComponentInParent<UINPCJiaoHuPop>();
            _uiNpcInfoPanel = GetComponentInParent<UINPCInfoPanel>();
            _fightAvatar = GetComponentInParent<InitAvatar>();
            _jiaoYiUIMag = GetComponentInParent<JiaoYiUIMag>();
            if (_uiNpcSvItem != null)
            {
                spineType = ESpineType.UINpcSvItem;
            }
            else if (_sayDialog != null)
            {
                spineType = ESpineType.SayDialog;
            }
            else if (_uiNpcJiaoHuPop != null)
            {
                spineType = ESpineType.UINpcJiaoHuPop;
            }
            else if (_uiNpcInfoPanel != null)
            {
                spineType = ESpineType.UINpcInfoPanel;
            }
            else if (_fightAvatar != null)
            {
                spineType = ESpineType.FightAvatar;
            }
            else if (_jiaoYiUIMag != null)
            {
                spineType = ESpineType.JiaoYiUIMag;
            }
            Reset();
        }
        public void SetAvatar(int avatar)
        {

        }
        private void OnEnable()
        {
            Init();
        }
        private void OnDisable()
        {
            if (spineType != ESpineType.JiaoYiUIMag)
            {
                Destroy(this);
            }

        }
        public void Reset()
        {
            CustomSpineOption customSpineOption = null;
            switch (spineType)
            {
                case ESpineType.None:
                case ESpineType.JiaoYiUIMag:
                    break;
                case ESpineType.SayDialog:
                    customSpineOption = CustomSpineOption.SayDialogPos;
                    break;
                case ESpineType.UINpcSvItem:
                    customSpineOption = CustomSpineOption.UINpcSvItemPos;
                    break;
                case ESpineType.UINpcJiaoHuPop:
                    customSpineOption = CustomSpineOption.UINpcJiaoHuPopPos;
                    break;
                case ESpineType.UINpcInfoPanel:
                    customSpineOption = CustomSpineOption.UINpcInfoPanelPos;
                    break;
                case ESpineType.FightAvatar:
                    customSpineOption = CustomSpineOption.FightAvatarPos;
                    break;
            }
            customSpineOption?.SetTransform(transform);
        }
        private void OnDestroy()
        {
            Reset();
        }
    }

    [HarmonyPatch(typeof(PlayerSetRandomFace), nameof(PlayerSetRandomFace.randomAvatar))]
    public static class PlayerSetRandomFaceRandomAvatarPatch
    {
        public static int m_avartarID;
        public static bool m_customSpine;
        public static List<int> CustomNpc = new List<int>()
        {
            8471,9740
        };
        public static bool Prefix(PlayerSetRandomFace __instance, int monstarID)
        {
            m_customSpine = false;
            m_avartarID = monstarID;


            if (SceneEx.NowSceneName == "MainMenu" || m_avartarID < 20000)
            {
                return true;
            }
            var avartarID = NPCEx.NPCIDToOld(m_avartarID);
            var skeletonGraphic = __instance.GetComponent<SkeletonGraphic>();
            if (AssetsUtils.GetSkeletonData(avartarID, out var skeletonData))
            {

             
                var skeletonAnimation = __instance.GetComponent<SkeletonAnimation>();
                if (skeletonGraphic != null)
                {
                    skeletonGraphic.skeletonDataAsset = skeletonData;
                    skeletonGraphic.initialSkinName = "default";
                    skeletonGraphic.startingAnimation = "Idle_0";
                    skeletonGraphic.Initialize(true);
                    var customSpine = __instance.gameObject.AddMissingComponent<CustomSpine>();
                    var jiaoYiUI = __instance.GetComponentInParent<JiaoYiUIMag>() == null;
                    m_customSpine = jiaoYiUI;
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
                            var name = entry.Animation.Name;
                            var hasAnimation = AssetsUtils.CheckAnimation(avartarID, name, out var isIdle);
                            if (!hasAnimation) return;
                            var trackEntry = skeletonAnimation1.AnimationState.GetCurrent(0);
                            if (trackEntry != null)
                            {
                                skeletonAnimation1.AnimationState.ClearTrack(0);
                            }
                            skeletonAnimation1.AnimationState.SetAnimation(0, name, isIdle);

                        };
                        m_customSpine = true;
                        __instance.gameObject.AddMissingComponent<CustomSpine>();
                    }
                }
                var baseSpine = __instance.BaseSpine;
                if (baseSpine != null)
                {
                    baseSpine.SetActive(m_customSpine);
                }
                var baseImage = __instance.BaseImage;
                if (baseImage != null)
                {
                    baseImage.SetActive(!m_customSpine);
                }
                if (m_customSpine)
                {
                    return false;
                }


            }

            if (!NpcUtils.IsFightScene || __instance.BaseImage != null || skeletonGraphic != null) return true;
            {
                var oldId = NPCEx.NPCIDToOld(m_avartarID);

                if (!CustomNpc.Contains(oldId) && !NpcUtils.GetNpcFightFace(oldId)) return true;
                var gameObject = new GameObject("Image", typeof(CustomImage));

                var customImage = gameObject.AddMissingComponent<CustomImage>();
                customImage.SetSpine(__instance);
                customImage.SetAvatarId(m_avartarID);

            }

            return true;
        }
    }

}