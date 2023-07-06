namespace BetterAiOptimization.Data.Json
{
    public interface IJsonManager
    {
        void OnInitFinish();
        void InitDataDict();
        bool TryGetData<T>(int id, out T data)where T:IJsonData;
    }
}