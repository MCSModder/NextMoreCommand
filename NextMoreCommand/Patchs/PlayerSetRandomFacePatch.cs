using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject;
using Fungus;
using HarmonyLib;
using JiaoYi;
using KBEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProGif.GifManagers;
using ProGif.Lib;
using SkySwordKill.Next.DialogSystem;
using SkySwordKill.NextMoreCommand.Utils;
using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Serialization;
using YSGame.Fight;
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
            string faceId;
            string lihui;
            if (avatar == 1)
            {
                faceId = Player.Face.ToString();
                lihui = Player.FaceWorkshop;
            }
            else
            {
                var npcJson = avatar.NPCJson();
                faceId = npcJson["face"].I.ToString();
                lihui = npcJson.HasField("workshoplihui") ? npcJson["workshoplihui"].str : string.Empty;
            }

            if (string.IsNullOrWhiteSpace(lihui) && faceId == "0")
            {
                DestroyImmediate(this);
                yield break;
            }

            var path = !string.IsNullOrWhiteSpace(lihui) ? $"workshop_{lihui}_{faceId}" : $"Effect/Prefab/gameEntity/Avater/Avater{faceId}/{faceId}";
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
        JiaoYiUIMag,
        FpUIMag,
        LunDaoManager
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
        public static readonly CustomSpineOption FpUIMagPos = new CustomSpineOption(new CustomSpinePos(0, -800), new CustomSpinePos(1, 1, 1));
        public static readonly CustomSpineOption LunDaoManagerPos = new CustomSpineOption(new CustomSpinePos(0, -750), new CustomSpinePos(1, 1, 1));

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
        private FpUIMag _fpUIMag;
        private CustomSpineOption customSpineOption;
        private LunDaoManager _lunDaoManager;

        public void Init()
        {
            spineType = ESpineType.None;
            _uiNpcSvItem = GetComponentInParent<UINPCSVItem>();
            _sayDialog = GetComponentInParent<SayDialog>();
            _uiNpcJiaoHuPop = GetComponentInParent<UINPCJiaoHuPop>();
            _uiNpcInfoPanel = GetComponentInParent<UINPCInfoPanel>();
            _fightAvatar = GetComponentInParent<InitAvatar>();
            _jiaoYiUIMag = GetComponentInParent<JiaoYiUIMag>();
            _fpUIMag = GetComponentInParent<FpUIMag>();
            _lunDaoManager = GetComponentInParent<LunDaoManager>();
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
            else if (_fpUIMag != null)
            {
                spineType = ESpineType.FpUIMag;
            }
            else if (_lunDaoManager)
            {
                spineType = ESpineType.LunDaoManager;
            }
            Reset();
        }

        public void SetAvatar(int avatar, bool isSay = false)
        {
            if (isSay)
            {
                spineType = ESpineType.SayDialog;
                Reset();
            }
            else
            {
                Init();
            }

            MyPluginMain.LogInfo($"avatar:{avatar} spineType:{spineType.GetName()}");
            AssetsUtils.GetCustomSpineOption(avatar, spineType, out customSpineOption);
            MyPluginMain.LogInfo($"customSpineOption:\n{customSpineOption}");
            if (customSpineOption == null)
            {
                Reset();
            }
            else
            {
                customSpineOption.SetTransform(transform);
            }

        }

        private void OnDisable()
        {
            Init();
        }
        public void Reset()
        {
            customSpineOption = null;
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
                case ESpineType.FpUIMag:
                    customSpineOption = CustomSpineOption.FpUIMagPos;
                    break;
                case ESpineType.LunDaoManager:
                    customSpineOption = CustomSpineOption.FpUIMagPos;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
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
        private static SkeletonGraphic skeletonGraphic;
        private static int avartarID;
        private static CustomSpine customSpine;
        // public static List<int> CustomNpc = new List<int>()
        // {
        //     8471,
        //     9740,
        //     7200
        // };
        public static bool SetSpine(PlayerSetRandomFace __instance)
        {
            var customImage = __instance.transform.parent.GetComponentInChildren<CustomImage>();
            var skeletonAnimation = __instance.GetComponent<SkeletonAnimation>();
            if (customImage != null)
            {
                Object.DestroyImmediate(customImage.gameObject);
                skeletonAnimation.maskInteraction = SpriteMaskInteraction.None;
            }
            if (!AssetsUtils.GetSkeletonData(avartarID, out var skeletonData) || !NpcUtils.GetNpcFightSpine(avartarID)) return false;
            var skinName = NpcUtils.GetNpcSkinSpine(avartarID);
            var skin = AssetsUtils.CheckSkin(avartarID, skinName) ? skinName : NpcUtils.GetNpcDefaultSkinSpine(avartarID);
            MyLog.Log($"skinName:{skinName} skin:{skin}");

            if (skeletonGraphic != null)
            {
                skeletonGraphic.skeletonDataAsset = skeletonData;
                skeletonGraphic.initialSkinName = skin;
                skeletonGraphic.startingAnimation = "Idle_0";
                skeletonGraphic.Initialize(true);
                var customSpine = __instance.gameObject.AddMissingComponent<CustomSpine>();
                var sayTransform = __instance.transform;
                var say = false;
                for (var i = 0; i < 3; i++)
                {
                    sayTransform = sayTransform.parent;
                    if (sayTransform == null)
                    {
                        break;
                    }
                    if (i == 2)
                    {
                        say = sayTransform.name == "SayDialog";
                    }
                }
                customSpine.SetAvatar(avartarID, say);
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
                    skeletonAnimation1.skeletonDataAsset = skeletonData;
                    skeletonAnimation1.initialSkinName = skin;
                    skeletonAnimation1.AnimationName = "Idle_0";
                    skeletonAnimation1.Initialize(true);
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
                    customSpine = gameObject.AddMissingComponent<CustomSpine>();
                    customSpine.SetAvatar(avartarID);
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
            return m_customSpine;
        }
        public static bool Prefix(PlayerSetRandomFace __instance, int monstarID)
        {
            m_customSpine = false;
            m_avartarID = monstarID;
            avartarID = NPCEx.NPCIDToOld(m_avartarID);
            customSpine = null;
            skeletonGraphic = null;

            if (SceneEx.NowSceneName == "MainMenu" || m_avartarID is > 1 and < 20000)
            {
                return true;
            }
            skeletonGraphic = __instance.GetComponent<SkeletonGraphic>();
            if (SetSpine(__instance))
            {
                return false;
            }
            if (!NpcUtils.GetNpcFightFace(avartarID)) return true;
            var isFight = NpcUtils.IsFightScene;
            if (!isFight || __instance.BaseImage != null) return true;

            // if (!CustomNpc.Contains(oldId) && !NpcUtils.GetNpcFightFace(oldId)) return true;

            var go = new GameObject("Image", typeof(CustomImage));

            var customImage = go.AddMissingComponent<CustomImage>();
            customImage.SetSpine(__instance);
            customImage.SetAvatarId(m_avartarID);


            return true;
        }
    }

}