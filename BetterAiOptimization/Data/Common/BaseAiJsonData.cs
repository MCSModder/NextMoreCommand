using System;
using System.Collections.Generic;

namespace BetterAiOptimization.Data.Json
{
    public abstract class BaseAiJsonData<T> : IJsonManager where T : IJsonData
    {
        public static readonly Dictionary<int, T> DataDict = new Dictionary<int, T>();

        public static readonly List<T> DataList = new List<T>();
        public static string TypeName = typeof(T).Name;
        public static void AddData(Action<T> callback)
        {
            var data = CreateData();
            callback?.Invoke(data);
            var id = data.GetId();
            if (id <= 0)
            {
                return;
            }
            var typeName = TypeName;
            try
            {

                if (DataDict.ContainsKey(id))
                {
                    PreloadManager.LogException($"!!!错误!!!{typeName}.DataDict添加数据时出现重复的键，Key:{id.ToString()}，已跳过，请检查配表");
                }
                else
                {
                    DataDict.Add(id, data);
                    DataList.Add(data);
                }
            }
            catch (Exception arg)
            {
                PreloadManager.LogException($"!!!错误!!!{typeName}.DataDict添加数据时出现异常，已跳过，请检查配表");
                PreloadManager.LogException(string.Format("异常信息:\n{0}", arg));
                //PreloadManager.LogException(string.Format("数据序列化:\n{0}", jsonobject));
            }
        }

        public static void Clear()
        {
            DataDict.Clear();
            DataList.Clear();
        }
        public event Action OnInitFinishAction;
        public void OnInitFinish()
        {
            OnInitFinishAction?.Invoke();
        }
        public abstract void InitDataDict();
        public static T CreateData() => Activator.CreateInstance<T>();
        public bool TryGetData<TJsonData>(int id, out TJsonData data) where TJsonData : IJsonData
        {
            data = default;
            if (!DataDict.TryGetValue(id, out var jsonData)) return false;
            data = (TJsonData)(jsonData as IJsonData);
            return true;
        }
    }
}