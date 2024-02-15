using System.Collections.Generic;

namespace SkySwordKill.NextMoreCommand.NextSeachNpcExtension
{
    public interface ISearchNpcMatch
    {
        List<string> Alias { get; }
        bool         Match(SearchNpcDataInfo searchNpcDataInfo);
    }
}