using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using HarmonyLib;
using KBEngine;
using Live2D.Cubism.Core;
using Live2D.Cubism.Framework.Json;
using MG.GIF;
using SkySwordKill.Next;
using SkySwordKill.NextMoreCommand.Utils;
using Spine.Unity;
using ThreeDISevenZeroR.UnityGifDecoder;
using UnityEngine;
using UnityEngine.Networking;
using GameObject = UnityEngine.GameObject;

namespace SkySwordKill.NextMoreCommand.Patchs
{
    struct GifFrame
    {
        public float Delay { get; set; }
        public Texture2D Texture { get; set; }
        private Sprite _sprite;
        public Sprite Sprite
        {
            get => _sprite ? _sprite : CreateSprite();
        }
        private Sprite CreateSprite()
        {

            var texture = Texture;
            if (texture == null)
            {
                return null;
            }
            _sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            return _sprite;
        }
        public GifFrame(Image image)
        {
            Texture = image.CreateTexture();
            Delay = image.Delay / 1000.0f;
        }
    }

    class AnimatedTextures : MonoBehaviour
    {
        private List<GifFrame> frames = new List<GifFrame>();
        private int avatarId;
        private int curFrame = 0;
        private float time = 0.0f;
        private bool isFinish = false;
        private SpriteRenderer renderer;
        private Decoder decoder;
        private void Start()
        {
            renderer = GetComponent<SpriteRenderer>();
        }
        private void Update()
        {

            if (!isFinish && decoder != null)
            {
                StartCoroutine(SetGif());
            }
            if (frames.Count == 0 || avatarId <= 0)
            {
                return;
            }
            if (!isFinish) return;
            time += Time.deltaTime;
            var frame = frames[curFrame];
            if (!(time >= frame.Delay)) return;
            curFrame = (curFrame + 1) % frames.Count;
            time = 0.0f;
            //renderer.sprite = frames[curFrame].Sprite;


        }
        public void SetAvatar(int monstarId)
        {
            avatarId = monstarId;
            if (frames.Count != 0) return;
            if (!Main.Res.TryGetFileAsset($"Assets/Gif/{NPCEx.NPCIDToOld(avatarId).ToString()}.gif", out var fileAsset)) return;
            decoder = new Decoder(File.ReadAllBytes(fileAsset.FileRawPath));

        }
        public IEnumerator SetGif()
        {

            // using (var gifStream = new GifStream(new FileStream(fileAsset.FileRawPath,FileMode.Open)))
            // {
            //     while (gifStream.HasMoreData)
            //     {
            //         switch (gifStream.CurrentToken)
            //         {
            //             case GifStream.Token.Image:
            //                 var image = gifStream.ReadImage();
            //                 var frame = new Texture2D(
            //                     gifStream.Header.width,
            //                     gifStream.Header.height,
            //                     TextureFormat.ARGB32, false);
            //
            //                 frame.SetPixels32(image.colors);
            //                 frame.Apply();
            //
            //                 frames.Add(frame);
            //                 var sprite = Sprite.Create(frame, new Rect(0, 0, frame.width, frame.height), new Vector2(0.5f, 0.5f));
            //                 framesSprite.Add(sprite);
            //                 frameDelays.Add(image.SafeDelaySeconds);
            //                 break;
            //             // do something with image
            //
            //
            //             case GifStream.Token.Comment:
            //                 var comment = gifStream.ReadComment();
            //                 break;
            //
            //             default:
            //                 gifStream.SkipToken();
            //                 break;
            //         }
            //     }
            // }


            var img = decoder.NextImage();
            if (img == null)
            {
                isFinish = true;

            }
            else
            {
                var gifFrame = new GifFrame(img);

                frames.Add(gifFrame);
                if (frames.Count == 1)
                {
                    renderer.sprite = gifFrame.Sprite;
                }

            }
            yield break;
        }


    }

    class CustomImage : MonoBehaviour
    {
        public Transform spine;
        private SpriteRenderer _spriteRenderer;
        private SkeletonAnimation _skeletonAnimation;
        private int _avatarId;
        private AnimatedTextures _animatedTextures;
        private CubismModel3Json model3Json;
        private CubismModel model;
        private void Awake()
        {
            _spriteRenderer = gameObject.AddMissingComponent<SpriteRenderer>();
            _animatedTextures = gameObject.AddMissingComponent<AnimatedTextures>();
        }
        public void SetSpine(PlayerSetRandomFace playerSetRandom)
        {
            spine = playerSetRandom.transform;
            _skeletonAnimation = playerSetRandom.GetComponent<SkeletonAnimation>();
            var transform1 = transform;
            transform1.SetParent(spine.parent);
            transform1.localRotation = spine.localRotation;
            transform1.localPosition = new Vector3(0, 3, 0);
            transform1.localScale = new Vector3(0.5f, 0.5f, 0);
        }
        private static Avatar Player => PlayerEx.Player;
        public bool SetAvatarId(int monstarID)
        {
            _avatarId = NPCEx.NPCIDToOld(monstarID);
            string faceId;
            if (_avatarId == 1)
            {
                var player = Player;
                // lihui = player.FaceWorkshop;
                faceId = player.Face.ToString();
            }
            else
            {
                var npcJson = _avatarId.ToNpcNewId().NPCJson();
                //lihui = npcJson.HasField("workshoplihui") ? npcJson["workshoplihui"].str : string.Empty;
                faceId = npcJson["face"].I.ToString();
            }
            // MyLog.Log($"角色id:{faceId} 立绘id:{faceId}");
            //var imagePath = string.IsNullOrWhiteSpace(lihui) ? $"Effect/Prefab/gameEntity/Avater/Avater{faceId}/{faceId}" : $"workshop_{lihui}_{faceId}";
            var sprite = ModResources.LoadSprite($"Effect/Prefab/gameEntity/Avater/Avater{faceId}/{faceId}");
            var hasSprite = sprite != null;
            _spriteRenderer.sprite = sprite;
            if (_skeletonAnimation != null)
            {
                _skeletonAnimation.maskInteraction = hasSprite ? SpriteMaskInteraction.VisibleInsideMask : SpriteMaskInteraction.None;
                //if (hasSprite)
                //{
                //    transform.DOScale(0.65f,1f).SetLoops(-1, LoopType.Yoyo);
                //}

            }
            _spriteRenderer.gameObject.SetActive(hasSprite);
            if (hasSprite)
            {
                // SetGif();
            }

            return hasSprite;
        }
        public void SetGif()
        {
            _animatedTextures.SetAvatar(_avatarId);
        }
        public void SetLive2D()
        {
            if (!Main.Res.TryGetFileAsset($"Assets/Live2D/{_avatarId.ToString()}/{_avatarId.ToString()}.model3.json", out var fileAsset)) return;
            model3Json = CubismModel3Json.LoadAtPath(fileAsset.FileRawPath, BuiltinLoadAssetAtPath);
            model = model3Json.ToModel();
            model.transform.SetParent(transform.parent);
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
        public static bool Prefix(PlayerSetRandomFace __instance, int monstarID)
        {
            if (!NpcUtils.IsFightScene || __instance.GetComponent<SkeletonGraphic>() != null) return true;


            if (__instance.BaseImage == null)
            {
                var gameObject = new GameObject("Image", typeof(CustomImage));
                gameObject.GetComponent<CustomImage>().SetSpine(__instance);
                __instance.BaseImage = gameObject;
            }
            var customImage = __instance.BaseImage.AddMissingComponent<CustomImage>();
            return !customImage.SetAvatarId(monstarID);
        }
    }

}