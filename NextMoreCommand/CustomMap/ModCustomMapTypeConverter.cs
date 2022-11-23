using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkySwordKill.Next;
using SkySwordKill.Next.Mod;
using SkySwordKill.NextMoreCommand.Attribute;
using SkySwordKill.NextMoreCommand.CustomMap.CustomMapType;

namespace SkySwordKill.NextMoreCommand.CustomMap;

public class ModCustomMapTypeConverter : JsonConverter
{
    private static Dictionary<string, Type> s_typeMap = new Dictionary<string, Type>();

    public static void Init()
    {
        foreach (var type in typeof(ModCustomMapType).Assembly.GetTypes())
        {
            if (type.IsSubclassOf(typeof(ModCustomMapType)) && !type.IsAbstract)
            {
                var settingType = type.GetCustomAttribute<CustomMapTypeAttribute>();
                if (settingType != null)
                {
                    s_typeMap.Add(type.GetCustomAttribute<CustomMapTypeAttribute>().Type, type);
                }
            }
        }
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(List<ModCustomMapType>);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var jArray = JArray.Load(reader);
        var list = new List<ModCustomMapType>(jArray.Count);
        foreach (var token in jArray)
        {
            var jObject = (JObject)token;
            string type = (string)jObject["Type"];
            ModCustomMapType item;
            if ( s_typeMap.TryGetValue(type!, out var typeValue))
            {
                item = (ModCustomMapType)Activator.CreateInstance(typeValue);
            }
            else
            {
                throw new Exception("Unknown mod setting type: " + type);
            }

            serializer.Populate(jObject.CreateReader(), item);
            item.RawJson = jObject;

            list.Add(item);
        }

        return list;
    }
}