using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkySwordKill.Next;
using SkySwordKill.Next.Extension;
using SkySwordKill.Next.Mod;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.Custom.NPC;

namespace SkySwordKill.NextMoreCommand.CustomModData
{
    [ModData("CustomABSpineData")]
    public class CustomABSpineData : IModData
    {

        public void Read(ModConfig modConfig)
        {            var assetDir = modConfig.GetAssetDir();
            CustomModDataManager.LoadData(assetDir, "Avatar", modConfig, LoadCustomAb);
            CustomModDataManager.LoadData(assetDir, "CG", modConfig, LoadCustomAb);
            CustomModDataManager.LoadData(assetDir, "MapPlayer", modConfig, LoadCustomAb);
        }
        public static void LoadCustomAb(string filepath, ModConfig modConfig)
        {
            if (!Directory.Exists(filepath))
            {
                return;
            }
            var files = Directory.GetFiles(filepath, "*.ab", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                Main.LogInfo($"添加AB {file}");
                try
                {
                    Main.Res.AddABAsset(file);
                }
                catch (Exception e)
                {
                    Main.LogWarning(e.ToString());
                }
           
            }
        }
        public bool Check(ModConfig modConfig)
        {
            var assetDir = modConfig.GetAssetDir();
            return assetDir.CombinePath("Avatar").HasPath() || assetDir.CombinePath("CG").HasPath() ;
        }
    }
}