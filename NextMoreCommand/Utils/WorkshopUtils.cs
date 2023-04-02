using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using script.Steam;
using SkySwordKill.NextMoreCommand.Utils;
using Steamworks;
using UnityEngine;

namespace MCSSubscribeDependencies;

public class WorkshopItemInfo
{
    public ulong Id { get; }
    public PublishedFileId_t PublishedFileId { get; }

    public CallResult<RemoteStorageGetPublishedFileDetailsResult_t> CallResult;

    // public CallResult<GetAppDependenciesResult_t> GetAppDependenciesResult;
    // public List<AppId_t> Dependencies = new List<AppId_t>();
    public WorkshopItemInfo(ulong id)
    {
        Id = id;
        PublishedFileId = new PublishedFileId_t(id);
        CallResult =
            CallResult<RemoteStorageGetPublishedFileDetailsResult_t>.Create(
                OnRemoteStorageGetPublishedFileDetailsResult);
        // GetAppDependenciesResult =       CallResult<GetAppDependenciesResult_t>.Create(
        //     OnGetAppDependenciesResult);
        // GetDependencies();
    }

    // private void OnGetAppDependenciesResult(GetAppDependenciesResult_t pCallback, bool biofailure)
    // {
    //     if (pCallback.m_eResult  == EResult.k_EResultOK)
    //     {
    //         foreach (var appId in pCallback.m_rgAppIDs)
    //         {
    //             Dependencies.Add(appId);
    //         }
    //     }
    // }
    // public void GetDependencies()
    // {
    //     var handle = SteamUGC.SendQueryUGCRequest(SteamUGC.CreateQueryUGCDetailsRequest(new[]{PublishedFileId},1));
    //     GetAppDependenciesResult.Set(handle);
    // }
    public void SetSubscribe()
    {
        var handle = SteamRemoteStorage.GetPublishedFileDetails(PublishedFileId, 200);
        CallResult.Set(handle);
    }

    public void OnRemoteStorageGetPublishedFileDetailsResult(
        RemoteStorageGetPublishedFileDetailsResult_t pCallback, bool biofailure)
    {
        if (pCallback.m_eResult == EResult.k_EResultOK)
        {
            UIPopTip.Inst.Pop($"开始订阅{pCallback.m_rgchTitle}Mod");
            MyLog.Log("创意工坊订阅", $"开始缺少前置订阅{pCallback.m_rgchTitle}Mod");
            SteamUGC.SubscribeItem(pCallback.m_nPublishedFileId);
        }
    }
}

public static class WorkshopUtils
{
    public static List<WorkShopItem> WorkShopItems;
    public static void Subscribe(params ulong[] items)
    {
        var isAdd = false;
        var banList = new List<ulong>() { 2921046709, 2862679721 };
        var allMod = GetAllMod();
        foreach (var id in items)
        {
            if (!banList.Contains(id) && !allMod.Contains(id))
            {
                if (UIPopTip.Inst == null)
                {
                    return;
                }

                isAdd = true;
                var workshopItemInfo = new WorkshopItemInfo(id);
                workshopItemInfo.SetSubscribe();
            }
        }

        if (isAdd)
        {
            USelectBox.Show("修改创意工坊设置后，需要重启生效,点击确定关闭游戏", Application.Quit);
        }
    }

    public static void Subscribe() => Subscribe(GetAllModDependencies().ToArray());


    public static List<ulong> GetAllMod()
    {
        return WorkshopTool
            .GetAllModDirectory()
            .Where(item => Convert.ToUInt64(item.Name) != 0)
            .Select(item => Convert.ToUInt64(item.Name)).ToList();
    }

    public static List<WorkshopItemInfo> GetAllModWorkshopItemInfo()
    {
        return GetAllMod().Select(item => new WorkshopItemInfo(item)).ToList();
    }

    public static List<WorkShopItem> GetAllModWorkshopItem()
    {
        return WorkshopTool
            .GetAllModDirectory()
            .Where(item => File.Exists(Path.Combine(item.FullName, "mod.bin")))
            .Select(item =>
            {
                var file = Path.Combine(item.FullName, "mod.bin");
                var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
                var workShopItem = (WorkShopItem)new BinaryFormatter().Deserialize(fileStream);
                fileStream.Close();
                return workShopItem;
            }).ToList();
    }

    public static List<List<ulong>> HasAllModLastDependencies()
    {
        return GetAllModWorkshopItem().Where(item => item.LastDependencies != null && item.LastDependencies.Count != 0)
            .Select(item => item.LastDependencies).ToList();
    }

    public static List<List<ulong>> HasAllModDependencies()
    {
        return GetAllModWorkshopItem().Where(item => item.Dependencies != null && item.Dependencies.Count != 0)
            .Select(item => item.Dependencies).ToList();
    }

    public static List<ulong> GetAllModDependencies()
    {
        var dependencies = new List<ulong>();
        var workshopItems = HasAllModDependencies();
        foreach (var item in workshopItems)
        {
            foreach (var id in item)
            {
                if (dependencies.Contains(id))
                {
                    continue;
                }

                dependencies.Add(id);
            }
        }

        return dependencies;
    }
}