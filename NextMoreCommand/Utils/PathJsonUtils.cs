using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;
using SkySwordKill.Next;
using SkySwordKill.Next.Extension;
using SkySwordKill.Next.Mod;

namespace SkySwordKill.NextMoreCommand.Utils;

public static class PathJsonUtils
{
    public static void PatchJsonObjectArray(FieldInfo fieldInfo, JSONObject[] jsonObjects, JSONObject[] jsonDatas)
    {
        for (int i = 0; i < jsonObjects.Length; i++)
        {
            if (jsonObjects[i] == null)
                continue;
            PatchJsonObject(fieldInfo, jsonObjects[i],jsonDatas[i]);
        }
    }

    public static void PatchJsonObject(FieldInfo fieldInfo, JSONObject jsonObject, JSONObject jsonData)
    {
        try
        {
            JSONObject dataTemplate = null;
            if (jsonObject.Count > 0)
            {
                dataTemplate = jsonObject[0];
            }


            foreach (var key in jsonData.keys)
            {
                var curData = jsonData.GetField(key);
                if (jsonObject.HasField(key))
                {
                    // Old data
                    var tagData = jsonObject.GetField(key);
                    foreach (var fieldKey in curData.keys)
                    {
                        tagData.TryAddOrReplace(fieldKey, curData.GetField(fieldKey));
                    }
                }
                else
                {
                    // New data
                    if (dataTemplate != null)
                    {
                        foreach (var fieldKey in dataTemplate.keys)
                        {
                            if (!curData.HasField(fieldKey))
                            {
                                curData.AddField(fieldKey, dataTemplate[fieldKey].Clone());
                                Main.LogWarning(string.Format("ModManager.DataMissingField".I18N(),
                                    fieldInfo.Name,
                                    fieldKey,
                                    dataTemplate[fieldKey]));
                            }
                        }
                    }

                    jsonObject.AddField(key, curData);
                }
            }

           MyPluginMain.LogInfo(string.Format("ModManager.LoadData".I18N(), $"{fieldInfo.Name}"));
        }
        catch (Exception e)
        {
            throw new ModLoadException($"文件【{fieldInfo.Name}】加载失败", e);
        }
    }

    public static void PatchJObject(FieldInfo fieldInfo, string filePath, JObject jObject, JObject jsonData)
    {
        try
        {
            var dataTemplate = jObject.Properties().First().Value;
            foreach (var property in jsonData.Properties())
            {
                if (property.Value.Type != JTokenType.Object)
                {
                    jObject.TryAddOrReplace(property.Name, property.Value);
                    continue;
                }

                var curData = (JObject)property.Value;
                if (jObject.ContainsKey(property.Name))
                {
                    var tagData = jObject.GetValue(property.Name);
                    if (tagData?.Type == JTokenType.Object)
                    {
                        var tagDataObject = (JObject)tagData;
                        foreach (var field in curData.Properties())
                        {
                            if (tagDataObject.ContainsKey(field.Name))
                                tagDataObject.Remove(field.Name);
                            tagDataObject.Add(field.Name, curData.GetValue(field.Name));
                        }
                    }
                }
                else
                {
                    if (dataTemplate.Type == JTokenType.Object)
                    {
                        foreach (var field in JObject.FromObject(dataTemplate).Properties())
                        {
                            if (!curData.ContainsKey(field.Name))
                            {
                                curData.Add(field.Value.DeepClone());
                                Main.LogWarning(string.Format("ModManager.DataMissingField".I18N(),
                                    fieldInfo.Name,
                                    property.Name,
                                    field.Name,
                                    field.Value));
                            }
                        }
                    }

                    jObject.Add(property.Name, property.Value.DeepClone());
                }
            }

           MyPluginMain.LogInfo(string.Format("ModManager.LoadData".I18N(),
                $"{Path.GetFileNameWithoutExtension(filePath)}.json"));
        }
        catch (Exception e)
        {
            throw new ModLoadException($"文件【{filePath}】加载失败", e);
        }
    }

    // public static void PatchDicData(FieldInfo fieldInfo,
    //     jsonData.YSDictionary<string, JSONObject> dicData,
    //     JSONObject toJsonObject)
    // {
    //     var dataTemplate = toJsonObject[0];
    //     foreach (var filePath in Directory.GetFiles(dirPathForData))
    //     {
    //         try
    //         {
    //             var curData = LoadJSONObject(filePath);
    //             var key = Path.GetFileNameWithoutExtension(filePath);
    //
    //             if (toJsonObject.HasField(key))
    //             {
    //                 var tagData = toJsonObject.GetField(key);
    //                 foreach (var fieldKey in curData.keys)
    //                 {
    //                     tagData.TryAddOrReplace(fieldKey, curData.GetField(fieldKey));
    //                 }
    //             }
    //             else
    //             {
    //                 foreach (var fieldKey in dataTemplate.keys)
    //                 {
    //                     if (!curData.HasField(fieldKey))
    //                     {
    //                         curData.AddField(fieldKey, dataTemplate[fieldKey].Clone());
    //                         Main.LogWarning(string.Format("ModManager.DataMissingField".I18N(),
    //                             fieldInfo.Name,
    //                             key,
    //                             fieldKey,
    //                             dataTemplate[fieldKey]));
    //                     }
    //                 }
    //
    //                 dicData[key] = curData;
    //                 toJsonObject.AddField(key, curData);
    //             }
    //
    //            MyPluginMain.LogInfo(string.Format("ModManager.LoadData".I18N(),
    //                 $"{Path.GetFileNameWithoutExtension(dirPathForData)}/{Path.GetFileNameWithoutExtension(filePath)}.json [{key}]"));
    //         }
    //         catch (Exception e)
    //         {
    //             throw new ModLoadException($"文件 {filePath} 解析失败", e);
    //         }
    //     }
    // }
}